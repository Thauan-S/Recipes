using System.Runtime.CompilerServices;
using Tropical.Domain.Repositories;
using Tropical.Domain.Repositories.User;
using Tropical.Domain.Security.Tokens;

namespace Tropical.Application.UseCases.Login.ExternalLogin
{
    public class ExternalLoginUseCase:IExternalLoginUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IAccessTokenGenerator _accessTokenGenerator;

        public ExternalLoginUseCase(
            IUserReadOnlyRepository userReadOnlyRepository,
            IUserWriteOnlyRepository userWriteOnlyRepository,
            IUnityOfWork unityOfWork,
            IAccessTokenGenerator accessTokenGenerator)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _unityOfWork = unityOfWork;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<string> Execute(string userName, string userEmail)
        {
            var user = await _userReadOnlyRepository.GetByEmail(userEmail);
            ///TODO verificar se existe uma forma de obter a senha do google 
            if (user == null) {
                user = new Domain.Entities.User()
                {
                    Name = userName,
                    Email = userEmail,
                    Password = "-"
                };
                await _userWriteOnlyRepository.AddUser(user);
                await _unityOfWork.Commit();
            }
            return _accessTokenGenerator.Generate(user.UserId);
        }
    }
}
