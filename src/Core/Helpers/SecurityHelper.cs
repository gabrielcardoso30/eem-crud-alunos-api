using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Interfaces.Helpers;
using Core.Interfaces.Security;
using DinkToPdf;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Core.Helpers
{

    public class SecurityHelper
    {

		private readonly IHttpContextAccessor _httpContextAccessor;

        public SecurityHelper(
            
        )
        {
			_httpContextAccessor = new HttpContextAccessor();
        }

        public Guid GuidLogin()
        {
            try
            {
                var guidLogin = GetClaimsIdentity().FirstOrDefault(a => a.Type == "id")?.Value;

                return string.IsNullOrEmpty(guidLogin) ? Guid.Empty : Guid.Parse(guidLogin);
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _httpContextAccessor.HttpContext.User.Claims;
        }

    }

}
