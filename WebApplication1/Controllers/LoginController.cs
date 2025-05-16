//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using WebApplication1.Login.DoLogin;
//using WebApplication1.Models;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace WebApplication1.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LoginController : ControllerBase 
//    {
//        private readonly IDoLoginUseCase _useCase;

//        public LoginController(IDoLoginUseCase useCase)
//        {
//            _useCase = useCase;
//        }
//        [HttpPost]
//        public async Task<IActionResult> Login( [FromBody] RequestLoginJson request)
//        {
//            var response = await _useCase.Execute(request);
//            return Ok(response);
//        }
//    }
//}
