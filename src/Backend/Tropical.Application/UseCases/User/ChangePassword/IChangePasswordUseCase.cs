

using Tropical.Comunication.Requests;

namespace Tropical.Application.UseCases.User.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        Task Execute(RequestChangePasswordUserJson request);
    }
}
