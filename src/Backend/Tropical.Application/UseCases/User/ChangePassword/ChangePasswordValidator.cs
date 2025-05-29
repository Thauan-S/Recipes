
using FluentValidation;
using Tropical.Application.SharedValidators;
using Tropical.Comunication.Requests;


namespace Tropical.Application.UseCases.User.Register
{
    public class ChangePasswordValidator:AbstractValidator<RequestChangePasswordUserJson>
    {
        public ChangePasswordValidator() {
            RuleFor(password => password.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordUserJson>());//nesse caso não criei nenhuma validação adicional pois o register já define
// e caso eu precisasse mudar a regra de negócio teria que modificar aqui também 
            
        }
    }
}
