
using System.Linq.Expressions;
using AutoMapper;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Tropical.Application.Extension;
using Tropical.Comunication.Requests;
using Tropical.Comunication.Responses;
using Tropical.Domain.Entities;
using Tropical.Domain.Repositories;
using Tropical.Domain.Repositories.User;
using Tropical.Domain.Services.LoggedUser;
using Tropical.Domain.Services.Storage;
using Tropical.Exceptions;
using Tropical.Exceptions.Exceptions;
using Tropical.Infrastructure.Data;

namespace Tropical.Application.UseCases.Recipe.Register
{
    public class RegisterRecipeUseCase : IRegisterRecipeUseCase
    {
        private readonly IRecipeWriteOnlyRepository _repository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly IBlobStorageService _blobStorageService;

        public RegisterRecipeUseCase(IRecipeWriteOnlyRepository repository, ILoggedUser loggedUser, IUnityOfWork unityOfWork, IMapper mapper, IBlobStorageService blobStorageService)
        {
            _repository = repository;
            _loggedUser = loggedUser;
            _unityOfWork = unityOfWork;
            _mapper = mapper;
            _blobStorageService = blobStorageService;
        }
        public async Task<ResponseRegisteredRecipeJson> Execute(RequestRecipeRegisterFormData request)
        {
            Validate(request);
            var loggedUser = await _loggedUser.User();

            var recipe = _mapper.Map<Domain.Entities.Recipe>(request);

            recipe.UserId = loggedUser.Id;

            var instructions = recipe.Instructions.OrderBy(i => i.Step).ToList();
            for (int i = 0; i < instructions.Count; i++)
            {
                instructions[i].Step = i + 1;
            }
            recipe.Instructions = _mapper.Map<IList<Instruction>>(instructions);
            if (request.Image != null)
            {
                var fileStream = request.Image.OpenReadStream();
                
                (var IsvalidImage, var extension) = fileStream.ValidateAndGetImageExtension();
                // observe a quantidade de código diminuída com os métodos de extensão 
                // usando o método de extensão ValidateAndGetImageExtension();
                //if (!fileStream.Is<PortableNetworkGraphic>()&& !fileStream.Is<JointPhotographicExpertsGroup>())
                //{
                //    throw new ErrorOnValidationException([ResourceMessagesException.ONLY_IMAGES_SUPPORTED]);
                //} 
                if (!IsvalidImage)
                {
                    throw new ErrorOnValidationException([ResourceMessagesException.OLY_IMAGES_SUPPORTED]);
                }
               // fileStream.Position = 0;
                // recipe.ImageIdentifier = $"{Guid.NewGuid()}{Path.GetExtension(request.Image.FileName)}";
                recipe.ImageIdentifier = $"{Guid.NewGuid()}{extension}";
                await _blobStorageService.Updload(loggedUser,fileStream,recipe.ImageIdentifier);
            }
            await _repository.Add(recipe);
            await _unityOfWork.Commit();

            return _mapper.Map<ResponseRegisteredRecipeJson>(recipe);

        }
        private static void Validate(RequestRecipeJson request)
        {
            var result = new RecipeValidator().Validate(request);
            if (!result.IsValid)
            {
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
            }
        }
    }
}
