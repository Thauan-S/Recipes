using FluentValidation;
using Tropical.Comunication.Requests;
using Tropical.Exceptions;

namespace Tropical.Application.UseCases.User.Register
{
    public class UpdateUserValidator:AbstractValidator<RequestUpdateUserJson>//<T> classe que eu quero validar
    {
        public UpdateUserValidator() {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);//  criei esse arquivo em exceptions
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_MAIL);
            When(user => string.IsNullOrEmpty(user.Email) == false,() =>
            { // só executa essa regra se o email não é nulo ou vazio
                RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.INVALID_MAIL); //
            });
            
        }
    }
}
