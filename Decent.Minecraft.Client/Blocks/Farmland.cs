using System;

namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Cobblestone">Gamepedia link</a>.
    /// </summary>
    public class Farmland : Block
    {
        public Farmland(int wetness) : base(BlockType.Farmland)
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
