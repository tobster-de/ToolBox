using System;

namespace ToolBox.Algebra
{
    /// <summary>
    /// Mathematische Matrix mit Brüchen
    /// </summary>
    public class MatrixEx
    {
        /// <summary>
        /// Komponenten
        /// </summary>
        private readonly Fraction[][] _components;

        /// <summary>
        /// Anzahl der Zeilen
        /// </summary>
        public uint Rows => (uint)this._components.Length;
        
        /// <summary>
        /// Anzahl der Spalten
        /// </summary>
        public uint Columns => (uint)this._components[0].Length;

        /// <summary>
        /// Zugriff auf Komponenten des Vektors
        /// </summary>
        /// <param name="indexX"></param>
        /// <param name="indexY"></param>
        /// <returns></returns>
        public Fraction this[uint indexX, uint indexY]
        {
            get
            {
                if (indexX < this.Rows && indexY < this.Columns)
                {
                    return this._components[indexX][indexY];
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (indexX < this.Rows && indexY < this.Columns)
                {
                    this._components[indexX][indexY] = value;
                    return;
                }
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Determinante
        /// </summary>
        public Fraction Determinant => MatrixEx.CalcDeterminant(this);

        #region Construction

        /// <summary>
        /// Konstruktor für Matrix
        /// </summary>
        /// <param name="rows">Anzahl Zeilen</param>
        /// <param name="columns">Anzahl Spalten</param>
        public MatrixEx(uint rows, uint columns)
        {
            this._components = new Fraction[rows][];
            for (int i = 0; i < rows; i++)
            {
                this._components[i] = new Fraction[columns];
            }
        }

        /// <summary>
        /// Kopierkonstruktor
        /// </summary>
        /// <param name="other">Original</param>
        internal MatrixEx(MatrixEx other)
        {
            this._components = new Fraction[other.Rows][];
            for (int i = 0; i < other.Rows; i++)
            {
                this._components[i] = new Fraction[other.Columns];
                for (int j = 0; j < other.Columns; j++)
                {
                    this._components[i][j] = new Fraction(other._components[i][j]);
                }
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static MatrixEx operator +(MatrixEx m1, MatrixEx m2)
        {
            if (m1.Columns != m2.Columns ||
                m1.Rows != m2.Rows)
            {
                throw new ArgumentException("Dimensions of matrices do not match.");
            }
            MatrixEx matrix = new MatrixEx(m1);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    matrix._components[i][j] += m2._components[i][j];
                }
            }
            return matrix;
        }

        /// <summary>
        /// Subtraktion
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static MatrixEx operator -(MatrixEx m1, MatrixEx m2)
        {
            if (m1.Columns != m2.Columns ||
                m1.Rows != m2.Rows)
            {
                throw new ArgumentException("Dimensions of matrices do not match.");
            }
            MatrixEx matrix = new MatrixEx(m1);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    matrix._components[i][j] -= m2._components[i][j];
                }
            }
            return matrix;
        }

        /// <summary>
        /// Multiplikation
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static MatrixEx operator *(MatrixEx m1, MatrixEx m2)
        {
            if (m1.Columns != m2.Rows)
            {
                throw new ArgumentException("Inner dimensions of matrices need to match.");
            }
            MatrixEx matrix = new MatrixEx(m1.Rows, m2.Columns);
            for (uint i = 0; i < matrix.Rows; i++)
            {
                for (uint j = 0; j < matrix.Columns; j++)
                {
                    Fraction sum = new Fraction(0);
                    for (uint k = 0; k < m1.Columns; k++)
                    {
                        sum += m1[i, k] * m2[k, j];
                    }
                    matrix._components[i][j] = sum;
                }
            }
            return matrix;

        }

        /// <summary>
        /// Multiplikation mit Skalar
        /// </summary>
        /// <param name="skalar"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static MatrixEx operator *(Fraction skalar, MatrixEx m)
        {
            MatrixEx matrix = new MatrixEx(m);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    matrix._components[i][j] *= skalar;
                }
            }
            return matrix;
        }

