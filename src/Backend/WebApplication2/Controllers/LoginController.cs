using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tropical.Application.UseCases.Login.DoLogin;
using Tropical.Application.UseCases.Login.ExternalLogin;
using Tropical.Comunication.Requests;
using Tropical.Comunication.Responses;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tropical.API.Controllers
{

    public class LoginController : TropicalBaseController 
    {
        private readonly ILogger<LoginController> _logger;
       
public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase,[FromBody] RequestLoginJson request)
        {
            _logger.LogInformation("Tentando fazer login");
            var response = await useCase.Execute(request);
            return Ok(response);
        }
        [HttpGet]
        [Route("google")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> LoginByGoogle(
            string returnUrl,
            [FromServices] IExternalLoginUseCase useCase
            )
        {
          var result= await Request.HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (IsNotAuthenciated(result))
            {
                return Challenge(GoogleDefaults.AuthenticationScheme);
            }
            else {
                var claims = result.Principal!.Identities.First().Claims;
                
                var userName = claims.First(c => c.Type ==ClaimTypes.Name).Value;
                var userMail = claims.First(c => c.Type ==ClaimTypes.Email).Value;
                var token = await useCase.Execute(userName, userMail);
                
                return Redirect($"{returnUrl}/{token}");
            }
        }
    }
}


