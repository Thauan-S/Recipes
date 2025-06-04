
using System.ComponentModel.DataAnnotations.Schema;
using Tropical.Domain.Entities;
using Tropical.Domain.Enums;

namespace Tropical.Domain.Dtos
{
    public class RecipesDto
    {
        public required string Title { get; set; } = string.Empty;
        public CookingTime? CookingTime { get; set; }
        public Difficulty? Difficulty { get; set; }
        public IList<Ingredient> Ingredients { get; set; } = new List<Ingredient>(); 
        public IList<Instruction> Instructions { get; set; } = [];
        public IList<Domain.Entities.DishType> DishTypes { get; set; } = [];
        public string? ImageIdentifier { get; set; }
        public required long UserId { get; set; }
        public required User User { get; set; }

        public static IList<RecipesDto> EntityToDto(IList<Recipe> recipes) {
            IList<RecipesDto> recipesDtos= new List<RecipesDto>();
            RecipesDto recipeDto;
            foreach (var r in recipes)
            {
                recipeDto = new RecipesDto {
                    Title = r.Title,
                    UserId = r.UserId,
                    User = r.User,
                    //Ingredients = r.Ingredients,
                    CookingTime = r.CookingTime,
                    Difficulty = r.Difficulty,
                    DishTypes = r.DishTypes,
                    ImageIdentifier = r.ImageIdentifier,
                    //Instructions = r.Instructions
                };
                recipesDtos.Add(recipeDto);
            }
            return recipesDtos;
        }
    }
}
