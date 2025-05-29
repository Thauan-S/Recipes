using Tropical.Domain.Entities;

namespace Tropical.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWithEmail(string email);
        public Task<Entities.User?> GetByEmail(string email);
        public Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);
    }
}
