namespace Decent.Minecraft.Client.Blocks
{
    public class Cactus : Block
    {
        public Cactus(byte age) : base(BlockType.Cactus, age)
        {
            Age = age;
        }

        public byte Age
        {
            get
            {
                return Data;
            }
            set
            {
                Data = value;
            }
        }
    }
}
