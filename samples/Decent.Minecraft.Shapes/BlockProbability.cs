using Decent.Minecraft.Client;

namespace Decent.Minecraft.Shapes
{
    public class BlockProbability
    {
        public BlockProbability(IBlock block, float percentage)
        {
            Block = block;
            Percentage = percentage;
        }

        public IBlock Block { get; }
        public float Percentage { get; }
    }
}
