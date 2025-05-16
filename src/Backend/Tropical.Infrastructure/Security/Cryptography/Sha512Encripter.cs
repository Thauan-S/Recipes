
using System.Security.Cryptography;
using System.Text;
using Tropical.Domain.Security.Cryptography;


namespace Tropical.Infrastructure.Security.Cryptography
{
    public class Sha512Encripter : IPasswordEncripter
    {

        /// <summary>
        ///  UTILIZAR O BCRYPT
        /// </summary>

        private readonly string _additionalKey;
        public Sha512Encripter(string additionalKey)
        {
            _additionalKey = additionalKey;
        }

        public string Encrypt(string password)
        {
            //aditional key está vindo do appsetings
            var newPassword = $"{password}{_additionalKey}";// chave adicionada à senha para aumentar a segurança
            var bytes = Encoding.UTF8.GetBytes(newPassword);
            var hashBytes = SHA512.HashData(bytes);

            return StringBytes(hashBytes);
        }
        private static string StringBytes(byte[] bytes)
        {//converte o array de bytes em uma string
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
