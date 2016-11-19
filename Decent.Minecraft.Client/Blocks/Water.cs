namespace Decent.Minecraft.Client.Blocks
{
    public class Water : Block
    {
        public Water() : base(BlockType.WaterStationary) { }
        protected Water(BlockType blockType) : base(blockType) { }
    }

    public class WaterFlowing : Water
    {
        public WaterFlowing() : base(BlockType.WaterFlowing) { }
    }
}