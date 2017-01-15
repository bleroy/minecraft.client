using Decent.Minecraft.Client.Blocks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using static Decent.Minecraft.Client.Direction;
using static Decent.Minecraft.Client.Java.JavaBlockTypes;

namespace Decent.Minecraft.Client.Java
{
    /// <summary>
    /// A representation of a Minecraft block used in communication with Java Minecraft instances.
    /// </summary>
    public class JavaBlock
    {
        /// <summary>
        /// The Java block data.
        /// </summary>
        public byte Data { get; }

        /// <summary>
        /// The Java Minecraft type of this block.
        /// </summary>
        public int TypeId { get; }

        public JavaBlock(int type, byte data = 0)
        {
            Data = data;
            TypeId = type;
        }

        // This is an array of construction logic so building the right type of block is just a lookup in a table.
        // I'm aware that this is slightly ugly, and I wish the compiler would make that super-efficient while I
        // could just write a simple switch statement, but eh.
        private static Func<int, IBlock>[] _ctors;

        private static Dictionary<WoodSpecies, int> _speciesToDoorId = new Dictionary<WoodSpecies, int>()
            {
                {WoodSpecies.Acacia, AcaciaWoodenDoor},
                {WoodSpecies.Birch, BirchWoodenDoor},
                {WoodSpecies.DarkOak, DarkOakWoodenDoor},
                {WoodSpecies.Jungle, JungleWoodenDoor},
                {WoodSpecies.Oak, OakWoodenDoor},
                {WoodSpecies.Spruce, SpruceWoodenDoor}
            };

        static JavaBlock()
        {
            // Prepare the lookup table once and for all.
            _ctors = new Func<int, IBlock>[0x100];

            // Let's loop over the Java block registry:
            foreach(var blockType in Types)
            {
                var type = blockType.Type;
                var typeInfo = type.GetTypeInfo();
                var typeId = blockType.TypeId;
                // Look for a parameterless constructor, so we can do the work
                // without the block author having to bother about it.
                var ctor = typeInfo.GetConstructor(Type.EmptyTypes);
                if (ctor != null)
                {
                    _ctors[typeId] = d => (Expression.Lambda<Func<IBlock>>(Expression.New(type))).Compile()();
                }
            }

            // Remain to be defined only the deserialization of blocks that need parameters:
            _ctors[Id<Bed>()] = d => (d & 0x8) == 0 ?
                (Bed)new BedFoot((Direction)(d & 0x3), (d & 0x4) != 0) :
                new BedHead((Direction)(d & 0x3), (d & 0x4) != 0);
            _ctors[Id<Cactus>()] = d => new Cactus(d);
            _ctors[Id<Chest>()] = d => new Chest(new[] { Direction.North, Direction.North, Direction.South, Direction.West, Direction.East }[d]);
            _ctors[Id<Cobblestone>()] = d => d == 1 ? new MossyCobblestone() : new Cobblestone();
            _ctors[Id<Dirt>()] = d => d == 2 ? new Podzol() : d == 1 ? new CoarseDirt() : new Dirt();
            _ctors[Id<IronDoor>()] = d => (d & 0x8) == 0 ?
                (IronDoor)new IronDoorBottom((d & 0x4) != 0, new[] { Direction.East, Direction.South, Direction.West, Direction.North }[(d & 0x3)]) :
                new IronDoorTop((d & 0x1) == 1, (d & 0x2) != 0);

            _ctors[Id<Lava>()] = d => new Lava((Level)(d & 0x7), true, (d & 0x8) != 0);
            _ctors[StationaryLava] = d => new Lava((Level)(d & 0x7), false, (d & 0x8) != 0);

            // Handle all the wooden doors
            foreach (KeyValuePair<WoodSpecies, int> pair in _speciesToDoorId)
            {
                _ctors[pair.Value] = d => (d & 0x8) == 0 ?
                (WoodenDoor)new WoodenDoorBottom((d & 0x4) != 0, new[] { Direction.East, Direction.South, Direction.West, Direction.North }[(d & 0x3)], pair.Key) :
                new WoodenDoorTop((d & 0x1) == 1, (d & 0x2) != 0, pair.Key);
            }

            _ctors[Id<Farmland>()] = d => new Farmland(d);
            _ctors[Id<FenceGate>()] = d => new FenceGate((Direction)(d & 0x3), (d & 0x4) != 0);
            _ctors[Id<Fire>()] = d => new Fire(d);
            _ctors[Id<Sand>()] = d => d == 1 ? new RedSand() : new Sand();
            _ctors[Id<Sapling>()] = d => new Sapling((WoodSpecies)(d & 0x3), (d & 0x8) != 0);

            _ctors[Id<Snow>()] = d => new Snow(8);
            _ctors[SnowLayer] = d => new Snow(d);

            _ctors[Id<Sponge>()] = d => d == 1 ? new WetSponge() : new Sponge();
            _ctors[Id<Torch>()] = d => new Torch(new[] { Direction3.Nowhere, Direction3.East, Direction3.West, Direction3.South, Direction3.North, Direction3.Up }[d]);
            _ctors[Id<StainedClay>()] = d => new StainedClay((Color)d);
            _ctors[Id<StainedGlass>()] = d => new StainedGlass((Color)d);
            _ctors[Id<Stone>()] = d => new Stone((Mineral)d);
            _ctors[Id<StoneBricks>()] = d =>
                d == 0 ? new StoneBricks() :
                d == 1 ? new MossyStoneBricks() :
                d == 2 ? new CrackedStoneBricks() :
                (StoneBricks)new ChiseledStoneBricks();

            _ctors[Id<Water>()] = d => new Water((Level)(d & 0x7), true, (d & 0x8) != 0);
            _ctors[StationaryWater] = d => new Water((Level)(d & 0x7), false, (d & 0x8) != 0);

            _ctors[Id<Wood>()] = d => new Wood((WoodSpecies)(d & 0x3), (Axis)(d & 0xC));
            _ctors[AcaciaAndDarkOakWood] = d => new Wood((WoodSpecies)((d & 0x3) + 4), (Axis)(d & 0xC));

            _ctors[Id<WoodPlanks>()] = d => new WoodPlanks((WoodSpecies)(d & 0x3));
            _ctors[Id<Wool>()] = d => new Wool((Color)d);
        }

