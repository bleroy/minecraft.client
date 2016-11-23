namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Stone">Gamepedia link</a>.
    /// </summary>
    public class Stone : Block
    {
        public Stone(Mineral mineral = Mineral.Stone) : base(BlockType.Stone)
        {
            Mineral = mineral;
        }

        public Mineral Mineral { get; }
    }
}
