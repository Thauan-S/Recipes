

using AutoMapper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tropical.Application.Extension;
using Tropical.Application.UseCases.Recipe.Delete;
using Tropical.Comunication.Pagination;
using Tropical.Comunication.Responses;
using Tropical.Domain.Dtos;
using Tropical.Domain.Entities;
using Tropical.Domain.Repositories.Recipe;
using Tropical.Domain.Services.LoggedUser;
using Tropical.Domain.Services.Storage;
using Tropical.Infrastructure.Services.Caching;

namespace Tropical.Application.UseCases.Recipe.DashBoard
{
    public class GetDashBoardUseCase : IGetDashBoardUseCase
    {
        private readonly  IRecipeReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IBlobStorageService _blobStorageService;
        private readonly ICachingService _cachingService;
        public GetDashBoardUseCase(IRecipeReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser, IBlobStorageService blobStorageService, ICachingService cachingService)
        {
            _repository = repository;
            _mapper = mapper;
            _loggedUser = loggedUser;
            _blobStorageService = blobStorageService;
            _cachingService = cachingService;
        }

        public async Task<ResponseRecipesJson> Execute(PaginationParameters parameters)
        {   
            var responseShortRecipeJson = new ResponseRecipesJson();

            IList <RecipesDto>? recipesDto;
            var loggedUser=await _loggedUser.User();

            var recipesCache = await _cachingService.GetAsync(loggedUser.Id.ToString());
            if (!string.IsNullOrWhiteSpace(recipesCache) ) {
               var recipe = JsonConvert.DeserializeObject<IList<ResponseShortRecipeJson>>(recipesCache);

                return new ResponseRecipesJson
                {
                    Recipes = recipe
                };
            }

            var recipes = await _repository.GetForDashBoard(loggedUser,parameters);
           

            if (recipes.Count>0) {
                await _cachingService.SetAsync(loggedUser.Id.ToString(), JsonConvert.SerializeObject(RecipesDto.EntityToDto(recipes)));
            }
                return new ResponseRecipesJson
            {
               
                Recipes = await recipes.MapToShortRecipeJson(loggedUser, _blobStorageService, _mapper)
            };
        }
    }
}
