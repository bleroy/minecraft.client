using System;

namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Farmland">Gamepedia link</a>.
    /// </summary>
    public class Farmland : IBlock
    {
        public Farmland(int wetness)
        {
            if (wetness < 0 || wetness > 7)
            {
                throw new ArgumentException("Farm land wetness must be between 0 and 7.", "thickness");
            }
            Wetness = wetness;
        }

        public int Wetness { get; }
    }
}
