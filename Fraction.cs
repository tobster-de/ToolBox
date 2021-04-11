using System;
using System.Collections.Generic;
using System.Text;

namespace ToolBox
{
    /// <summary>
    /// Bruch
    /// </summary>
    public class Fraction
    {
        #region Fields

        private int m_Numerator;                // Zähler
        private int m_Denominator;              // Nenner

        #endregion

        #region Properties

        /// <summary>
        /// Zähler
        /// </summary>
        public int Numerator
        {
            get
            {
                return m_Numerator;
            }
            //set
            //{
            //    m_Numerator = value;
            //    CancelDown();
            //}
        }

        /// <summary>
        /// Nenner
        /// </summary>
        public int Denominator
        {
            get
            {
                return m_Denominator;
            }
            //set
            //{
            //    if (value == 0)
            //    {
            //        throw new ArgumentOutOfRangeException("denominator", "Denominator must not be zero!");
            //    }
            //    m_Denominator = value;
            //    CancelDown();
            //}
        }

        /// <summary>
        /// Gebrochener Wert des Bruchs
        /// </summary>
        public double Value
        {
            get
            {
                return (double)m_Numerator / (double)m_Denominator;
            }
        }


        #endregion

        #region Construction

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentOutOfRangeException("denominator", "Denominator must not be zero!");
            }
            m_Numerator = numerator;
            m_Denominator = denominator;
            CancelDown();
        }

        public Fraction(int value)
        {
            m_Numerator = value;
            m_Denominator = 1;
        }

        public Fraction(double value)
        {
            Fraction dummy = MakeRational(value);
            m_Numerator = dummy.Numerator;
            m_Denominator = dummy.Denominator;
        }

        public Fraction(Fraction original)
        {
            m_Numerator = original.m_Numerator;
            m_Denominator = original.m_Denominator;
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Bestimmt den größten gemeinsamen Teiler
        /// </summary>
        /// <param name="value1">Wert 1</param>
        /// <param name="value2">Wert 1</param>
        /// <returns></returns>
        private int FindHighestCommonFactor(int value1, int value2)
        {
            int r;
            // Euklidscher Algorithmus
            value1 = Math.Abs(value1);
            value2 = Math.Abs(value2);
            if (value1 < value2)
            {
                r = value1;
                value1 = value2;
                value2 = r;
            }
            while (value2 > 0)
            {
                r = value1 % value2;
                value1 = value2;
                value2 = r;
            }
            return value1;
        }

        /// <summary>
        /// Kürzt den Bruch
        /// </summary>
        private void CancelDown()
        {
            int factor = FindHighestCommonFactor(m_Numerator, m_Denominator);
            if (factor != 0)
            {
                m_Numerator = m_Numerator / factor;
                m_Denominator = m_Denominator / factor;
            }
            if (m_Denominator < 0)
            {
                m_Numerator = m_Numerator * -1;
                m_Denominator = m_Denominator * -1;
            }
            else if (m_Denominator == 0)
            {
                m_Numerator = 0;
                m_Denominator = 1;
            }
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Gibt Absolutwert wieder
        /// </summary>
        /// <param name="frac">Bruch</param>
        /// <returns>Absolutwert</returns>
        public static Fraction Abs(Fraction frac)
        {
            Fraction result = new Fraction(frac);
            result.CancelDown();
            if (result.Numerator < 0)
            {
                result.m_Numerator *= -1;
            }
            return result;
        }

        /// <summary>
        /// Kehrwert
        /// </summary>
        /// <returns></returns>
        public Fraction Reciprocal()
        {
            return new Fraction(Denominator, Numerator);
        }

        public static Fraction MakeRational(double x, double eps)
        {
            bool negative = false;
            Fraction result = null;
            if (x < 0)
            {
                negative = true;
                x = -x;
            }
            if (x < eps)
            {
                return new Fraction(0);
            }
            if (x > 1)
            {
                result = MakeRational(x - Math.Floor(x), eps) + (int)Math.Floor(x);
                if (negative)
                {
                    result = -result;
                }
                return result;
            }
            double invR = 1 / x;
            int inv = (int)Math.Floor(invR);
            double rest = invR - inv;
            double n_eps = inv * eps;
            if (n_eps > 1 || rest <= (n_eps * inv) / (1 - n_eps)
                || rest >= (1 - n_eps * (inv + 1)) / (1 + eps * (inv + 1)))
            {
                result = new Fraction(1, (2 * rest > 1 ? inv + 1 : inv));
                if (negative)
                {
                    result = -result;
                }
                return result;
            }
            result = (new Fraction(1) / (MakeRational(rest, 2 * eps * invR) + inv));
            if (negative)
            {
                result = -result;
            }
            return result;
        }

        public static Fraction MakeRational(double x)
        {
            return MakeRational(x, 1e-13);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Dividieren
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Fraction operator /(Fraction a, Fraction b)
        {
            return new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }

        public static Fraction operator /(Fraction frac, int value)
        {
            if (value == 0)
            {
                throw new ArgumentOutOfRangeException("value", "Division by Zero not defined!");
            }
            return new Fraction(frac.Numerator, frac.Denominator * value);
        }

        public static Fraction operator /(int value, Fraction frac)
        {
            return value * frac.Reciprocal();
        }

        /// <summary>
        /// Multiplizieren
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        }

        public static Fraction operator *(Fraction frac, int value)
        {
            return new Fraction(frac.Numerator * value, frac.Denominator);
        }

        public static Fraction operator *(int value, Fraction frac)
        {
            return new Fraction(frac.Numerator * value, frac.Denominator);
        }

        /// <summary>
        /// Addieren
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Fraction operator +(Fraction a, Fraction b)
        {
            return new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);
        }

        public static Fraction operator +(Fraction frac, int value)
        {
            return new Fraction(frac.Numerator + value * frac.Denominator, frac.Denominator);
        }

        public static Fraction operator +(int value, Fraction frac)
        {
            return new Fraction(frac.Numerator + value * frac.Denominator, frac.Denominator);
        }

        /// <summary>
        /// Subtrahieren
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Fraction operator -(Fraction a, Fraction b)
        {
            return new Fraction(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator);
        }

        public static Fraction operator -(Fraction frac, int value)
        {
            return new Fraction(frac.Numerator - value * frac.Denominator, frac.Denominator);
        }

        public static Fraction operator -(int value, Fraction frac)
        {
            return new Fraction(value) - frac;
        }

        /// <summary>
        /// Negierung
        /// </summary>
        /// <param name="frac"></param>
        /// <returns></returns>
        public static Fraction operator -(Fraction frac)
        {
            return new Fraction(-frac.Numerator, frac.Denominator);
        }

        /// <summary>
        /// Implizite Konvertierung in Gleitkommawert
        /// </summary>
        /// <param name="frac"></param>
        /// <returns></returns>
        public static implicit operator double(Fraction frac)
        {
            return frac.Value;
        }

        #endregion

        #region Overrides

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Vergleich
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Fraction)
            {
                CancelDown();
                Fraction other = new Fraction(obj as Fraction);
                other.CancelDown();
                return (other.Numerator == m_Numerator && other.Denominator == m_Denominator);
            }
            if (obj is int)
            {
                CancelDown();
                int other = (int)obj;
                return (other == m_Numerator && 1 == m_Denominator);
            }
            return false;
        }

        /// <summary>
        /// Konvertierung in Zeichenkette
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (m_Denominator > 1 || m_Denominator < -1)
            {
                return String.Format("{0}/{1}", m_Numerator, m_Denominator);
            }
            else
            {
                return m_Numerator.ToString();
            }
        }

        #endregion
    }

}
