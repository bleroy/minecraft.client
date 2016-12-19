namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// TODO: make wood species a property
    /// <a href="http://minecraft.gamepedia.com/Door">Gamepedia link</a>.
    /// </summary>
    public abstract class Door : IBlock
    {
    }

    public abstract class DoorTop : Door
    {
        protected DoorTop(bool hingeOnTheLeft, bool powered)
        {
            HingeOnTheLeft = hingeOnTheLeft;
            Powered = powered;
        }

        public bool HingeOnTheLeft { get; }

        public bool Powered { get; }
    }

    public abstract class DoorBottom : Door
    {
        protected DoorBottom(bool open, Direction facing)
        {
            IsOpen = open;
            Facing = facing;
        }

        public bool IsOpen { get; }

        public Direction Facing { get; }
    }

    public interface IronDoor : IBlock { }
    public interface WoodenDoor : IBlock { }

    public class IronDoorTop : DoorTop, IronDoor
    {
        public IronDoorTop(bool hingeOnTheLeft, bool powered) : base(hingeOnTheLeft, powered) { }
    }

    public class IronDoorBottom : DoorBottom, IronDoor
    {
        public IronDoorBottom(bool open, Direction facing) : base(open, facing) { }
    }

    public class WoodenDoorTop : DoorTop, WoodenDoor
    {
        public WoodenDoorTop(bool hingeOnTheLeft, bool powered) : base(hingeOnTheLeft, powered) { }
    }

    public class WoodenDoorBottom : DoorBottom, WoodenDoor
    {
        public WoodenDoorBottom(bool open, Direction facing) : base(open, facing) { }
    }
}