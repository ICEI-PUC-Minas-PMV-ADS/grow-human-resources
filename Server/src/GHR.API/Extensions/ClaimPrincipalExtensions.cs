using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GHR.API.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user){

            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static int GetUserId(this ClaimsPrincipal user) {

            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        public static string GetVisao(this ClaimsPrincipal user)
        {

            return user.FindFirst(ClaimTypes.Actor)?.Value;
        }

    }
}