using static Decent.Minecraft.Client.Direction;

namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Chest">Gamepedia link</a>.
    /// </summary>
    public class Chest : Block
    {
        public Chest(Direction facing = North) : base(BlockType.Chest)
        {
            Facing = facing;
        }

        public Direction Facing { get; }
    }
}
