using System;
using System.Text.RegularExpressions;

namespace ToolBox.Algebra
{
    /// <summary>
    /// This type represents a fraction to perform precise computation.
    /// </summary>
    public struct Fraction : IEquatable<Fraction>, ICloneable
    {
        #region Exposed constants

        //public static readonly Fraction NaN = new Fraction(Indeterminates.NaN);
        //public static readonly Fraction PositiveInfinity = new Fraction(Indeterminates.PositiveInfinity);
        //public static readonly Fraction NegativeInfinity = new Fraction(Indeterminates.NegativeInfinity);

        public static readonly Fraction Zero = new Fraction(0);
        public static readonly Fraction Epsilon = new Fraction(1, Int64.MaxValue);
        public static readonly Fraction MaxValue = new Fraction(Int64.MaxValue);
        public static readonly Fraction MinValue = new Fraction(Int64.MinValue);

        #endregion

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

        /// <summary>
        /// Returns the reciprocal of the fraction.
        /// </summary>
        public Fraction Reciprocal => new Fraction(this.Denominator, this.Numerator);

        /// <summary>
        /// Returns the absolute value of the fraction.
        /// </summary>
        public Fraction Absolute
        {
            get
            {
                Fraction result = new Fraction(this);
                result.Reduce();
                if (result.Numerator < 0)
                {
                    result.Numerator *= -1;
                }

                return result;
            }
        }

        #endregion

        #region Construction

        public Fraction(string strValue)
        {
            this = Parse(strValue);
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
            this = MakeRational(value);
        }

        public Fraction(Fraction original)
        {
            this.Numerator = original.Numerator;
            this.Denominator = original.Denominator;
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// Determines the greates common divider of the two provided values using
        /// the euclidean algorithm.
        /// </summary>
        private long GreatestCommonDivider(long value1, long value2)
        {
            long r;
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
        /// Reduces the fraction
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
        /// Returns the square root of the fraction.
        /// </summary>
        public Fraction Sqrt()
        {
            return new Fraction(Math.Sqrt(this.Numerator) / Math.Sqrt(this.Denominator));
        }

        /// <summary>
        /// Converts a floating point number into a fraction regarding to a defined precision.
        /// </summary>
        public static Fraction MakeRational(double x, double eps = 1e-13)
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
                return negative ? -result1 : result1;
            }

            double invR = 1 / x;
            long inv = (long)Math.Floor(invR);
            double rest = invR - inv;
            double n_eps = inv * eps;
            if (n_eps > 1
                || rest <= (n_eps * inv) / (1 - n_eps)
                || rest >= (1 - n_eps * (inv + 1)) / (1 + eps * (inv + 1)))
            {
                Fraction result2 = new Fraction(1, (2 * rest > 1 ? inv + 1 : inv));
                return negative ? -result2 : result2;
            }

            Fraction result3 = (new Fraction(1) / (MakeRational(rest, 2 * eps * invR) + inv));
            return negative ? -result3 : result3;
        }

        /// <summary>
        /// Parses the provided string and returns its corresponding reduced fraction.
        /// The string can be an in the form of and integer, double or fraction.
        /// e.g. it can be like "123" or "123.321" or "123/456"
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Exception is thrown when the string cannot be converted successfully.
        /// </exception>
        public static Fraction Parse(string strValue)
        {
            if (TryParse(strValue, out Fraction fraction))
            {
                return fraction;
            }

            throw new ArgumentException("Cannot convert the provided string value.", nameof(strValue));
        }

        /// <summary>
        /// Parses the provided string and returns its corresponding reduced fraction.
        /// The string can be an in the form of and integer, double or fraction.
        /// e.g. it can be like "123" or "123.321" or "123/456"
        /// </summary>
        /// <returns>
        /// True when the conversion was successful, otherwise false.
        /// </returns>
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
        /// Division of fractions
        /// </summary>
        public static Fraction operator /(Fraction a, Fraction b)
            => new Fraction(a.Numerator * b.Denominator,
                            a.Denominator * b.Numerator);

        /// <summary>
        /// Division of fraction by a whole number
        /// </summary>
        public static Fraction operator /(Fraction fraction, long value)
        {
            if (value == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Division by Zero not defined!");
            }

            return new Fraction(fraction.Numerator, fraction.Denominator * value);
        }

        /// <summary>
        /// Division of a whole number by a fraction
        /// </summary>
        public static Fraction operator /(long value, Fraction fraction)
            => value * fraction.Reciprocal;

        /// <summary>
        /// Multiply fractions
        /// </summary>
        public static Fraction operator *(Fraction a, Fraction b)
            => new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

        /// <summary>
        /// Multiply fraction with whole number
        /// </summary>
        public static Fraction operator *(Fraction fraction, long value)
            => new Fraction(fraction.Numerator * value, fraction.Denominator);

        /// <summary>
        /// Multiply whole number with fraction
        /// </summary>
        public static Fraction operator *(long value, Fraction fraction)
            => new Fraction(fraction.Numerator * value, fraction.Denominator);

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
        /// Subtract fractions
        /// </summary>
        public static Fraction operator -(Fraction a, Fraction b)
            => new Fraction(a.Numerator * b.Denominator - b.Numerator * a.Denominator,
                            a.Denominator * b.Denominator);

