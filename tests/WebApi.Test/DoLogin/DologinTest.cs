using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using Tropical.Comunication.Requests;
using WebApi.Test.InlineData;
using CommonTestUtilities.Requests;
using System.Globalization;
using Tropical.Exceptions;

namespace WebApi.Test.DoLogin
{
    public class DologinTest : TropicalClassFixture
    {
        private readonly string method = "login";

        ///TODO USAR O MOQ
        private readonly string _password;
        private readonly string _email;
        private readonly string _name;
        public DologinTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _email = factory.GetEmail();
            _password = factory.GetPassword();
            _name = factory.GetName();
        }
        [Fact]
        public async Task Success()
        { // teste de integração 
            var request = new RequestLoginJson()
            {
                Email = _email,
                Password = _password
            };
            var response = await DoPost(method, request);// nome do controller e a request

            await using var responseBody = await response.Content.ReadAsStreamAsync(); //deve ser stream

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var name = responseData.RootElement.GetProperty("name").GetString();// camel case sempre virá minusculo
            var token = responseData.RootElement.GetProperty("token").GetString();
            Assert.NotEmpty(name);
            Assert.NotEmpty(token);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Theory]
        [ClassData(typeof(CultureInlineDataTests))]
        public async Task Error_Login_Invalid(string culture)
        {
            var request = RequestLoginJsonBuilder.Build();

            var response = await DoPost(method, request, culture);// nome do controller e a request

            await using var responseBody = await response.Content.ReadAsStreamAsync(); //método deve ser stream

            var responseData = await JsonDocument.ParseAsync(responseBody);

            //  errors camel case sempre virá minusculo
            //a propriedade errors vem de comunication ResponseErrorJson
            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

            //obtendo a mensagem esperada de acordo com a culture
            var expectedMessage = ResourceMessagesException
              .ResourceManager.GetString("EMAIL_OR_OASSWORD_INVALID", new CultureInfo(culture));

            Assert.NotEmpty(errors);
            Assert.Single(errors);
            Assert.Contains(expectedMessage, errors.Select(e => e.GetString()));
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            //tenho que verificar se não está salvando no banco após configurar tudo
        }
    }
}
