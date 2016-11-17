namespace Decent.Minecraft.Client.Blocks
{
    public class Wool : Block, IColoredBlock
    {
        public Wool(Color color = Color.White) : base(BlockType.Wool)
        {
            Color = color;
        }

        public Color Color { get; }
    }
}
