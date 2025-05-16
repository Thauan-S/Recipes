//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using Microsoft.IdentityModel.Tokens;
//using Tropical.Domain.Security.Tokens;

//namespace Tropical.Infrastructure.Security.Tokens.Access.validator
//{
//    public class JwtTokenValidator : JwtTokenHandler, IAccessTokenValidator
//    {
//        private readonly string _signingKey;
//        //lembrando que essa classe não é obrigatória , basta configurar isso no program.cs , aula 100
//        public JwtTokenValidator(string signingKey)
//        {
//            _signingKey = signingKey;
//        }

//        public Guid ValidateAndGetUserIdentifier(string token)
//        {
//            var validationParameter = new TokenValidationParameters
//            {
//                ValidateAudience = false,// não valido quem vai utilizar o token
//                ValidateIssuer = false, //não valido quem emitiu o token
//                //nesse caso só valido tempo de expiração e chave
//                IssuerSigningKey = SecurityKey(_signingKey),
//                ClockSkew = new TimeSpan(), // evita problemas de tempo de expiração
//            };
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var principal=tokenHandler.ValidateToken(token, validationParameter, out _);// out retorna um Security token , mas não preciso
//           var userIdentifier= principal.Claims.First(c=>c.Type==ClaimTypes.Sid).Value;// tipo usado nas claims do JwtTokenGenerator
//            return Guid.Parse(userIdentifier); 
//            //criei na apí uma classe de AuthenticatedUserAttribute
//        }
//    }
//}
