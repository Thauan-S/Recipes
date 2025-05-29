using Microsoft.AspNetCore.Mvc;
using Tropical.Application.UseCases.Token.RefreshToken;
using Tropical.Comunication.Requests;
using Tropical.Comunication.Responses;

namespace Tropical.API.Controllers
{
    public class TokenController : TropicalBaseController
    {
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(ResponseTokensJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken([FromServices] IUserRefreshTokenUseCase useCase,
            [FromBody] RequestNewTokenJson request)
        {
           var result=await useCase.Execute(request);
            return Ok(result);
        }
    }
}
