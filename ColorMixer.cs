using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ToolBox
{
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
            if (Math.Abs((int)(c1.R - c2.R)) > delta ||
                Math.Abs((int)(c1.G - c2.G)) > delta ||
                Math.Abs((int)(c1.B - c2.B)) > delta)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Wandelt die Farbe in einen Grauwert um
        /// </summary>
        /// <param name="col">Eine Farbe</param>
        /// <returns>Grauwert</returns>
        public static Color ConvertToGray(this Color col)
        {
            int gray = (int)Math.Round((double)col.R * 0.299 + (double)col.G * 0.587 + (double)col.B * 0.114);
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
            if (name.IndexOf("RGB") >= 0)
            {
                int b = name.IndexOf('(') + 1;
                int e = name.IndexOf(')');
                //string s = name.Substring(b, e - b);
                string[] rgb = name.Substring(b, e - b).Split(',');
                return Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
            }
            else
                return Color.FromName(name);
        }

    }
}
