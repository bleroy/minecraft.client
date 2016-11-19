using static Decent.Minecraft.Client.Direction;

namespace Decent.Minecraft.Client.Blocks
{
    public class Chest : Block
    {
        public Chest(Direction facing = North) : base(BlockType.Chest)
        {
            Facing = facing;
        }

        public Direction Facing { get; }
    }
}
