using System;

namespace Decent.Minecraft.Client.Blocks
{
    public class Fire : Block
    {
        public Fire(byte intensity) : base(BlockType.Fire)
        {
            if (intensity < 0 || intensity > 15)
            {
                throw new ArgumentException("Fire intensity must be between 0 and 15.", "intensity");
            }
            Intensity = intensity;
        }

        public byte Intensity { get; }
    }
}
