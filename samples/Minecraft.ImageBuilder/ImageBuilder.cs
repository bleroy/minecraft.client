using Decent.Minecraft.Client;
using Decent.Minecraft.Client.Blocks;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Minecraft.ImageBuilder
{
    public class ImageBuilder
    {
        private Dictionary<MagickColor, Clay.Color> colorPalette;
        private IWorld world;

        public ImageBuilder(IWorld world)
        {
            this.world = world;

            this.colorPalette = GetMinecraftColors();
        }

        public void DrawImage(string imagePath, int maxSize = 100)
        {
            var targetPosition = world.Player.GetPosition() + new Vector3(-30, 0, -30);

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

                    world.SetBlock(brick, targetPosition + new Vector3(x, y, 1));
                }
            }
        }

        /// <summary>
        /// Algorithm taken on http://www.codeproject.com/Articles/17044/Find-the-Nearest-Color-with-C-Using-the-Euclidean
        /// </summary>
        private Clay.Color GetClosestMinecraftColor(MagickColor pixelColor)
        {
            // set a default color
            Clay.Color nearestColor = Clay.Color.Black;

            double minimumDistance = 500;

            double pixelRed = Convert.ToDouble(pixelColor.R);
            double pixelGreen = Convert.ToDouble(pixelColor.G);
            double pixelBlue = Convert.ToDouble(pixelColor.B);

            foreach (var color in colorPalette)
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

        /// <summary>
        /// Maps colors of the Minecraft wools to the Color enumeration
        /// see http://minecraft.gamepedia.com/Data_values#Wool.2C_Stained_Clay.2C_Stained_Glass_and_Carpet
        /// </summary>
        private Dictionary<MagickColor, Clay.Color> GetMinecraftColors()
        {
            var colorPalette = new Dictionary<MagickColor, Clay.Color>();
            colorPalette.Add(MagickColor.FromRgb(221, 221, 221), Clay.Color.White);
            colorPalette.Add(MagickColor.FromRgb(219, 125, 62), Clay.Color.Orange);
            colorPalette.Add(MagickColor.FromRgb(179, 80, 188), Clay.Color.Magenta);
            colorPalette.Add(MagickColor.FromRgb(107, 138, 201), Clay.Color.LightBlue);
            colorPalette.Add(MagickColor.FromRgb(177, 166, 39), Clay.Color.Yellow);
            colorPalette.Add(MagickColor.FromRgb(65, 174, 56), Clay.Color.Lime);
            colorPalette.Add(MagickColor.FromRgb(208, 132, 153), Clay.Color.Pink);
            colorPalette.Add(MagickColor.FromRgb(64, 64, 64), Clay.Color.Gray);
            colorPalette.Add(MagickColor.FromRgb(154, 161, 161), Clay.Color.LightGray);
            colorPalette.Add(MagickColor.FromRgb(46, 110, 137), Clay.Color.Cyan);
            colorPalette.Add(MagickColor.FromRgb(126, 61, 181), Clay.Color.Purple);
            colorPalette.Add(MagickColor.FromRgb(46, 56, 141), Clay.Color.Blue);
            colorPalette.Add(MagickColor.FromRgb(79, 50, 31), Clay.Color.Brown);
            colorPalette.Add(MagickColor.FromRgb(53, 70, 27), Clay.Color.Green);
            colorPalette.Add(MagickColor.FromRgb(150, 52, 48), Clay.Color.Red);
            colorPalette.Add(MagickColor.FromRgb(25, 22, 22), Clay.Color.Black);

            return colorPalette;
        }
    }
}
