namespace Decent.Minecraft.Client.Blocks
{
    public class Cactus : Block
    {
        public Cactus(byte age) : base(BlockType.Cactus)
        {
            Age = age;
        }

        public byte Age { get; }
    }
}
