using Tropical.Comunication.Requests;
using Tropical.Comunication.Responses;

namespace Tropical.Application.UseCases.Recipe.Generation
{
    public interface IGenerateRecipeUseCase
    {
        Task<ResponseGeneratedRecipeJson> Execute(RequestGenerateRecipeJson request);
    }
}
