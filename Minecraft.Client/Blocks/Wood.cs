namespace Decent.Minecraft.Client.Blocks
{
    public class Wood : Block
    {
        private Wood(): base(BlockType.Wood) { }

        public Wood(WoodType type = WoodType.Oak, Orientation orientation = Orientation.None): this()
        {
            Data = (ushort)((ushort)type ^ (ushort)orientation);
        }

        public class Oak : Wood
        {
            public Oak(Orientation orientation = Orientation.None) : base(WoodType.Oak, orientation) { }
        }

        public class Spruce :  Wood
        {
            public Spruce(Orientation orientation = Orientation.None) : base(WoodType.Spruce, orientation) { }
        }

        public class Birch : Wood
        {
            public Birch(Orientation orientation = Orientation.None) : base(WoodType.Birch, orientation) { }
        }

        public class Jungle : Wood
        {
            public Jungle(Orientation orientation = Orientation.None) : base(WoodType.Jungle, orientation) { }
        }

        public enum WoodType : ushort
        {
            Oak = 0,
            Spruce,
            Birch,
            Jungle
        }
    }
}
