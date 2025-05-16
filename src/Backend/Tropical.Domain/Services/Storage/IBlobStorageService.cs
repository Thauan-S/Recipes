using Tropical.Domain.Entities;


namespace Tropical.Domain.Services.Storage
{
    public interface IBlobStorageService
    { // interface para envio de arquivos para o azure
        Task Updload(User user ,Stream file ,string fileName);
        Task<string> GetFileUrl(User loggedUser,string fileName);
        Task Delete(User loggedUser, string fileName);
        Task DeleteContainer(Guid userIdentifier);
    }
}
