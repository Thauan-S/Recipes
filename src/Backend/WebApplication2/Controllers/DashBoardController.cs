using Microsoft.AspNetCore.Mvc;
using Tropical.API.Attributes;
using Tropical.Application.UseCases.Recipe.DashBoard;
using Tropical.Comunication.Pagination;
using Tropical.Comunication.Responses;

namespace Tropical.API.Controllers
{
    [AuthenticatedUser]
    public class DashBoardController : TropicalBaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRecipesJson),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(
            [FromQuery]PaginationParameters paginationParameters,
            [FromServices] IGetDashBoardUseCase useCase)
        {
            var response = await useCase.Execute(paginationParameters);
            if (response.Recipes.Any())
            {
                return Ok(response);
            }
            return NoContent();
        }
    }
}
