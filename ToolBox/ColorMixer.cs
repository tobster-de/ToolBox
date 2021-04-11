using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ToolBox
{

    public enum ImageChannel
    {
        Red,
        Green,
        Blue
    }

    public static class ColorMixer
    {
        /// <summary>
        /// Bestimmt, ob eine Farbe ähnlich einer anderen ist
        /// </summary>
        /// <param name="c1">Eine Farbe</param>
        /// <param name="c2">Eine Farbe</param>
        /// <param name="delta">Maximum Unterschied pro Kanal</param>
        /// <returns></returns>
        public static bool Alike(this Color c1, Color c2, int delta)
        {
            return Math.Abs(c1.R - c2.R) <= delta && Math.Abs(c1.G - c2.G) <= delta && Math.Abs(c1.B - c2.B) <= delta;
        }

        /// <summary>
        /// Bestimmt, ob eine Farbe ähnlich einer anderen ist
        /// </summary>
        /// <param name="c1">Eine Farbe</param>
        /// <param name="c2">Eine Farbe</param>
        /// <returns>delta value</returns>
        public static int Alike(this Color c1, Color c2)
        {
            return Math.Abs(c1.R - c2.R) + Math.Abs(c1.G - c2.G) + Math.Abs(c1.B - c2.B);
        }

        /// <summary>
        /// Wandelt die Farbe in einen Grauwert um
        /// </summary>
        /// <param name="col">Eine Farbe</param>
        /// <returns>Grauwert</returns>
        public static Color ConvertToGray(this Color col)
        {
            int gray = (int)Math.Round(col.R * 0.299 + col.G * 0.587 + col.B * 0.114);
            return Color.FromArgb(gray, gray, gray);
        }

        /// <summary>
        /// Mischt zwei Farben zu einem bestimmten Prozentsatz miteinander
        /// </summary>
        /// <param name="col1">Farbe 1</param>
        /// <param name="col2">Farbe 2</param>
        /// <param name="percentage">Prozentueler Anteil der Farbe 2</param>
        /// <returns>Die Mischfarbe</returns>
        public static Color Mix(this Color col1, Color col2, int percentage)
        {
            int r = col1.R - (col1.R - col2.R) * percentage / 100;
            int g = col1.G - (col1.G - col2.G) * percentage / 100;
            int b = col1.B - (col1.B - col2.B) * percentage / 100;
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Gibt den Namen einer Farbe zurück bzw. "RGB(rrr, ggg, bbb)", wenn
        /// die Farbe nicht benannt ist.
        /// </summary>
        /// <param name="col">Die Farbe</param>
        /// <returns>Der Farbenname</returns>
        public static string ColorName(this Color col)
        {
            if (col.IsNamedColor)
                return col.Name;
            return String.Format("RGB({0}, {1}, {2})", col.R, col.G, col.B);
        }

        /// <summary>
        /// Liefert die zugeordnete Farbe eines Farbennamens. Konvertiert auch
        /// die Zeichenketten "RGB(rrr, ggg, bbb)" wieder in eine Farbe
        /// </summary>
        /// <param name="name">Der Farbenname bzw. "RGB(rrr, ggg, bbb)"</param>
        /// <returns>Die Farbe</returns>
        public static Color FromColorName(string name)
        {
            if (name.IndexOf("RGB") < 0)
            {
                return Color.FromName(name);
            }
            int b = name.IndexOf('(') + 1;
            int e = name.IndexOf(')');
            //string s = name.Substring(b, e - b);
            string[] rgb = name.Substring(b, e - b).Split(',');
            return Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
        }

        /// <summary>
        /// Seperates the specified channel of the image
        /// </summary>
        /// <param name="image">image to retrieve the channel from</param>
        /// <param name="channel">the channel to seperate</param>
        /// <returns>channel of an image</returns>
        public static Image GetImageChannel(this Image image, ImageChannel channel)
        {
            Bitmap bmp = new Bitmap(image);
            Bitmap img = new Bitmap(image.Width, image.Height);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color color = bmp.GetPixel(i, j);
                    int value = 0;
                    switch (channel)
                    {
                        case ImageChannel.Red:
                            color = Color.FromArgb(color.R, 0, 0);
                            value = color.R;// - color.G - color.B;
                            break;
                        case ImageChannel.Green:
                            color = Color.FromArgb(0, color.G, 0);
                            value = color.G;// - color.R - color.B;
                            break;
                        case ImageChannel.Blue:
                            color = Color.FromArgb(0, 0, color.B);
                            value = color.B; // - color.R - color.G;
                            break;
                    }
                    if (value < 0) value = 0;
                    img.SetPixel(i, j, /*Color.FromArgb(value, value, value)*/color);
                }
            }
            return img;
        }

        /// <summary>
        /// Seperates the specified channel of the image
        /// </summary>
        /// <param name="image">image to retrieve the channel from</param>
        /// <param name="compare">color to compare</param>
        /// <returns>channel of an image</returns>
        public static Image GetDiffImage(this Image image, Color compare)
        {
            Bitmap bmp = new Bitmap(image);
            Bitmap img = new Bitmap(image.Width, image.Height);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color color = bmp.GetPixel(i, j);
                    int value = 255 - (int)(color.Alike(compare) / 3.0);

                    if (value < 0) value = 0;
                    img.SetPixel(i, j, Color.FromArgb(value, value, value));
                }
            }
            return img;
        }



        /// <summary>
        /// Seperates the specified channel of the image
        /// </summary>
        /// <param name="image">image to retrieve the channel from</param>
        /// <param name="compare">color to compare</param>
        /// <returns>channel of an image</returns>
        public static Image FilterImage(this Image image, Predicate<Color> predicate)
        {
            Bitmap bmp = new Bitmap(image);
            Bitmap img = new Bitmap(image.Width, image.Height);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color color = bmp.GetPixel(i, j);
                    if (!predicate(color))
                    {
                        color = Color.Black;
                    }
                    img.SetPixel(i, j, color);
                }
            }
            return img;
        }
    }
}
