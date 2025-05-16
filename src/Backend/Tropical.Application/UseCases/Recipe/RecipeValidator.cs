using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Tropical.Comunication.Requests;
using Tropical.Exceptions;

namespace Tropical.Application.UseCases.Recipe
{
    public class RecipeValidator :AbstractValidator<RequestRecipeJson>
    {
        public RecipeValidator() { 
            RuleFor(recipe=>recipe.Title).NotEmpty().WithMessage(ResourceMessagesException.RECIPE_TITLE_EMPTY);
            RuleFor(recipe => recipe.CookingTime).IsInEnum().WithMessage(ResourceMessagesException.COOKING_TIME_NOT_SUPPORTED);
            RuleFor(recipe => recipe.Difficulty).IsInEnum().WithMessage(ResourceMessagesException.DIFFICULTY_LEVEL_NOT_SUPPORTED);
            RuleFor(recipe => recipe.Ingredients.Count).GreaterThan(0).WithMessage(ResourceMessagesException.AT_LEAST_ONE_INGREDIENT);
            RuleFor(recipe => recipe.Instructions.Count).GreaterThan(0).WithMessage(ResourceMessagesException.AT_LEAST_ONE_INSTRUCTION);
           // executa para cada elemento na lista 
            RuleForEach(recipe => recipe.DishTypes).IsInEnum().WithMessage(ResourceMessagesException.DISH_TYPE_NOT_SUPPORTED);
            RuleForEach(recipe => recipe.Ingredients).NotEmpty().WithMessage(ResourceMessagesException.INGREDIENT_EMPTY);
            // validando lista de objetos
            RuleForEach(recipe => recipe.Instructions).ChildRules(instructionRule => {
                instructionRule.RuleFor(instruction => instruction.Step).GreaterThan(0).WithMessage(ResourceMessagesException.NON_NEGATIVE_INSTRUCTION_STEP);
                instructionRule.RuleFor(instruction => instruction.Text).NotEmpty()
                .WithMessage(ResourceMessagesException.EMPTY_INSTRUCTION_TEXT).MaximumLength(2000)
                .WithMessage(ResourceMessagesException.INSTRUCTION_EXCEEDS_LIMIT_CHARACTERS);
            });
            // validando que o usuário não passe dois passos duplicados ex passo 1... passo 1...
            RuleFor(recipe => recipe.Instructions)
                .Must(instructions=>instructions.Select(i=>i.Step).Distinct().Count()==instructions.Count )
                .WithMessage(ResourceMessagesException.TWO_OR_MORE_INSTRUCTIONS_SAME_ORDER);
        }
    }
}
