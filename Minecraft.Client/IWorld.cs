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
        IWorld PostToChat(string message);
        Task PostToChatAsync(string message);
    }
}