namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Wool">Gamepedia link</a>.
    /// </summary>
    public class Wool : IBlock, IColoredBlock
    {
        public Wool(Color color = Color.White)
        {
            Color = color;
        }

        public Color Color { get; }
    }
}
