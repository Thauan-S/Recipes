using Tropical.Comunication.Requests;
using Tropical.Comunication.Responses;

namespace Tropical.Application.UseCases.Recipe
{
    public interface IRegisterRecipeUseCase
    {
        Task<ResponseRegisteredRecipeJson> Execute(RequestRecipeRegisterFormData request);
    }
}
