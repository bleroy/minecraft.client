namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Door">Gamepedia link</a>.
    /// </summary>
    public abstract class Door : IBlock
    {
    }

    public abstract class DoorTop : Door
    {
        protected DoorTop(bool isHingeOnTheRight, bool isPowered)
        {
            IsHingeOnTheRight = isHingeOnTheRight;
            IsPowered = isPowered;
        }

        public bool IsHingeOnTheRight { get; }

        public bool IsPowered { get; }
    }

    public abstract class DoorBottom : Door
    {
        protected DoorBottom(bool open, Direction facing)
        {
            IsOpen = open;
            // A Door is facing the direction you are facing when you place it or the direction it swings in.
            Facing = facing;
        }

        public bool IsOpen { get; }

        public Direction Facing { get; }
    }

    public interface IronDoor : IBlock { }
    public interface WoodenDoor : IBlock { }

    public class IronDoorTop : DoorTop, IronDoor
    {
        public IronDoorTop(bool isHingeOnTheRight, bool powered) : base(isHingeOnTheRight, powered) { }
    }

    public class IronDoorBottom : DoorBottom, IronDoor
    {
        public IronDoorBottom(bool open = false, Direction facing = Direction.South) : base(open, facing) { }
    }

    public class WoodenDoorTop : DoorTop, WoodenDoor
    {
        public WoodenDoorTop(bool isHingeOnTheRight = false, bool powered = false, WoodSpecies species = WoodSpecies.Oak) : base(isHingeOnTheRight, powered)
        {
            Species = species;
        }
        public WoodSpecies Species { get; }
    }

    public class WoodenDoorBottom : DoorBottom, WoodenDoor
    {
        public WoodenDoorBottom(bool open = false, Direction facing = Direction.South, WoodSpecies species = WoodSpecies.Oak) : base(open, facing)
        {
            Species = species;
        }
        public WoodSpecies Species { get; }
    }
}