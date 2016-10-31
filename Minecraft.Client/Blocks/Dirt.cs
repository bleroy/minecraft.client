namespace Decent.Minecraft.Client.Blocks
{
    public class Dirt : Block
    {
        public Dirt() : base(BlockType.Dirt) { }
        protected Dirt(byte type) : base(BlockType.Dirt, type) { }
    }

    public class CoarseDirt : Dirt
    {
        public CoarseDirt() : base(1) { }
    }

    public class Podzol : Dirt
    {
        public Podzol() : base(2) { }
    }
}
