using System;

namespace LeetCharConverter.Enums
{
    public static class EnumEx
    {
        public static T[] GetValues<T>() where T : Enum
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static T GetValueFromString<T>(string value) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
