using S.S.L.Domain.Models;
using System.Linq;

namespace S.S.L.Web.Infrastructure.Extensions
{
    public static class UserExtensions
    {
        public static bool HasRole(this UserModel user, string role) => user.Roles.Any(u => u == role);
    }
}