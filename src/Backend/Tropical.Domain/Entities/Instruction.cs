using System.ComponentModel.DataAnnotations.Schema;

namespace Tropical.Domain.Entities
{
    public class Instruction:EntityBase
    {
        public int Step { get; set; }
        public string Text { get; set; } = string.Empty;
        [ForeignKey(nameof(Recipe))]
        public long RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}