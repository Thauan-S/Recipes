using Microsoft.Extensions.Configuration;

namespace Tropical.Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {// EXTENSION METHOD
        public static bool IsUnitTestEnvironment(this IConfiguration configuration)
        {   //tem que instalar o extensions.ConfigurationBinder
            return configuration.GetValue<bool>("InMemoryTest");
        }

        public static string ConnectionString(this  IConfiguration configuration)
        {
            return configuration.GetConnectionString("DefaultConnection");
        }
    }
}
