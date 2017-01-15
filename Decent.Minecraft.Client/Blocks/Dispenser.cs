namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Dispenser">Gamepedia link</a>.
    /// </summary>
    public class Dispenser : IBlock
    {
        public Dispenser(Direction3 facing = Direction3.Down, bool isActivated = false)
        {
            Facing = facing;
            IsActivated = isActivated;
        }

        public Direction3 Facing { get; }
        public bool IsActivated { get; }
    }
}
