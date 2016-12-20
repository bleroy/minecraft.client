using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Decent.Minecraft.Client
{
    public class BlockRegistry
    {
        /// <summary>
        /// The list of registered types.
        /// </summary>
        public IList<BlockType> Types { get; } = new List<BlockType>();
        private Dictionary<int, Type> _idToType = new Dictionary<int, Type>();
        private Dictionary<Type, int> _typeToId = new Dictionary<Type, int>();

        /// <summary>
        /// Get the type for an id.
        /// </summary>
        /// <param name="typeId">The id</param>
        /// <returns>The type if it exists, UnknownBlock otherwise.</returns>
        public Type GetType(int typeId)
        {
            if (_idToType.ContainsKey(typeId))
            {
                return _idToType[typeId];
            }
            return null;
        }

        /// <summary>
        /// Get the id for a type.
        /// </summary>
        /// <typeparam name="TBlock">The block type</typeparam>
        /// <returns>The type id</returns>
        public int GetTypeId<TBlock>() where TBlock : IBlock
        {
            return GetTypeId(typeof(TBlock));
        }

        /// <summary>
        /// Get the id for a type.
        /// </summary>
        /// <param name="type">The block type</param>
        /// <returns>The type id</returns>
        public int GetTypeId(Type type)
        {
            if (_typeToId.ContainsKey(type))
            {
                return _typeToId[type];
            }
            throw new ArgumentException("Unregistered type", nameof(type));
        }

        /// <summary>
        /// Register a type.
        /// </summary>
        /// <typeparam name="TBlock">The block type to register</typeparam>
        /// <param name="typeId">The type id</param>
        /// <returns>The registry, allowing for chained calls</returns>
        public BlockRegistry Register<TBlock>(int typeId) where TBlock : IBlock
        {
            var type = typeof(TBlock);
            Debug.Assert(!_idToType.ContainsKey(typeId), $"A type has already been registered with the id {typeId}.");
            Debug.Assert(!_typeToId.ContainsKey(type), $"Id {typeId} has already been registered for type {type}.");
            Types.Add(new BlockType(typeId, type));
            _idToType.Add(typeId, type);
            _typeToId.Add(type, typeId);
            return this;
        }

        /// <summary>
        /// Unregisters a previously registered block type.
        /// </summary>
        /// <typeparam name="TBlock">The type of block to unregister</typeparam>
        /// <param name="typeId">The type id</param>
        /// <returns>The registry, allowing for chained calls</returns>
        public BlockRegistry Unregister<TBlock>(int typeId) where TBlock : IBlock
        {
            var blockType = Types[typeId];
            Types.Remove(blockType);
            _typeToId.Remove(typeof(TBlock));
            _idToType.Remove(typeId);
            return this;
        }
    }
}
