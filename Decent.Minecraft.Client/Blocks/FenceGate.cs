namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Fence_Gate">Gamepedia link</a>.
    /// </summary>
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
