using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tropical.API.Attributes;
using Tropical.Application.UseCases.User.ChangePassword;
using Tropical.Application.UseCases.User.Delete.RequestDeleteUseCase;
using Tropical.Application.UseCases.User.Profile;
using Tropical.Application.UseCases.User.Register;
using Tropical.Application.UseCases.User.Update;
using Tropical.Comunication.Requests;

namespace Tropical.API.Controllers
{

    public class UserController : TropicalBaseController // controller para mudar a rota globalmente
    {
        
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
        public async Task <IActionResult> Register(
            [FromServices]IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson user)
        {// [FromServices] ja recebe a interface por injeção de dependência configurado na classe DependencyInjectionExtension
                var result = await useCase.Execute(user);
                return Created(string.Empty, result);
        }
        [HttpGet]
        [AuthenticatedUser] // é igual ao  o [Authorize(Roles ="")],mas personalizado
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserProfile(
            [FromServices] IGetUserProfileUseCase useCase
            )
        {// [FromServices] ja recebe a interface por injeção de dependência configurado na classe DependencyInjectionExtension
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
        {// [FromServices] ja recebe a interface por injeção de dependência configurado na classe DependencyInjectionExtension
            await useCase.Execute(request);
            return NoContent();
        }
        [HttpPut("change-password")]
        [AuthenticatedUser]
        
        public async Task<IActionResult> ChangePassword(
           [FromServices] IChangePasswordUseCase useCase,
           [FromBody] RequestChangePasswordUserJson request
           )
        {// [FromServices] ja recebe a interface por injeção de dependência configurado na classe DependencyInjectionExtension
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
