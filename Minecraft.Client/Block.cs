using Decent.Minecraft.Client.Blocks;
using System;

namespace Decent.Minecraft.Client
{
    /// <summary>
    /// Describes a Minecraft block that can be sent to Minecraft.setBlock(s).
    /// </summary>
    public partial class Block
    {
        public BlockType Type { get; set; } = BlockType.Air;
        public byte Data { get; set; }

        protected Block(BlockType type)
        {
            Type = type;
            Data = 0;
        }

        protected Block(BlockType type, byte data)
        {
            Data = data;
        }

        // This is an array of construction logic so building the right type of block is just a lookup in a table.
        // I'm aware that this is slightly ugly, and I wish the compiler would make that super-efficient while I
        // could just write a simple switch statement, but eh.
        private static Func<byte, Block>[] _ctors;

        static Block()
        {
            // Prepare the lookup table once and for all.
            var _ctors = new Func<byte, Block>[(int)BlockType.NetherReactorCore];

            // TODO: remove knowledge of the contents of d from here, or put it all here, or somewhere else.
            // In other words, should the bock classes be POCOs and serialization logic be put somewhere else?
            _ctors[(int)BlockType.Air] = d => new Air();
            _ctors[(int)BlockType.Bed] = d =>
            {
                if ((d & 0x8) == 0)
                {
                    return new BedFoot((Direction)(d & 0x3), (d & 0x4) != 0);
                }
                else
                {
                    return new BedHead((Direction)(d & 0x3), (d & 0x4) != 0);
                }
            };
            _ctors[(int)BlockType.Bedrock] = d => new Bedrock();
            _ctors[(int)BlockType.BedrockInvisible] = d => new BedrockInvisible();
            _ctors[(int)BlockType.Bookshelf] = d => new Bookshelf();
            _ctors[(int)BlockType.BrickBlock] = d => new BrickBlock();
            _ctors[(int)BlockType.Cactus] = d => new Cactus(d);
            _ctors[(int)BlockType.Chest] = d => new Chest(
                d == 2 ? Direction.North :
                d == 3 ? Direction.South :
                d == 4 ? Direction.West :
                Direction.East);
            _ctors[(int)BlockType.Clay] = d => new Clay((Clay.Color)d);
            _ctors[(int)BlockType.CoalOre] = d =>
            {
                if (d == 1) return new Charcoal();
                return new Coal();
            };
            _ctors[(int)BlockType.Cobblestone] = d =>
            {
                if (d == 1) return new MossyCobblestone();
                return new Cobblestone();
            };
            _ctors[(int)BlockType.Cobweb] = d => new Cobweb();
            _ctors[(int)BlockType.CraftingTable] = d => new CraftingTable();
            _ctors[(int)BlockType.DiamondBlock] = d => new Diamond();
            _ctors[(int)BlockType.DiamondOre] = d => new DiamondOre();
            _ctors[(int)BlockType.Dirt] = d =>
            {
                if (d == 2) return new Podzol();
                if (d == 1) return new CoarseDirt();
                return new Dirt();
            };
            _ctors[(int)BlockType.DoorIron] = d =>
            {
                if ((d & 0x8) == 0)
                {
                    return new IronDoorBottom((d & 0x4) != 0, new[] { Direction.East, Direction.South, Direction.West, Direction.North }[d & 0xC]);
                }
                return new IronDoorTop((d & 0x1) != 0, (d & 0x2) != 0);
            };
        }

        public static Block Create(BlockType type = BlockType.Air, byte data = 0)
        {
            // Look-up the right construction logic
            var ctor = _ctors[(int)type];
            // Execute it, which will return a block of the correct concrete type
            // (which is not necessarily "Concrete", but can be Clay, Wood, etc.)
            return ctor(data);
        }
    }
}
