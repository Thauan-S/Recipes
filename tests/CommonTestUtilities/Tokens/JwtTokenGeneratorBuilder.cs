using Tropical.Domain.Security.Tokens;
using Tropical.Infrastructure.Security.Tokens.Access.Generator;

namespace CommonTestUtilities.Tokens
{
    public static class JwtTokenGeneratorBuilder
    {
        public static IAccessTokenGenerator Build()=> 
            new JwtTokenGenerator(expirationTimeMinutes:5,
                signingKey: "kjs93Jfnwq8sl02mvlsRjv39dlfjw93kwww");//32 ou +  caracteres
    }
}
