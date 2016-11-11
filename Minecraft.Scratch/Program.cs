using Decent.Minecraft.Client;
using Decent.Minecraft.Client.Blocks;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Threading;

namespace Minecraft.Scratch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var world = JavaWorld.Connect(args[0]))
            {
                var imageBuilder = new Minecraft.ImageBuilder.ImageBuilder(world);

                imageBuilder.DrawImage("mvp.jpg", 100);
            }
        }
    }
}
