using Tropical.Comunication.Responses;

namespace Tropical.Application.UseCases.Recipe.GetById
{
    public interface IGetRecipeByIdUseCase
    {
        Task<ResponseRecipeJson> Execute(long recipeId);
    }
}
