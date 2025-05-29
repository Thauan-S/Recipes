
namespace Tropical.Exceptions.Exceptions
{
    public class RefreshTokenExpiredException : MyTropicalException
    {
        public RefreshTokenExpiredException():base("Token expired") // passa uma string vazia para mytropical exception
        {
            
        }
    }
}

