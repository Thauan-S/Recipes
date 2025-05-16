

using Tropical.Application.UseCases.User.Register;
using Tropical.Comunication.Requests;
using Tropical.Domain.Repositories;
using Tropical.Domain.Repositories.User;
using Tropical.Domain.Services.LoggedUser;
using Tropical.Exceptions;
using Tropical.Exceptions.Exceptions;

namespace Tropical.Application.UseCases.User.Update
{
    public class UpdateUserUseCase:IUpdateUserUseCase
    {
        ILoggedUser _loggedUser;
        IUserUpdateOnlyRepository _repository;
        IUserReadOnlyRepository _userReadOnlyRepository;
        IUnityOfWork _unityOfWork;

        public UpdateUserUseCase(ILoggedUser loggedUser, IUserUpdateOnlyRepository repository, IUserReadOnlyRepository userReadOnlyRepository, IUnityOfWork unityOfWork)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _unityOfWork = unityOfWork;
        }
        public async Task Execute(RequestUpdateUserJson request)
        {
            var loggedUser=await _loggedUser.User();
            await Validate(request, loggedUser.Email);
            var user = await _repository.GetByIdAsync(loggedUser.Id);
            user.Email = request.Email;
            user.Name = request.Name;
             _repository.Update(user);
            await _unityOfWork.Commit();
        }

        public async Task Validate(RequestUpdateUserJson request, string currentEmail)
        {
            var validator = new UpdateUserValidator();
            var result = validator.Validate(request);
            if (!currentEmail.Equals(request.Email))
            {
                var userExist=await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
                if (userExist) result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessagesException.INVALID_MAIL));
            }
            if (!result.IsValid)
            {
                var errorMessages=result.Errors.Select(e=>e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
