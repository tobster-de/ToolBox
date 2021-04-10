using System;
using System.Collections.Generic;

namespace ToolBox
{
    public class Complex : IEquatable<Complex>
    {
        #region Fields

        private Fraction m_Real;
        private Fraction m_Imaginary;

        #endregion

        #region Properties

        /// <summary>
        /// Realteil
        /// </summary>
        public Fraction Real
        {
            get
            {
                return m_Real;
            }
            set
            {
                m_Real = value;
            }
        }

        /// <summary>
        /// Imaginärteil
        /// </summary>
        public Fraction Imaginary
        {
            get
            {
                return m_Imaginary;
            }
            set
            {
                m_Imaginary = value;
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Konstruktor mit Integer
        /// </summary>
        /// <param name="real">Realteil</param>
        /// <param name="imag">Imaginärteil</param>
        public Complex(int real, int imag)
        {
            m_Real = new Fraction(real);
            m_Imaginary = new Fraction(imag);
        }

        /// <summary>
        /// Konstruktor mit Double
        /// </summary>
        /// <param name="real">Realteil</param>
        /// <param name="imag">Imaginärteil</param>
        public Complex(double real, double imag)
        {
            m_Real = new Fraction(real);
            m_Imaginary = new Fraction(imag);
        }

        /// <summary>
        /// Konstruktor mit Brüchen
        /// </summary>
        /// <param name="real">Realteil</param>
        /// <param name="imag">Imaginärteil</param>
        public Complex(Fraction real, Fraction imag)
        {
            m_Real = real != default ? real : throw new ArgumentNullException(nameof(real)); 
            m_Imaginary = imag != default ? imag : throw new ArgumentNullException(nameof(imag));
        }

        /// <summary>
        /// Konstruktor mit Integer
        /// </summary>
        /// <param name="real">Realteil</param>
        public Complex(int real)
        {
            m_Real = new Fraction(real);
            m_Imaginary = new Fraction(0);
        }

        /// <summary>
        /// Konstruktor mit Double
        /// </summary>
        /// <param name="real">Realteil</param>
        public Complex(double real)
        {
            m_Real = new Fraction(real);
            m_Imaginary = new Fraction(0);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="real">Realteil</param>
        public Complex(Fraction real)
        {
            m_Real = real != default ? real : throw new ArgumentNullException(nameof(real));
            m_Imaginary = new Fraction(0);
        }

        /// <summary>
        /// Kopierkonstruktor
        /// </summary>
        /// <param name="original">Originalobjekt</param>
        public Complex(Complex original)
        {
            m_Real = new Fraction(original.Real);
            m_Imaginary = new Fraction(original.Imaginary);
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

        #region Public Implementation

        /// <summary>
        /// Konjugiert komplexe Zahl
        /// </summary>
        /// <param name="comp"></param>
        /// <returns></returns>
        public Complex Conjunction()
        {
            return new Complex(m_Real, -m_Imaginary);
        }

        /// <summary>
        /// Gibt Absolutwert wieder
        /// </summary>
        /// <param name="comp">Komplexe Zahl</param>
        /// <returns>Absolutwert</returns>
        /// <remarks>Verlust der Präzision, </remarks>
        public static Fraction Abs(Complex comp)
        {
            //TODO: Wurzel eines Bruches implementieren
            double abs = Math.Sqrt((comp.Real * comp.Real).Value + (comp.Imaginary * comp.Imaginary).Value);
            return new Fraction(abs);
        }
        
        #endregion

        #region Overrides

        public override string ToString()
        {
            String result = "";
            if (Math.Abs(m_Real.Value) > 1e-13)
            {
                result = m_Real.ToString();
                if (m_Imaginary.Value > 1e-13)
                    result += "+";
            }
            else
            {
                if (Math.Abs(m_Imaginary.Value) < 1e-13)
                    result = "0";
            }
            if (Math.Abs(m_Imaginary.Value) < 1e-13)
            {
                return result;
            }
            if (!Fraction.Abs(m_Imaginary).Equals(new Fraction(1)))
            {
                    result += m_Imaginary.ToString() + "i";
            }
            else
            {
                if (m_Imaginary.Value > 0)
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

        #endregion

        public bool Equals(Complex other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(this.m_Real, other.m_Real) && Equals(this.m_Imaginary, other.m_Imaginary);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Complex) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.m_Real != null ? this.m_Real.GetHashCode() : 0) * 397) ^ (this.m_Imaginary != null ? this.m_Imaginary.GetHashCode() : 0);
            }
        }
    }
}
