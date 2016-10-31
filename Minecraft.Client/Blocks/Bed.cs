namespace Decent.Minecraft.Client.Blocks
{
    public abstract class Bed : Block
    {
        public Bed() : base(BlockType.Bed) { }

        public Direction HeadFacing {
            get
            {
                return (Direction)(Data & 0x3);
            }
            set
            {
                Data = (byte)(Data & 0xC | (byte)value);
            }
        }

        public bool Occupied {
            get
            {
                return (Data & 0x4) != 0;
            }
            set
            {
                if (value)
                {
                    Data |= 0x4;
                }
                else
                {
                    Data &= 0xD;
                }
            }
        }
    }

    public class BedHead : Bed
    {
        public BedHead(Direction headFacing, bool occupied) : base()
        {
            Data = (byte)((byte)headFacing | (occupied ? 0x4 : 0x0) | 0x8);
        }
    }

    public class BedFoot : Bed
    {
        public BedFoot(Direction headFacing, bool occupied) : base()
        {
            Data = (byte)((byte)headFacing | (occupied ? 0x4 : 0x0));
        }
    }
}
