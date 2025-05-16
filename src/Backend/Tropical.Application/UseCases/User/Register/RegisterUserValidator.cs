
using System.Net.Mail;
using FluentValidation;
using Tropical.Application.SharedValidators;
using Tropical.Comunication.Requests;
using Tropical.Exceptions;

namespace Tropical.Application.UseCases.User.Register
{
    public class RegisterUserValidator:AbstractValidator<RequestRegisterUserJson>//<T> classe que eu quero validar
    {
        public RegisterUserValidator() {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);//  criei esse arquivo em exceptions
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_MAIL);
            RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
            When(user => string.IsNullOrEmpty(user.Email) == false,() =>
            { // só executa essa regra se o email não é nulo ou vazio
                RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.INVALID_MAIL); // p user name não pode ser vazio
            });
            
        }
    }
}
