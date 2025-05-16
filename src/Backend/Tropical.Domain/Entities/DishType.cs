

using System.ComponentModel.DataAnnotations.Schema;

namespace Tropical.Domain.Entities
{
    public class DishType:EntityBase
    {
        public Enums.DishType Type { get; set; }
        [ForeignKey(nameof(Recipe))]
        public long RecipeId { get; set; }
        public required Recipe Recipe { get; set; }
    }
}
