

using BCrypt.Net;
using Tropical.Domain.Security.Cryptography;

namespace Tropical.Infrastructure.Security.Cryptography
{
    public  class PasswordEncripter:IPasswordEncripter
    {
        public  string Encrypt(string password)
        {
           return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool IsValid(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password,passwordHash);
        }
    }
}
