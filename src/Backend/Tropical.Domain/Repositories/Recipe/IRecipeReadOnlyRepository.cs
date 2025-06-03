using System.Text;
using Tropical.Comunication.Pagination;
using Tropical.Domain.Dtos;

namespace Tropical.Domain.Repositories.Recipe
{
    public interface IRecipeReadOnlyRepository
    {
        Task<IList<Entities.Recipe>> Filter(Entities.User user, FilterRecipesDto filters);
        Task<Entities.Recipe?> GetById(Entities.User user, long recipeId);
        Task<bool> RecipeExists(Entities.User user, long recipeId);
        Task<IList<Entities.Recipe>> GetForDashBoard(Entities.User user,PaginationParameters parameters);
    }
}
