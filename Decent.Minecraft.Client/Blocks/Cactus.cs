using System;

namespace Decent.Minecraft.Client.Blocks
{
    public class Cactus : Block
    {
        public Cactus(int age) : base(BlockType.Cactus)
        {
            if (age < 0 || age > 15)
            {
                throw new ArgumentException("Cactus age must be between 0 and 15.", "age");
            }
            Age = age;
        }

        public int Age { get; }
    }
}
