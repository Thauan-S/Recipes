namespace Tropical.Application.UseCases.Recipe.Delete
{
    public interface IDeleteRecipeUseCase
    {
        Task Execute(long id);
    }
}
