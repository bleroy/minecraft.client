using System;

namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Snow">Gamepedia link</a>.
    /// </summary>
    public class Snow : IBlock
    {
        public Snow(int thickness = 8)
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