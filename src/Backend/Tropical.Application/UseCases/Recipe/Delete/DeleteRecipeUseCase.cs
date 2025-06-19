

using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sqids;
using Tropical.Comunication.Responses;
using Tropical.Domain.Entities;
using Tropical.Domain.Repositories;
using Tropical.Domain.Repositories.Recipe;
using Tropical.Domain.Repositories.User;
using Tropical.Domain.Services.LoggedUser;
using Tropical.Domain.Services.Storage;
using Tropical.Exceptions;
using Tropical.Exceptions.Exceptions;
using Tropical.Infrastructure.Services.Caching;

namespace Tropical.Application.UseCases.Recipe.Delete
{
    public class DeleteRecipeUseCase : IDeleteRecipeUseCase
    {
        private readonly IRecipeWriteOnlyRepository _writeOnlyrepository;
        private readonly IRecipeReadOnlyRepository _recipeReadOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly ILoggedUser _loggedUser;
        private readonly ICachingService _cachingService;
        private readonly SqidsEncoder<long> _encoder;
        private readonly IBlobStorageService _blobStorageService;
        public DeleteRecipeUseCase(ILoggedUser loggedUser, IRecipeWriteOnlyRepository writeOnlyrepository, IRecipeReadOnlyRepository recipeReadOnlyRepository, IUnityOfWork unityOfWork, IBlobStorageService blobStorageService, ICachingService cachingService, SqidsEncoder<long> encoder)
        {

            _loggedUser = loggedUser;
            _writeOnlyrepository = writeOnlyrepository;
            _recipeReadOnlyRepository = recipeReadOnlyRepository;
            _unityOfWork = unityOfWork;
            _blobStorageService = blobStorageService;
            _cachingService = cachingService;
            _encoder = encoder;
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
            var cachedData = await _cachingService.GetAsync(loggedUser.Id.ToString());

            if (string.IsNullOrEmpty(cachedData))
            {
                return;
            }

            var recipes = JsonConvert.DeserializeObject<List<ResponseShortRecipeJson>>(cachedData);

            if (recipes is null) return;

            var encodedId = _encoder.Encode(id);
            var recipeToRemove = recipes.FirstOrDefault(r => r.Id == encodedId);
            if (recipeToRemove != null)
            {
                recipes.Remove(recipeToRemove);

                var updatedData = JsonConvert.SerializeObject(recipes);
                await _cachingService.SetAsync(loggedUser.Id.ToString(), updatedData);
            }
        }
    }
}
