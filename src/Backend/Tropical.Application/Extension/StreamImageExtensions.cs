using FileTypeChecker.Extensions;
using FileTypeChecker.Types;

namespace Tropical.Application.Extension
{
    public static class StreamImageExtensions
    {// método de extensão para verificação do tipo de arquivo , evitando repetição do método Is
        public static (bool isValidImage, string extension) ValidateAndGetImageExtension(this Stream stream) {
            var result = (false,string.Empty);

            if (stream.Is<PortableNetworkGraphic>()) {
                result = (true, NormalizeExtension(PortableNetworkGraphic.TypeExtension));
            }else if ( stream.Is<JointPhotographicExpertsGroup>())
            {
                result = (true, NormalizeExtension(JointPhotographicExpertsGroup.TypeExtension));
            }
            stream.Position = 0;// começa ler o stream a partir do início
            return result;
        }
        private static string NormalizeExtension(string extension) { 
            return extension.StartsWith(".") ? extension :$".{extension}";
        }
    }
}
