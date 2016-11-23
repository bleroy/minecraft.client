namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Dirt">Gamepedia link</a>.
    /// </summary>
    public class Dirt : Block
    {
        public Dirt() : base(BlockType.Dirt) { }
    }

    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Coarse_Dirt">Gamepedia link</a>.
    /// </summary>
    public class CoarseDirt : Dirt { }

    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Podzol">Gamepedia link</a>.
    /// </summary>
    public class Podzol : Dirt { }
}
