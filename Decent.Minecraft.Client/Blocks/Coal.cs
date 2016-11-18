namespace Decent.Minecraft.Client.Blocks
{
    public class Coal : Block
    {
        public Coal() : base(BlockType.Coal) { }
        protected Coal(bool charred) : base(BlockType.Coal)
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
