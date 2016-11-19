namespace Decent.Minecraft.Client.Blocks
{
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
