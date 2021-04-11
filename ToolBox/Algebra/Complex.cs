using System;

namespace ToolBox.Algebra
{
    public struct Complex : IEquatable<Complex>, ICloneable
    {
        #region Properties

        /// <summary>
        /// Realteil
        /// </summary>
        public Fraction Real { get; }

        /// <summary>
        /// Imaginärteil
        /// </summary>
        public Fraction Imaginary { get; }

        /// <summary>
        /// Gibt Absolutwert wieder
        /// </summary>
        /// <returns>Absolutwert</returns>
        /// <remarks>Verlust der Präzision, </remarks>
        public Fraction Absolute
        {
            get
            {
                //TODO: Wurzel eines Bruches implementieren
                double abs = Math.Sqrt((this.Real * this.Real).Value + (this.Imaginary * this.Imaginary).Value);
                return new Fraction(abs);
            }
        }

        /// <summary>
        /// Konjugiert komplexe Zahl
        /// </summary>
        public Complex Conjugation => new Complex(this.Real, -this.Imaginary);

        #endregion

        #region Construction

        /// <summary>
        /// Konstruktor mit Integer
        /// </summary>
        /// <param name="real">Realteil</param>
        /// <param name="imag">Imaginärteil</param>
        public Complex(int real, int imag)
        {
            this.Real = new Fraction(real);
            this.Imaginary = new Fraction(imag);
        }

        /// <summary>
        /// Konstruktor mit Double
        /// </summary>
        /// <param name="real">Realteil</param>
        /// <param name="imag">Imaginärteil</param>
        public Complex(double real, double imag)
        {
            this.Real = new Fraction(real);
            this.Imaginary = new Fraction(imag);
        }

        /// <summary>
        /// Konstruktor mit Brüchen
        /// </summary>
        /// <param name="real">Realteil</param>
        /// <param name="imag">Imaginärteil</param>
        public Complex(Fraction real, Fraction imag)
        {
            this.Real = real != default ? real : throw new ArgumentNullException(nameof(real)); 
            this.Imaginary = imag != default ? imag : throw new ArgumentNullException(nameof(imag));
        }

        /// <summary>
        /// Konstruktor mit Integer
        /// </summary>
        /// <param name="real">Realteil</param>
        public Complex(int real)
        {
            this.Real = new Fraction(real);
            this.Imaginary = new Fraction(0);
        }

        /// <summary>
        /// Konstruktor mit Double
        /// </summary>
        /// <param name="real">Realteil</param>
        public Complex(double real)
        {
            this.Real = new Fraction(real);
            this.Imaginary = new Fraction(0);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="real">Realteil</param>
        public Complex(Fraction real)
        {
            this.Real = real != default ? real : throw new ArgumentNullException(nameof(real));
            this.Imaginary = new Fraction(0);
        }

        /// <summary>
        /// Kopierkonstruktor
        /// </summary>
        /// <param name="original">Originalobjekt</param>
        public Complex(Complex original)
        {
            this.Real = new Fraction(original.Real);
            this.Imaginary = new Fraction(original.Imaginary);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Dividieren
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
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

        public static Complex operator /(Complex comp, long value)
        {
            if (value == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Division by Zero not defined!");
            }
            return new Complex(comp.Real / value, comp.Imaginary / value);
        }

        public static Complex operator /(Complex comp, Fraction value)
        {
            if ((long)value == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Division by Zero not defined!");
            }
            return new Complex(comp.Real / value, comp.Imaginary / value);
        }

        /// <summary>
        /// Multiplizieren
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary,
                               a.Real * b.Imaginary + a.Imaginary * b.Real);
        }

        public static Complex operator *(Complex comp, int value)
        {
            return new Complex(comp.Real * value, comp.Imaginary * value);
        }

        public static Complex operator *(Complex comp, Fraction value)
        {
            return new Complex(comp.Real * value, comp.Imaginary * value);
        }

        /// <summary>
        /// Addieren
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }

        public static Complex operator +(Complex comp, int value)
        {
            return new Complex(comp.Real + value, comp.Imaginary);
        }

        public static Complex operator +(Complex comp, Fraction value)
        {
            return new Complex(comp.Real + value, comp.Imaginary);
        }

        /// <summary>
        /// Subtrahieren
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }

        public static Complex operator -(Complex comp, int value)
        {
            return new Complex(comp.Real - value, comp.Imaginary);
        }

        public static Complex operator -(Complex comp, Fraction value)
        {
            return new Complex(comp.Real - value, comp.Imaginary);
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            String result = "";
            if (Math.Abs(this.Real.Value) > 1e-13)
            {
                result = this.Real.ToString();
                if (this.Imaginary.Value > 1e-13)
                    result += "+";
            }
            else
            {
                if (Math.Abs(this.Imaginary.Value) < 1e-13)
                    result = "0";
            }
            if (Math.Abs(this.Imaginary.Value) < 1e-13)
            {
                return result;
            }
            if (!Fraction.Abs(this.Imaginary).Equals(new Fraction(1)))
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

            //  if FReal <> 0.0 then begin
            //    if frac(FReal) > 0 then Result := FloatToStr(FReal) else
            //      Result := IntToStr(Trunc(FReal));
            //    if FImaginary > 0 then Result := Result+'+';
            //  end else if FImaginary = 0.0 then Result := '0';
            //  if FImaginary <> 0.0 then begin
            //    if abs(FImaginary) <> 1.0  then begin
            //      if frac(FImaginary) > 0 then Result := Result+FloatToStr(FImaginary)+'j' else
            //        Result := Result+IntToStr(Trunc(FImaginary))+'j';
            //    end else if FImaginary > 0 then Result := Result+'j' else Result := Result+'-j';
            //  end;

        }

        public object Clone()
        {
            return new Complex(this);
        }

        #endregion

        #region Equality

        public bool Equals(Complex other)
        {
            return this.Real.Equals(other.Real) && this.Imaginary.Equals(other.Imaginary);
        }

        public override bool Equals(object obj)
        {
            return obj is Complex other && Equals(other);
        }

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
