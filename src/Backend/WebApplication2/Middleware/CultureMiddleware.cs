using System.Globalization;

namespace Tropical.API.Middleware
{
    public class CultureMiddleware // não esquecer de adicionar ao program.cs
    {

        private readonly RequestDelegate   _next;
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);// recebe todas os tipos de culturas
            var requestedCulture=context.Request.Headers.AcceptLanguage.FirstOrDefault();// pegando os valores através do header da requisição
            var cultureInfo = new CultureInfo("en");
            if ( !string.IsNullOrWhiteSpace(requestedCulture)
                && supportedLanguages.Any(c=>c.Name.Equals(requestedCulture)))
            {
                cultureInfo= new CultureInfo(requestedCulture);
            }
            CultureInfo.CurrentCulture= cultureInfo;
            CultureInfo.CurrentUICulture= cultureInfo;

            await _next(context);
        }
    }
}
