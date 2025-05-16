

using AutoMapper;
using Tropical.Application.Extension;
using Tropical.Application.UseCases.Recipe.Delete;
using Tropical.Comunication.Responses;
using Tropical.Domain.Repositories.Recipe;
using Tropical.Domain.Services.LoggedUser;
using Tropical.Domain.Services.Storage;

namespace Tropical.Application.UseCases.Recipe.DashBoard
{
    public class GetDashBoardUseCase : IGetDashBoardUseCase
    {
        private readonly  IRecipeReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IBlobStorageService _blobStorageService;
        public GetDashBoardUseCase(IRecipeReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser, IBlobStorageService blobStorageService)
        {
            _repository = repository;
            _mapper = mapper;
            _loggedUser = loggedUser;
            _blobStorageService = blobStorageService;
        }

        public async Task<ResponseRecipesJson> Execute()
        {
            var loggedUser=await _loggedUser.User();
            var recipes = await _repository.GetForDashBoard(loggedUser);
            return new ResponseRecipesJson
            {
                //Recipes = _mapper.Map<IList<ResponseShortRecipeJson>>(recipes)
                //usando extension methods
                Recipes = await recipes.MapToShortRecipeJson(loggedUser, _blobStorageService, _mapper)
            };
        }
    }
}
