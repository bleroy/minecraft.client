namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// Base class for liquids
    /// </summary>
    public class Liquid
    {
        public Liquid(Level level = Level.Source, bool isFlowing = false, bool isFalling = false)
        {
            Level = level;
            IsFlowing = isFlowing;
            IsFalling = isFalling;
        }

        public bool IsFlowing { get; }

        public bool IsFalling { get; }

        public Level Level { get; }
    }
}