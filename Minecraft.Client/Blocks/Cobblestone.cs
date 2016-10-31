namespace Decent.Minecraft.Client.Blocks
{
    public class Cobblestone : Block
    {
        public Cobblestone() : base(BlockType.Cobblestone) { }
        protected Cobblestone(bool mossy) : base(BlockType.Cobblestone, (byte)(mossy ? 1 : 0)) { }
    }

    public class MossyCobblestone : Cobblestone
    {
        public MossyCobblestone() : base(mossy: true) { }
    }
}
