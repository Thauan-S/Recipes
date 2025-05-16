using WebApplication1.Models;

namespace WebApplication1.Services.User
{
    public interface IUserService
    {
       Task  Add(Models.User user);
        Task<Models.User> GetByEmail(string email);
    }
}
