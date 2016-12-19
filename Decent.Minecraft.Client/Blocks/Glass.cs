namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Glass">Gamepedia link</a>.
    /// </summary>
    public class Glass : IBlock
    {
    }

    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Glass">Gamepedia link</a>.
    /// </summary>
    public class StainedGlass : Glass, IColoredBlock
    {
        public StainedGlass(Color color = Color.White)
        {
            Color = color;
        }

        public Color Color { get; }
    }
}
