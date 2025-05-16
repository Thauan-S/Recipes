using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Tropical.Infrastructure.Security.Tokens.Access
{
    public abstract class JwtTokenHandler
    {
        protected SymmetricSecurityKey SecurityKey(string signingKeu)
        {
            var bytes = Encoding.UTF8.GetBytes(signingKeu);//converte a string em um array de bytes
            return new SymmetricSecurityKey(bytes);
        }
    }
}
