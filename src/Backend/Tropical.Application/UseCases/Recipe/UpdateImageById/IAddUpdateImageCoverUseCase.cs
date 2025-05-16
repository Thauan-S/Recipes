using Microsoft.AspNetCore.Http;

namespace Tropical.Application.UseCases.Recipe.UpdateImageById
{
    public interface IAddUpdateImageCoverUseCase
    {
        Task Execute(long recipeId,IFormFile file);
    }
}
