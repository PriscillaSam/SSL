using System;
using System.Linq;
using System.Reflection;

namespace S.S.L.Web.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumType)
        {
            Type type = enumType.GetType();
            MemberInfo[] memberInfos = type.GetMember(enumType.ToString());
            if (memberInfos != null && memberInfos.Length > 0)
            {
                var attri = memberInfos[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attri != null && attri.Count() > 0)
                {
                    return ((System.ComponentModel.DescriptionAttribute)attri.ElementAt(0)).Description;
                }
            }

            return enumType.ToString();
        }


    }
}