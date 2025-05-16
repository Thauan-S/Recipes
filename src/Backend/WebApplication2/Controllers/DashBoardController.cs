using Microsoft.AspNetCore.Mvc;
using Tropical.API.Attributes;
using Tropical.Application.UseCases.Recipe.DashBoard;
using Tropical.Comunication.Responses;

namespace Tropical.API.Controllers
{
    [AuthenticatedUser]
    public class DashBoardController : TropicalBaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRecipesJson),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromServices] IGetDashBoardUseCase useCase)
        {
            var response = await useCase.Execute();
            if (response.Recipes.Any())
            {
                return Ok(response);
            }
            return NoContent();
        }
    }
}
