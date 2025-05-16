using Tropical.Comunication.Enums;

namespace Tropical.Comunication.Requests
{
    public class RequestFilterRecipeJson
    {
        
        public string? RecipeTitle_Ingredient { get; set; }
        // sempre será uma lista se o cliente enviar mais de uma opção
        public IList<CookingTime> CookingTimes { get; set; } = [];
        public IList<Difficulty> Difficulties { get; set; } = [];
        public IList<DishType> DishTypes { get; set; } = [];
    }
}
