using System;

namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Cactus">Gamepedia link</a>.
    /// </summary>
    public class Cactus : IBlock
    {
        public Cactus(int age)
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
