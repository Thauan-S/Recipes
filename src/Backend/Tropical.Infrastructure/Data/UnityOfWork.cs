using Tropical.Domain.Repositories;
namespace Tropical.Infrastructure.Data
{
    public class UnityOfWork:IUnityOfWork
    {
        private readonly AppDbContext _appDbContext;
        
        public UnityOfWork(AppDbContext appDbContext) {
        _appDbContext = appDbContext;
        }
        public async Task Commit() 
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
