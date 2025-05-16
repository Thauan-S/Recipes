using Tropical.Domain.Security.Tokens;

namespace Tropical.API.Token
{
    public class HttpContextTokenValue : ITokenProvider
    { // a interface ItokenProvider está sendo utilizada no meu Infra
        //builder.Services.AddScoped<ITokenProvider,HttpContextTokenValue>();
        //adicionar a config em PROGRAM.CSSSSSSSSSSS
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextTokenValue(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string Value()
        {
            var authentication = _contextAccessor
                .HttpContext!
                .Request
                .Headers
                .Authorization
                .ToString();
            return authentication["Bearer ".Length..].Trim();
        }
    }
}
