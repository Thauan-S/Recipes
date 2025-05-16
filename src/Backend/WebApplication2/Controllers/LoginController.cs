using Microsoft.AspNetCore.Mvc;
using Tropical.Application.UseCases.Login.DoLogin;
using Tropical.Comunication.Requests;
using Tropical.Comunication.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tropical.API.Controllers
{
    
    public class LoginController : TropicalBaseController // controller gerado para mudar a route globalmente
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase,[FromBody] RequestLoginJson request)
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }
    }
}


