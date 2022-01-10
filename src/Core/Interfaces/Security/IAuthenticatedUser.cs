using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Core.Interfaces.Security
{
    public interface IAuthenticatedUser
    {
        string Email();
        string Name();
        string RemoteIp();
        Guid GuidLogin();
        IEnumerable<Claim> GetClaimsIdentity();
        string GetPermissionClaims();
        string GetTokenTypeClaims();
        string GetUserAgent();
    }
}