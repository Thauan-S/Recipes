using Tropical.Domain.Enums;

namespace Tropical.Domain.Dtos
{
    public record FilterRecipesDto
        //RECORDS NÃO PODEM TER MÉTODOS
    { // OBSERVE QUE O DTO É UM RECORD POR BOAS PRÁTICAS DA MS
        //Os records são imutáveis , não consigo alterar um valor após ser atribuído a ele
        public string? RecipeTitle_Ingredient { get; init; }// observe o init, ele impede setar o valor novamente , o código
        //abaico funciona da mesma forma, com init.
        public IList<CookingTime> CookingTimes { get; init; } = [];
        public IList<Difficulty> Difficulties { get; init; } = [];
        public IList<DishType> DishTypes { get; init; } = [];
    }
    // abaixo também é o mesmo record , mas não é muito recomendada
    //public record Pessoa(int idade,string nome);
}