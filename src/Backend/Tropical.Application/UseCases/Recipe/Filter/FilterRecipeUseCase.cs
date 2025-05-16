using AutoMapper;
using Tropical.Application.Extension;
using Tropical.Comunication.Requests;
using Tropical.Comunication.Responses;
using Tropical.Domain.Repositories.Recipe;
using Tropical.Domain.Services.LoggedUser;
using Tropical.Domain.Services.Storage;
using Tropical.Exceptions.Exceptions;

namespace Tropical.Application.UseCases.Recipe.Filter
{
    public class FilterRecipeUseCase : IFilterRecipeUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IRecipeReadOnlyRepository _repository;
        private readonly IBlobStorageService _blobStorageService;
        public FilterRecipeUseCase(IMapper mapper, ILoggedUser loggedUser, IRecipeReadOnlyRepository repository, IBlobStorageService blobStorageService)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            _blobStorageService = blobStorageService;
        }

        public async Task<ResponseRecipesJson> Execute(RequestFilterRecipeJson request)
        {
            Validate(request);
            var loggedUser = await _loggedUser.User();

            var filters = new Domain.Dtos.FilterRecipesDto
            {
                RecipeTitle_Ingredient = request.RecipeTitle_Ingredient,
                CookingTimes = request.CookingTimes.Distinct().Select(c => (Domain.Enums.CookingTime)c).ToList(),
                DishTypes = request.DishTypes.Distinct().Select(c => (Domain.Enums.DishType)c).ToList(),
                Difficulties = request.Difficulties.Distinct().Select(c => (Domain.Enums.Difficulty)c).ToList()
            };
            var recipes= await _repository.Filter(loggedUser,filters);
            return new ResponseRecipesJson
            {
                //Recipes = _mapper.Map<List<ResponseShortRecipeJson>>(recipes)
                Recipes = await recipes.MapToShortRecipeJson(loggedUser,_blobStorageService,_mapper)
            };
        }
        private static void Validate(RequestFilterRecipeJson request) {
         var validator= new FilterRecipeValidator();

            var result=validator.Validate(request);
            if (!result.IsValid) {
                throw new ErrorOnValidationException(result.Errors.Select(e=>e.ErrorMessage).ToList());
            }
        }
        
    }
}