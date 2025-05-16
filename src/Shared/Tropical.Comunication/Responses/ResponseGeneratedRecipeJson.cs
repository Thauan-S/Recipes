using Tropical.Comunication.Enums;

namespace Tropical.Comunication.Responses
{
    public class ResponseGeneratedRecipeJson
    {
        public string Title { get; set; } = string.Empty;
        public IList<string> Ingredients { get; set; } = [];
        public IList<ResponseGeneratedInstructionJson>  Instructions { get;set;}=[];
        public CookingTime CookingTime { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
