using Tropical.Comunication.Requests;

namespace Tropical.Application.UseCases.User.Update
{
    public interface IUpdateUserUseCase
    {
        public Task Validate(RequestUpdateUserJson request, string currentEmail);
        public Task Execute(RequestUpdateUserJson request);
    }
}
