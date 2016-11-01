namespace Decent.Minecraft.Client.Blocks
{
    public class Cobblestone : Block
    {
        public Cobblestone() : base(BlockType.Cobblestone) { }
        protected Cobblestone(bool mossy) : base(BlockType.Cobblestone)
        {
            IsMossy = mossy;
        }

        protected bool IsMossy { get; }
    }

    public class MossyCobblestone : Cobblestone
    {
        public MossyCobblestone() : base(mossy: true) { }
    }
}
