
using WebApplication1.Repositories;

namespace WebApplication1.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task  Add(Models.User user)
        {
            await _userRepository.Add(user);
        }

        public async Task<Models.User> GetByEmail(string email)
        {
           return await _userRepository.GetByEmail(email);
        }
    }
}
