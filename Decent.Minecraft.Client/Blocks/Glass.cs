namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Glass">Gamepedia link</a>.
    /// </summary>
    public class Glass : Block
    {
        public Glass() : base(BlockType.Glass) { }
        protected Glass(BlockType type) : base(type) { }

    }

    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Glass">Gamepedia link</a>.
    /// </summary>
    public class StainedGlass : Glass, IColoredBlock
    {
        public StainedGlass(Color color = Color.White) : base()
        {
            Color = color;
        }

        public Color Color { get; }
    }
}
