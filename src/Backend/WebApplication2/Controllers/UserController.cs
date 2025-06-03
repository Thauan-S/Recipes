using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Tropical.API.Attributes;
using Tropical.Application.UseCases.User.ChangePassword;
using Tropical.Application.UseCases.User.Delete.RequestDeleteUseCase;
using Tropical.Application.UseCases.User.Profile;
using Tropical.Application.UseCases.User.Register;
using Tropical.Application.UseCases.User.Update;
using Tropical.Comunication.Requests;

namespace Tropical.API.Controllers
{

    public class UserController : TropicalBaseController 
    {
        
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
        public async Task <IActionResult> Register(
            [FromServices]IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson user)
        {
                var result = await useCase.Execute(user);
                return Created(string.Empty, result);
        }
        [HttpGet]
        [AuthenticatedUser] 
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserProfile(
            [FromServices] IGetUserProfileUseCase useCase
            )
        {
            var result = await useCase.Execute();
            return Ok(result);
        }
        [HttpPut]
        [AuthenticatedUser] 
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateUserUseCase useCase,
            [FromBody] RequestUpdateUserJson request
            )
        {
            await useCase.Execute(request);
            return NoContent();
        }
        [HttpPut("change-password")]
        [AuthenticatedUser]
        [EnableRateLimiting("ResetPasswordLimiter")]
        public async Task<IActionResult> ChangePassword(
           [FromServices] IChangePasswordUseCase useCase,
           [FromBody] RequestChangePasswordUserJson request
           )
        {
            await useCase.Execute(request);
            return NoContent();
        }
        [HttpDelete]
        [AuthenticatedUser]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(
         [FromServices] IRequestDeleteUserUseCase useCase)
        {
            await useCase.Execute();
            return NoContent();
        }
    }
}
