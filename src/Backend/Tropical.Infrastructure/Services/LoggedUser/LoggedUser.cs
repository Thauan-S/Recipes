using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Tropical.Domain.Entities;
using Tropical.Domain.Security.Tokens;
using Tropical.Domain.Services;
using Tropical.Domain.Services.LoggedUser;
using Tropical.Infrastructure.Data;

namespace Tropical.Infrastructure.Services.LoggedUser
{
    public class LoggedUser : ILoggedUser // responsável por identificar o usuário logado
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly AppDbContext _dbContext;

        public LoggedUser(ITokenProvider tokenProvider, AppDbContext dbContext)
        {
            _tokenProvider = tokenProvider;
            _dbContext = dbContext;
        }


        public async Task<User> User()
        {// lendo o token 
            var token = _tokenProvider.Value();
            var tokenHandller = new JwtSecurityTokenHandler();
            var jwSecurityToken = tokenHandller.ReadJwtToken(token);
            var identifier = jwSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var userId = Guid.Parse(identifier);
            return await _dbContext
                .Users
                .AsNoTracking()
                .FirstAsync(user => user.UserId == userId && user.Active);
        }
    }
}
