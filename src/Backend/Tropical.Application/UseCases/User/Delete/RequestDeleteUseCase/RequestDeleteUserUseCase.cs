using Tropical.Domain.Repositories;
using Tropical.Domain.Repositories.User;
using Tropical.Domain.Services.LoggedUser;
using Tropical.Domain.Services.ServiceBus;
namespace Tropical.Application.UseCases.User.Delete.RequestDeleteUseCase
{
    public class RequestDeleteUserUseCase : IRequestDeleteUserUseCase
    {
        private readonly IDeleteUserQueue _deleteUserQueue;
        private readonly ILoggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        public RequestDeleteUserUseCase(ILoggedUser loggedUser, IUserUpdateOnlyRepository userUpdateOnlyRepository, IUnityOfWork unityOfWork, IDeleteUserQueue deleteUserQueue)
        {
            _loggedUser = loggedUser;
            _userUpdateOnlyRepository = userUpdateOnlyRepository;
            _unityOfWork = unityOfWork;
            _deleteUserQueue = deleteUserQueue;
        }

        public async Task Execute()
        {// como posso ter muitos dados do meu usuário eu quero que outro serviço faca isso
         // evitando um timeout na api
         //  já que a deleção  não precisa ser imediata
            var loggedUser = await _loggedUser.User();
            var user = await _userUpdateOnlyRepository.GetByIdAsync(loggedUser.Id);

            user.Active = false;
            _userUpdateOnlyRepository.Update(user);

            await _unityOfWork.Commit();

            await _deleteUserQueue.SendMessage(loggedUser);

        }
    }
}
