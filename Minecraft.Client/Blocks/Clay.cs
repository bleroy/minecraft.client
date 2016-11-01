namespace Decent.Minecraft.Client.Blocks
{
    public class Clay : Block
    {
        public Clay() : base(BlockType.Clay) { }
        protected Clay(BlockType type) : base(type) { }

        public enum Color : byte
        {
            White = 0,
            Orange,
            Magenta,
            LightBlue,
            Yellow,
            Lime,
            Pink,
            Gray,
            LightGray,
            Cyan,
            Purple,
            Blue,
            Brown,
            Green,
            Red,
            Black
        }
    }

    public class HardenedClay : Clay
    {
        public HardenedClay() : base(BlockType.HardenedClay) { }
    }

    public class StainedClay : HardenedClay
    {
        public StainedClay(Color stain = Color.White) : base()
        {
            Stain = stain;
        }

        public Color Stain { get; }
    }
}
