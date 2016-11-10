using System.Collections;
using System.Globalization;
using System.Linq;
using System.Numerics;

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
                    yield return str.Replace('\r', ' ').Replace('\n', ' ');
                    continue;
                }

                decimal d;
                if (decimal.TryParse(item?.ToString(), out d))
                {
                    yield return d.ToString(CultureInfo.InvariantCulture);
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
            if (listAsString != null) return listAsString.Replace('\r', ' ').Replace('\n', ' ');
            return string.Join(",", Flatten(list).Cast<object>().Select(o => o.ToString()));
        }

        public static Vector3 ParseCoordinates(string coordinates)
        {
            var parsedCoordinates = coordinates.Split(',')
                .Select(c => float.Parse(c, NumberFormatInfo.InvariantInfo))
                .ToList();
            return new Vector3(parsedCoordinates[0], parsedCoordinates[1], parsedCoordinates[2]);
        }
    }
}
