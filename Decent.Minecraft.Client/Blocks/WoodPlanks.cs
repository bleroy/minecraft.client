namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Wood_Planks">Gamepedia link</a>.
    /// </summary>
    public class WoodPlanks : IBlock
    {
        public WoodPlanks(WoodSpecies species = WoodSpecies.Oak)
        {
            Species = species;
        }
        
        public WoodSpecies Species { get; }
    }
}
