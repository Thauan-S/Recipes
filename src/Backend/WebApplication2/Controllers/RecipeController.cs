using Microsoft.AspNetCore.Mvc;
using Tropical.API.Attributes;
using Tropical.API.Binders;
using Tropical.Application.UseCases.Recipe;
using Tropical.Application.UseCases.Recipe.Delete;
using Tropical.Application.UseCases.Recipe.Filter;
using Tropical.Application.UseCases.Recipe.GetById;
using Tropical.Application.UseCases.Recipe.UpdateById;
using Tropical.Application.UseCases.Recipe.UpdateImageById;
using Tropical.Comunication.Requests;
using Tropical.Comunication.Responses;

namespace Tropical.API.Controllers
{
    [AuthenticatedUser]
    public class RecipeController : TropicalBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredRecipeJson), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterRecipeUseCase useCase,
            [FromForm] RequestRecipeRegisterFormData request) 
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpPost("filter")]
        [ProducesResponseType(typeof(ResponseRecipesJson), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Filter(
            [FromServices] IFilterRecipeUseCase useCase,
            [FromBody] RequestFilterRecipeJson request)
        {
            var response = await useCase.Execute(request);
            if (response.Recipes.Any())
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseRecipeJson), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetById(
           [FromServices] IGetRecipeByIdUseCase useCase,
           [FromRoute][ModelBinder(typeof(MyTropicalBinder))] long id)
        {

            var response = await useCase.Execute(id);
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")] 
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
           [FromServices] IUpdateRecipeUseCase useCase,
           [FromRoute][ModelBinder(typeof(MyTropicalBinder))] long id,
           [FromBody] RequestRecipeJson request)
        {
            await useCase.Execute(id, request);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")] 
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
           [FromServices] IDeleteRecipeUseCase useCase,
           [FromRoute][ModelBinder(typeof(MyTropicalBinder))] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }
        [HttpPut]
        [Route("image/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateImage(
            [FromServices] IAddUpdateImageCoverUseCase useCase,
            [FromRoute][ModelBinder(typeof(MyTropicalBinder))] long id,
            IFormFile file 
            )
        {
          
            await useCase.Execute(id,file);
            return NoContent();
        }

    }
}