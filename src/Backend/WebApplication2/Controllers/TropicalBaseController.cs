using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tropical.API.Controllers
{
    [Route("[controller]")]
    [ApiController] 

    public class TropicalBaseController : ControllerBase
    {
       
        protected static bool IsNotAuthenciated(AuthenticateResult result)
        {
            return result.Succeeded.Equals(false) 
                || result.Principal == null
                || result.Principal.Identities.Any(id=>id.IsAuthenticated).Equals(false);
        }
    }
}
