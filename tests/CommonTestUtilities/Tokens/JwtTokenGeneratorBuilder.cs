using Tropical.Domain.Security.Tokens;
using Tropical.Infrastructure.Security.Tokens.Access.Generator;

namespace CommonTestUtilities.Tokens
{
    public static class JwtTokenGeneratorBuilder
    {
        public static IAccessTokenGenerator Build()=> 
            new JwtTokenGenerator(expirationTimeMinutes:5,
                signingKey:"ttttttttttttttttttttttttttttttt");//32 caracteres
    }
}
