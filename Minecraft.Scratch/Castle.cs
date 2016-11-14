using Decent.Minecraft.Client;
using Decent.Minecraft.Client.Blocks;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace Minecraft.Scratch
{
    public class Castle
    {
        public Castle(IWorld world, Vector3 position, byte wallSize = 51)
        {
            World = world;
            Util = new Util(world);
            Position = new Vector3(
                (float)Math.Round(position.X),
                (float)Math.Round(position.Y),
                (float)Math.Round(position.Z));
            WallSize = wallSize;
        }

        private IWorld World { get; }
        private Util Util { get; }
        public Vector3 Position { get; }
        public byte WallSize { get; }

        private IEnumerable<BlockProbability> _distribution = new BlockProbability[]
        {
            new BlockProbability(new MossStone(), 5f),
            new BlockProbability(new MossyStoneBricks(), 10f),
            new BlockProbability(new CrackedStoneBricks(), 20f),
            new BlockProbability(new StoneBricks(), 65f)
        };

        public async Task BuildAsync()
        {
            var groundY = await Util.GetHeightBelowAsync(Position) + 1;
            // Walls
            await CrenellatedSquareWithInnerWall(
                new Vector3(Position.X, groundY, Position.Z),
                WallSize, 9, 10, _distribution);
            // Towers
            await Tower(
                new Vector3(Position.X - 7, groundY, Position.Z - 7),
                9, 12, 13, 11, _distribution);
            await Tower(
                new Vector3(Position.X + WallSize - 2, groundY, Position.Z + WallSize - 2),
                9, 12, 13, 11, _distribution);
            await Tower(
                new Vector3(Position.X - 7, groundY, Position.Z + WallSize - 2),
                9, 12, 13, 11, _distribution);
            await Tower(
                new Vector3(Position.X + WallSize - 1, groundY, Position.Z - 7),
                9, 12, 13, 11, _distribution);
            // Keep
            var keepStartX = Position.X + WallSize / 4;
            var keepStartZ = Position.Z + WallSize / 4;
            var keepWidth = (byte)(WallSize / 6 * 3);
            await Tower(
                new Vector3(keepStartX, groundY, keepStartZ),
                keepWidth, 16, 17, 15, _distribution);
            // Moat
            var moatStartX = Position.X - 12;
            var moatStartZ = Position.Z - 12;
            var moatInnerSize = WallSize + 24;
            for (var i = 0; i < 6; i++)
            {
                await MoatSquare(
                    new Vector3(moatStartX - i, groundY - 1, moatStartZ - i),
                    (byte)(moatInnerSize + 2 * i), 2);
            }
        }

        public void Build()
        {
            BuildAsync().Wait();
        }

        private async Task Wall(
            Vector3 corner1, Vector3 corner2,
            byte baseHeight, byte altHeight,
            IEnumerable<BlockProbability> distribution)
        {
            var x = corner1.X;
            var z = corner1.Z;

            while (true)
            {
                var height = (x - corner1.X + z - corner1.Z) % 2 == 0 ?
                    altHeight : baseHeight;
                var y0 = await Util.GetHeightBelowAsync(new Vector3(x, corner1.Y, z));
                await Util.RectangularPrismAsync(
                    new Vector3(x, y0, z),
                    new Vector3(x, corner1.Y + height, z),
                    distribution);
                if (x >= corner2.X && z >= corner2.Z) return;
                if (x < corner2.X)
                {
                    x++;
                }
                if (z < corner2.Z)
                {
                    z++;
                }
            }
        }

        private async Task MoatSide(Vector3 corner1, Vector3 corner2, byte depth)
        {
            var x = corner1.X;
            var z = corner1.Z;
            var water = new Water();

            while (true)
            {
                var y0 = await Util.GetHeightBelowAsync(new Vector3(x, corner1.Y, z));
                await World.SetBlocksAsync(water, new Vector3(x, y0 - depth + 1, z), new Vector3(x, y0, z));
                if (x >= corner2.X && z >= corner2.Z) return;
                if (x < corner2.X)
                {
                    x++;
                }
                if (z < corner2.Z)
                {
                    z++;
                }
            }
        }

        private async Task CrenellatedSquare(
            Vector3 origin,
            byte width, byte height, byte altHeight,
            IEnumerable<BlockProbability> distribution)
        {
            await Wall(
                origin,
                new Vector3(origin.X + width - 1, origin.Y, origin.Z),
                height, altHeight, distribution);
            await Wall(
                origin,
                new Vector3(origin.X, origin.Y, origin.Z + width - 1),
                height, altHeight, distribution);
            await Wall(
                new Vector3(origin.X + width - 1, origin.Y, origin.Z),
                new Vector3(origin.X + width - 1, origin.Y, origin.Z + width - 1),
                height, altHeight, distribution);
            await Wall(
                new Vector3(origin.X, origin.Y, origin.Z + width - 1),
                new Vector3(origin.X + width - 1, origin.Y, origin.Z + width - 1),
                height, altHeight, distribution);
        }

        private async Task Square(
            Vector3 origin, byte width, byte height,
            IEnumerable<BlockProbability> distribution)
        {
            await CrenellatedSquare(origin, width, height, height, distribution);
        }

        private async Task MoatSquare(Vector3 origin, byte width, byte depth)
        {
            await MoatSide(origin, new Vector3(origin.X + width - 1, origin.Y, origin.Z), depth);
            await MoatSide(origin, new Vector3(origin.X, origin.Y, origin.Z + width - 1), depth);
            await MoatSide(
                new Vector3(origin.X + width - 1, origin.Y, origin.Z),
                new Vector3(origin.X + width - 1, origin.Y, origin.Z + width - 1),
                depth);
            await MoatSide(
                new Vector3(origin.X, origin.Y, origin.Z + width - 1),
                new Vector3(origin.X + width - 1, origin.Y, origin.Z + width - 1),
                depth);
        }

        private async Task CrenellatedSquareWithInnerWall(
            Vector3 origin,
            byte width, byte baseHeight, byte altHeight,
            IEnumerable<BlockProbability> distribution)
        {
            await CrenellatedSquare(origin, width, baseHeight, altHeight, distribution);
            await Square(new Vector3(origin.X + 1, origin.Y, origin.Z + 1), (byte)(width - 2), (byte)(baseHeight - 1), distribution);
        }

        private async Task Tower(
            Vector3 origin,
            byte width, byte baseHeight, byte altHeight, byte innerHeight,
            IEnumerable<BlockProbability> distribution)
        {
            await CrenellatedSquareWithInnerWall(origin, width, baseHeight, altHeight, distribution);
            await Util.RectangularPrismAsync(
                new Vector3(origin.X + 2, origin.Y + innerHeight - 1, origin.Z + 2),
                new Vector3(origin.X + width - 3, origin.Y + innerHeight - 1, origin.Z + width - 3),
                distribution);
        }
    }
}
