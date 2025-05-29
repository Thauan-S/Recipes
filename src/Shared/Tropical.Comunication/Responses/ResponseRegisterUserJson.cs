
using Tropical.Comunication.Responses;

namespace Tropical.Comunication.Requests
{
    public class ResponseRegisterUserJson
    {
        public string Name { get; set; }=string.Empty;
        public ResponseTokensJson Tokens { get; set; } = default!;
    }
}
