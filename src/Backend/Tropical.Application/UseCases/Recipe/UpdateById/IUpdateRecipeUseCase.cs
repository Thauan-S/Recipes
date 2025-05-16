using Tropical.Comunication.Requests;

namespace Tropical.Application.UseCases.Recipe.UpdateById
{
    public interface IUpdateRecipeUseCase
    {
        Task Execute(long id, RequestRecipeJson request);
    }
}
