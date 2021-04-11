using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ToolBox
{
    /// <summary>
    /// Konvertiert kartesische in Polarkoordinaten und zurück
    /// </summary>
    /// <example>
    /// Polar polar = Polar.FromPoint(new Point(5, 5));
    /// Console.WriteLn(polar.Angle);
    /// Console.WriteLn(polar.Radius);
    /// ---
    /// Polar polar = new Polar(Math.PI, 10);
    /// Point point = polar.ToPoint();
    /// Console.WriteLn(point);
    /// </example>
    public sealed class Polar
    {
        #region Fields

        private double mAngle;
        private double mRadius;

        #endregion

        #region Properties

        /// <summary>
        /// Winkel in Polarform
        /// </summary>
        public double Angle
        {
            get
            {
                return mAngle;
            }
            set
            {
                mAngle = value;
                while (mAngle < 0)
                    mAngle += 2 * Math.PI;
                while (mAngle > 2 * Math.PI)
                    mAngle -= 2 * Math.PI;
            }
        }

        /// <summary>
        /// Radius in Polarform
        /// </summary>
        public double Radius
        {
            get
            {
                return mRadius;
            }
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");
                mRadius = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Erstellt eine neue Koordinate in Polarform
        /// </summary>
        /// <param name="angle">Winkel</param>
        /// <param name="radius">Radius</param>
        public Polar(double angle, double radius)
        {
            this.Angle = angle;
            this.Radius = radius;
        }

        /// <summary>
        /// Kopierkonstruktor
        /// </summary>
        /// <param name="original"></param>
        public Polar(Polar original)
        {
            this.Angle = original.Angle;
            this.Radius = original.Radius;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Konvertiert eine kartesische Koordinate in die Polarform
        /// </summary>
        /// <param name="point">Koordinate mit X- und Y-Werten</param>
        /// <returns>Polarkoordinate</returns>
        public static Polar FromPoint(Point point)
        {
            if (point.X > 0)
                return new Polar(Math.Atan((double)point.Y / point.X), CalcRadius(point));
            if (point.X < 0)
                return new Polar(Math.Atan((double)point.Y / point.X) + Math.PI, CalcRadius(point));
            if (point.Y < 0)
                return new Polar(3 * Math.PI / 2, CalcRadius(point));
            if (point.Y > 0)
                return new Polar(Math.PI / 2, CalcRadius(point));
            return new Polar(0,0);
        }

        private static double CalcRadius(Point point)
        {
            return Math.Sqrt(point.Y * point.Y + point.X * point.X);
        }

        /// <summary>
        /// Konvertiert die aktuelle Polarkoordinate in die kartesische Form
        /// </summary>
        /// <returns>kartesische Koordinate</returns>
        public Point ToPoint()
        {
            return new Point(
                (int)Math.Round(mRadius * Math.Cos(mAngle)),
                (int)Math.Round(mRadius * Math.Sin(mAngle))
                );
        }

        /// <summary>
        /// Statisch: Konvertiert eine Polarkoordinate in die kartesische Form
        /// </summary>
        /// <param name="angle">Winkel</param>
        /// <param name="radius">Radius</param>
        /// <returns>kartesische Koordinate</returns>
        public static Point ToPoint(double angle, double radius)
        {
            return new Point(
                (int)Math.Round(radius * Math.Cos(angle)),
                (int)Math.Round(radius * Math.Sin(angle))
                );
        }

        #endregion

    }
}
