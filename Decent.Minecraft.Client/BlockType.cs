using System;
using System.Collections.Generic;
using Decent.Minecraft.Client.Blocks;

namespace Decent.Minecraft.Client
{
    /// <summary>
    /// Minecraft block types.
    /// The TypeId values correspond to actual Minecraft block IDs.
    /// <a href="http://minecraft.gamepedia.com/Data_values/Block_IDs">Gamepedia link</a>.
    /// </summary>
    public class BlockType
    {
        public BlockType(int typeId, Type type)
        {
            TypeId = typeId;
            Type = type;
        }

        /// <summary>
        /// The id of the type as it must be communicated in Java API calls.
        /// </summary>
        public int TypeId { get; }

        /// <summary>
        /// The block type.
        /// </summary>
        public Type Type { get; }
    }
}