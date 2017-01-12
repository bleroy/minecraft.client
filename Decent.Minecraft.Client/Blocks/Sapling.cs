namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Sapling">Gamepedia link</a>.
    /// </summary>
    public class Sapling : IBlock
    {
        public Sapling(WoodSpecies species = WoodSpecies.Oak, bool isReadyToGrow = false)
        {
            Species = species;
            IsReadyToGrow = isReadyToGrow;
        }

        public bool IsReadyToGrow{ get; }

        public WoodSpecies Species { get; }
    }
}
