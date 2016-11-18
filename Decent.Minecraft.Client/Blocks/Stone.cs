namespace Decent.Minecraft.Client.Blocks
{
    public class Stone : Block
    {
        public Stone(Mineral mineral = Mineral.Stone) : base(BlockType.Stone)
        {
            Mineral = mineral;
        }

        public Mineral Mineral { get; }
    }
}
