﻿using System.Data;
using FluentValidation;
using Tropical.Comunication.Requests;
using Tropical.Exceptions;

namespace Tropical.Application.UseCases.Recipe.Filter
{
    public class FilterRecipeValidator:AbstractValidator<RequestFilterRecipeJson>
    {
        public FilterRecipeValidator() {
            RuleForEach(r => r.CookingTimes).IsInEnum().WithMessage(ResourceMessagesException.COOKING_TIME_NOT_SUPPORTED);
            RuleForEach(r => r.DishTypes).IsInEnum().WithMessage(ResourceMessagesException.DISH_TYPE_NOT_SUPPORTED);
            RuleForEach(r => r.Difficulties).IsInEnum().WithMessage(ResourceMessagesException.DIFFICULTY_LEVEL_NOT_SUPPORTED);
        }
    }
}
