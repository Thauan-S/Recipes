using System.Net.Http.Json;

namespace WebApi.Test
{
    public class TropicalClassFixture : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        public TropicalClassFixture(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();

        } ///TODO USAR O MOQ
        protected async Task<HttpResponseMessage> DoPost(string method, object request, string culture = "en")
        {
            ChangeRequestCulture(culture);
            // method == nome do controller
            // post ou get ou put etc... depende do método
            return await _httpClient.PostAsJsonAsync(method, request);
        }
        private void ChangeRequestCulture(string culture)
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
            { //veririfa se o request reader já tem a accept language e remove ela
              //pois multiplas cultures serão testadas
                _httpClient.DefaultRequestHeaders.Remove("Accept-Language");
            }
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);
        }
    }
}
