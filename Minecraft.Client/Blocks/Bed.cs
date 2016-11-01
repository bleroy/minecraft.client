namespace Decent.Minecraft.Client.Blocks
{
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
