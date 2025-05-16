using Tropical.Comunication.Requests;

namespace Tropical.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        public Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson user);
    }
}
