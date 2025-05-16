//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using WebApplication1.Dto;
//using WebApplication1.Services.Token;

//namespace WebApplication1.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TokenController : ControllerBase
//    {
//        private readonly ItokenService _tokenService;

//        public  TokenController(ItokenService tokenService)
//        {
//            _tokenService = tokenService;
//        }

//        [HttpPost("login",Name ="login")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public ActionResult Login( LoginRequest loginRequest)
//        {
//           var token =_tokenService.GenerateToken(loginRequest);
            
//            if(token =="")return Unauthorized();
           
//            return Ok(token);
//        }
//    }
//}
