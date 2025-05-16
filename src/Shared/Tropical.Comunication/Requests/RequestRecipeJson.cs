
using Tropical.Comunication.Enums;

namespace Tropical.Comunication.Requests
{
    public  class RequestRecipeJson
    {
        public string Title { get; set; }=string.Empty;
        public CookingTime? CookingTime { get; set; }

        public Difficulty? Difficulty { get; set; }

        public IList<string> Ingredients { get; set; }=new List<string>();
        public IList<RequestInstructionJson> Instructions { get; set; } =new List<RequestInstructionJson>();

        public IList<DishType> DishTypes { get; set; } = new List<DishType>();

    }
}
