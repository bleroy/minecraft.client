namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// The bed is a multi block structure, allowing the player to sleep in the night.<br />
    /// Beds are made out of a <see cref="BedFoot">foot</see> and a <see cref="BedHead">head</see> part.<br />
    /// Breaking one of the two parts will destroy the other one and drop the bed item.<br />
    /// <a href="http://minecraft.gamepedia.com/Bed">Gamepedia link</a>.
    /// </summary>
    public abstract class Bed : Block
    {
        protected Bed(Direction headFacing, bool occupied = false) : base(BlockType.Bed)
        {
            HeadFacing = headFacing;
            Occupied = occupied;
        }

        public Direction HeadFacing { get; }

        public bool Occupied { get; }
    }

    public class BedHead : Bed
    {
        public BedHead(Direction headFacing, bool occupied) : base(headFacing, occupied) { }
    }

    public class BedFoot : Bed
    {
        public BedFoot(Direction headFacing, bool occupied) : base(headFacing, occupied) { }
    }
}
