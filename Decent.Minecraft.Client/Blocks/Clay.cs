namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Clay_(block)">Gamepedia link</a>.
    /// </summary>
    public class Clay : IBlock
    {
    }

    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Hardened_Clay">Gamepedia link</a>.
    /// </summary>
    public class HardenedClay : Clay
    {
    }

    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Hardened_Clay">Gamepedia link</a>.
    /// </summary>
    public class StainedClay : HardenedClay, IColoredBlock
    {
        public StainedClay(Color color = Color.White)
        {
            Color = color;
        }

        public Color Color { get; }
    }
}
