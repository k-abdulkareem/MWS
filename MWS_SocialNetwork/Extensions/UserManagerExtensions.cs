using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity
{
    public static class UserManagerExtensions
    {
        public static string GetCurrentUserId(IHttpContextAccessor httpContextAcessor)
        {
            var id = httpContextAcessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return id;
        }

    }

   
}
