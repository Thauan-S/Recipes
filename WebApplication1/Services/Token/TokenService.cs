//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.IdentityModel.Tokens;
//using WebApplication1.Context;
//using WebApplication1.Dto;
//using WebApplication1.Models;
//using WebApplication1.Repositories;
//using WebApplication1.Services.Client;

//namespace WebApplication1.Services.Token
//{
//    public class TokenService : ItokenService
//    {
//        private readonly string _secretKey = "d33b5e2e-e925-40c3-9991-f84aaab0825c";

//        private readonly string _issuer = "back";
//        private readonly string _audience = "backend";
//        private readonly IClientRepository _clientRepository;

//        public TokenService( IClientRepository repository) {
        
//        _clientRepository = repository;
//        }
//        public   string GenerateToken(LoginRequest loginRequest)
//        {

        
//        var clientDb =   _clientRepository.FindByEmail (loginRequest.Email);
//           if (clientDb.Result.Data.Email != loginRequest.Email || loginRequest.Password != clientDb.Result.Data.Password)
//                return String.Empty;
//                                // Lib IdentityModelTokens
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
           

//            var signinCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//                new Claim(type:ClaimTypes.Email,loginRequest.Email),
//                new Claim(type:ClaimTypes.Role,clientDb.Result.Data.Role),
//                //new Claim("role",clientDb.Result.Data.Role),
               
//            };

//            var token = new JwtSecurityToken(
//                issuer: _issuer,
//                audience: _audience,
//                claims: claims,
//                expires: DateTime.Now.AddHours(1),
//                signingCredentials: signinCredentials
//                );
//            return new JwtSecurityTokenHandler().WriteToken(token);
            
//        }
//    }
//}
