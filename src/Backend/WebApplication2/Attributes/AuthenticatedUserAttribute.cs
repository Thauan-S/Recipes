using Microsoft.AspNetCore.Mvc;
using Tropical.API.Filters;

namespace Tropical.API.Attributes
{
    public class AuthenticatedUserAttribute : TypeFilterAttribute

    {
        //criar em API AuthenticatedUserFilter filter
        public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
        {

        }
    }
}
