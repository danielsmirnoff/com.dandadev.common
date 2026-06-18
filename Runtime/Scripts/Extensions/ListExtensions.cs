using System.Collections.Generic;

namespace CommonDan
{
    public static class ListExtensions
    {
        public static T GetFirstOrNull<T>(this List<T> list)
        {
            return list.Count > 0 ? list[0] : default(T);
        }
    }
}