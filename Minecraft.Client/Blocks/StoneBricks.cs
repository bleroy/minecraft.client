namespace Decent.Minecraft.Client.Blocks
{
    public class StoneBricks : Block
    {
        public StoneBricks() : this(StoneQuality.Normal) { }

        protected StoneBricks(StoneQuality quality) : base(BlockType.Stone)
        {
            Quality = quality;
        }

        public StoneQuality Quality { get; }

        public enum StoneQuality : byte
        {
            Normal = 0,
            Mossy,
            Cracked,
            Chiseled
        }
    }

    public class MossyStoneBricks : StoneBricks
    {
        public MossyStoneBricks() : base(StoneQuality.Mossy) { }
    }

    public class CrackedStoneBricks : StoneBricks
    {
        public CrackedStoneBricks() : base(StoneQuality.Cracked) { }
    }

    public class ChiseledStoneBricks : StoneBricks
    {
        public ChiseledStoneBricks() : base(StoneQuality.Chiseled) { }
    }
}