        public static IBlock Create(int typeId, byte data)
        {
            // Look-up the right construction logic
            var ctor = _ctors[typeId];
            if (ctor == null) return new UnknownBlock();
            // Execute it, which will return a block of the correct concrete type
            // (which is not necessarily "Concrete", but can be Clay, Wood, etc.)
            return ctor(data);
        }

        public static JavaBlock From(IBlock block)
        {
            // This will look so much better in C# 7 with pattern matching...
            var bed = block as Bed;
            if (bed != null)
            {
                return new JavaBlock(Id<Bed>(), (byte)(
                    (byte)bed.HeadFacing |
                    (bed.Occupied ? 0x4 : 0x0) |
                    (bed is BedHead ? 0x8 : 0x0)));
            }

            var cactus = block as Cactus;
            if (cactus != null)
            {
                return new JavaBlock(Id<Cactus>(), (byte)cactus.Age);
            }

            var chest = block as Chest;
            if (chest != null)
            {
                return new JavaBlock(Id<Chest>(), (byte)(
                    chest.Facing == Direction.North ? 2 :
                    chest.Facing == Direction.South ? 3 :
                    chest.Facing == Direction.West ? 4 :
                    5));
            }

            var cobblestone = block as Cobblestone;
            if (cobblestone != null)
            {
                return new JavaBlock(Id<Cobblestone>(), (byte)(cobblestone is MossyCobblestone ? 1 : 0));
            }

            var dirt = block as Dirt;
            if (dirt != null)
            {
                return new JavaBlock(Id<Dirt>(), (byte)(dirt is CoarseDirt ? 1 : dirt is Podzol ? 2 : 0));
            }

            var doorTop = block as DoorTop;
            if (doorTop != null)
            {
                return new JavaBlock(doorTop is IronDoorTop ? Id<IronDoor>() : _speciesToDoorId[((WoodenDoorTop)doorTop).Species],
                    (byte)(0x8 | (doorTop.IsHingeOnTheRight ? 0x1 : 0x0) | (doorTop.IsPowered ? 0x2 : 0x0)));
            }

            var doorBottom = block as DoorBottom;
            if (doorBottom != null)
            {
                return new JavaBlock(doorBottom is IronDoorBottom ? Id<IronDoor>() : _speciesToDoorId[((WoodenDoorBottom)doorBottom).Species],
                    (byte)((doorBottom.IsOpen ? 0x4 : 0x0) |
                    (doorBottom.Facing == Direction.East ? 0 :
                    doorBottom.Facing == Direction.South ? 1 :
                    doorBottom.Facing == Direction.West ? 2 :
                    3)));
            }

            var farmland = block as Farmland;
            if (farmland != null)
            {
                return new JavaBlock(Id<Farmland>(), (byte)farmland.Wetness);
            }

            var fenceGate = block as FenceGate;
            if (fenceGate != null)
            {
                return new JavaBlock(Id<FenceGate>(), (byte)((byte)fenceGate.Facing | (fenceGate.IsOpen ? 0x4 : 0x0)));
            }

            var fire = block as Fire;
            if (fire != null)
            {
                return new JavaBlock(Id<Fire>(), (byte)fire.Intensity);
            }

            var lava = block as Lava;
            if (lava != null)
            {
                return new JavaBlock(lava.IsFlowing ? Id<Lava>() : StationaryLava, (byte)((byte)lava.Level | (lava.IsFalling ? 0x8 : 0x0)));
            }

            var sapling = block as Sapling;
            if (sapling != null)
            {
                return new JavaBlock(Id<Sapling>(), (byte)((byte)sapling.Species | (sapling.IsReadyToGrow ? 0x8 : 0x0)));
            }
            
            var sand = block as Sand;
            if (sand != null)
            {
                return new JavaBlock(Id<Sand>(), (byte)(sand is RedSand ? 1 : 0));
            }

            var snow = block as Snow;
            if (snow != null)
            {
                return snow.Thickness == 8 ?
                    new JavaBlock(Id<Snow>()) :
                    new JavaBlock(SnowLayer, (byte)snow.Thickness);
            }

            var sponge = block as Sponge;
            if (sponge != null)
            {
                return new JavaBlock(Id<Sponge>(), (byte)(sponge is Sponge ? 1 : 0));
            }

            var stainedClay = block as StainedClay;
            if (stainedClay != null)
            {
                return new JavaBlock(Id<Clay>(), (byte)stainedClay.Color);
            }

            var stainedGlass = block as StainedGlass;
            if (stainedGlass != null)
            {
                return new JavaBlock(Id<StainedGlass>(), (byte)stainedGlass.Color);
            }

            var stone = block as Stone;
            if (stone != null)
            {
                return new JavaBlock(Id<Stone>(), (byte)stone.Mineral);
            }

            var stoneBrick = block as StoneBricks;
            if (stoneBrick != null)
            {
                return new JavaBlock(Id<StoneBricks>(), (byte)stoneBrick.Quality);
            }

            var torch = block as Torch;
            if (torch != null)
            {
                return new JavaBlock(Id<Torch>(), (byte)(
                    torch.Facing == Direction3.East? 1 :
                    torch.Facing == Direction3.West? 2 :
                    torch.Facing == Direction3.South? 3 :
                    torch.Facing == Direction3.North? 4 :
                    5));
            }

            var water = block as Water;
            if (water != null)
            {
                return new JavaBlock(water.IsFlowing ? Id<Water>() : StationaryWater, (byte)((byte)water.Level | (water.IsFalling ? 0x8 : 0x0)));
            }

            var wood = block as Wood;
            if (wood != null)
            {
               return (byte)(wood.Species) < 0x4 ?                    
                    new JavaBlock(Id<Wood>(), (byte)((byte)wood.Species ^ (byte)wood.Orientation)) :
                    new JavaBlock(AcaciaAndDarkOakWood, (byte)(((byte)wood.Species - 4) ^ (byte)wood.Orientation));
            }

            var woodPlanks = block as WoodPlanks;
            if (woodPlanks != null)
            {
                return new JavaBlock(Id<WoodPlanks>(), (byte)woodPlanks.Species);
            }

            var wool = block as Wool;
            if (wool != null)
            {
                return new JavaBlock(Id<Wool>(), (byte)wool.Color);
            }

            var unknown = block as UnknownBlock;
            if (unknown != null)
            {
                throw new InvalidOperationException("Can't serialize an unknown block.");
            }

            // All other types are simply represented.
            return new JavaBlock(GetTypeId(block.GetType()));
        }
    }
}
