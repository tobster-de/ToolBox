using System;

namespace ToolBox.Algebra
{
    public struct Complex : IEquatable<Complex>, ICloneable
    {
        #region Properties

        /// <summary>
        /// Real value
        /// </summary>
        public Fraction Real { get; }

        /// <summary>
        /// Imaginary value
        /// </summary>
        public Fraction Imaginary { get; }

        /// <summary>
        /// Returns the absolute value of the complex number.
        /// </summary>
        public Fraction Absolute => (this.Real * this.Real + this.Imaginary * this.Imaginary).Sqrt();

        /// <summary>
        /// Returns the complex conjugate of this complex number.
        /// </summary>
        public Complex Conjugate => new Complex(this.Real, -this.Imaginary);

        #endregion

        #region Construction

        /// <summary>
        /// Constructor with integer components
        /// </summary>
        /// <param name="real">Real part</param>
        /// <param name="imag">Imaginary part</param>
        public Complex(int real, int imag)
        {
            this.Real = new Fraction(real);
            this.Imaginary = new Fraction(imag);
        }

        /// <summary>
        /// Constructor with floating point components
        /// </summary>
        /// <param name="real">Real part</param>
        /// <param name="imag">Imaginary part</param>
        public Complex(double real, double imag)
        {
            this.Real = new Fraction(real);
            this.Imaginary = new Fraction(imag);
        }

        /// <summary>
        /// Constructor with fraction components
        /// </summary>
        /// <param name="real">Real part</param>
        /// <param name="imag">Imaginary part</param>
        public Complex(Fraction real, Fraction imag)
        {
            this.Real = real != default ? real : throw new ArgumentNullException(nameof(real));
            this.Imaginary = imag != default ? imag : throw new ArgumentNullException(nameof(imag));
        }

        /// <summary>
        /// Constructor with integer real part
        /// </summary>
        /// <param name="real">Real part</param>
        public Complex(int real)
        {
            this.Real = new Fraction(real);
            this.Imaginary = new Fraction(0);
        }

        /// <summary>
        /// Constructor with floating point real part
        /// </summary>
        /// <param name="real">Real part</param>
        public Complex(double real)
        {
            this.Real = new Fraction(real);
            this.Imaginary = new Fraction(0);
        }

        /// <summary>
        /// Constructor with a fraction real part
        /// </summary>
        /// <param name="real">Real part</param>
        public Complex(Fraction real)
        {
            this.Real = real != default ? real : throw new ArgumentNullException(nameof(real));
            this.Imaginary = new Fraction(0);
        }

        /// <summary>
        /// Copying Constructor
        /// </summary>
        /// <param name="original">Original object</param>
        public Complex(Complex original)
        {
            this.Real = new Fraction(original.Real);
            this.Imaginary = new Fraction(original.Imaginary);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Divide
        /// </summary>
        public static Complex operator /(Complex a, Complex b)
        {
            Fraction den = (b.Real * b.Real + b.Imaginary * b.Imaginary);
            if (den.Numerator == 0)
            {
                throw new ArgumentOutOfRangeException("value", "Division by Zero not defined!");
            }
            return new Complex((a.Real * b.Real + a.Imaginary * b.Imaginary) / den,
                               (a.Imaginary * b.Real - a.Real * b.Imaginary) / den);
        }

        /// <summary>
        /// Divide
        /// </summary>
        public static Complex operator /(Complex comp, long value)
        {
            if (value == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Division by Zero not defined!");
            }
            return new Complex(comp.Real / value, comp.Imaginary / value);
        }

        /// <summary>
        /// Divide
        /// </summary>
        public static Complex operator /(Complex comp, Fraction value)
        {
            if ((long)value == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Division by Zero not defined!");
            }
            return new Complex(comp.Real / value, comp.Imaginary / value);
        }

        /// <summary>
        /// Multiply
        /// </summary>
        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary,
                               a.Real * b.Imaginary + a.Imaginary * b.Real);
        }

        /// <summary>
        /// Multiply
        /// </summary>
        public static Complex operator *(Complex comp, int value)
        {
            return new Complex(comp.Real * value, comp.Imaginary * value);
        }

        /// <summary>
        /// Multiply
        /// </summary>
        public static Complex operator *(Complex comp, Fraction value)
        {
            return new Complex(comp.Real * value, comp.Imaginary * value);
        }

        /// <summary>
        /// Add
        /// </summary>
        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }

        /// <summary>
        /// Add
        /// </summary>
        public static Complex operator +(Complex comp, int value)
        {
            return new Complex(comp.Real + value, comp.Imaginary);
        }

        /// <summary>
        /// Add
        /// </summary>
        public static Complex operator +(Complex comp, Fraction value)
        {
            return new Complex(comp.Real + value, comp.Imaginary);
        }

        /// <summary>
        /// Subtract
        /// </summary>
        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }

        /// <summary>
        /// Subtract
        /// </summary>
        public static Complex operator -(Complex comp, int value)
        {
            return new Complex(comp.Real - value, comp.Imaginary);
        }

        /// <summary>
        /// Subtract
        /// </summary>
        public static Complex operator -(Complex comp, Fraction value)
        {
            return new Complex(comp.Real - value, comp.Imaginary);
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString()
        {
            string result = string.Empty;
            if (Math.Abs(this.Real.Value) > 1e-13)
            {
                result = this.Real.ToString();
                if (this.Imaginary.Value > 1e-13)
                {
                    result += "+";
                }
            }
            else if (Math.Abs(this.Imaginary.Value) < 1e-13)
            {
                result = "0";
            }

            if (Math.Abs(this.Imaginary.Value) < 1e-13)
            {
                return result;
            }

            if (!this.Imaginary.Absolute.Equals(new Fraction(1)))
            {
                result += this.Imaginary.ToString() + "i";
            }
            else
            {
                if (this.Imaginary.Value > 0)
                {
                    result += "i";
                }
                else
                {
                    result += "-i";
                }
            }

            return result;
        }

        /// <inheritdoc />
        public object Clone() => new Complex(this);

        #endregion

        #region Equality

        /// <inheritdoc />
        public bool Equals(Complex other)
        {
            return this.Real.Equals(other.Real) && this.Imaginary.Equals(other.Imaginary);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Complex other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Real.GetHashCode() * 397) ^ this.Imaginary.GetHashCode();
            }
        }

        #endregion
    }
}
