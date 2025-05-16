using Tropical.Application.UseCases.User.Register;
using Tropical.Comunication.Requests;
using Tropical.Domain.Repositories;
using Tropical.Domain.Repositories.User;
using Tropical.Domain.Security.Cryptography;
using Tropical.Domain.Services.LoggedUser;
using Tropical.Exceptions;
using Tropical.Exceptions.Exceptions;

namespace Tropical.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordUseCase:IChangePasswordUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IPasswordEncripter _passwordEncripter;

        public ChangePasswordUseCase(ILoggedUser loggedUser, IUserUpdateOnlyRepository repository, IUnityOfWork unityOfWork, IPasswordEncripter passwordEncripter)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _unityOfWork = unityOfWork;
            _passwordEncripter = passwordEncripter;
        }
        public async Task Execute(RequestChangePasswordUserJson request)
        {
            var loggedUser = await _loggedUser.User();
            Validate(request, loggedUser);
            var user=await _repository.GetByIdAsync(loggedUser.Id);

            user.Password=_passwordEncripter.Encrypt(request.NewPassword);
            _repository.Update(user);
            await _unityOfWork.Commit();
        }

        private void Validate(RequestChangePasswordUserJson request,Domain.Entities.User loggedUser)
        {
            var result = new ChangePasswordValidator().Validate(request);
            var currentPasswordEncripted = _passwordEncripter.Encrypt(request.Password);
            if (!currentPasswordEncripted.Equals(loggedUser.Password))
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.INVALID_PASSWORD));
            }
            if (!result.IsValid)
            {
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
            }

        }
    }
}
