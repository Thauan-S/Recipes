namespace Tropical.Exceptions.Exceptions
{
    public class InvalidLoginException:MyTropicalException
    {

        public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_OASSWORD_INVALID)// envia para a classe pai a mensagem
        { 
            
        }
    }
}
