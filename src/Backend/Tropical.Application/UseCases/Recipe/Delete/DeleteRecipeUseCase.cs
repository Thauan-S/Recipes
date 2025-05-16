

using AutoMapper;
using Tropical.Domain.Repositories;
using Tropical.Domain.Repositories.Recipe;
using Tropical.Domain.Repositories.User;
using Tropical.Domain.Services.LoggedUser;
using Tropical.Domain.Services.Storage;
using Tropical.Exceptions;
using Tropical.Exceptions.Exceptions;

namespace Tropical.Application.UseCases.Recipe.Delete
{
    public class DeleteRecipeUseCase : IDeleteRecipeUseCase
    {
        private readonly IRecipeWriteOnlyRepository _writeOnlyrepository;
        private readonly IRecipeReadOnlyRepository _recipeReadOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly ILoggedUser _loggedUser;

        private readonly IBlobStorageService _blobStorageService;
        public DeleteRecipeUseCase(ILoggedUser loggedUser, IRecipeWriteOnlyRepository writeOnlyrepository, IRecipeReadOnlyRepository recipeReadOnlyRepository, IUnityOfWork unityOfWork, IBlobStorageService blobStorageService)
        {

            _loggedUser = loggedUser;
            _writeOnlyrepository = writeOnlyrepository;
            _recipeReadOnlyRepository = recipeReadOnlyRepository;
            _unityOfWork = unityOfWork;
            _blobStorageService = blobStorageService;
        }



        public  async Task Execute(long id)
        {
            var loggedUser = await _loggedUser.User();
            var recipe= await _recipeReadOnlyRepository.GetById(loggedUser,id);
            if (recipe== null) 
            {
                throw new NotFoundException(ResourceMessagesException.RECIPE_NOT_FOUND);
            }
            if (!string.IsNullOrEmpty(recipe.ImageIdentifier))
            {
                await _blobStorageService.Delete(loggedUser,recipe.ImageIdentifier);

            }
            await _writeOnlyrepository.Delete(id); 
            await _unityOfWork.Commit();
        }
    }
}
