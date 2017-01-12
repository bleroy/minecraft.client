namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Water">Gamepedia link</a>.
    /// </summary>
    public class Water : IBlock
    {
        public Water(WaterLevel level = WaterLevel.Source, bool isFlowing = false, bool isFalling = false)
        {
            Level = level;
            IsFlowing = isFlowing;
            IsFalling = isFalling;
        }

        public bool IsFlowing { get; }

        public bool IsFalling { get; }

        public WaterLevel Level { get; }
    }
}