using Decent.Minecraft.Client;
using Decent.Minecraft.Client.Blocks;
using ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

using ImageColor = ImageSharp.Color;
using MinecraftColor = Decent.Minecraft.Client.Color;

namespace Decent.Minecraft.ImageBuilder
{
    public class ImageBuilder
    {
        private static Dictionary<ImageColor, MinecraftColor> _colorPalette;
        private IWorld _world;

        public ImageBuilder(IWorld world)
        {
            _world = world;
        }

        /// <summary>
        /// Maps colors from Minecraft colored blocks to the Color enumeration
        /// see http://minecraft.gamepedia.com/Data_values#Wool.2C_Stained_Clay.2C_Stained_Glass_and_Carpet
        /// </summary>
        static ImageBuilder()
        {
            _colorPalette = new Dictionary<ImageColor, MinecraftColor>()
            {
                { new ImageColor(221, 221, 221), MinecraftColor.White },
                { new ImageColor(219, 125, 62), MinecraftColor.Orange },
                { new ImageColor(179, 80, 188), MinecraftColor.Magenta },
                { new ImageColor(107, 138, 201), MinecraftColor.LightBlue },
                { new ImageColor(177, 166, 39), MinecraftColor.Yellow },
                { new ImageColor(65, 174, 56), MinecraftColor.Lime },
                { new ImageColor(208, 132, 153), MinecraftColor.Pink },
                { new ImageColor(64, 64, 64), MinecraftColor.Gray },
                { new ImageColor(154, 161, 161), MinecraftColor.LightGray },
                { new ImageColor(46, 110, 137), MinecraftColor.Cyan },
                { new ImageColor(126, 61, 181), MinecraftColor.Purple },
                { new ImageColor(46, 56, 141), MinecraftColor.Blue },
                { new ImageColor(79, 50, 31), MinecraftColor.Brown },
                { new ImageColor(53, 70, 27), MinecraftColor.Green },
                { new ImageColor(150, 52, 48), MinecraftColor.Red },
                { new ImageColor(25, 22, 22), MinecraftColor.Black }
            };
        }

        public void DrawImage(string imagePath, Vector3 targetPosition, int maxSize = 100)
        {
            using (var stream = File.OpenRead(imagePath))
            {
                var image = new Image(stream);
                Image<ImageColor, uint> resized = null;
                // resize image
                if (image.Width > maxSize || image.Height > maxSize)
                {
                    resized = (image.Width > image.Height)
                        ? image.Resize(maxSize, (int)Math.Floor((double)(image.Height * maxSize) / image.Width))
                        : image.Resize((int)Math.Floor((double)(image.Width * maxSize) / image.Height), maxSize);
                }
                resized = resized ?? image;
                // get all pixels
                using (var pixels = resized.Lock())
                {

                    // iterate over pixels and draw a block
                    for (int y = 0; y < resized.Height; y++)
                    {
                        for (int x = 0; x < resized.Width; x++)
                        {
                            var pixel = pixels[x, resized.Height - y - 1];
                            var color = GetClosestMinecraftColor(pixel);

                            var brick = new Wool(color);

                            _world.SetBlock(brick, targetPosition + new Vector3(x, y, 0));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Algorithm taken on http://www.codeproject.com/Articles/17044/Find-the-Nearest-Color-with-C-Using-the-Euclidean
        /// </summary>
        private MinecraftColor GetClosestMinecraftColor(ImageColor pixelColor)
        {
            // set a default color
            MinecraftColor nearestColor = MinecraftColor.Black;

            double minimumDistance = 500;

            double pixelRed = Convert.ToDouble(pixelColor.R);
            double pixelGreen = Convert.ToDouble(pixelColor.G);
            double pixelBlue = Convert.ToDouble(pixelColor.B);

            foreach (var color in _colorPalette)
            {
                // compute the Euclidean distance between the two colors
                var red = Math.Pow(color.Key.R - pixelRed, 2.0);
                var green = Math.Pow(color.Key.G - pixelGreen, 2.0);
                var blue = Math.Pow(color.Key.B - pixelBlue, 2.0);

                var temp = Math.Sqrt(blue + green + red);

                // explore the result and store the nearest color
                if (temp == 0.0)
                {
                    nearestColor = color.Value;
                    break;
                }
                else if (temp < minimumDistance)
                {
                    minimumDistance = temp;
                    nearestColor = color.Value;
                }
            }

            return nearestColor;
        }
    }
}
