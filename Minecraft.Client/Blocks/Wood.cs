namespace Decent.Minecraft.Client.Blocks
{
    public abstract class Wood : Block
    {
        private Wood(): base(BlockType.Wood) { }

        protected Wood(Species species = Species.Oak, Orientation orientation = Orientation.None): this()
        {
            Data = (byte)((byte)species ^ (byte)orientation);
        }

        public Orientation Orientation
        {
            get
            {
                return (Orientation)(Data & 0xC);
            }
            set
            {
                Data = (byte)(Data & 0x3 | (byte)value);
            }
        }

        public Species WoodSpecies
        {
            get
            {
                return (Species)(Data & 0x3);
            }
            set
            {
                Data = (byte)(Data & 0xC | (byte)value);
            }
        }

        public class Oak : Wood
        {
            public Oak(Orientation orientation = Orientation.None) : base(Species.Oak, orientation) { }
        }

        public class Spruce :  Wood
        {
            public Spruce(Orientation orientation = Orientation.None) : base(Species.Spruce, orientation) { }
        }

        public class Birch : Wood
        {
            public Birch(Orientation orientation = Orientation.None) : base(Species.Birch, orientation) { }
        }

        public class Jungle : Wood
        {
            public Jungle(Orientation orientation = Orientation.None) : base(Species.Jungle, orientation) { }
        }

        public enum Species : byte
        {
            Oak = 0,
            Spruce,
            Birch,
            Jungle
        }
    }
}
