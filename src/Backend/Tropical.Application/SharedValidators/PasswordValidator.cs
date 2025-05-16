

using FluentValidation;
using FluentValidation.Validators;
using Tropical.Exceptions;

namespace Tropical.Application.SharedValidators
{
    public class PasswordValidator<T>:PropertyValidator<T,string>
    { // validator genérico para senha 
        public override string Name => "PasswordValidator";
        protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";// valor do append argument abaixo
  
        public override bool IsValid(ValidationContext<T> context, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                context.MessageFormatter.AppendArgument("ErrorMessage","Password Empty");
                return false;
            }
            if (password.Length<6)
            {
                context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMessagesException.INVALID_PASSWORD);
                return false;
            }
            return true;
        }

      
    }
}
