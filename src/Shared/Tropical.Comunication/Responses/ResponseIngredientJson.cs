namespace Tropical.Comunication.Responses
{
    public class ResponseIngredientJson
    {
        public string Id { get; set; } = string.Empty;
        public IList<string> Item { get; set; } = [];
    }
}