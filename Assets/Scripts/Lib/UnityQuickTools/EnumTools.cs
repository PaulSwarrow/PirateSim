using System;

namespace Lib.UnityQuickTools
{
    public static class EnumTools
    {
        public static bool Includes<T>(this T mask, T flags) where T : Enum
        {
            int maskValue = (int) (object) mask;
            int flagsValue = (int) (object) flags;
            return (maskValue & flagsValue) == flagsValue;
        }

        public static bool Crosses<T>(this T mask, T flags) where T : Enum
        {
            int maskValue = (int) (object) mask;
            int flagsValue = (int) (object) flags;
            return (maskValue & flagsValue) != 0;
        }
    }
}