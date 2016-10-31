namespace Decent.Minecraft.Client.Blocks
{
    public class Clay : Block
    {
        public Clay(Color stain = Color.White) : base(BlockType.Clay)
        {
            Stain = stain;
        }

        public Color Stain
        {
            get
            {
                return (Color)Data;
            }
            set
            {
                Data = (byte)value;
            }
        }

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
}
