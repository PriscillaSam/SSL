using S.S.L.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace S.S.L.Domain.Enums
{
    public enum GymGroup
    {
        [Description("GYM ZERO ONE")]
        [Display(Name = "GYM ZERO ONE")]
        Gym01 = 1,

        [Description("GYM ZERO THREE")]
        [Display(Name = "GYM ZERO THREE")]
        Gym03 = 2,

        [Description("GYM ZERO FIVE")]
        [Display(Name = "GYM ZERO FIVE")]
        Gym05 = 3,

        [Description("GYM ZERO SEVEN")]
        [Display(Name = "GYM ZERO SEVEN")]
        Gym07 = 4,

        [Description("GYM ZERO EIGHT")]
        [Display(Name = "GYM ZERO EIGHT")]
        Gym08 = 5,

        [Description("GYM TWO ONE")]
        [Display(Name = "GYM TWO ONE")]
        Gym21 = 6,

        [Description("GYM FOUR ZERO")]
        [Display(Name = "GYM FOUR ZERO")]
        Gym40 = 7
    }

    public class GymGroupView
    {
        public GymGroup GroupName { get; set; }
        public IEnumerable<UserModel> GroupMembers { get; set; }


    }
}
