using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IUserRepository
    {
        public  Task Add(User user);
        public Task<User?> GetByEmail(string email);
    }
}
