using Decent.Minecraft.Client;

namespace Minecraft.Scratch
{
    public class BlockProbability
    {
        public BlockProbability(Block block, float percentage)
        {
            Block = block;
            Percentage = percentage;
        }

        public Block Block { get; }
        public float Percentage { get; }
    }
}
