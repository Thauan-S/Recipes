

namespace Tropical.Domain.Security.Tokens
{
    public interface ITokenProvider
    { // envia o token para o projeto de infra
        public string Value();
    }
}
