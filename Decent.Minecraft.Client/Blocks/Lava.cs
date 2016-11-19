namespace Decent.Minecraft.Client.Blocks
{
    public class Lava : Block
    {
        public Lava() : base(BlockType.LavaStationary) { }
        protected Lava(BlockType blockType) : base(blockType) { }
    }

    public class LavaFlowing : Lava
    {
        public LavaFlowing() : base(BlockType.LavaFlowing) { }
    }
}