using Tropical.Domain.Entities;

namespace Tropical.Domain.Repositories.RefreshToken
{
    public interface ITokenRepository
    {
        Task<Entities.RefreshToken?> Get(string refreshToken);
        Task SaveNewRefreshToken(Entities.RefreshToken refreshToken);
    }
}
