namespace Tropical.Application.UseCases.User.Delete.Delete
{
    public interface IDeleteUserAccountUseCase
    {
        Task Execute(Guid userIdentifyer);
    }
}
