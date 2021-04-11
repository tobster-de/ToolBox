using System;

namespace ToolBox.Algebra
{
    /// <summary>
    /// Bruch
    /// </summary>
    public struct Fraction : IEquatable<Fraction>, ICloneable
    {
        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// Zähler
        /// </summary>
        public long Numerator { get; private set; }

        /// <summary>
        /// Nenner
        /// </summary>
        public long Denominator { get; private set; }

        /// <summary>
        /// Value of the fraction as double
        /// </summary>
        public double Value => (double)this.Numerator / (double)this.Denominator;

        #endregion

        #region Construction

        public Fraction(string strValue)
        {
            Fraction temp = Parse(strValue);
            this.Numerator = temp.Numerator;
            this.Denominator = temp.Denominator;
        }

        public Fraction(long numerator, long denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(denominator), "Denominator must not be zero!");
            }
            this.Numerator = numerator;
            this.Denominator = denominator;
            this.Reduce();
        }

        public Fraction(long value)
        {
            this.Numerator = value;
            this.Denominator = 1;
        }

        public Fraction(double value)
        {
            Fraction dummy = MakeRational(value);
            this.Numerator = dummy.Numerator;
            this.Denominator = dummy.Denominator;
        }

        public Fraction(Fraction original)
        {
            this.Numerator = original.Numerator;
            this.Denominator = original.Denominator;
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Bestimmt den größten gemeinsamen Teiler
        /// </summary>
        /// <param name="value1">Wert 1</param>
        /// <param name="value2">Wert 1</param>
        /// <returns></returns>
        private long GreatestCommonDivider(long value1, long value2)
        {
            long r;
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
        private void Reduce()
        {
            long factor = this.GreatestCommonDivider(this.Numerator, this.Denominator);
            if (factor != 0)
            {
                this.Numerator /= factor;
                this.Denominator /= factor;
            }
            if (this.Denominator < 0)
            {
                this.Numerator *= -1;
                this.Denominator *= -1;
            }
            else if (this.Denominator == 0)
            {
                this.Numerator = 0;
                this.Denominator = 1;
            }
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Gibt Absolutwert wieder
        /// </summary>
        /// <param name="fraction">Bruch</param>
        /// <returns>Absolutwert</returns>
        public static Fraction Abs(Fraction fraction)
        {
            Fraction result = new Fraction(fraction);
            result.Reduce();
            if (result.Numerator < 0)
            {
                result.Numerator *= -1;
            }
            return result;
        }

        /// <summary>
        /// Kehrwert
        /// </summary>
        public Fraction Inverse()
        {
            return new Fraction(this.Denominator, this.Numerator);
        }

        public static Fraction MakeRational(double x, double eps)
        {
            bool negative = x < 0;
            if (negative)
            {
                x = -x;
            }
            if (x < eps)
            {
                return new Fraction(0);
            }
            if (x > 1)
            {
                Fraction result1 = MakeRational(x - Math.Floor(x), eps) + (long)Math.Floor(x);
                if (negative)
                {
                    result1 = -result1;
                }
                return result1;
            }

            double invR = 1 / x;
            long inv = (long)Math.Floor(invR);
            double rest = invR - inv;
            double n_eps = inv * eps;
            if (n_eps > 1 || rest <= (n_eps * inv) / (1 - n_eps)
                || rest >= (1 - n_eps * (inv + 1)) / (1 + eps * (inv + 1)))
            {
                Fraction result2 = new Fraction(1, (2 * rest > 1 ? inv + 1 : inv));
                if (negative)
                {
                    result2 = -result2;
                }
                return result2;
            }

            Fraction result3 = (new Fraction(1) / (MakeRational(rest, 2 * eps * invR) + inv));
            if (negative)
            {
                result3 = -result3;
            }
            return result3;
        }

        public static Fraction MakeRational(double x)
        {
            return MakeRational(x, 1e-13);
        }

        /// <summary>
        /// The function takes an string as an argument and returns its corresponding reduced fraction
        /// the string can be an in the form of and integer, double or fraction.
        /// e.g it can be like "123" or "123.321" or "123/456"
        /// </summary>
        public static Fraction Parse(string strValue)
        {
            if (TryParse(strValue, out Fraction fraction))
            {
                return fraction;
            }

            throw new ArgumentException("Cannot convert the provided string value.", nameof(strValue));
        }

        public static bool TryParse(string strValue, out Fraction fraction)
        {
            if (string.IsNullOrWhiteSpace(strValue))
            {
                fraction = new Fraction(0, 1);
                return false;
            }

            int i = strValue.IndexOf('/');
            if (i == -1)
            {
                // if string is not in the form of a fraction
                // then it is double or integer

                fraction = MakeRational(Convert.ToDouble(strValue));
                return true;
            }

            // string is in the form of Numerator/Denominator
            long numerator = Convert.ToInt64(strValue.Substring(0, i));
            long denominator = Convert.ToInt64(strValue.Substring(i + 1));
            fraction = new Fraction(numerator, denominator);

            return true;
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
            => new Fraction(a.Numerator * b.Denominator, 
                            a.Denominator * b.Numerator);

        public static Fraction operator /(Fraction fraction, long value)
        {
            if (value == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Division by Zero not defined!");
            }
            
            return new Fraction(fraction.Numerator, fraction.Denominator * value);
        }

        public static Fraction operator /(long value, Fraction fraction) 
            => value * fraction.Inverse();

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

        public static Fraction operator *(Fraction fraction, long value)
        {
            return new Fraction(fraction.Numerator * value, fraction.Denominator);
        }

        public static Fraction operator *(long value, Fraction fraction)
        {
            return new Fraction(fraction.Numerator * value, fraction.Denominator);
        }

        /// <summary>
        /// Add fractions
        /// </summary>
        public static Fraction operator +(Fraction a, Fraction b)
            => new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator,
                            a.Denominator * b.Denominator);

        /// <summary>
        /// Add a whole number to a fraction
        /// </summary>
        public static Fraction operator +(Fraction fraction, long value)
            => new Fraction(fraction.Numerator + value * fraction.Denominator,
                            fraction.Denominator);

        /// <summary>
        /// Add a whole number to a fraction
        /// </summary>
        public static Fraction operator +(long value, Fraction fraction)
            => new Fraction(fraction.Numerator + value * fraction.Denominator,
                            fraction.Denominator);

        /// <summary>
        /// Subtrahieren
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Fraction operator -(Fraction a, Fraction b)
            => new Fraction(a.Numerator * b.Denominator - b.Numerator * a.Denominator,
                            a.Denominator * b.Denominator);

        public static Fraction operator -(Fraction fraction, long value)
            => new Fraction(fraction.Numerator - value * fraction.Denominator,
                            fraction.Denominator);

        public static Fraction operator -(long value, Fraction fraction)
            => new Fraction(value) - fraction;

        /// <summary>
        /// Negierung
        /// </summary>
        /// <param name="fraction"></param>
        /// <returns></returns>
        public static Fraction operator -(Fraction fraction)
            => new Fraction(-fraction.Numerator, fraction.Denominator);

        public static bool operator ==(Fraction frac1, Fraction frac2)
            => Equals(frac1, frac2);

        public static bool operator !=(Fraction frac1, Fraction frac2)
            => (!Equals(frac1, frac2));
        public static bool operator <(Fraction frac1, Fraction frac2)
            => frac1.Numerator * frac2.Denominator < frac2.Numerator * frac1.Denominator;

        public static bool operator >(Fraction frac1, Fraction frac2)
            => frac1.Numerator * frac2.Denominator > frac2.Numerator * frac1.Denominator;

        public static bool operator <=(Fraction frac1, Fraction frac2)
            => frac1.Numerator * frac2.Denominator <= frac2.Numerator * frac1.Denominator;
        public static bool operator >=(Fraction frac1, Fraction frac2)
            => frac1.Numerator * frac2.Denominator >= frac2.Numerator * frac1.Denominator;

        /// <summary>
        /// Convert from fraction to double
        /// </summary>
        public static explicit operator double(Fraction fraction)
        {
            return fraction.Value;
        }

        /// <summary>
        /// Convert from fraction to string
        /// </summary>
        public static explicit operator string(Fraction fraction)
        {
            return fraction.ToString();
        }

        /// <summary>
        /// Convert from numeric data type long to fraction
        /// </summary>
        public static implicit operator Fraction(long value)
        {
            return new Fraction(value);
        }

        /// <summary>
        /// Convert from numeric data type long to fraction
        /// </summary>
        public static implicit operator Fraction(double value)
        {
            return new Fraction(value);
        }

        public static explicit operator Fraction(string value)
        {
            return new Fraction(value);
        }

        #endregion

        #region Overrides

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Numerator.GetHashCode() * 397) ^ this.Denominator.GetHashCode();
            }
        }

        public object Clone()
        {
            return new Fraction(this);
        }

        /// <summary>
        /// Konvertierung in Zeichenkette
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.Denominator > 1 || this.Denominator < -1)
            {
                return $"{this.Numerator}/{this.Denominator}";
            }

            return this.Numerator.ToString();
        }

        #endregion

        #region Equality

        public bool Equals(Fraction other)
        {
            return this.Numerator == other.Numerator && this.Denominator == other.Denominator;
        }

        public override bool Equals(object obj)
        {
            return obj is Fraction other && this.Equals(other);
        }

        #endregion

    }

}
