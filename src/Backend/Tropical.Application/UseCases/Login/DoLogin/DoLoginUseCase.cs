using Tropical.Comunication.Requests;
using Tropical.Domain.Repositories.User;
using Tropical.Domain.Security.Cryptography;
using Tropical.Domain.Security.Tokens;
using Tropical.Exceptions.Exceptions;

namespace Tropical.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;

        public DoLoginUseCase(IPasswordEncripter passwordEncripter, IUserReadOnlyRepository repository, IAccessTokenGenerator accessTokenGenerator)
        {
            _passwordEncripter = passwordEncripter;
            _repository = repository;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseRegisterUserJson> Execute(RequestLoginJson request)
        {
            var encriptedPassword = _passwordEncripter.Encrypt(request.Password);
            var user = await _repository.GetUserByEmailAndPassword(request.Email, encriptedPassword) ?? throw new InvalidLoginException();
            return new ResponseRegisterUserJson()
            {
                Token = _accessTokenGenerator.Generate(user.UserId),
                Name = user.Name,
            };
        }
    }
}