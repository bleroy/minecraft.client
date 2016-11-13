using System;
using System.Numerics;
using System.Threading.Tasks;

namespace Decent.Minecraft.Client
{
    public interface IWorld : IDisposable
    {
        Entity Player { get; }

        Block GetBlock(Vector3 coordinates);
        Block GetBlock(float x, float y, float z);
        T GetBlock<T>(Vector3 coordinates) where T : Block;
        T GetBlock<T>(float x, float y, float z) where T : Block;
        Task<Block> GetBlockAsync(Vector3 coordinates);
        Task<Block> GetBlockAsync(float x, float y, float z);
        Task<T> GetBlockAsync<T>(Vector3 coordinates) where T : Block;
        Task<T> GetBlockAsync<T>(float x, float y, float z) where T : Block;
        BlockType GetBlockType(float x, float y, float z);
        Task<BlockType> GetBlockTypeAsync(float x, float y, float z);
        Block[,,] GetBlocks(Vector3 corner1, Vector3 corner2);
        Task<Block[,,]> GetBlocksAsync(Vector3 corner1, Vector3 corner2);
        IWorld SetBlock(Block block, float x, float y, float z);
        IWorld SetBlock(Block block, Vector3 coordinates);
        Task<IWorld> SetBlockAsync(Block block, float x, float y, float z);
        Task<IWorld> SetBlockAsync(Block block, Vector3 coordinates);
        IWorld SetBlocks(Block block, Vector3 corner1, Vector3 corner2);
        Task<IWorld> SetBlocksAsync(Block block, Vector3 corner1, Vector3 corner2);
        Task<int> GetHeightAsync(Vector3 coordinates);
        Task<int> GetHeightAsync(float x, float z);
        int GetHeight(Vector3 coordinates);
        int GetHeight(float x, float z);
        IWorld PostToChat(string message);
        Task<IWorld> PostToChatAsync(string message);
    }
}