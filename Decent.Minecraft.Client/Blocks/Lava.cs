namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Lava">Gamepedia link</a>.
    /// </summary>
    public class Lava : Block
    {
        public Lava() : base(BlockType.LavaStationary) { }
        protected Lava(BlockType blockType) : base(blockType) { }
    }

    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Lava">Gamepedia link</a>.
    /// </summary>
    public class LavaFlowing : Lava
    {
        public LavaFlowing() : base(BlockType.LavaFlowing) { }
    }
}