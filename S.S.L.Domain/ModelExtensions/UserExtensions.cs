using S.S.L.Domain.Enums;
using S.S.L.Domain.Models;
using System.Linq;

namespace S.S.L.Domain.ModelExtensions
{
    public static class UserExtensions
    {

        public static bool IsAdmin(this UserModel user) => user.UserType == UserType.Administrator;

        public static bool IsMentee(this UserModel user) => user.UserType == UserType.Mentee;

        public static bool IsFacilitator(this UserModel user) => user.UserType == UserType.Facilitator;
        public static bool HasRole(this UserModel user, string role) => user.Roles.Any(u => u == role);

        public static bool HasGymGroup(this UserModel user) => user.GymGroup != 0;
    }
}
