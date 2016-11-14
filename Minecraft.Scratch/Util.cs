using Decent.Minecraft.Client;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using static System.Math;

namespace Minecraft.Scratch
{
    public class Util
    {
        public Util(IWorld world)
        {
            World = world;
        }

        private IWorld World { get; }

        public async Task<int> GetHeightBelowAsync(Vector3 position)
        {
            var y0 = position.Y - 255;
            var y = position.Y;
            while (position.Y > y0)
            {
                if (await World.GetBlockTypeAsync(position.X, y, position.Z) != BlockType.Air)
                {
                    return (int)y;
                }
                y--;
            }
            return (int)Min(await World.GetHeightAsync(position.X, position.Z), y);
        }

        public async Task RectangularPrismAsync(Vector3 corner1, Vector3 corner2, Block block)
        {
            var x1 = (int)Round(corner1.X);
            var y1 = (int)Round(corner1.Y);
            var z1 = (int)Round(corner1.Z);
            var x2 = (int)Round(corner2.X);
            var y2 = (int)Round(corner2.Y);
            var z2 = (int)Round(corner2.Z);
            for (var x = Min(x1, x2); x <= Max(x1, x2); x++)
            {
                for (var y = Min(y1, y2); y <= Max(y1, y2); y++)
                {
                    for (var z = Min(z1, z2); z <= Max(z1, z2); z++)
                    {
                        await World.SetBlockAsync(block, x, y, z);
                    }
                }
            }
        }

        public async Task RectangularPrismAsync(Vector3 corner1, Vector3 corner2, IEnumerable<BlockProbability> distribution)
        {
            var rnd = new Random();
            var x1 = (int)Round(corner1.X);
            var y1 = (int)Round(corner1.Y);
            var z1 = (int)Round(corner1.Z);
            var x2 = (int)Round(corner2.X);
            var y2 = (int)Round(corner2.Y);
            var z2 = (int)Round(corner2.Z);
            for (var x = Min(x1, x2); x <= Max(x1, x2); x++)
            {
                for (var y = Min(y1, y2); y <= Max(y1, y2); y++)
                {
                    for (var z = Min(z1, z2); z <= Max(z1, z2); z++)
                    {
                        var random = (float)rnd.NextDouble() * 100;
                        foreach(var blockProbability in distribution)
                        {
                            random -= blockProbability.Percentage;
                            if (random < 0)
                            {
                                await World.SetBlockAsync(blockProbability.Block, x, y, z);
                            }
                        }
                    }
                }
            }
        }
    }
}
