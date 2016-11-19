namespace Decent.Minecraft.Client
{
    public abstract class Block
    {
        protected Block(BlockType type)
        {
            Type = type;
        }

        public BlockType Type { get; }
    }
}
