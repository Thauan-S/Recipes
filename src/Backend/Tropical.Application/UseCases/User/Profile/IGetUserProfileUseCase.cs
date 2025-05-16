
using Tropical.Comunication.Responses;

namespace Tropical.Application.UseCases.User.Profile
{
    public interface IGetUserProfileUseCase
    {
        public Task<ResponseUserProfileJson> Execute();
    }
}