        /// <summary>
        /// Multiplikation mit Skalar
        /// </summary>
        /// <param name="m"></param>
        /// <param name="skalar"></param>
        /// <returns></returns>
        public static MatrixEx operator *(MatrixEx m, Fraction skalar)
        {
            return skalar * m;
        }

        /// <summary>
        /// Division durch Skalar
        /// </summary>
        /// <param name="v"></param>
        /// <param name="skalar"></param>
        /// <returns></returns>
        public static MatrixEx operator /(MatrixEx m, Fraction skalar)
        {
            MatrixEx matrix = new MatrixEx(m);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    matrix._components[i][j] /= skalar;
                }
            }
            return matrix;
        }

        public static bool operator ==(MatrixEx v1, MatrixEx v2)
        {
            return Equals(v1, v2);
        }

        public static bool operator !=(MatrixEx v1, MatrixEx v2)
        {
            return !Equals(v1, v2);
        }

        #endregion

        #region Overrides

        public override bool Equals(object other)
        {
            if (other is MatrixEx == false)
            {
                return false;
            }
            MatrixEx oMatrix = (MatrixEx)other;
            bool equal = this.Columns == oMatrix.Columns;
            equal &= this.Rows == oMatrix.Rows;
            if (!equal)
            {
                return false;
            }
            for (int i = 0; i < oMatrix.Rows; i++)
            {
                for (int j = 0; j < oMatrix.Columns; j++)
                {
                    equal &= this._components[i][j] == oMatrix._components[i][j];
                }
            }
            return equal;
        }

        public override string ToString()
        {
            string str = "Matrix: (";
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    str += this._components[i][j].ToString();
                    if (j < this.Columns - 1)
                    {
                        str += ",";
                    }
                }
                if (i < this.Rows - 1)
                {
                    str += ";";
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
        /// Einheitsmatrix
        /// </summary>
        /// <param name="count">Anzahl Zeilen und Spalten</param>
        /// <returns></returns>
        public static Matrix Unity(uint count)
        {
            Matrix matrix = new Matrix(count, count);
            for (uint i = 0; i < count; i++)
            {
                matrix[i, i] = 1;
            }
            return matrix;
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Transponierte Matrix
        /// </summary>
        /// <returns>Transponierte Matrix</returns>
        public MatrixEx Transpone()
        {
            MatrixEx trans = new MatrixEx(this.Columns, this.Rows);
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    trans._components[j][i] = this._components[i][j];
                }
            }
            return trans;
        }

        #endregion

        #region Private Implementation

        private static Fraction CalcDeterminant(MatrixEx matrix)
        {
            if (matrix.Rows != matrix.Columns)
            {
                //Determinante nur für quadr. Matrizen
                throw new NotSupportedException("Determinant only applicable to quadratic matrices.");
            }
            if (matrix.Columns == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];
            }
            else
            {
                Fraction sum = new Fraction(0);
                //Laplace'scher Entwicklungssatz
                for (uint i = 0; i < matrix.Columns; i++)
                {
                    //Matrix nach Spalte i entwickeln
                    MatrixEx eMat = new MatrixEx(matrix.Rows - 1, matrix.Columns - 1);
                    for (uint m = 1; m < matrix.Rows; m++)
                    {
                        for (uint n = 0; n < matrix.Columns; n++)
                        {
                            if (n < i) eMat[m - 1, n] = new Fraction(matrix[m, n]);
                            else if (n > i) eMat[m - 1, n - 1] = new Fraction(matrix[m, n]);
                        }
                    }
                    //rekursiver Aufruf
                    if (matrix[0, i].Numerator != 0)
                    {
                        Fraction det = matrix[0, i] * CalcDeterminant(eMat);
                        // odd(i)
                        if (i % 2 != 0) sum -= det;
                        else sum += det;
                    }
                }
                return sum;
            }
        }

        #endregion
    }
}
