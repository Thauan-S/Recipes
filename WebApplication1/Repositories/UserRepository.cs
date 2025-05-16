//using Microsoft.EntityFrameworkCore;
//using WebApplication1.Context;
//using WebApplication1.Models;

//namespace WebApplication1.Repositories
//{
//    public class UserRepository:IUserRepository
//    {
//        private readonly AppDbContext _context;

//        public UserRepository(AppDbContext context)
//        {
//            _context = context;
//        }
//        public async Task Add(User user)
//        {
//                var userFromDb = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
//                if (userFromDb != null) throw new Exception("Já existe um cliente cadastrado com esse email");
               
//                _context.Users.Add(user);
//                await _context.SaveChangesAsync();

//        }
//        public async Task<User?> GetByEmail(string email)
//        {
//            var userFromDb = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
//            return (User?)userFromDb;
//        }
//    }
//}
