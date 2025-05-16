using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tropical.API.Controllers
{
    [Route("[controller]")]
    [ApiController] // controller gerado para mudar o nome da route globalmente

    public class TropicalBaseController : ControllerBase
    {
       
    }
}
