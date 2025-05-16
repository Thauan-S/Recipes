using Tropical.Domain.Enums;

namespace Tropical.Domain.Dtos
{
    public class GeneratedRecipeDto
    {
        public string Title { get; init; } = string.Empty;
        public IList<string> Ingredients { get; init; } = [];
        public IList<GeneratedInstructionDto> Instructions { get; set; } = [];
        public CookingTime CookingTime { get; init; }
    }
}