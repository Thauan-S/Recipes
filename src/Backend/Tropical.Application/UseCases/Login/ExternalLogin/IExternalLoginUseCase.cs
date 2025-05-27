namespace Tropical.Application.UseCases.Login.ExternalLogin
{
    public interface IExternalLoginUseCase
    {
        Task<string> Execute(string userName,string userEmail);
    }
}
