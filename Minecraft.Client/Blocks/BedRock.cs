namespace Decent.Minecraft.Client.Blocks
{
    public class Bedrock : Block
    {
        protected Bedrock(bool visible) : base(visible ? BlockType.Bedrock : BlockType.BedrockInvisible) { }

        public Bedrock() : base(BlockType.Bedrock) { }
    }

    public class InvisibleBedrock : Bedrock
    {
        public InvisibleBedrock() : base(false) { }
    }
}
