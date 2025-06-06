﻿using FluentValidation;
using Tropical.Comunication.Requests;
using Tropical.Domain.Entities;
using Tropical.Domain.ValueObjects;
using Tropical.Exceptions;

namespace Tropical.Application.UseCases.Recipe.Generate
{
    public class GenerateRecipeValidator:AbstractValidator<RequestGenerateRecipeJson>
    {
        public GenerateRecipeValidator() {
            var maximum_number_ingredients = MyTropicalRuleConstants.MAXIMUM_NUMBER_INGREDIENTS_GENERATE_RECIPE;
            RuleFor(request => request.Ingredients.Count).InclusiveBetween(1,maximum_number_ingredients).WithMessage(ResourceMessagesException.INVALID_NUMBER_INGREDIENTS);
            RuleFor(request => request.Ingredients).Must(ingredients=>ingredients.Count==ingredients.Distinct().Count()).WithMessage(ResourceMessagesException.DUPLICATED_INGREDIENT);
            RuleFor(request => request.Ingredients).ForEach(rule =>{

                rule.Custom((value, context) =>
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        context.AddFailure("Ingredient",ResourceMessagesException.INGREDIENT_EMPTY);
                        return;
                    }
                    if (value.Count(c => c == ' ') > 3 || value.Count(c => c == '/') > 1) {
                        context.AddFailure("Ingredient",ResourceMessagesException.INGREDIENT_NOT_FOLLOWING_PATTERN);
                        return;
                    } 
                });
            });
        }
    }
}
