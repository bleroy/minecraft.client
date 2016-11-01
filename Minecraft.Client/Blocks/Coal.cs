namespace Decent.Minecraft.Client.Blocks
{
    public class Coal : Block
    {
        public Coal() : base(BlockType.CoalOre) { }
        protected Coal(bool charred) : base(BlockType.CoalOre)
        {
            IsCharred = charred;
        }

        protected bool IsCharred { get; }
    }

    public class Charcoal : Coal
    {
        public Charcoal() : base(charred: true) { }
    }
}
