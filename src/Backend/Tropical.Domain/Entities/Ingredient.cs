using System.ComponentModel.DataAnnotations.Schema;

namespace Tropical.Domain.Entities
{
    public class Ingredient:EntityBase
    {
        public required string Item { get; set; }
        [ForeignKey(nameof(Recipe))]
        public required long RecipeId { get; set; }
        public required Recipe Recipe { get; set; }
        //o ondelete deve ser feito nas tabelas filhas, nesse caso aqui
    }
}