        /// <summary>
        /// Subtract a whole number from a fraction
        /// </summary>
        public static Fraction operator -(Fraction fraction, long value)
            => new Fraction(fraction.Numerator - value * fraction.Denominator,
                            fraction.Denominator);

        /// <summary>
        /// Subtract a fraction from a whole number
        /// </summary>
        public static Fraction operator -(long value, Fraction fraction)
            => new Fraction(value) - fraction;

        /// <summary>
        /// Negate a fraction
        /// </summary>
        public static Fraction operator -(Fraction fraction)
            => new Fraction(-fraction.Numerator, fraction.Denominator);

        /// <summary>
        /// Compare fractions
        /// </summary>
        public static bool operator ==(Fraction frac1, Fraction frac2) => Equals(frac1, frac2);

        /// <summary>
        /// Compare fractions
        /// </summary>
        public static bool operator !=(Fraction frac1, Fraction frac2) => (!Equals(frac1, frac2));

        /// <summary>
        /// Compare fractions
        /// </summary>
        public static bool operator <(Fraction frac1, Fraction frac2)
            => frac1.Numerator * frac2.Denominator < frac2.Numerator * frac1.Denominator;

        /// <summary>
        /// Compare fractions
        /// </summary>
        public static bool operator >(Fraction frac1, Fraction frac2)
            => frac1.Numerator * frac2.Denominator > frac2.Numerator * frac1.Denominator;

        /// <summary>
        /// Compare fractions
        /// </summary>
        public static bool operator <=(Fraction frac1, Fraction frac2)
            => frac1.Numerator * frac2.Denominator <= frac2.Numerator * frac1.Denominator;

        /// <summary>
        /// Compare fractions
        /// </summary>
        public static bool operator >=(Fraction frac1, Fraction frac2)
            => frac1.Numerator * frac2.Denominator >= frac2.Numerator * frac1.Denominator;

        /// <summary>
        /// Convert from fraction to double
        /// </summary>
        public static explicit operator double(Fraction fraction) => fraction.Value;

        /// <summary>
        /// Convert from fraction to string
        /// </summary>
        public static explicit operator string(Fraction fraction) => fraction.ToString();

        /// <summary>
        /// Convert from numeric data type long to fraction
        /// </summary>
        public static implicit operator Fraction(long value) => new Fraction(value);

        /// <summary>
        /// Convert from numeric data type long to fraction
        /// </summary>
        public static implicit operator Fraction(double value) => new Fraction(value);

        /// <summary>
        /// Convert from string to fraction
        /// </summary>
        public static explicit operator Fraction(string value) => new Fraction(value);

        #endregion

        #region Overrides

        /// <inheritdoc />
        public object Clone()
        {
            return new Fraction(this);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (this.Denominator > 1 || this.Denominator < -1)
            {
                return $"{this.Numerator}/{this.Denominator}";
            }

            return this.Numerator.ToString();
        }

        /// <summary>
        /// Converts the fraction to a string regarding the provided format containing placeholders:
        /// <list>
        /// <item>
        ///     %W - Whole number if the whole part is not zero. If no other elements are used
        ///     this also prints a zero.
        /// </item>
        /// <item>
        ///     %N - The single numerator if there is no whole part in the format or
        ///     the numerator of the remainder of the mixed fraction.
        /// </item>
        /// <item>
        ///     %D - The denominator if it does not equal 1
        /// </item>
        /// <item>
        ///     %S - The conditional fraction slash if a denominator is applicable
        /// </item>
        /// </list>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(string format)
        {
            format = format.ToUpperInvariant();
            string result = format;

            bool whole = Regex.IsMatch(format, "%W");
            bool numden = Regex.IsMatch(format, "%N") && Regex.IsMatch(format, "%D");
            long wholeNum = this.Numerator / this.Denominator;
            long restNum = this.Numerator % this.Denominator;

            Regex regex = new Regex("%[WNDS]");
            foreach (Match match in regex.Matches(format))
            {
                string replacement;
                switch (match.Value)
                {
                    case "%N":
                        replacement = whole && wholeNum != 0
                                          ? (restNum != 0 ? Math.Abs(restNum).ToString() : string.Empty)
                                          : this.Numerator.ToString();
                        break;
                    case "%D":
                        replacement = this.Denominator != 1 ? this.Denominator.ToString() : string.Empty;
                        break;
                    case "%S":
                        replacement = this.Denominator != 1 ? "/" : string.Empty;
                        break;
                    case "%W":
                        replacement = wholeNum != 0 || !numden
                                          ? wholeNum.ToString()
                                          : string.Empty;
                        break;
                    default:
                        replacement = string.Empty;
                        break;
                }

                result = Regex.Replace(result, match.Value, replacement);
            }

            return result.Trim();
        }

        #endregion

        #region Equality

        /// <inheritdoc />
        public bool Equals(Fraction other)
        {
            return this.Numerator == other.Numerator && this.Denominator == other.Denominator;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Fraction other && this.Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Numerator.GetHashCode() * 397) ^ this.Denominator.GetHashCode();
            }
        }

        #endregion
    }

}
