using Decent.Minecraft.Client.Blocks;
using System;
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
            _ctors[Id<Chest>()] = d => new Chest(new[] { North, North, South, West, East }[d]);
            _ctors[Id<Cobblestone>()] = d => d == 1 ? new MossyCobblestone() : new Cobblestone();
            _ctors[Id<Dirt>()] = d => d == 2 ? new Podzol() : d == 1 ? new CoarseDirt() : new Dirt();
            _ctors[Id<IronDoor>()] = d => (d & 0x8) == 0 ?
                (IronDoor)new IronDoorBottom((d & 0x4) != 0, new[] { East, South, West, North }[(d & 0xC) >> 2]) :
                new IronDoorTop((d & 0x1) != 0, (d & 0x2) != 0);
            _ctors[Id<WoodenDoor>()] = d => (d & 0x8) == 0 ?
                (WoodenDoor)new WoodenDoorBottom((d & 0x4) != 0, new[] { East, South, West, North }[(d & 0xC) >> 2]) :
                new WoodenDoorTop((d & 0x1) != 0, (d & 0x2) != 0);
            _ctors[Id<Farmland>()] = d => new Farmland(d);
            _ctors[Id<FenceGate>()] = d => new FenceGate((Direction)(d & 0x3), (d & 0x4) != 0);
            _ctors[Id<Fire>()] = d => new Fire(d);
            _ctors[Id<Snow>()] = d => new Snow(8);
            _ctors[SnowLayer] = d => new Snow(d);
            _ctors[Id<StainedClay>()] = d => new StainedClay((Color)d);
            _ctors[Id<StainedGlass>()] = d => new StainedGlass((Color)d);
            _ctors[Id<Stone>()] = d => new Stone((Mineral)d);
            _ctors[Id<StoneBricks>()] = d =>
                d == 0 ? new StoneBricks() :
                d == 1 ? new MossyStoneBricks() :
                d == 2 ? new CrackedStoneBricks() :
                (StoneBricks)new ChiseledStoneBricks();
            _ctors[Id<Wood>()] = d => new Wood((WoodSpecies)(d & 0x3), (Axis)(d & 0xC));
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
                    chest.Facing == North ? 2 :
                    chest.Facing == South ? 3 :
                    chest.Facing == West ? 4 :
                    5));
            }

            var stone = block as Stone;
            if (stone != null)
            {
                return new JavaBlock(Id<Stone>(), (byte)stone.Mineral);
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
                return new JavaBlock(doorTop is IronDoorTop ? Id<IronDoor>() : Id<WoodenDoor>(),
                    (byte)(0x8 | (doorTop.HingeOnTheLeft ? 0x1 : 0x0) | (doorTop.Powered ? 0x2 : 0x0)));
            }

            var doorBottom = block as DoorBottom;
            if (doorBottom != null)
            {
                return new JavaBlock(doorBottom is IronDoorBottom ? Id<IronDoor>() : Id<WoodenDoor>(),
                    (byte)((doorBottom.IsOpen ? 0x4 : 0x0) |
                    (doorBottom.Facing == East ? 0 :
                    doorBottom.Facing == South ? 1 :
                    doorBottom.Facing == West ? 2 :
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

            var snow = block as Snow;
            if (snow != null)
            {
                return snow.Thickness == 8 ?
                    new JavaBlock(Id<Snow>()) :
                    new JavaBlock(SnowLayer, (byte)snow.Thickness);
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

            var stoneBrick = block as StoneBricks;
            if (stoneBrick != null)
            {
                return new JavaBlock(Id<StoneBricks>(), (byte)stoneBrick.Quality);
            }

            var wood = block as Wood;
            if (wood != null)
            {
                return new JavaBlock(Id<Wood>(),
                    (byte)((byte)wood.Species ^ (byte)wood.Orientation));
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
