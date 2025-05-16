using System.ComponentModel.DataAnnotations.Schema;
using Tropical.Domain.Enums;

namespace Tropical.Domain.Entities
{
    public class Recipe:EntityBase
    {
        // o id está na entitybase
        public required string Title { get; set; }= string.Empty;
        public CookingTime? CookingTime { get; set; }//enum
        public Difficulty? Difficulty { get; set; }//enum
        public IList<Ingredient> Ingredients { get; set; } = new List<Ingredient>(); // IList é o mesmo que List, porém estamos usando a Programação
        // orientada e interfaces
        public IList<Instruction> Instructions { get; set; } = [];// é a mesma declaração de cima, porém simplificada
        public IList<DishType> DishTypes { get; set; } = [];
        public string? ImageIdentifier { get; set; }
        [ForeignKey(nameof(User))]
        public required long UserId { get; set; }
        public required User User { get; set; } 
    }


}
