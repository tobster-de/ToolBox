using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Globalization;

namespace ToolBox
{
    /// <summary>
    /// Mathematischer Vektor
    /// </summary>
    [Serializable]
    public class Vector
    {
        /// <summary>
        /// Komponenten
        /// </summary>
        private double[] m_cmps;

        /// <summary>
        /// Anzahl der Dimensionen dieses Vektors
        /// </summary>
        public uint Dimensions
        {
            get
            {
                return (uint)m_cmps.Length;
            }
        }

        /// <summary>
        /// Zugriff auf Komponenten des Vektors
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[uint index]
        {
            get
            {
                if (index < Dimensions)
                {
                    return m_cmps[index];
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index < Dimensions)
                {
                    m_cmps[index] = value;
                    return;
                }
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Länge des Vektors
        /// </summary>
        public double Length
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < Dimensions; i++)
                {
                    sum += m_cmps[i] * m_cmps[i];
                }
                return Math.Sqrt(sum);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlElement(ElementName = "Components")]
        public double[] Components
        {
            get
            {
                return m_cmps;
            }
            set
            {
                /*if (value.Count > m_cmps.Length) {
                    //double[] copy = double[m_cmps.Length];
                    //for (int i= 0; i < m_cmps.Length; i++) 
                    //    copy[i] = m_cmps[i];
                    m_cmps = new double[value.Count];
                }
                for (int i = 0; i < value.Count; i++) m_cmps[i] = value[i];*/
                m_cmps = value;
            }
        }

        #region Construction

        /// <summary>
        /// Deserialisation
        /// </summary>
        public Vector() {
            m_cmps = new double[2];
            m_cmps[0] = 0;
            m_cmps[1] = 0;
        }

        /// <summary>
        /// Vektor für dreidimensionalen Vektor
        /// </summary>
        /// <param name="c1">1. Komponente</param>
        /// <param name="c2">2. Komponente</param>
        /// <param name="c3">3. Komponente</param>
        public Vector(double c1, double c2, double c3)
        {
            m_cmps = new double[3];
            m_cmps[0] = c1;
            m_cmps[1] = c2;
            m_cmps[2] = c3;
        }

        /// <summary>
        /// Vektor n-dimensional
        /// </summary>
        /// <param name="dimensions"></param>
        public Vector(uint dimensions)
        {
            m_cmps = new double[dimensions];
        }

        /// <summary>
        /// Konstruktor für zweidimensionalen Vektor
        /// </summary>
        /// <param name="c1">1. Komponente</param>
        /// <param name="c2">2. Komponente</param>
        public Vector(double c1, double c2)
        {
            m_cmps = new double[2];
            m_cmps[0] = c1;
            m_cmps[1] = c2;
        }

        /// <summary>
        /// Konstruktor für mehrdimensionalen Vektor
        /// </summary>
        /// <param name="components">Komponenten</param>
        public Vector(params double[] components)
        {
            m_cmps = components;
        }

