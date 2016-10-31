namespace Decent.Minecraft.Client.Blocks
{
    public abstract class Door : Block
    {
        protected Door(BlockType type) : base(type) { }
    }

    public abstract class DoorTop : Door
    {
        protected DoorTop(BlockType type, bool hingeOnTheLeft, bool powered) : base(type)
        {
            Data |= 0x8;
            HingeOnTheLeft = hingeOnTheLeft;
            Powered = powered;
        }

        public bool HingeOnTheLeft
        {
            get
            {
                return (Data & 0x1) != 0;
            }
            set
            {
                if (value)
                {
                    Data |= 0x1;
                }
                else
                {
                    Data &= 0xE;
                }
            }
        }

        public bool Powered
        {
            get
            {
                return (Data & 0x2) != 0;
            }
            set
            {
                if (value)
                {
                    Data |= 0x2;
                }
                else
                {
                    Data &= 0xD;
                }
            }
        }
    }

    public abstract class DoorBottom : Door
    {
        protected DoorBottom(BlockType type, bool open, Direction facing) : base(type)
        {
            Data &= 0x7;
            IsOpen = open;
            Facing = facing;
        }

        public bool IsOpen
        {
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
                    Data &= 0xB;
                }
            }
        }

        public Direction Facing
        {
            get
            {
                var raw = Data & 0xC;
                return new[] { Direction.East, Direction.South, Direction.West, Direction.North }[raw];
            }
            set
            {
                Data = (byte)((Data & 0xC) | (
                    value == Direction.East ? 0 :
                    value == Direction.South ? 1 :
                    value == Direction.West ? 2 :
                    3));
            }
        }
    }

    public class IronDoorTop : DoorTop
    {
        public IronDoorTop(bool hingeOnTheLeft, bool powered) : base(BlockType.DoorIron, hingeOnTheLeft, powered) { }
    }

    public class IronDoorBottom : DoorBottom
    {
        public IronDoorBottom(bool open, Direction facing) : base(BlockType.DoorIron, open, facing) { }
    }

    public class WoodenDoorTop : DoorTop
    {
        public WoodenDoorTop(bool hingeOnTheLeft, bool powered) : base(BlockType.DoorWood, hingeOnTheLeft, powered) { }
    }

    public class WoodenDoorBottom : DoorBottom
    {
        public WoodenDoorBottom(bool open, Direction facing) : base(BlockType.DoorWood, open, facing) { }
    }
}