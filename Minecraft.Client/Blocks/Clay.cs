namespace Decent.Minecraft.Client.Blocks
{
    public class Clay : Block
    {
        public Clay() : base(BlockType.Clay) { }
        protected Clay(BlockType type) : base(type) { }

    }

    public class HardenedClay : Clay
    {
        public HardenedClay() : base(BlockType.HardenedClay) { }
    }

    public class StainedClay : HardenedClay, IColoredBlock
    {
        public StainedClay(Color color = Color.White) : base()
        {
            Color = color;
        }

        public Color Color { get; }
    }
}
