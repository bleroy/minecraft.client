namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Wood">Gamepedia link</a>.
    /// </summary>
    public class Wood : IBlock
    {
        public Wood(WoodSpecies species = WoodSpecies.Oak, Axis orientation = Axis.None)
        {
            Orientation = orientation;
            Species = species;
        }

        public Axis Orientation { get; }

        public WoodSpecies Species { get; }
    }
}
