using Tropical.Domain.Entities;

namespace Tropical.Domain.Repositories.User
{
    public interface IRecipeWriteOnlyRepository
    {
        Task Add(Entities.Recipe recipe);
        Task Delete(long recipeId);
    }
}
