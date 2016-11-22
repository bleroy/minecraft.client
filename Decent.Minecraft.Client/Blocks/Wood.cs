namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Wood">Gamepedia link</a>.
    /// </summary>
    public class Wood : Block
    {
        private Wood(): base(BlockType.Wood) { }

        public Wood(WoodSpecies species = WoodSpecies.Oak, Orientation orientation = Orientation.None): this()
        {
            Orientation = orientation;
            Species = species;
        }

        public Orientation Orientation { get; }

        public WoodSpecies Species { get; }
    }
}
