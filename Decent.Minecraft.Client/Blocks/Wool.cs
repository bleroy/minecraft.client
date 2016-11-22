namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Wool">Gamepedia link</a>.
    /// </summary>
    public class Wool : Block, IColoredBlock
    {
        public Wool(Color color = Color.White) : base(BlockType.Wool)
        {
            Color = color;
        }

        public Color Color { get; }
    }
}
