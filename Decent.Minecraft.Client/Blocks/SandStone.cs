namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Sandstone">Gamepedia link</a>.
    /// </summary>
    public class Sandstone : IBlock
    {
        public Sandstone(Finish finish = Finish.None)
        {
            Finish = finish;
        }

        public Finish Finish { get; }
    }

    public class RedSandstone : IBlock
    {
        public RedSandstone(Finish finish = Finish.None)
        {
            Finish = finish;
        }

        public Finish Finish { get; }
    }
}
