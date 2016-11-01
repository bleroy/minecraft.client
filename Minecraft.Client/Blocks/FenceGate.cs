namespace Decent.Minecraft.Client.Blocks
{
    public class FenceGate : Block
    {
        public FenceGate(Direction facing, bool isOpen) : base(BlockType.FenceGate)
        {
            Facing = facing;
            IsOpen = isOpen;
        }

        public Direction Facing { get; }
        public bool IsOpen { get; }
    }
}
