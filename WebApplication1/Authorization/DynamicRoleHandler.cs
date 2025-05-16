using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Authorization
{
    public class DynamicRoleHanlder : AuthorizationHandler<DynamicRoleRequirement>
    {
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DynamicRoleRequirement requirement)
        {
            var test = context.User.Claims.ToList();//
            var userRoles = context.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            var hasRole = context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            var allClaims = context.User.Claims.ToList();
            var  claim =context.User.Claims.Select(c => c.ValueType);

            var roles = context.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            var i = context.User.Claims.First().Properties; 
            // Retornar as roles como uma string concatenada (ou qualquer outro formato que desejar)
            
            //if (context.User.Identities.Contains)
            if (context.User.IsInRole(requirement.RoleName))// o valor do requirement = RoleName="Admin"
            {
                context.Succeed(requirement);
            }
            else
            {

                throw new Exception(string.Join(", ", roles+ $"      aaa User does not have role {requirement.RoleName} tipo {claim}"));
            }

            return Task.CompletedTask;
        }
    }

    public class DynamicRoleRequirement : IAuthorizationRequirement
    {
        public string RoleName { get; }
        public DynamicRoleRequirement(string roleName)
        {
            RoleName = roleName;
        }
    }
}
