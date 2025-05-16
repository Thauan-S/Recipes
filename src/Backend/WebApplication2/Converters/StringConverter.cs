using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Tropical.API.Converters
{
    public partial class StringConverter : JsonConverter<string>
    {// classe criada para eliminar espaços em branco // ela é  implementada no program.cs 
     // DEVO ADICIONAR A PROPRIEDADE AO PROGRAM.CS  CUIDADO PARA NÃO IMPORTAR A CLASSE ERRADA !!!!
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
           
            var value=reader.GetString()?.Trim();//Trim() remove espacos em branco
            if (value == null) 
                return null;
            return RemoveExtraWhiteSpaces().Replace(value, " ");//remove os espaços do meio e adiciona apenas um espaco em branco
            // "teste           teste"
            
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }

        [GeneratedRegex(@"\s+")]
        private static partial Regex RemoveExtraWhiteSpaces();
    }
}
