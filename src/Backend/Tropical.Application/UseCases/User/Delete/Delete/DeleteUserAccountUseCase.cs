
using Tropical.Domain.Repositories;
using Tropical.Domain.Repositories.User;
using Tropical.Domain.Services.Storage;

namespace Tropical.Application.UseCases.User.Delete.Delete
{
    public class DeleteUserAccountUseCase : IDeleteUserAccountUseCase
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IUserDeleteOnlyRepository _repository;
        private readonly IBlobStorageService _blobStorageService;

        public DeleteUserAccountUseCase(IUnityOfWork unityOfWork, IUserDeleteOnlyRepository userDeleteOnlyRepository, IBlobStorageService blobStorageService)
        {
            _unityOfWork = unityOfWork;
            _repository = userDeleteOnlyRepository;
            _blobStorageService = blobStorageService;
        }

        public async Task Execute(Guid userIdentifyer)
        {
            try
            {
                await _blobStorageService.DeleteContainer(userIdentifyer);
                await _repository.DeleteAccount(userIdentifyer);
                await _unityOfWork.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
