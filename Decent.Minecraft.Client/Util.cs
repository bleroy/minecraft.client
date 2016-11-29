using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Decent.Minecraft.Client
{
    /// <summary>
    /// A bunch of helpers.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Flattens a hierarchy of objects into a flat enumeration of strings.
        /// </summary>
        /// <param name="list">The hierarchy of objects.</param>
        /// <returns>The flattened enumeration.</returns>
        public static IEnumerable Flatten(this IEnumerable list)
        {
            foreach (var item in list)
            {
                if (item == null)
                {
                    continue;
                }
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

        /// <summary>
        /// Recursively flattens a list of objects into a
        /// comma-delimited string representation.
        /// </summary>
        /// <param name="list">The list of objects.</param>
        /// <returns>The serialized comma-delimited list of objects.</returns>
        public static string FlattenToString(this IEnumerable list)
        {
            var listAsString = list as string;
            if (listAsString != null) return listAsString.Replace('\r', ' ').Replace('\n', ' ');
            return string.Join(",", Flatten(list).Cast<object>().Select(o => o.ToString()));
        }

        /// <summary>
        /// Parses three comma-separated coordinates into a `Vector3`.
        /// </summary>
        /// <param name="coordinates">The comma-separated string representation of the coordinates.</param>
        /// <returns>The parsed `Vector3`.</returns>
        public static Vector3 ParseCoordinates(this string coordinates)
        {
            var parsedCoordinates = coordinates.Split(',')
                .Select(c => float.Parse(c, NumberFormatInfo.InvariantInfo))
                .ToList();
            return new Vector3(parsedCoordinates[0], parsedCoordinates[1], parsedCoordinates[2]);
        }

        /// <summary>
        /// Unescapes pipes and ampersands.
        /// </summary>
        /// <param name="s">The string to unescape.</param>
        /// <returns>The unescaped string.</returns>
        public static string FixPipe(string s)
        {
            return s.Replace("&#124;", "|").Replace("&amp;", "&");
        }
        
        /// <summary>
        /// Computes the displacement of a `Vector3` a number of blocks in a certain direction.
        /// </summary>
        /// <param name="current">The coordinates to displace.</param>
        /// <param name="direction">The direction of the displacement.</param>
        /// <param name="times">The number of blocks by which to displace.</param>
        /// <returns>The displaced vector.</returns>
        public static Vector3 Towards(this Vector3 current, Direction direction, int times = 1)
        {
            switch (direction)
            {
                case Direction.North:
                    current.Z -= times;
                    break;
                case Direction.South:
                    current.Z += times;
                    break;
                case Direction.West:
                    current.X -= times;
                    break;
                case Direction.East:
                    current.X += times;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
            return current;
        }

        /// <summary>
        /// Computes the displacement of a `Vector3` a number of blocks down.
        /// </summary>
        /// <param name="current">The coordinates to displace.</param>
        /// <param name="times">The number of blocks by which to displace.</param>
        /// <returns>The displaced vector.</returns>
        public static Vector3 Downwards(this Vector3 current, int times = 1)
        {
            current.Y -= times;
            return current;
        }
    }
}
