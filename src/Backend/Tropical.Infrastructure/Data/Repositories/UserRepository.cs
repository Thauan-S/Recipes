using Microsoft.EntityFrameworkCore;
using Tropical.Domain.Entities;
using Tropical.Domain.Repositories.User;

namespace Tropical.Infrastructure.Data.Repositories
{
    
    public class UserRepository: IUserWriteOnlyRepository, IUserReadOnlyRepository,IUserUpdateOnlyRepository,IUserDeleteOnlyRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)=>  _dbContext = dbContext;
        public async Task AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public  async Task<bool> ExistActiveUserWithEmail(string email) {
            var isActive = await _dbContext.Users.AnyAsync(u => u.Email.Equals(email) && u.Active);
            return isActive;
        }

        public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier)
        {
           return await _dbContext.Users.AnyAsync(u => u.UserId.Equals(userIdentifier)&& u.Active);
        }

        public async  Task<User?> GetByIdAsync(long id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u=>u.Id==id);
        }

        public async Task<User?> GetUserByEmailAndPassword(string email, string password)
        {
             return await _dbContext.Users
                .AsNoTracking() // se eu não estiver fazendo atualização do dado savechangesAsync() ou delete()
                                // devo   usar asNoTraking() por questões de performance, assim o ef não fica fazendo tracking 
                                //desse dado
                .FirstOrDefaultAsync(u => 
            u.Email.Equals(email) && u.Password.Equals(password) && u.Active );
           
        }

        public void Update(User user)
        {
           _dbContext.Users.Update(user);
        }
        public async Task DeleteAccount(Guid userIdentifyer)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u=>u.UserId==userIdentifyer);
            if(user is null)
            {
                return;
            }
            var recipes=_dbContext.Recipes.Where(recipe => recipe.UserId == user.Id);
            // posso adicionar o ondelete cascade em minha table , assim não precisaria deletar as recipes primeiro
            _dbContext.Recipes.RemoveRange(recipes);
            _dbContext.Users.Remove(user);
        }
    }
}

