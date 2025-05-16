using Tropical.Domain.Security.Cryptography;
using Tropical.Infrastructure.Security.Cryptography;

namespace CommonTestUtilities.Cryptography
{
    public static class PasswordEncrypterBuilder
    {
        public static IPasswordEncripter Build()
        {
            return new Sha512Encripter("wwwwww");
        }
    }
}
