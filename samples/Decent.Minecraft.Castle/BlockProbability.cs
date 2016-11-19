using Decent.Minecraft.Client;

namespace Decent.Minecraft.Castle
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
