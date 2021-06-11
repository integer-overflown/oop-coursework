using System.Collections.Generic;
using DynamicData;

namespace CourseWork.Utils
{
    public static class ListExtensions
    {
        public static void SetContent<T>(this IList<T> list, IEnumerable<T> items)
        {
            list.Clear();
            list.AddRange(items);
        }
    }
}