// Ported by B. Le Roy from Python code originally written by Alexander Pruss
// (see https://github.com/arpruss/raspberryjammod/blob/master/mcpipy/borromean.py)
using Decent.Minecraft.Client;
using Decent.Minecraft.Client.Blocks;
using System.Collections.Generic;
using System.Numerics;
using static System.Math;

namespace Decent.Minecraft.Shapes
{
    public class Borromean
    {
        public Borromean(IWorld world, Vector3 position, int scale = 30)
        {
            World = world;
            Position = position;
            Scale = scale;
        }

        private HashSet<Vector3> _done;

        public Vector3 Position { get; }
        public int Scale { get; }
        public IWorld World { get; }

        public void Build()
        {
            var r = Sqrt(3) / 3;

            var gold = new Gold();
            _done = new HashSet<Vector3>();
            var t = 0.0;
            while (t < 2 * PI)
            {
                var position = new Vector3(
                    Position.X + (int)(Scale * Cos(t)),
                    Position.Y + (int)(Scale * (Sin(t) + r)),
                    Position.Z + (int)(Scale * -Cos(3 * t) / 3));
                Ball(position, 4, gold);
                t += 2 * PI / 10000;
            }

            var lapis = new LapisLazuli();
            _done = new HashSet<Vector3>();
            t = 0.0;
            while (t < 2 * PI)
            {
                var position = new Vector3(
                    Position.X + (int)(Scale * (Cos(t) + 0.5)),
                    Position.Y + (int)(Scale * (Sin(t) - r / 2)),
                    Position.Z + (int)(Scale * -Cos(3 * t) / 3));
                Ball(position, 4, lapis);
                t += 2 * PI / 10000;
            }

            var diamond = new Diamond();
            _done = new HashSet<Vector3>();
            t = 0.0;
            while (t < 2 * PI)
            {
                var position = new Vector3(
                    Position.X + (int)(Scale * (Cos(t) - 0.5)),
                    Position.Y + (int)(Scale * (Sin(t) - r / 2)),
                    Position.Z + (int)(Scale * -Cos(3 * t) / 3));
                Ball(position, 4, diamond);
                t += 2 * PI / 10000;
            }
        }

        private void Ball(Vector3 center, int radius, Block material)
        {
            var squaredRadius = radius * radius;
            for (var x = -radius; x <= radius; x++)
            {
                for (var y = -radius; y <= radius; y++)
                {
                    for (var z = -radius; z <= radius; z++)
                    {
                        var position = center + new Vector3(x, y, z);
                        if ((x * x + y * y + z * z <= squaredRadius)
                            && !_done.Contains(position))
                        {
                            World.SetBlock(material, position);
                            _done.Add(position);
                        }
                    }
                }
            }
        }
    }
}
