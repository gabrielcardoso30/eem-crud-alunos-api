using Microsoft.AspNetCore.Authorization;

namespace Core.Security.Policy
{
    public class PolicyRequirement : IAuthorizationRequirement
    {
        public PolicyRequirement(string permission)
        {
            Permission = permission;
        }
        public string Permission { get; private set; }
    }
}