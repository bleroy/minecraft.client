using Decent.Minecraft.Client;
using Decent.Minecraft.Client.Blocks;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Decent.Minecraft.ImageBuilder
{
    public class ImageBuilder
    {
        private static Dictionary<MagickColor, Color> _colorPalette;
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
            _colorPalette = new Dictionary<MagickColor, Color>()
            {
                { MagickColor.FromRgb(221, 221, 221), Color.White },
                { MagickColor.FromRgb(219, 125, 62), Color.Orange },
                { MagickColor.FromRgb(179, 80, 188), Color.Magenta },
                { MagickColor.FromRgb(107, 138, 201), Color.LightBlue },
                { MagickColor.FromRgb(177, 166, 39), Color.Yellow },
                { MagickColor.FromRgb(65, 174, 56), Color.Lime },
                { MagickColor.FromRgb(208, 132, 153), Color.Pink },
                { MagickColor.FromRgb(64, 64, 64), Color.Gray },
                { MagickColor.FromRgb(154, 161, 161), Color.LightGray },
                { MagickColor.FromRgb(46, 110, 137), Color.Cyan },
                { MagickColor.FromRgb(126, 61, 181), Color.Purple },
                { MagickColor.FromRgb(46, 56, 141), Color.Blue },
                { MagickColor.FromRgb(79, 50, 31), Color.Brown },
                { MagickColor.FromRgb(53, 70, 27), Color.Green },
                { MagickColor.FromRgb(150, 52, 48), Color.Red },
                { MagickColor.FromRgb(25, 22, 22), Color.Black }
            };
        }

        public void DrawImage(string imagePath, Vector3 targetPosition, int maxSize = 100)
        {
            MagickImage image = new MagickImage(imagePath);

            // resize image
            if (image.Width > maxSize || image.Height > maxSize)
            {
                if (image.Width > image.Height)
                {
                    image.Resize(maxSize, (int)Math.Floor((double)(image.Height * maxSize) / image.Width));
                }
                else
                {
                    image.Resize((int)Math.Floor((double)(image.Width * maxSize) / image.Height), maxSize);
                }
            }

            // get all pixels
            var pixelCollection = image.GetPixels();

            // iterate over pixels and draw a block
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var pixel = pixelCollection.GetPixel(x, image.Height - y - 1);
                    var color = GetClosestMinecraftColor(pixel.ToColor());

                    var brick = new Wool(color);

                    _world.SetBlock(brick, targetPosition + new Vector3(x, y, 0));
                }
            }
        }

        /// <summary>
        /// Algorithm taken on http://www.codeproject.com/Articles/17044/Find-the-Nearest-Color-with-C-Using-the-Euclidean
        /// </summary>
        private Color GetClosestMinecraftColor(MagickColor pixelColor)
        {
            // set a default color
            Color nearestColor = Color.Black;

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
