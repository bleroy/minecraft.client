namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Clay_(block)">Gamepedia link</a>.
    /// </summary>
    public class Clay : Block
    {
        public Clay() : base(BlockType.Clay) { }
        protected Clay(BlockType type) : base(type) { }

    }

    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Hardened_Clay">Gamepedia link</a>.
    /// </summary>
    public class HardenedClay : Clay
    {
        public HardenedClay() : base(BlockType.HardenedClay) { }
    }

    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Hardened_Clay">Gamepedia link</a>.
    /// </summary>
    public class StainedClay : HardenedClay, IColoredBlock
    {
        public StainedClay(Color color = Color.White) : base()
        {
            Color = color;
        }

        public Color Color { get; }
    }
}
