using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Tropical.Domain.Entities;
using Tropical.Domain.Services.Storage;
using Tropical.Domain.ValueObjects;

namespace Tropical.Infrastructure.Services.Storage
{
    public class AzureStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        
        public AzureStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

      

        public async Task<string> GetFileUrl(User loggedUser,string fileName)
        {
            var containerName=loggedUser.UserId.ToString();
            var containerClient=_blobServiceClient.GetBlobContainerClient(containerName);
            var exist = await containerClient.ExistsAsync();
            if (!exist.Value)
            {
                return string.Empty;
            }
            var blobClient = containerClient.GetBlobClient(fileName);
            exist=await blobClient.ExistsAsync();
            
            if (exist.Value) {
                var sasBuilder = new BlobSasBuilder { // constrói uma assinatura de acesso compartilhado Shared Access Signatures (SAS)
                    // acessa a url com níveis de permissão em meu app 
                    BlobContainerName = containerName,
                    BlobName = fileName,
                    Resource= "b",// tipo do arquivo: blob
                    ExpiresOn = DateTime.UtcNow.AddMinutes(MyTropicalRuleConstants.MAXIMUM_IMAGE_URL_LIFETIME_IN_MINUTES),
                };
                sasBuilder.SetPermissions(BlobSasPermissions.Read);
                return blobClient.GenerateSasUri(sasBuilder).ToString();
            }
            return string.Empty;
        }

        // integraçao com o blob storage do azure
        public async Task Updload(User user, Stream file, string fileName)
        {
           var container= _blobServiceClient.GetBlobContainerClient(user.UserId.ToString());
           await container.CreateIfNotExistsAsync();// cria o container no azure caso nao exista
           
            var blobClient= container.GetBlobClient(fileName);
                                              // sobrescreve o nome se o arquivo já existir
           await blobClient.UploadAsync(file,overwrite:true);
        }
        public async Task Delete(User loggedUser, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(loggedUser.UserId.ToString());
            var exist=await containerClient.ExistsAsync();
            if (exist.Value) {
                await containerClient.DeleteBlobIfExistsAsync(fileName);
            }
        }
        public async Task DeleteContainer(Guid userIdentifier)
        {
            var container = _blobServiceClient.GetBlobContainerClient(userIdentifier.ToString());
             await container.DeleteIfExistsAsync();
        }
    }
}
