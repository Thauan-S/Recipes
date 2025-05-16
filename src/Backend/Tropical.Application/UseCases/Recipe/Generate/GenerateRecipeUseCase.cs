//using Tropical.Application.UseCases.Recipe.Generate;
//using Tropical.Comunication.Requests;
//using Tropical.Comunication.Responses;
//using Tropical.Domain.Entities;
//using Tropical.Domain.Services.OpenAI;
//using Tropical.Exceptions.Exceptions;

//namespace Tropical.Application.UseCases.Recipe.Generation
//{
//    public class GenerateRecipeUseCase : IGenerateRecipeUseCase
//    {
//        private readonly IGenerateRecipeAI _generator;

//        public GenerateRecipeUseCase(IGenerateRecipeAI generator)
//        {
//            _generator = generator;
//        }

//        public async Task<ResponseGeneratedRecipeJson> Execute(RequestGenerateRecipeJson request)
//        {
//            Validate(request);
//            var response = await _generator.Generate(request.Ingredients);

//            return new ResponseGeneratedRecipeJson()
//            {
//                Title = response.Title,
//                Ingredients = response.Ingredients,
//                CookingTime = (Comunication.Enums.CookingTime)response.CookingTime,
//                Instructions = response.Instructions.Select(istruction=>new ResponseGeneratedInstructionJson
//                {
//                    Step =istruction.Step,
//                    Text =istruction.Text,
//                }).ToList(),
//                Difficulty = Comunication.Enums.Difficulty.Low
//            };
//        }
//        private static void Validate(RequestGenerateRecipeJson request) {
//            var result = new GenerateRecipeValidator().Validate(request) ;
//            if (!result.IsValid)
//            {
//                throw new ErrorOnValidationException(result.Errors.Select(e=>e.ErrorMessage).ToList());
//            }
//        }
//    }
//}
