using Tropical.Comunication.Requests;
using Tropical.Domain.Repositories.User;
using Tropical.Domain.Security.Cryptography;
using Tropical.Domain.Security.Tokens;
using Tropical.Exceptions.Exceptions;
using Tropical.Comunication.Responses;
using Tropical.Domain.Entities;
using Tropical.Domain.Repositories.RefreshToken;
using Tropical.Infrastructure.Security.Tokens.Refresh;
using Tropical.Domain.Repositories;
namespace Tropical.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUnityOfWork _unityOfWork;
        public DoLoginUseCase(IPasswordEncripter passwordEncripter, IUserReadOnlyRepository repository, IAccessTokenGenerator accessTokenGenerator, IRefreshTokenGenerator refreshTokenGenerator, ITokenRepository tokenRepository, IUnityOfWork unityOfWork)
        {
            _passwordEncripter = passwordEncripter;
            _repository = repository;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _tokenRepository = tokenRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task<ResponseRegisterUserJson> Execute(RequestLoginJson request)
        {
        
            var user = await _repository.GetByEmail(request.Email);
           
            if (user == null|| !_passwordEncripter.IsValid(request.Password, user.Password))
            {
                throw new InvalidLoginException();
            }
            var refreshToken= await CreateAndSaveRefreshToken(user);

            return new ResponseRegisterUserJson()
            {
                Name = user.Name,
                Tokens = new ResponseTokensJson
                {
                    AccesToken= _accessTokenGenerator.Generate(user.UserId),
                    RefreshToken= refreshToken
                }
            };
        }
        private async Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user)
        {
            var refreshToken = _refreshTokenGenerator.Generate();

            await _tokenRepository.SaveNewRefreshToken(new RefreshToken
            {
                Value = refreshToken,
                UserId = user.Id
            });

            await _unityOfWork.Commit();

            return refreshToken;
        }

    }
}