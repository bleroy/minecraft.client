namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Torch">Gamepedia link</a>.
    /// </summary>
    public class Torch : IBlock
    {
        public Torch(Direction3 facing = Direction3.Up)
        {
            Facing = facing;
        }

        public Direction3 Facing { get; }
    }
}
