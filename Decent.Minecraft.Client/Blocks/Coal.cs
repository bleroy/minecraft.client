namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Block_of_Coal">Gamepedia link</a>.
    /// </summary>
    public class Coal : Block
    {
        public Coal() : base(BlockType.Coal) { }
        protected Coal(bool charred) : base(BlockType.Coal)
        {
            IsCharred = charred;
        }

        protected bool IsCharred { get; }
    }

    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Coal_Ore">Gamepedia link</a>.
    /// </summary>
    public class Charcoal : Coal
    {
        public Charcoal() : base(charred: true) { }
    }
}
