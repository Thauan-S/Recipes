using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Tropical.Domain.Security.Tokens;

namespace Tropical.Infrastructure.Security.Tokens.Access.Generator
{
    public class JwtTokenGenerator : JwtTokenHandler, IAccessTokenGenerator
    {   // verificar registerUseCase APPLICATION
        //as configs estão em appsetings.development
        private readonly uint _expirationTimeMinutes;
        private readonly string _signingKey;
        public JwtTokenGenerator(uint expirationTimeMinutes, string signingKey)
        {
            _expirationTimeMinutes = expirationTimeMinutes;
            _signingKey = signingKey;
        }
        // OBSERVAR A  CLASSE DEPENDENCY INJECTION EXTENSION
        public string Generate(Guid userIdentifier)// id do user
        { // instalar em infra System.IdentityModel.Token
            var securityKey = SecurityKey(_signingKey);// tenho que converter a string em security key, a função está em Jwt Token Handler

            var claims=new List<Claim>() // identificadores do usuário
            {   // não informar dados pessoais 
                new Claim(ClaimTypes.Sid,userIdentifier.ToString())// claimTypes.name etcc.. tem vários
                // new Claim(ClaimTypes.Email,email)
            };
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
                SigningCredentials = new SigningCredentials(// chave de assinatura
                    securityKey,
                    SecurityAlgorithms.HmacSha256Signature), 
                    // algoritmo de assinatura para a chave
                IssuedAt = DateTime.UtcNow,
                Issuer = "mybackend",
                Subject=new ClaimsIdentity(claims) // dono do token
            };
            var tokenHanlder = new JwtSecurityTokenHandler(); // gerando o token
            var securityToken =tokenHanlder.CreateToken(tokenDescriptor);
            return tokenHanlder.WriteToken(securityToken); 
        }

    }
}
