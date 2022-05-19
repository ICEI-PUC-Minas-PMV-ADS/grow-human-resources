using System.Security.Claims;

namespace GHR.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RecuperarUserName(this ClaimsPrincipal user){

            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static int RecuperarUserId(this ClaimsPrincipal user) {

            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        public static string RecuperarVisao(this ClaimsPrincipal user)
        {

            return user.FindFirst(ClaimTypes.Actor)?.Value;
        }

    }
}