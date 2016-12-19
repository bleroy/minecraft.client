using System;

namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Fire">Gamepedia link</a>.
    /// </summary>
    public class Fire : IBlock
    {
        public Fire(int intensity)
        {
            if (intensity < 0 || intensity > 15)
            {
                throw new ArgumentException("Fire intensity must be between 0 and 15.", "intensity");
            }
            Intensity = intensity;
        }

        public int Intensity { get; }
    }
}
