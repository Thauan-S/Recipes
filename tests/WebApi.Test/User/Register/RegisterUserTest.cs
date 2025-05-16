//using System.Globalization;
//using System.Net;
//using System.Net.Http.Json;
//using System.Text.Json;
//using CommonTestUtilities.Requests;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Tropical.Exceptions;
//using WebApi.Test.InlineData;

//namespace WebApi.Test.User.Register
//{   // TESTES DE INTEGRAÇÃO
//    //Microsoft.AspNetCore.Mvc.Testing instalar
//    //criar uma classe no program para conseguir devolver e usar aqui
//    //instalar Ef.Inmemory
//    // criei a classe customWebApplicationFactory
//    public class RegisterUserTest : TropicalClassFixture
//    {

//        private readonly string method = "user";
//        // repassa a factory para a TropicalClassFixture
//        public RegisterUserTest(CustomWebApplicationFactory factory) : base(factory) { }

//        [Fact]
//        public async Task Sucess()
//        {
//            var request = RequestRegisterUserJsonBuilder.Build();
//            // post ou get ou put etc... depende do método

//            var response = await DoPost(method, request);// nome do controller e a request
////            await using var responseBody = await response.Content.ReadAsStreamAsync(); //deve ser stream
////            var responseData = await JsonDocument.ParseAsync(responseBody);
////            var name = responseData.RootElement.GetProperty("name").GetString();// camel case sempre virá minusculo
////            Assert.NotEmpty(name);
////            Assert.Equal(request.Name, name);
////            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
////            //tenho que verificar se não está salvando no banco após configurar tudo
////        }
////        [Theory]
////        [ClassData(typeof(CultureInlineDataTests))]// classe criada com as cultures
////        public async Task Error_Empty_Name(string culture)
////        {

////            var request = RequestRegisterUserJsonBuilder.Build();
////            request.Name = string.Empty;


////            // post ou get ou put etc... depende do método
////            var response = await DoPost(method, request, culture);// nome do controller e a request

////            await using var responseBody = await response.Content.ReadAsStreamAsync(); //método deve ser stream

////            var responseData = await JsonDocument.ParseAsync(responseBody);

////            //  errors camel case sempre virá minusculo
////            //a propriedade errors vem de comunication ResponseErrorJson
////            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

////            //obtendo a mensagem esperada de acordo com a culture
////            var expectedMessage = ResourceMessagesException
////              .ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

////            Assert.NotEmpty(errors);
////            Assert.Single(errors);
////            Assert.Contains(expectedMessage, errors.Select(e => e.GetString()));
////            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
////            //tenho que verificar se não está salvando no banco após configurar tudo
////        }
////    }
////}
