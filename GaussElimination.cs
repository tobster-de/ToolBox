using System;
using System.Collections.Generic;

namespace ToolBox
{
    public static class GaussElimination
    {

        private static bool SolveFromPivot(ref MatrixEx matrix, uint row, uint col)
        {
            bool result = true;

            //int max = 0;
            //if rad_equation.Checked then max := edt_Dimension.Value else if rad_inverse.Checked then max := edt_Dimension.Value*2-1;

            //Pivotelement ist Null - tausche Pivotzeile mit anderer Zeile
            if (matrix[row, col].Numerator == 0)
            {
                bool valid = false;
                //gültige Zeile ohne Null suchen
                for (uint i = row + 1; i < matrix.Rows; i++)
                {
                    if (matrix[i, col].Numerator != 0)
                    {
                        valid = true;
                        //Zeilenelemente tauschen
                        for (uint j = 0; j < /*max*/matrix.Columns; j++)
                        {
                            Fraction h = matrix[row, j];
                            matrix[row, j] = matrix[i, j];
                            matrix[i, j] = h;
                        }
                        break;
                    }
                }
                result = valid;
            }
            // Erzeugen von Nullen unterhalb des Pivotelements
            if (result)
            {
                for (uint i = row + 1; i < matrix.Rows; i++)
                {
                    if (matrix[i, col].Numerator != 0)
                    {
                        // Zeilenmodifikator bestimmen
                        Fraction mul = matrix[i, col] / matrix[row, col] * -1;
                        for (uint j = col; j < /*col*/matrix.Columns; j++)
                        {
                            matrix[i, j] = matrix[row, j] * mul + matrix[i, j];
                        }
                    }
                }
            }
            // Mit nächster Spalte weitermachen
            if (row + 1 < matrix.Rows - 1 && result)
            {
                result = SolveFromPivot(ref matrix, row + 1, col + 1);
            }
            return result;
        }

        private static bool ReverseSolveFromPivot(ref MatrixEx matrix, uint row, uint col)
        {
            bool result = true;
            //max := 0;        
            //if rad_equation.Checked then max := edt_Dimension.Value else if rad_inverse.Checked then max := edt_Dimension.Value*2-1;

            //Pivotelement ist nicht Eins - gesamte Zeile dividieren
            if (matrix[row, col].Equals(1) == false)
            {
                Fraction h = new Fraction(matrix[row, col]);
                for (uint j = /*max*/matrix.Columns - 1; j >= 0; j--)
                {
                    matrix[row, j] /= h;
                    if (j == 0)
                        break;
                }
            }
            // Erzeugen von Nullen überhalb des Pivotelements
            if (row > 0)
            {
                for (uint i = row - 1; ; i--)
                {
                    // Zeilenmodifikator bestimmen
                    //mul := Matrix.Element[i,Col]/Matrix.Element[Row,Col]*-1;
                    Fraction mul = new Fraction(matrix[i, col]) / matrix[row, col] * -1;
                    for (uint j = matrix.Columns - 1; ; j--)
                    {
                        //Matrix.Element[i,j] := mul*Matrix.Element[Row,j]+Matrix.Element[i,j];
                        Fraction h = new Fraction(matrix[row, j]) * mul + matrix[i, j];
                        matrix[i, j] = h;
                        if (j == 0)
                            break;
                    }
                    if (i == 0)
                        break;
                }
            }
            // Mit kleinerer Matrix fortsetzen
            if (row > 0 && result)
            {
                result = ReverseSolveFromPivot(ref matrix, row - 1, col - 1);
            }
            return result;
        }

        private static uint Rank(MatrixEx enhMat, uint rows, uint columns)
        {
            uint count = 0;
            Fraction rowsum;
            for (uint i = 0; i < rows; i++)
            {
                rowsum = new Fraction(0);
                for (uint j = 0; j < columns; j++)
                {
                    rowsum += enhMat[i, j];
                }
                if (rowsum.Numerator != 0)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Löst ein Gleichungssystem "A*x=b" und gibt den Lösungsvektor x zurück.
        /// </summary>
        /// <param name="A">Matrix A</param>
        /// <param name="b">Vektor b</param>
        /// <returns>Lösungsvektor x</returns>
        public static Vector SolveEquation(Matrix A, Vector b)
        {
            if (A == null || b == null)
            {
                throw new ArgumentNullException("Parameters must not be null.");
            }
            if (A.Rows != b.Dimensions)
            {
                throw new NotSupportedException("Dimensions of matrix and vector do not match.");
            }
            //erweiterte Matrix erzeugen
            MatrixEx enhMat = new MatrixEx(A.Rows, A.Columns + 1);
            for (uint i = 0; i < A.Rows; i++)
            {
                for (uint j = 0; j < A.Columns; j++)
                {
                    enhMat[i, j] = new Fraction(A[i, j]);
                }
                enhMat[i, A.Columns] = new Fraction(b[i]);
            }
            if (SolveFromPivot(ref enhMat, 0, 0))
            {
                //Rang der Matrix prüfen (wenn kleiner Rows -> nicht eindeutig lösbar)
                if (Rank(enhMat, A.Rows, A.Columns) != A.Rows)
                {
                    return null;
                }
                ReverseSolveFromPivot(ref enhMat, A.Rows - 1, A.Columns - 1);
            }
            //Ergebnis aus erw. Matrix lesen
            Vector result = new Vector(b.Dimensions);
            for (uint i = 0; i < A.Rows; i++)
            {
                result[i] = enhMat[i, A.Columns].Value;
            }
            return result;
        }

        /// <summary>
        /// Invertiert eine Matrix
        /// </summary>
        /// <param name="A">Zu intertierende Matrix</param>
        /// <returns>Inverse Matrix 1/A</returns>
        public static Matrix InvertMatrix(Matrix A)
        {
            if (A == null)
            {
                throw new ArgumentNullException("Matrix must not be null.");
            }
            if (A.Rows != A.Columns)
            {
                throw new NotSupportedException("Only quadratic matrices are invertable.");
            }
            //erweiterte Matrix erzeugen
            MatrixEx enhMat = new MatrixEx(A.Rows, A.Columns * 2);
            for (uint i = 0; i < A.Rows; i++)
            {
                for (uint j = 0; j < A.Columns; j++)
                {
                    enhMat[i, j] = new Fraction(A[i, j]);
                    if (i == j) enhMat[i, j + A.Columns] = new Fraction(1);
                    else enhMat[i, j + A.Columns] = new Fraction(0);
                }
            }
            if (SolveFromPivot(ref enhMat, 0, 0))
            {
                //Rang der Matrix prüfen (wenn kleiner Rows -> nicht eindeutig lösbar)
                if (Rank(enhMat, A.Rows, A.Columns) != A.Rows)
                {
                    return null;
                }
                ReverseSolveFromPivot(ref enhMat, A.Rows - 1, A.Columns - 1);
            }
            //Ergebnis aus erw. Matrix lesen
            Matrix result = new Matrix(A.Rows, A.Columns);
            for (uint i = 0; i < A.Rows; i++)
            {
                for (uint j = 0; j < A.Columns; j++)
                {
                    result[i, j] = enhMat[i, j + A.Columns].Value;
                }
            }
            return result;
        }

    }
}
