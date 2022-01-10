using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Core.Security.Policy
{
    internal class PolicyAuthorizationHandler : AuthorizationHandler<PolicyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirement requirement)
        {
            
            if (!context.User.HasClaim(c => c.Type == "permission"))
            {
                return Task.CompletedTask;
            }

            var permission = context.User.FindFirst(c => c.Type == "permission").Value;
            if (permission.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}