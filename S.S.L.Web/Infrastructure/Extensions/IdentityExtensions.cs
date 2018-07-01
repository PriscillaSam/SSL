using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Security.Principal;
using static S.S.L.Domain.Enums.UserType;

namespace S.S.L.Web.Infrastructure.Extensions
{
    public static class IdentityExtensions
    {

        public static string GetUserType(this IIdentity identity) => ((ClaimsIdentity)identity)?.FindFirstValue(ClaimTypes.Actor);

        public static bool IsAdmin(this IPrincipal user) => user.IsInRole(nameof(Administrator));
        public static bool IsSuperAdmin(this IIdentity identity) => ((ClaimsIdentity)identity)?.FindFirstValue(ClaimTypes.Actor) == nameof(Administrator);
        public static string GetAdminType(this IPrincipal user) => nameof(Administrator);
    }
}