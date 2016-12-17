using System;
using System.Numerics;
using System.Threading.Tasks;

namespace Decent.Minecraft.Client
{
    /// <summary>
    /// The contract for a Minecraft world representation.
    /// </summary>
    public interface IWorld : IDisposable
    {
        /// <summary>
        /// The player.
        /// </summary>
        IPlayer Player { get; }

        /// <summary>
        /// Gets the block at the provided coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates of the block.</param>
        /// <returns>The block. Can be cast to a concrete type.</returns>
        Block GetBlock(Vector3 coordinates);

        /// <summary>
        /// Gets the block at the provided coordinates.
        /// </summary>
        /// <param name="x">The X coordinate of the block.</param>
        /// <param name="y">The Y coordinate of the block.</param>
        /// <param name="z">The Z coordinate of the block.</param>
        /// <returns>The block. Can be cast to a concrete type.</returns>
        Block GetBlock(float x, float y, float z);

        /// <summary>
        /// Gets the block of type `T` at the provided coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates of the block.</param>
        /// <returns>The block or null if the block is not of the type `T`.</returns>
        T GetBlock<T>(Vector3 coordinates) where T : Block;

        /// <summary>
        /// Gets the block of type `T` at the provided coordinates.
        /// </summary>
        /// <param name="x">The X coordinate of the block.</param>
        /// <param name="y">The Y coordinate of the block.</param>
        /// <param name="z">The Z coordinate of the block.</param>
        /// <returns>The block or null if the block is not of the type `T`.</returns>
        T GetBlock<T>(float x, float y, float z) where T : Block;

        /// <summary>
        /// Asynchronously gets the block at the provided coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates of the block.</param>
        /// <returns>The block. Can be cast to a concrete type.</returns>
        Task<Block> GetBlockAsync(Vector3 coordinates);

        /// <summary>
        /// Asynchronously gets the block at the provided coordinates.
        /// </summary>
        /// <param name="x">The X coordinate of the block.</param>
        /// <param name="y">The Y coordinate of the block.</param>
        /// <param name="z">The Z coordinate of the block.</param>
        /// <returns>The block. Can be cast to a concrete type.</returns>
        Task<Block> GetBlockAsync(float x, float y, float z);

        /// <summary>
        /// Asynchronously gets the block of type `T` at the provided coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates of the block.</param>
        /// <returns>The block or null if the block is not of the type `T`.</returns>
        Task<T> GetBlockAsync<T>(Vector3 coordinates) where T : Block;

        /// <summary>
        /// Asynchronously gets the block of type `T` at the provided coordinates.
        /// </summary>
        /// <param name="x">The X coordinate of the block.</param>
        /// <param name="y">The Y coordinate of the block.</param>
        /// <param name="z">The Z coordinate of the block.</param>
        /// <returns>The block or null if the block is not of the type `T`.</returns>
        Task<T> GetBlockAsync<T>(float x, float y, float z) where T : Block;

        /// <summary>
        /// Gets an array containing all the blocks in the parallelipiped between
        /// the two provided corners.
        /// </summary>
        /// <param name="corner1">The first corner.</param>
        /// <param name="corner2">The second corner.</param>
        /// <returns>An array of blocks.</returns>
        Block[,,] GetBlocks(Vector3 corner1, Vector3 corner2);

        /// <summary>
        /// Asynchronously gets an array containing all the blocks in the parallelipiped between
        /// the two provided corners.
        /// </summary>
        /// <param name="corner1">The first corner.</param>
        /// <param name="corner2">The second corner.</param>
        /// <returns>An array of blocks.</returns>
        Task<Block[,,]> GetBlocksAsync(Vector3 corner1, Vector3 corner2);

        /// <summary>
        /// Sets a block at specific coordinates.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="z">The Z coordinate.</param>
        /// <returns>The world, enabling chaining of calls.</returns>
        IWorld SetBlock(Block block, float x, float y, float z);

