using Tropical.Comunication.Requests;
using Tropical.Comunication.Responses;
using Tropical.Domain.Repositories;
using Tropical.Domain.Repositories.RefreshToken;
using Tropical.Domain.Security.Tokens;
using Tropical.Domain.ValueObjects;
using Tropical.Exceptions.Exceptions;

namespace Tropical.Application.UseCases.Token.RefreshToken
{
    public class UserRefreshTokenUseCase : IUserRefreshTokenUseCase
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IAccessTokenGenerator _accesTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;

        public UserRefreshTokenUseCase(ITokenRepository tokenRepository, IUnityOfWork unityOfWork, IAccessTokenGenerator accesTokenGenerator, IRefreshTokenGenerator refreshTokenGenerator)
        {
            _tokenRepository = tokenRepository;
            _unityOfWork = unityOfWork;
            _accesTokenGenerator = accesTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
        }

        public async Task<ResponseTokensJson> Execute(RequestNewTokenJson request)
        {
            var refreshToken = await _tokenRepository.Get(request.RefreshToken);
            
            if (refreshToken == null)
            {
                throw new RefreshTokenNotFoundException();
            }

            var refreshTokenValidUntil = refreshToken.CreatedOn.AddMinutes(MyTropicalRuleConstants.REFRESH_TOKEN_EXPIRATION_MINUTES);
            
            if (DateTime.Compare(refreshTokenValidUntil, DateTime.UtcNow) < 0)
            {
                throw new RefreshTokenExpiredException();
            }

            var newRefreshToken = new Domain.Entities.RefreshToken
            {
                Value = _refreshTokenGenerator.Generate(),
                UserId = refreshToken.User.Id,
            };

            await _tokenRepository.SaveNewRefreshToken(newRefreshToken);
            await _unityOfWork.Commit();

            return new ResponseTokensJson
            {
                AccesToken = _accesTokenGenerator.Generate(refreshToken.User.UserId),
                RefreshToken = refreshToken.Value
            };
        }
    }
}