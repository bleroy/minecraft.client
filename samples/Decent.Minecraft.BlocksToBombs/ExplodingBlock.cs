using Decent.Minecraft.Client;
using Decent.Minecraft.Client.Blocks;
using System.Numerics;
using System.Threading.Tasks;

namespace Decent.Minecraft.BlocksToBombs
{
    public class ExplodingBlock
    {
        public ExplodingBlock(IWorld world, Vector3 position, int fuseInSeconds, int blastRadius)
        {
            World = world;
            Position = position;
            Fuse = fuseInSeconds;
            BlastRadius = blastRadius;
        }

        public int BlastRadius { get; }
        public int Fuse { get; }
        public Vector3 Position { get; }
        public IWorld World { get; }

        public void Explode()
        {
            var block = World.GetBlock(Position);
            var air = new Air();
            // Flash the block for the duration of the fuse.
            for (var fuse = 0; fuse < Fuse; fuse++)
            {
                World.SetBlock(air, Position);
                Task.Delay(500).Wait();
                World.SetBlock(block, Position);
                Task.Delay(500).Wait();
            }
            // Create concentric shells of air.
            for (var radius = 0; radius < BlastRadius; radius++)
            {
                var squaredRadius = radius * radius;
                var squaredRadiusPlusOne = (radius + 1) * (radius + 1);
                for (var x = -BlastRadius; x <= BlastRadius; x++)
                {
                    for (var y = -BlastRadius; y <= BlastRadius; y++)
                    {
                        for (var z = -BlastRadius; z <= BlastRadius; z++)
                        {
                            var squaredDistance = x * x + y * y + z * z;
                            if ((squaredDistance < squaredRadiusPlusOne)
                                && (squaredDistance >= squaredRadius))
                            {
                                var currentBlockPosition = Position + new Vector3(x, y, z);
                                // Check if the block is bedrock, as this should not be destroyed.
                                // Also avoid transforming air into air.
                                var currentBlock = World.GetBlock(currentBlockPosition);
                                if (!(currentBlock is Bedrock || currentBlock is Air))
                                {
                                    World.SetBlock(air, currentBlockPosition);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
