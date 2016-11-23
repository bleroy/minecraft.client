namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Water">Gamepedia link</a>.
    /// </summary>
    public class Water : Block
    {
        public Water() : base(BlockType.WaterStationary) { }
        protected Water(BlockType blockType) : base(blockType) { }
    }

    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Water">Gamepedia link</a>.
    /// </summary>
    public class WaterFlowing : Water
    {
        public WaterFlowing() : base(BlockType.WaterFlowing) { }
    }
}