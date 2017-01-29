namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Water">Gamepedia link</a>.
    /// </summary>
    public class Water : Liquid, IBlock 
    {
        public Water(Level level = Level.Source, bool isFlowing = false, bool isFalling = false)
            : base(level, isFlowing, isFalling) {}
    }
}