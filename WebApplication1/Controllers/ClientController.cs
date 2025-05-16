//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using WebApplication1.Dto;
//using WebApplication1.Models;
//using WebApplication1.Services.Client;

//namespace WebApplication1.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
    
//   // [Authorize(Roles ="Basic")] //adicionando roles que acessam os dados
//    public class ClientController : ControllerBase
//    {
//        private readonly IClientService _clientservice;
//        // definindo as permissões // depois mandar para o accountcontroller
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;
//        private readonly IConfiguration _configuration;


//        public ClientController(IClientService clientService,UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration)
//        {
//            _clientservice = clientService;
//        }

//        //[HttpPut("EditarCliente")]

//        //public async Task<ActionResult<ResponseModel<ClientDto>>> UpdateClient(ClientDto clientDto) {
//        //    var client=await _clientservice.Update(clientDto);
//        //    return Ok(client);
//        //}


        
//        [HttpPost("create")]
//        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
//        public async Task<ActionResult<ResponseModel<ClientDto>>> CreateClient([FromBody]ClientDto clientDto)
//        {
//            var client=await _clientservice.Create(clientDto);
//            var uri = Url.Link("GetClient", new { id = client.Data.Id });
//            return Created(uri, client);
//        }
//        //[HttpGet("ListarClientes")]
//        //public async Task<ActionResult<ResponseModel<List<ClientModel>>>> FindAllClients()
//        //{
//        //    var clients = await _clientservice.FindAllClients();
//        //    return Ok(clients);
//        //}
//        //[HttpGet("BuscarPorId/{idClient}")]
//        //public async Task<ActionResult<ResponseModel<ClientModel>>> FindById(Guid idClient)
//        //{
//        //    var client = await _clientservice.FindById(idClient);
//        //    return Ok(client);
//        //}
//        [HttpGet("FindByEmail/{email}")]
//        public async Task<ActionResult<ResponseModel<ClientModel>>> FindById(string  email)
//        {
//            var client = await _clientservice.FindByEmail(email);
//            return Ok(client);
//        }
//        //[HttpGet("DeleteById/{idClient}")]
//        //public async Task<ActionResult<ResponseModel<ClientModel>>> DeleteById(Guid idClient)
//        //{
//        //    var client = await _clientservice.DeleteById(idClient);
//        //    if (client.Data == null)
//        //    {
//        //        return BadRequest(client.Message);
//        //    } 
//        //    return NoContent();
//        //}


//    }
//}
