
namespace Tropical.Domain.Repositories.User
{
    public interface IUserUpdateOnlyRepository
    {
        Task<Entities.User?> GetByIdAsync(long id);
        public void Update(Entities.User user);
    }
}
