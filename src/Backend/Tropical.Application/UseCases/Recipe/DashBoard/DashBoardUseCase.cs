

using AutoMapper;
using Tropical.Application.Extension;
using Tropical.Application.UseCases.Recipe.Delete;
using Tropical.Comunication.Pagination;
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

        public async Task<ResponseRecipesJson> Execute(PaginationParameters parameters)
        {
            var loggedUser=await _loggedUser.User();
            var recipes = await _repository.GetForDashBoard(loggedUser,parameters);
            return new ResponseRecipesJson
            {
               
                Recipes = await recipes.MapToShortRecipeJson(loggedUser, _blobStorageService, _mapper)
            };
        }
    }
}
