using System.Security.Claims;

namespace GHR.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RecuperarUserNameClaim(this ClaimsPrincipal user){

            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static int RecuperarEmpresaIdClaim(this ClaimsPrincipal user) {

            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        public static string RecuperarVisaoClaim(this ClaimsPrincipal user)
        {

            return user.FindFirst(ClaimTypes.Actor)?.Value;
        }

    }
}