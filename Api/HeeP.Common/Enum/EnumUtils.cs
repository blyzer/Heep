using System.Reflection;


namespace HeeP.Common.Enum
{
    public static class EnumUtils
    {
        public static string GetDescritionAtribute(this System.Enum enumValue)
        {
            var attribute = enumValue.GetAttributeOfType<AssemblyDescriptionAttribute>();
            return attribute == null ? string.Empty : attribute.Description;
        }

        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example>string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;</example>
        public static T GetAttributeOfType<T>(this System.Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            int result = 0;

            var memInfo = type.GetMember(enumVal.ToString());
            if (memInfo.Length == 0)
            {
                return null;
            }
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);

            using (var enumerator = attributes.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    result++;
            }

            return (result > 0) ? (T)attributes : null;
        }
    }
}