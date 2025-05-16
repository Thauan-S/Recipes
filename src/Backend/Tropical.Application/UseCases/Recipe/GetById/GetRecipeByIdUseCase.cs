using AutoMapper;
using Tropical.Comunication.Responses;
using Tropical.Domain.Repositories;
using Tropical.Domain.Repositories.Recipe;
using Tropical.Domain.Services.LoggedUser;
using Tropical.Domain.Services.Storage;
using Tropical.Exceptions;
using Tropical.Exceptions.Exceptions;

namespace Tropical.Application.UseCases.Recipe.GetById
{
    public class GetRecipeByIdUseCase:IGetRecipeByIdUseCase
    {
        private readonly IRecipeReadOnlyRepository _repository;
        private readonly ILoggedUser _loggedUser;
        private readonly IMapper _mapper;
        private readonly IBlobStorageService _blobStorageService;

        public GetRecipeByIdUseCase(IRecipeReadOnlyRepository repository, ILoggedUser loggedUser, IMapper mapper, IBlobStorageService blobStorageService)
        {
            _repository = repository;
            _loggedUser = loggedUser;
            _mapper = mapper;
            _blobStorageService = blobStorageService;
        }

        public async Task<ResponseRecipeJson> Execute(long recipeId)
        {
            var loggedUser = await _loggedUser.User();

            var recipe = await _repository.GetById(loggedUser,recipeId);
            if (recipe == null)
                throw new NotFoundException(ResourceMessagesException.RECIPE_NOT_FOUND);
            
            var response= _mapper.Map<ResponseRecipeJson>(recipe);
            if (!string.IsNullOrEmpty(recipe.ImageIdentifier))
            {
                var url =await  _blobStorageService.GetFileUrl(loggedUser,recipe.ImageIdentifier);
                response.ImageUrl = url;
            }
            return response;

        }
    }
}
