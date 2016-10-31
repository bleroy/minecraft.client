using static Decent.Minecraft.Client.Direction;

namespace Decent.Minecraft.Client.Blocks
{
    public class Chest : Block
    {
        public Chest(Direction facing = Direction.North) : base(BlockType.Chest)
        {
            Facing = facing;
        }

        public Direction Facing
        {
            get
            {
                return Data == 2 ? North :
                    Data == 3 ? South :
                    Data == 4 ? West :
                    East;
            }
            set
            {
                Data = (byte)(
                    value == North ? 2 :
                    value == South ? 3 :
                    value == West ? 4 :
                    5);
            }
        }
    }
}
