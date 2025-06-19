
using Tropical.Comunication.Pagination;

namespace Tropical.Infrastructure.Services.Caching
{
    public interface ICachingService
    {
        Task<string> GetAsync(string key );
        Task SetAsync(string key,string value);
        Task RemoveAsync(string key);
        
    }
}