        /// <summary>
        /// Sets a block at specific coordinates.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns>The world, enabling chaining of calls.</returns>
        IWorld SetBlock(Block block, Vector3 coordinates);

        /// <summary>
        /// Sets a block at specific coordinates.
        /// </summary>
        /// <typeparam name="TBlock">The type of the block to set.</typeparam>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="z">The Z coordinate.</param>
        /// <returns>The world, enabling chaining of calls.</returns>
        IWorld SetBlock<TBlock>(float x, float y, float z) where TBlock : Block, new();

        /// <summary>
        /// Sets a block at specific coordinates.
        /// </summary>
        /// <typeparam name="TBlock">The type of the block to set.</typeparam>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns>The world, enabling chaining of calls.</returns>
        IWorld SetBlock<TBlock>(Vector3 coordinates) where TBlock : Block, new();

        /// <summary>
        /// Asynchronously sets a block at specific coordinates.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="z">The Z coordinate.</param>
        /// <returns>The world, enabling chaining of calls.</returns>
        Task<IWorld> SetBlockAsync(Block block, float x, float y, float z);

        /// <summary>
        /// Asynchronously sets a block at specific coordinates.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns>The world, enabling chaining of calls.</returns>
        Task<IWorld> SetBlockAsync(Block block, Vector3 coordinates);

        /// <summary>
        /// Asynchronously sets a block at specific coordinates.
        /// </summary>
        /// <typeparam name="TBlock">The type of the block to set.</typeparam>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="z">The Z coordinate.</param>
        /// <returns>The world, enabling chaining of calls.</returns>
        Task<IWorld> SetBlockAsync<TBlock>(float x, float y, float z) where TBlock : Block, new();

        /// <summary>
        /// Asynchronously sets a block at specific coordinates.
        /// </summary>
        /// <typeparam name="TBlock">The type of the block to set.</typeparam>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns>The world, enabling chaining of calls.</returns>
        Task<IWorld> SetBlockAsync<TBlock>(Vector3 coordinates) where TBlock : Block, new();

        /// <summary>
        /// Fills the parallelepiped between the two corners with the provided block.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="corner1">The first corner.</param>
        /// <param name="corner2">The second corner.</param>
        /// <returns>The world, enabling chaining of calls.</returns>
        IWorld SetBlocks(Block block, Vector3 corner1, Vector3 corner2);

        /// <summary>
        /// Asynchronously fills the parallelepiped between the two corners
        /// with the provided block.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="corner1">The first corner.</param>
        /// <param name="corner2">The second corner.</param>
        /// <returns>The world, enabling chaining of calls.</returns>
        Task<IWorld> SetBlocksAsync(Block block, Vector3 corner1, Vector3 corner2);

        /// <summary>
        /// Asynchronously gets the height of the terrain at the provided coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns>The height.</returns>
        Task<int> GetHeightAsync(Vector3 coordinates);

        /// <summary>
        /// Asynchronously gets the height of the terrain at the provided coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="z">The Z coordinate.</param>
        /// <returns>The height.</returns>
        Task<int> GetHeightAsync(float x, float z);

        /// <summary>
        /// Gets the height of the terrain at the provided coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns>The height.</returns>
        int GetHeight(Vector3 coordinates);

        /// <summary>
        /// Gets the height of the terrain at the provided coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="z">The Z coordinate.</param>
        /// <returns>The height.</returns>
        int GetHeight(float x, float z);

        /// <summary>
        /// Posts a message to the chat.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The world, enabling chaining of calls.</returns>
        IWorld PostToChat(string message);

        /// <summary>
        /// Asynchronously posts a message to the chat.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The world, enabling chaining of calls.</returns>
        Task<IWorld> PostToChatAsync(string message);

        /// <summary>
        /// An event that gets triggered each time a message is posted on the chat.
        /// </summary>
        event EventHandler<ChatEventArgs> PostedToChat;

        /// <summary>
        /// An event that gets triggered each time a block is hit.
        /// </summary>
        event EventHandler<BlockEventArgs> BlockHit;
    }
}