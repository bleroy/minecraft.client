namespace Decent.Minecraft.Client
{
    /// <summary>
    /// The base class for all Minecraft blocks.
    /// </summary>
    public abstract class Block
    {
        /// <summary>
        /// All concrete block types should set their own type by calling into this constructor.
        /// </summary>
        /// <param name="type">The Minecraft block type</param>
        protected Block(BlockType type)
        {
            Type = type;
        }

        /// <summary>
        /// The Minecraft type of this block.
        /// </summary>
        public BlockType Type { get; }
    }
}
