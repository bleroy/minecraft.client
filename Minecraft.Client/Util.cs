using System.Collections;
using System.Linq;

namespace Minecraft.Client
{
    public static class Util
    {
        public static IEnumerable Flatten(this IEnumerable list)
        {
            foreach (var item in list)
            {
                var enumerable = item as IEnumerable;
                if (enumerable == null) yield return item;
                foreach (var deepItem in Flatten(list))
                {
                    yield return deepItem;
                }
            }
        }

        public static string FlattenToString(this IEnumerable list)
        {
            return string.Join(",", Flatten(list).Cast<object>().Select(o => o.ToString()));
        }
    }
}
