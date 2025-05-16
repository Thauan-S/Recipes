using Tropical.Comunication.Requests;
using Tropical.Comunication.Responses;

namespace Tropical.Application.UseCases.Recipe.Filter
{
    public interface IFilterRecipeUseCase
    {
        Task<ResponseRecipesJson> Execute(RequestFilterRecipeJson request);
    }
}
