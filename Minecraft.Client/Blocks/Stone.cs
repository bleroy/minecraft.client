namespace Decent.Minecraft.Client.Blocks
{
    public class Stone : Block
    {
        public Stone(StoneVariants variant = StoneVariants.Stone) : base(BlockType.Stone)
        {
            Variant = variant;
        }

        public StoneVariants Variant { get; }

        public enum StoneVariants : byte
        {
            Stone = 0,
            Granite,
            SmoothGranite,
            Diorite,
            SmoothDiorite,
            Andesite,
            SmoothAndesite
        }
    }
}
