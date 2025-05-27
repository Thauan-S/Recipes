using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Tropical.Comunication.Responses;
using Tropical.Domain.Repositories.User;
using Tropical.Domain.Security.Tokens;
using Tropical.Exceptions.Exceptions;
using Tropical.Exceptions;

namespace Tropical.API.Filters
{
    public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
    //ou :IauthorizationFilter <- usado quando não precisarei fazer métodos assincronos
    {
        // verifica se o token é valido , verifica se o usuário está autenticado
        private readonly IAccessTokenValidator _accessTokenValidator;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator,IUserReadOnlyRepository readOnlyRepository)
        {
            _accessTokenValidator = accessTokenValidator;
            _userReadOnlyRepository = readOnlyRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenOnRequest(context);
                var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);
                var exist = await _userReadOnlyRepository.ExistActiveUserWithIdentifier(userIdentifier);// verifica se há um user com o GUID
                if (!exist)
                {
                    throw new MyTropicalException(ResourceMessagesException.USER_WITHOUT_PERMISSSION_ACCESS_RESOURCE);
                }
            }
            catch (MyTropicalException e)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(e.Message));
            }
            catch (SecurityTokenExpiredException e)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("Token Expired")
                {
                    TokenIsExpired = true,
                }
                );
            }
            catch 
            {
                context.Result = new UnauthorizedObjectResult(
                    new ResponseErrorJson(ResourceMessagesException.USER_WITHOUT_PERMISSSION_ACCESS_RESOURCE));
            }
        }
        private static string TokenOnRequest(AuthorizationFilterContext context)
        {
            var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
            if (string.IsNullOrWhiteSpace(authentication))
            { // caso o token não venha
                throw new MyTropicalException(ResourceMessagesException.NO_TOKEN);
            }
            return authentication["Bearer ".Length..].Trim();
        }
       
        

    }
}
