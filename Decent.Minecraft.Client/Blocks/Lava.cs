namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Lava">Gamepedia link</a>.
    /// </summary>
    public class Lava : Liquid, IBlock
    {
        public Lava(Level level = Level.Source, bool isFlowing = false, bool isFalling = false)
            : base(level, isFlowing, isFalling) {}
    }
}