        /// <summary>
        /// Kopierkonstruktor
        /// </summary>
        /// <param name="other">Original</param>
        internal Vector(Vector other)
        {
            m_cmps = new double[other.Dimensions];
            for (int i = 0; i < other.Dimensions; i++)
            {
                m_cmps[i] = other.m_cmps[i];
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector operator +(Vector v1, Vector v2)
        {
            if (v1.Dimensions != v2.Dimensions)
            {
                throw new ArgumentException("Dimensions of Vectors do not match.");
            }
            Vector vector = new Vector(v1);
            for (int i = 0; i < vector.Dimensions; i++)
            {
                vector.m_cmps[i] += v2.m_cmps[i];
            }
            return vector;
        }

        /// <summary>
        /// Subtraktion
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector operator -(Vector v1, Vector v2)
        {
            if (v1.Dimensions != v2.Dimensions)
            {
                throw new ArgumentException("Dimensions of Vectors do not match.");
            }
            Vector vector = new Vector(v1);
            for (int i = 0; i < vector.Dimensions; i++)
            {
                vector.m_cmps[i] -= v2.m_cmps[i];
            }
            return vector;
        }

        /// <summary>
        /// Skalarmultiplikation
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double operator *(Vector v1, Vector v2)
        {
            if (v1.Dimensions != v2.Dimensions)
            {
                throw new ArgumentException("Dimensions of Vectors do not match.");
            }
            double result = 0;
            for (int i = 0; i < v1.Dimensions; i++)
            {
                result += v1.m_cmps[i] * v2.m_cmps[i];
            }
            return result;
        }

        /// <summary>
        /// Multiplikation mit Skalar
        /// </summary>
        /// <param name="skalar"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector operator *(double skalar, Vector v)
        {
            Vector result = new Vector(v);
            for (int i = 0; i < v.Dimensions; i++)
            {
                result.m_cmps[i] *= skalar;
            }
            return result;
        }

        /// <summary>
        /// Multiplikation mit Skalar
        /// </summary>
        /// <param name="v"></param>
        /// <param name="skalar"></param>
        /// <returns></returns>
        public static Vector operator *(Vector v, double skalar)
        {
            return skalar * v;
        }

        /// <summary>
        /// Division durch Skalar
        /// </summary>
        /// <param name="v"></param>
        /// <param name="skalar"></param>
        /// <returns></returns>
        public static Vector operator /(Vector v, double skalar)
        {
            Vector result = new Vector(v);
            for (int i = 0; i < v.Dimensions; i++)
            {
                result.m_cmps[i] /= skalar;
            }
            return result;
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !v1.Equals(v2);
        }

        #endregion

        #region Overrides

        public override bool Equals(object other)
        {
            if (other is Vector == false)
            {
                return false;
            }
            Vector otherVector = (Vector)other;
            bool equal = Dimensions == otherVector.Dimensions;
            if (!equal)
            {
                return false;
            }
            for (int i = 0; i < otherVector.Dimensions; i++)
            {
                equal &= m_cmps[i] == otherVector.m_cmps[i];
            }
            return equal;
        }

        public override string ToString()
        {
            //string str = "";
            string str = "Vector: (";
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.CurrencyDecimalSeparator = ".";
            for (int i = 0; i < Dimensions; i++)
            {
                str += String.Format(nfi,"{0:0.##}",m_cmps[i]);
                if (i < Dimensions - 1)
                {
                    str += ",";
                }
            }
            str += ")";
            return str;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region Statics

        /// <summary>
        /// Konvertiert einen Punkt in einen Vektor
        /// </summary>
        /// <param name="pointf">Gewünschter Punkt</param>
        /// <returns>Vector des Punktes</returns>
        public static Vector FromPointF(System.Drawing.PointF pointf)
        {
            return new Vector(pointf.X, pointf.Y);
        }

        /// <summary>
        /// Berechnet den Abstand eines Punktes zu einer Geraden
        /// </summary>
        /// <param name="linePV">Punktvektor der Linie</param>
        /// <param name="lineRV">Richtungsvektor der Linie</param>
        /// <param name="pointPV">Vektor des Punktes</param>
        /// <returns></returns>
        public static double DistanceLinePoint(Vector linePV, Vector lineRV, Vector pointPV)
        {
            return (pointPV - NearestPointOnLine(linePV, lineRV, pointPV)).Length;
        }

        /// <summary>
        /// Berechnet den den Punkt auf der Geraden der dem gegebenen am nächsten liegt
        /// </summary>
        /// <param name="linePV">Punktvektor der Linie</param>
        /// <param name="lineRV">Richtungsvektor der Linie</param>
        /// <param name="pointPV">Vektor des Punktes</param>
        /// <returns></returns>
        public static Vector NearestPointOnLine(Vector linePV, Vector lineRV, Vector pointPV)
        {
            //calculate the distance of the point to the line
            double lambda = (pointPV - linePV) * lineRV / (lineRV * lineRV);
            if (lambda > 1)
            {
                //distance line ending point to param point
                return (linePV + lineRV);
            }
            if (lambda < 0)
            {
                //distance line beginning point to param point
                return linePV;
            }
            return (linePV + lambda * lineRV);
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Konvertiert diesen Vektor in einen PointF
        /// </summary>
        /// <returns>PointF mit den Komponenten als Koordinaten</returns>
        public System.Drawing.PointF ToPointF()
        {
            if (m_cmps.Length > 2)
            {
                throw new NotSupportedException("Conversion to PointF only supported for Vector with two components.");
            }
            return new System.Drawing.PointF((float)m_cmps[0], (float)m_cmps[1]);
        }

        /// <summary>
        /// Abstand zu anderem Vector bestimmen
        /// </summary>
        /// <param name="other">Anderer Vektor</param>
        /// <returns>Abstand zu anderm Vektor</returns>
        public double Distance(Vector other)
        {
            if (this.Dimensions != other.Dimensions)
            {
                throw new ArgumentException("Dimensions of Vectors do not match.");
            }
            return (this - other).Length;
        }

        #endregion
    }
}
