namespace Decent.Minecraft.Client.Blocks
{
    public class Wood : Block
    {
        private Wood(): base(BlockType.Wood) { }

        public Wood(Species species = Species.Oak, Orientation orientation = Orientation.None): this()
        {
            Orientation = orientation;
            WoodSpecies = species;
        }

        public Orientation Orientation { get; }

        public Species WoodSpecies { get; }

        public enum Species : byte
        {
            Oak = 0,
            Spruce,
            Birch,
            Jungle
        }
    }
}
