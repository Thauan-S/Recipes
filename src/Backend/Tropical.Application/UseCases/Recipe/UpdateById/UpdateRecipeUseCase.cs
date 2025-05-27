
using AutoMapper;
using Tropical.Comunication.Requests;
using Tropical.Domain.Repositories;
using Tropical.Domain.Repositories.Recipe;
using Tropical.Domain.Services.LoggedUser;
using Tropical.Exceptions;
using Tropical.Exceptions.Exceptions;

namespace Tropical.Application.UseCases.Recipe.UpdateById
{
    public class UpdateRecipeUseCase : IUpdateRecipeUseCase
    {
        private readonly IRecipeUpdateOnlyRepository _repository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        public UpdateRecipeUseCase(
            IRecipeUpdateOnlyRepository repository,
            IUnityOfWork unityOfWork, IMapper mapper,
            ILoggedUser loggedUser)
        {
            _repository = repository;
            _unityOfWork = unityOfWork;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task Execute(long id,RequestRecipeJson request)
        {
            Validate(request);
            var loggedUser = await _loggedUser.User();
            var recipe = await _repository.GetById(loggedUser,id);
            if (recipe == null) {
                throw new NotFoundException(ResourceMessagesException.RECIPE_NOT_FOUND);
            }
            recipe.Instructions.Clear();
            recipe.Ingredients.Clear();
            recipe.DishTypes.Clear();

            _mapper.Map(request,recipe);
                                         
             var instructions =request.Instructions.OrderBy(i=>i.Step).ToList();
            for(var i=0; i< instructions.Count; i++)
            {
                instructions.ElementAt(i).Step = i+1;
                recipe.Instructions=_mapper.Map<IList<Domain.Entities.Instruction>>(instructions);
                _repository.Update(recipe);
                await _unityOfWork.Commit();
            }
            
        }
        private static void Validate(RequestRecipeJson request)
        {
            var result = new RecipeValidator().Validate(request);
            if (!result.IsValid)
            {
                throw new ErrorOnValidationException(result.Errors.Select(e=>e.ErrorMessage).Distinct().ToList());
            }
        }
    }
}
