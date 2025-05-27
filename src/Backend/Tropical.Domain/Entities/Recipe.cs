using System.ComponentModel.DataAnnotations.Schema;
using Tropical.Domain.Enums;

namespace Tropical.Domain.Entities
{
    public class Recipe:EntityBase
    {
        public required string Title { get; set; }= string.Empty;
        public CookingTime? CookingTime { get; set; }
        public Difficulty? Difficulty { get; set; }
        public IList<Ingredient> Ingredients { get; set; } = new List<Ingredient>(); // IList é o mesmo que List,
                                                                   
        // orientada e interfaces
        public IList<Instruction> Instructions { get; set; } = [];
        public IList<DishType> DishTypes { get; set; } = [];
        public string? ImageIdentifier { get; set; }
        [ForeignKey(nameof(User))]
        public required long UserId { get; set; }
        public required User User { get; set; } 
    }


}
