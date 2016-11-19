using System;

namespace Decent.Minecraft.Client.Blocks
{
    public class Snow : Block
    {
        public Snow(int thickness = 8) : base(BlockType.SnowLayer)
        {
            if (thickness < 0 || thickness > 8)
            {
                throw new ArgumentException("Snow thickness must be between 0 and 8.", "thickness");
            }
            Thickness = thickness;
        }

        public int Thickness { get; }
    }
}