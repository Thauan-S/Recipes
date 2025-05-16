

namespace Tropical.Domain.Repositories.User
{
    public interface IUserWriteOnlyRepository
    {
        public  Task AddUser(Entities.User user);
        
    }
}
