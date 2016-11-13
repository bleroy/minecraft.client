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
            _ctors = new Func<byte, Block>[0x100];

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
            _ctors[(int)BlockType.Bookshelf] = d => new Bookshelf();
            _ctors[(int)BlockType.Bricks] = d => new Bricks();
            _ctors[(int)BlockType.Cactus] = d => new Cactus(d);
            _ctors[(int)BlockType.Chest] = d => new Chest(new[] {North, North, South, West, East}[d]);
            _ctors[(int)BlockType.Coal] = d =>
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
            _ctors[(int)BlockType.Diamond] = d => new Diamond();
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
                    return new IronDoorBottom((d & 0x4) != 0, new[] { East, South, West, North }[(d & 0xC) >> 2]);
                }
                return new IronDoorTop((d & 0x1) != 0, (d & 0x2) != 0);
            };
            _ctors[(int)BlockType.DoorWood] = d =>
            {
                if ((d & 0x8) == 0)
                {
                    return new WoodenDoorBottom((d & 0x4) != 0, new[] { East, South, West, North }[(d & 0xC) >> 2]);
                }
                return new WoodenDoorTop((d & 0x1) != 0, (d & 0x2) != 0);
            };
            _ctors[(int)BlockType.Emerald] = d => new Emerald();
            _ctors[(int)BlockType.EmeraldOre] = d => new EmeraldOre();
            _ctors[(int)BlockType.Farmland] = d => new Farmland(d);
            _ctors[(int)BlockType.Fence] = d => new Fence();
            _ctors[(int)BlockType.FenceGate] = d => new FenceGate((Direction)(d & 0x3), (d & 0x4) != 0);
            _ctors[(int)BlockType.Fire] = d => new Fire(d);
            _ctors[(int)BlockType.Gold] = d => new Gold();
            _ctors[(int)BlockType.GoldOre] = d => new GoldOre();
            _ctors[(int)BlockType.Grass] = d => new Grass();
            _ctors[(int)BlockType.Iron] = d => new Iron();
            _ctors[(int)BlockType.IronOre] = d => new IronOre();
            _ctors[(int)BlockType.MossStone] = d => new MossStone();
            _ctors[(int)BlockType.Obsidian] = d => new Obsidian();
            _ctors[(int)BlockType.SnowLayer] = d => new SnowLayer();
            _ctors[(int)BlockType.StainedClay] = d => new StainedClay((Clay.Color)d);
            _ctors[(int)BlockType.StoneBricks] = d =>
                d == 0 ? new StoneBricks() :
                d == 1 ? new MossyStoneBricks() :
                d == 2 ? new CrackedStoneBricks() :
                (Block)new ChiseledStoneBricks();
            _ctors[(int)BlockType.Wood] = d => new Wood((Wood.Species)(d & 0x3), (Orientation)(d & 0xC));
        }

        public static Block Create(BlockType type, byte data)
        {
            // Look-up the right construction logic
            var ctor = _ctors[(int)type];
            if (ctor == null) return new UnknownBlock(type, data);
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

            var clay = block as StainedClay;
            if (clay != null)
            {
                return new JavaBlock(BlockType.Clay, (byte)clay.Stain);
            }

            var coal = block as Coal;
            if (coal != null)
            {
                return new JavaBlock(BlockType.Coal, (byte)(coal is Charcoal ? 1 : 0));
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


            var farmland = block as Farmland;
            if (farmland != null)
            {
                return new JavaBlock(BlockType.Farmland, farmland.Wetness);
            }

            var fenceGate = block as FenceGate;
            if (fenceGate != null)
            {
                return new JavaBlock(BlockType.FenceGate, (byte)((byte)fenceGate.Facing | (fenceGate.IsOpen ? 0x4 : 0x0)));
            }

            var fire = block as Fire;
            if (fire != null)
            {
                return new JavaBlock(BlockType.Fire, fire.Intensity);
            }

            var stoneBrick = block as StoneBricks;
            if (stoneBrick != null)
            {
                return new JavaBlock(BlockType.StoneBricks, (byte)stoneBrick.Quality);
            }

            var wood = block as Wood;
            if (wood != null)
            {
                return new JavaBlock(BlockType.Wood,
                    (byte)((byte)wood.WoodSpecies ^ (byte)wood.Orientation));
            }

            var unknown = block as UnknownBlock;
            if (unknown != null)
            {
                return new JavaBlock(unknown.Type, unknown.Data);
            }

            // All other types are simply represented.
            return new JavaBlock(block.Type, 0);
        }
    }
}
