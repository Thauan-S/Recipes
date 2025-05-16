using Tropical.Comunication.Requests;

namespace Tropical.Application.UseCases.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegisterUserJson> Execute(RequestLoginJson request);
    }
}
