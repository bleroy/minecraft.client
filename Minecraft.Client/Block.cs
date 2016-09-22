using System.Collections.Generic;
using System.Linq;

namespace Minecraft.Client
{
    /// <summary>
    /// Describes a Minecraft block that can be sent to Minecraft.setBlock(s).
    /// </summary>
    public class Block
    {
        public BlockType Type { get; }
        public IList<int> Data { get; }

        public Block(BlockType type)
        {
            Type = type;
            Data = new int[] { };
        }

        public Block(BlockType type, IEnumerable<int> data)
        {
            Data = data.ToList();
        }

        public Block(BlockType type, params int[] data)
        {
            Data = data;
        }

        public static Block Air = new Block(BlockType.Air);
    }
}
