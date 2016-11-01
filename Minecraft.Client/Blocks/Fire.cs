namespace Decent.Minecraft.Client.Blocks
{
    public class Fire : Block
    {
        public Fire(byte intensity) : base(BlockType.Fire)
        {
            Intensity = intensity;
        }

        public byte Intensity { get; }
    }
}
