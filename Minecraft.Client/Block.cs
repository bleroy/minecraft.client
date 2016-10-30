namespace Decent.Minecraft.Client
{
    /// <summary>
    /// Describes a Minecraft block that can be sent to Minecraft.setBlock(s).
    /// </summary>
    public partial class Block
    {
        public BlockType Type { get; protected set; }
        public ushort Data { get; protected set; }

        public Block(BlockType type)
        {
            Type = type;
            Data = 0;
        }

        public Block(BlockType type, ushort data)
        {
            Data = data;
        }
    }
}
