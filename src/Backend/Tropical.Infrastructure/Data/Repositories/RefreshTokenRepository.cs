using Microsoft.EntityFrameworkCore;
using Tropical.Domain.Entities;
using Tropical.Domain.Repositories.RefreshToken;

namespace Tropical.Infrastructure.Data.Repositories
{
    public class RefreshTokenRepository : ITokenRepository
    {
        private readonly AppDbContext _appDbContext;

        public RefreshTokenRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<RefreshToken?> Get(string refreshToken)
        {
           return await _appDbContext
                .RefreshTokens
                .AsNoTracking()
                .Include(t=>t.User)
                .FirstOrDefaultAsync(r=>r.Value.Equals(refreshToken));
        }

        public async Task SaveNewRefreshToken(RefreshToken refreshToken)
        {
            ///TODO se eu tiver outros tipos de aplicações web, app ...
            ///terei que refazer isso
            var tokens =  _appDbContext
                 .RefreshTokens
                 .Where(t => t.UserId == refreshToken.UserId);
            _appDbContext.RefreshTokens.RemoveRange(tokens);

            await _appDbContext.AddAsync(refreshToken);
        }
    }
}
