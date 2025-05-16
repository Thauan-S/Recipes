using Tropical.Domain.Entities;

namespace Tropical.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWithEmail(string email);
        public Task<Entities.User?> GetUserByEmailAndPassword(string email, string password);
        public Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);
    }
}
