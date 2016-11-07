namespace Decent.Minecraft.Client.Blocks
{
    public class UnknownBlock : Block
    {
        public UnknownBlock(BlockType type, byte data = 0) : base(type)
        {
            Data = data;
        }

        public byte Data { get; }
    }
}
