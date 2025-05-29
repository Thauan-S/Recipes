using Tropical.Comunication.Requests;
using Tropical.Comunication.Responses;

namespace Tropical.Application.UseCases.Token.RefreshToken
{
    public interface IUserRefreshTokenUseCase
    {
        Task<ResponseTokensJson> Execute(RequestNewTokenJson request);
    }
}
