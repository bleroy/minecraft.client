using System.Collections;
using System.Linq;

namespace Decent.Minecraft.Client
{
    public static class Util
    {
        public static IEnumerable Flatten(this IEnumerable list)
        {
            foreach (var item in list)
            {
                var str = item as string;
                if (str != null)
                {
                    yield return str;
                    continue;
                }
                var enumerable = item as IEnumerable;
                if (enumerable == null)
                {
                    yield return item;
                    continue;
                }
                foreach (var deepItem in Flatten(enumerable))
                {
                    yield return deepItem;
                }
            }
        }

        public static string FlattenToString(this IEnumerable list)
        {
            var listAsString = list as string;
            if (listAsString != null) return listAsString;
            return string.Join(",", Flatten(list).Cast<object>().Select(o => o.ToString()));
        }
    }
}
