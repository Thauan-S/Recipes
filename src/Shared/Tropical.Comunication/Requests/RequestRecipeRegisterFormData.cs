using Microsoft.AspNetCore.Http;

namespace Tropical.Comunication.Requests
{
    public class RequestRecipeRegisterFormData:RequestRecipeJson
    {// como minha  RequestRecipeJson está sendo usada tanto para criar uma recipe quanto atualizar
        //então criei essa classe que implementa a request , reaproveitando os atts

        public IFormFile? Image { get; set; }
    }
}
