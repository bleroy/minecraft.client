using Decent.Minecraft.Client.Blocks;
using System;
using static Decent.Minecraft.Client.Direction;

namespace Decent.Minecraft.Client
{
    /// <summary>
    /// A representation of a Minecraft block used in communication with Java Minecraft instances.
    /// </summary>
    public class JavaBlock : Block
    {
        public byte Data { get; }

        public JavaBlock(BlockType type, byte data) : base(type)
        {
            Data = data;
        }

        // This is an array of construction logic so building the right type of block is just a lookup in a table.
        // I'm aware that this is slightly ugly, and I wish the compiler would make that super-efficient while I
        // could just write a simple switch statement, but eh.
        private static Func<byte, Block>[] _ctors;

        static JavaBlock()
        {
            // Prepare the lookup table once and for all.
            var _ctors = new Func<byte, Block>[(int)BlockType.NetherReactorCore];

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
            _ctors[(int)BlockType.BedrockInvisible] = d => new InvisibleBedrock();
            _ctors[(int)BlockType.Bookshelf] = d => new Bookshelf();
            _ctors[(int)BlockType.BrickBlock] = d => new BrickBlock();
            _ctors[(int)BlockType.Cactus] = d => new Cactus(d);
            _ctors[(int)BlockType.Chest] = d => new Chest(new[] {North, North, South, West, East}[d]);
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
                    return new IronDoorBottom((d & 0x4) != 0, new[] { East, South, West, North }[d & 0xC]);
                }
                return new IronDoorTop((d & 0x1) != 0, (d & 0x2) != 0);
            };
        }

        public static Block Create(BlockType type, byte data)
        {
            // Look-up the right construction logic
            var ctor = _ctors[(int)type];
            // Execute it, which will return a block of the correct concrete type
            // (which is not necessarily "Concrete", but can be Clay, Wood, etc.)
            return ctor(data);
        }

        public static JavaBlock From(Block block)
        {
            // This will look so much better in C# 7 with pattern matching...
            var bed = block as Bed;
            if (bed != null)
            {
                return new JavaBlock(BlockType.Bed, (byte)(
                    (byte)bed.HeadFacing |
                    (bed.Occupied ? 0x4 : 0x0) |
                    (bed is BedHead ? 0x8 : 0x0)));
            }

            var cactus = block as Cactus;
            if (cactus != null)
            {
                return new JavaBlock(BlockType.Cactus, cactus.Age);
            }

            var chest = block as Chest;
            if (chest != null)
            {
                return new JavaBlock(BlockType.Chest, (byte)(
                    chest.Facing == North ? 2 :
                    chest.Facing == South ? 3 :
                    chest.Facing == West ? 4 :
                    5));
            }

            var clay = block as Clay;
            if (clay != null)
            {
                return new JavaBlock(BlockType.Clay, (byte)clay.Stain);
            }

            var coal = block as Coal;
            if (coal != null)
            {
                return new JavaBlock(BlockType.CoalOre, (byte)(coal is Charcoal ? 1 : 0));
            }

            var cobblestone = block as Cobblestone;
            if (cobblestone != null)
            {
                return new JavaBlock(BlockType.Cobblestone, (byte)(cobblestone is MossyCobblestone ? 1 : 0));
            }

            var dirt = block as Dirt;
            if (dirt != null)
            {
                return new JavaBlock(BlockType.Dirt, (byte)(dirt is CoarseDirt ? 1 : dirt is Podzol ? 2 : 0));
            }

            var doorTop = block as DoorTop;
            if (doorTop != null)
            {
                return new JavaBlock(doorTop is IronDoorTop ? BlockType.DoorIron : BlockType.DoorWood,
                    (byte)(0x8 | (doorTop.HingeOnTheLeft ? 0x1 : 0x0) | (doorTop.Powered ? 0x2 : 0x0)));
            }

            var doorBottom = block as DoorBottom;
            if (doorBottom != null)
            {
                return new JavaBlock(doorBottom is IronDoorBottom ? BlockType.DoorIron : BlockType.DoorWood,
                    (byte)((doorBottom.IsOpen ? 0x4 : 0x0) |
                    (doorBottom.Facing == East ? 0 :
                    doorBottom.Facing == South ? 1 :
                    doorBottom.Facing == West ? 2 :
                    3)));
            }

            // All other types are simply represented.
            return new JavaBlock(block.Type, 0);
        }
    }
}
