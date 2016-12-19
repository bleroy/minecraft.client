namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Stone">Gamepedia link</a>.
    /// </summary>
    public class Stone : IBlock
    {
        public Stone(Mineral mineral = Mineral.Stone)
        {
            Mineral = mineral;
        }

        public Mineral Mineral { get; }
    }
}
