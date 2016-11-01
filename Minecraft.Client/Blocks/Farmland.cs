namespace Decent.Minecraft.Client.Blocks
{
    public class Farmland : Block
    {
        public Farmland(byte wetness) : base(BlockType.Farmland)
        {
            Wetness = wetness;
        }

        public byte Wetness { get; }
    }
}
