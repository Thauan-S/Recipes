using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;
using Tropical.Application.Extension;
using Tropical.Domain.Repositories;
using Tropical.Domain.Repositories.Recipe;
using Tropical.Domain.Services.LoggedUser;
using Tropical.Domain.Services.Storage;
using Tropical.Exceptions;
using Tropical.Exceptions.Exceptions;

namespace Tropical.Application.UseCases.Recipe.UpdateImageById
{
    class AddUpdateImageCoverUseCase:IAddUpdateImageCoverUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IRecipeUpdateOnlyRepository _recipeUpdateOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IBlobStorageService _blobStorageService;

        public AddUpdateImageCoverUseCase(ILoggedUser loggedUser, IRecipeUpdateOnlyRepository recipeUpdateOnlyRepository, IUnityOfWork unityOfWork, IBlobStorageService blobStorageService)
        {
            _loggedUser = loggedUser;
            _recipeUpdateOnlyRepository = recipeUpdateOnlyRepository;
            _unityOfWork = unityOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task Execute(long recipeId, IFormFile file)
        {
            // apagar pasta obj e bin que foi instalada a lib, pois está tendo um bug
            var loggedUser = await _loggedUser.User();

            var recipe = await _recipeUpdateOnlyRepository.GetById(loggedUser,recipeId);

            if (recipe == null)
                throw new NotFoundException(ResourceMessagesException.RECIPE_NOT_FOUND);

           var fileStream= file.OpenReadStream();

            (var isValidImage, var extension) = fileStream.ValidateAndGetImageExtension();
            //método de extensão
            //File.TypeChecker package
           if(!isValidImage)
            {// PNG || JPG // verificar a classe no método de extensão .Is 
                throw new ErrorOnValidationException(new List<string> { ResourceMessagesException.ONLY_IMAGES_SUPPORTED });
            }
            if (string.IsNullOrEmpty(recipe.ImageIdentifier))
            {
                recipe.ImageIdentifier = $"{Guid.NewGuid()}{extension}";

                _recipeUpdateOnlyRepository.Update(recipe);
                
                await _unityOfWork.Commit();
            }
            //fileStream.Position = 0;// importante para uso em streams
            // ele garante que o stream seja lido do início, pois  a validação acima move o inicio do arr para 20
            await _blobStorageService.Updload(loggedUser,fileStream,recipe.ImageIdentifier);
        }
     }
}
