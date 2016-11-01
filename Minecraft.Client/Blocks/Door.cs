namespace Decent.Minecraft.Client.Blocks
{
    //TODO: make wood species a property
    public abstract class Door : Block
    {
        protected Door(BlockType type) : base(type) { }
    }

    public abstract class DoorTop : Door
    {
        protected DoorTop(BlockType type, bool hingeOnTheLeft, bool powered) : base(type)
        {
            HingeOnTheLeft = hingeOnTheLeft;
            Powered = powered;
        }

        public bool HingeOnTheLeft { get; }

        public bool Powered { get; }
    }

    public abstract class DoorBottom : Door
    {
        protected DoorBottom(BlockType type, bool open, Direction facing) : base(type)
        {
            IsOpen = open;
            Facing = facing;
        }

        public bool IsOpen { get; }

        public Direction Facing { get; }
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