
/*********************************************************************************************************************************************
 * *
 * *        File Name                : Matrix.cs
 * *        Creator                  : Fuhua Lai
 * *        Date Modified            : 2013-6-19 10:55:27
 * *        Functional Description   : Performs real Matrix operation.
 * *
 * *        Remarks                    This source code is free to use, you can redistribute it and modify it.
 * *                                   Math is fun,and also the computer programming.The source Code is written by Fuhua Lai.
 * *                                   I'm a student in grade 3 from department of mathematics, Zhangzhou Normal University,China.
 * *                                   i'm interested in programing and mathematics,So i implement this math library.
 * *                                   this library is written in pure c sharp language, and i put much of my time to optimize the source code.
 * *                                   the code is well written and carefully tuned and also well tested,you can enjoy my work.
 * *                                   if you find any bugs,please let me know.
 * *                                    
 * *        Email                    : 991592170@qq.com
 * *
 * *        Copyright(c)2012-2013  NumBit Work Studio.  All rights reserved. 
 * *
 * ********************************************************************************************************************************************/


using NumBitLib.Helper;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumBitLib.LinearAlgebra
{
    /// <summary>
    /// Represents a real matrix.
    /// </summary>
    public class Matrix
    {
        #region Fields，Properties,Indexers

        /// <summary>
        /// The rows of this Matrix.
        /// </summary>
        protected int rows;
        /// <summary>
        /// The columns of this Matrix.
        /// </summary>
        protected int columns;
        /// <summary>
        /// The elements of this Matrix.
        /// </summary>
        protected double[] elements;

        /// <summary>
        /// Gets the rows of this Matrix.
        /// </summary>
        public int Rows
        {
            get { return rows; }
        }
        /// <summary>
        /// Gets the columns of this Matrix.
        /// </summary>
        public int Columns
        {
            get { return columns; }
        }

        /// <summary>
        /// Gets the elements of this Matrix.
        /// </summary>
        public double[] Elements
        {
            get { return this.elements; }
        }

        /// <summary>
        /// Gets or sets the specified index of the element.
        /// </summary>
        /// <param name="i">Row index.</param>
        /// <param name="j">Column index.</param>
        /// <returns>An elements of this Matrix.</returns>
        /// <exception cref="IndexOutOfRangeException">Throw if index out of range.</exception>
        public double this[int i, int j]
        {
            get
            {
                return elements[(i - 1) * columns + j - 1];
            }
            set
            {
                elements[(i - 1) * columns + j - 1] = value;
            }
        }

        /// <summary>
        /// Gets or sets the specified row of this Matrix.
        /// </summary>
        /// <param name="i">The specified column.</param>
        /// <returns>The <paramref name="i"/>-th row of this matrix.</returns>
        /// <exception cref="IndexOutOfRangeException">Throw if index out of range.</exception>
        public Matrix this[int i]
        {
            get
            {
                i--;
                Matrix R = new Matrix(1, columns);
                for (int j = 0; j < columns; j++)
                    R.elements[j] = this.elements[i * columns + j];
                return R;
            }
            set
            {
                i--;
                for (int j = 0; j < columns; j++)
                    this.elements[i * columns + j] = value.elements[j];
            }
        }

        /// <summary>
        /// Gets or sets the sub-block matrix.
        /// </summary>
        /// <param name="RowStart">Row start position.</param>
        /// <param name="RowEnd">Row end position.</param>
        /// <param name="ColStart">Column start position.</param>
        /// <param name="ColEnd">Column end position.</param>
        /// <returns>A submatrix.</returns>
        /// <exception cref="IndexOutOfRangeException">Throw if index out of range.</exception>
        public Matrix this[int RowStart, int RowEnd, int ColStart, int ColEnd]
        {
            get
            {
                Matrix MatChild = new Matrix(RowEnd - RowStart + 1, ColEnd - ColStart + 1);
                for (int i = RowStart; i <= RowEnd; i++)
                    for (int j = ColStart; j <= ColEnd; j++)
                        MatChild[i - RowStart + 1, j - ColStart + 1] = this[i, j];
                return MatChild;
            }
            set
            {
                for (int i = RowStart; i <= RowEnd; i++)
                    for (int j = ColStart; j <= ColEnd; j++)
                        this[i, j] = value[i - RowStart + 1, j - ColStart + 1];
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Matrix.
        /// </summary>
        public Matrix()
        {
            rows = 1;
            columns = 1;
            elements = new double[1];
        }

        /// <summary>
        /// Initializes a new instance of Matrix with specified size.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <exception cref="ArgumentException">Throw if <paramref name="rows"/> or <paramref name="columns"/> is negative.</exception>
        public Matrix(int rows, int columns)
        {
            if (rows < 0 || columns < 0)
                throw new ArgumentException(Resources.Strings.MatrixCtor);
            this.rows = rows;
            this.columns = columns;
            elements = new double[rows * columns];
        }

        /// <summary>
        /// Initializes a new instance of Matrix with specified size an elements.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <param name="data">The array.</param>
        /// <exception cref="ArgumentException">Throw if matrix dimensions don't consistent with the elements number.</exception>
        public Matrix(int rows, int columns, params double[] data)
            : this(rows, columns)
        {
            if (data.Length != rows * columns)
                throw new ArgumentException(Resources.Strings.MatrixCtor1);
            data.CopyTo(this.elements, 0);
        }

        /// <summary>
        /// Initializes a Square Matrix with specified size.
        /// </summary>
        /// <param name="size">The size of the Square Matrix. </param>
        /// <exception cref="ArgumentException">Throw if <paramref name="size"/> is negative.</exception>
        public Matrix(int size)
        {
            if (size < 1)
                throw new ArgumentException(Resources.Strings.MatrixCtor2);
            rows = size;
            columns = size;
            elements = new double[size * size];
        }

        /// <summary>
        ///  Initializes a Square Matrix with specified size and elements.
        /// </summary>
        /// <param name="size">The size of the Square Matrix.</param>
        /// <param name="data">The array.</param>
        /// <exception cref="ArgumentException">Throw if matrix dimensions don't consistent with the elements number.</exception>
        public Matrix(int size, params double[] data)
            : this(size)
        {
            if (data.Length != size * size)
                throw new ArgumentException(Resources.Strings.MatrixCtor1);
            data.CopyTo(this.elements, 0);
        }

        /// <summary>
        /// Construct a row matrix use an array.
        /// </summary>
        /// <param name="data">The array.</param>
        public Matrix(params double[] data)
        {
            this.rows = 1;
            this.columns = data.Length;
            data.CopyTo(this.elements, 0);
        }

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="Cpy">The Matrix to be copied</param>
        public Matrix(Matrix Cpy)
        {
            rows = Cpy.rows;
            columns = Cpy.columns;
            this.elements = new double[Cpy.elements.Length];
            Cpy.elements.CopyTo(this.elements, 0);
        }

        #endregion

        #region Matrix Generator

        /// <summary>
        ///  Zeros matrix.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <returns>Zeros matrix.</returns>
        public static Matrix Zeros(int rows, int columns)
        {
            return new Matrix(rows, columns);
        }

        /// <summary>
        /// Ones matrix.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <returns>Ones matrix.</returns>
        public static Matrix Ones(int rows, int columns)
        {
            return new Matrix(rows, columns, MArray.Ones(rows * columns));
        }

        /// <summary>
        /// Identity matrix.
        /// </summary>
        ///<param name="size">The size of this matrix.</param>
        /// <returns>Identity matrix.</returns>
        public static Matrix Eye(int size)
        {
            Matrix E = new Matrix(size);
            for (int i = 1; i <= size; i++)
                E[i, i] = 1.0;
            return E;
        }

        /// <summary>
        /// Scalar matrix.
        /// </summary>
        ///<param name="size">The size of this matrix.</param>
        ///<param name="num">The element.</param>
        /// <returns>Scalar matrix.</returns>
        public static Matrix Scalar(int size, double num)
        {
            return new Matrix(size, MArray.Scalar(num, size * size));
        }

        /// <summary>
        /// Hilbert matrix.
        /// </summary>
        ///<param name="size">The size of this matrix.</param>
        /// <returns>Hilbert matrix.</returns>
        public static Matrix Hilb(int size)
        {
            Matrix h = new Matrix(size);
            for (int i = 1; i <= size; i++)
                for (int j = 1; j <= size; j++)
                    h[i, j] = 1.0 / (i + j - 1);
            return h;
        }

        /// <summary>
        ///  Uniformly distributed pseudorandom numbers.The interval is [0.0,1.0].
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <returns>A matrix with Uniformly distributed pseudorandom numbers.</returns>
        public static Matrix Rand(int rows, int columns)
        {
            return new Matrix(rows, columns, MArray.Rand(rows * columns));
        }

        /// <summary>
        /// Generate values from the uniform distribution on the interval [a, b].
        /// </summary>
        /// <param name="rows">Rows</param>
        /// <param name="columns">Columns</param>
        /// <param name="a">The start position of a interval.</param>
        /// <param name="b">The end position of a interval.</param>
        /// <returns>A matrix with Uniformly distributed pseudorandom numbers,the interval is between <paramref name="a"/> and <paramref name="b"/>.</returns>
        public static Matrix Rand(int rows, int columns, double a, double b)
        {
            return a + (b - a) * Rand(rows, columns);
        }

        /// <summary>
        /// Pseudorandom integers from a uniform discrete distribution. 
        /// Generate integer values from the uniform distribution on the set 0:10.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns>A matrix with integer values from the uniform distribution on the set 0:10.</returns>
        public static Matrix Randi(int rows, int columns)
        {
            return new Matrix(rows, columns, MArray.Randi(rows * columns));
        }

        #endregion

        #region Matrix Type

        /// <summary>
        ///  Returns if this matrix is a Square matrix.
        /// </summary>
        /// <returns>A boolean value indicates if this matrix is a Square matrix.</returns>
        public bool IsSquare()
        {
            return this.Rows == this.Columns ? true : false;
        }

        /// <summary>
        ///  Returns if this matrix is a symmetric matrix.
        /// </summary>
        /// <returns>A boolean value indicates if this matrix is a symmetric matrix.</returns>
        public bool IsSymmetric()
        {
            if (this.IsSquare())
            {
                for (int i = 1; i <= this.Rows; i++)
                    for (int j = 1; j < i; j++)
                        if (this[i, j] != this[j, i])
                            return false;
            }
            else
                return false;
            return true;
        }

        /// <summary>
        ///  Returns if this matrix is a tridiagonal matrix.
        /// </summary>
        /// <returns>A boolean value indicates if this matrix is a tridiagonal matrix.</returns>
        public bool IsTridiag()
        {
            if (this.IsSquare())
            {
                for (int i = 3; i <= this.Rows; i++)
                    for (int j = 1; j < i - 1; j++)
                        if (this[i, j] != 0 || this[j, i] != 0)
                            return false;
            }
            else
                return false;
            return true;
        }

        /// <summary>
        ///  Returns if this matrix is a normal matrix.
        /// </summary>
        /// <returns>A boolean value indicates if this matrix is a normal matrix.</returns>
        public bool IsNormal()
        {
            if (this.IsSquare())
            {
                double md;
                for (int i = 1; i <= this.Rows; i++)
                    for (int j = 1; j <= i; j++)
                    {
                        md = 0.0;
                        for (int k = 1; k <= this.rows; k++)
                            md += this[k, i] * this[k, j];
                        for (int k = 1; k <= this.rows; k++)
                            md -= this[i, k] * this[j, k];
                        if (Math.Abs(md) > LibData.Eps)
                            return false;
                    }
            }
            else
                return false;
            return true;
        }

        #endregion

        #region  Operator Overloading

        /// <summary>
        /// Matrix addition.
        /// </summary>
        /// <param name="A">The left-hand operand.</param>
        /// <param name="B">The right-hand operand.</param>
        /// <returns>The sum of two Matrix.</returns>
        /// <exception cref="ArgumentException">Throw if the size of matrix <paramref name="A"/> and <paramref name="B"/> don't match.</exception>
        public static Matrix operator +(Matrix A, Matrix B)
        {
            Checker.MatrixMatch(A, B);
            return new Matrix(A.rows, A.columns, MArray.Add(A.elements, B.elements));
        }

        /// <summary>
        /// Matrix subtraction.
        /// </summary>
        /// <param name="A">The left-hand operand.</param>
        /// <param name="B">The right-hand operand.</param>
        /// <returns>The subtion of two Matrix.</returns>
        /// <exception cref="ArgumentException">Throw if the size of matrix <paramref name="A"/> and <paramref name="B"/> don't match.</exception>
        public static Matrix operator -(Matrix A, Matrix B)
        {
            Checker.MatrixMatch(A, B);
            return new Matrix(A.rows, A.columns, MArray.Sub(A.elements, B.elements));
        }

        /// <summary>
        /// Matrix multiplication.
        /// </summary>
        /// <param name="A">The left-hand operand.</param>
        /// <param name="B">The right-hand operand.</param>
        /// <returns>The multiplication of two Matrix.</returns>
        /// <exception cref="ArgumentException">Throw if the Inner matrix dimensions don't agree.</exception>
        public static Matrix operator *(Matrix A, Matrix B)
        {
            int m = A.rows;
            int k = A.columns;
            int n = B.columns;
            if (k != B.rows)
                throw new ArithmeticException(Resources.Strings.MatrixMul);
            Matrix Mul = new Matrix(m, n);

            if (m > 30)
            {
                Parallel.For(1, m + 1, i =>
            {
                for (int j = 1; j <= n; j++)
                {
                    double temp = 0.0;
                    for (int t = 1; t <= k; t++)
                    {
                        temp += A[i, t] * B[t, j];
                    }
                    Mul[i, j] = temp;
                }
            });
                return Mul;
            }

            double tem;
            for (int i = 1; i <= m; i++)
                for (int j = 1; j <= n; j++)
                {
                    tem = 0.0;
                    for (int t = 1; t <= k; t++)
                        tem += A[i, t] * B[t, j];
                    Mul[i, j] = tem;
                }
            return Mul;
        }

        /// <summary>
        /// Matrix division.
        /// </summary>
        /// <param name="A">The left-hand operand.</param>
        /// <param name="B">The right-hand operand.</param>
        /// <returns>The division between <paramref name="A"/> and <paramref name="B"/>.</returns>
        public static Matrix operator /(Matrix A, Matrix B)
        {
            return A * Matrix.Pinv(B);
        }

        #endregion

        #region Array Operation

        /// <summary>
        /// Matrix addition.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The addition of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static Matrix operator +(double a, Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Add(A.elements, a));
        }

        /// <summary>
        /// Matrix addition.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The addition of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static Matrix operator +(Matrix A, double a)
        {
            return new Matrix(A.rows, A.columns, MArray.Add(A.elements, a));
        }

        /// <summary>
        /// Opposite matrix.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>A matrix whose elements is the opposite number  of <paramref name="A"/>.</returns>
        public static Matrix operator -(Matrix A)
        {
            return new Matrix(A.rows, A.columns, A.elements.Opp());
        }

        /// <summary>
        /// Matrix subtraction.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The subtraction of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static Matrix operator -(double a, Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Sub(a, A.elements));
        }

        /// <summary>
        /// Matrix subtraction.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The subtraction of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static Matrix operator -(Matrix A, double a)
        {
            return new Matrix(A.rows, A.columns, MArray.Sub(A.elements, a));
        }

        /// <summary>
        /// Matrix multiplication.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The multiplication of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static Matrix operator *(double a, Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Mul(A.elements, a));
        }

        /// <summary>
        /// Matrix multiplication.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The multiplication of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static Matrix operator *(Matrix A, double a)
        {
            return new Matrix(A.rows, A.columns, MArray.Mul(A.elements, a));
        }

        /// <summary>
        /// Matrix division.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The division of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static Matrix operator /(double a, Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Div(a, A.elements));
        }

        /// <summary>
        /// Matrix division.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The division of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static Matrix operator /(Matrix A, double a)
        {
            return new Matrix(A.rows, A.columns, MArray.Div(A.elements, a));
        }

        /// <summary>
        /// Array multiplication.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <param name="B">A Mtarix.</param>
        /// <returns>The array multiplication of <paramref name="A"/> and <paramref name="B"/>.</returns>
        public static Matrix Mul(Matrix A, Matrix B)
        {
            Checker.MatrixMatch(A, B);
            return new Matrix(A.rows, A.columns, MArray.Mul(A.elements, B.elements));
        }

        /// <summary>
        /// Array division.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <param name="B">A Mtarix.</param>
        /// <returns>The array division of <paramref name="A"/> and <paramref name="B"/>.</returns>
        public static Matrix Div(Matrix A, Matrix B)
        {
            Checker.MatrixMatch(A, B);
            return new Matrix(A.rows, A.columns, MArray.Div(A.elements, B.elements));
        }

        #endregion

        #region Mathematical Functions

        /// <summary>
        /// Absolute value.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The absolute value.</returns>
        public static Matrix Abs(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Abs(A.elements));
        }

        /// <summary>
        /// Square root.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The Squqre root.</returns>
        public static Matrix Sqrt(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Sqrt(A.elements));
        }

        /// <summary>
        /// Array power.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <param name="p">The specified power.</param>
        /// <returns>The array power root.</returns>
        public static Matrix Pow(Matrix A, int p)
        {
            return new Matrix(A.rows, A.columns, MArray.Pow(A.elements, p));
        }

        /// <summary>
        /// Array power.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <param name="p">The specified power.</param>
        /// <returns>The array power root.</returns>
        public static Matrix Pow(Matrix A, double p)
        {
            return new Matrix(A.rows, A.columns, MArray.Pow(A.elements, p));
        }

        /// <summary>
        /// Array exponent.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The array exponent root.</returns>
        public static Matrix Exp(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Exp(A.elements));
        }

        /// <summary>
        /// Natural logarithm.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The natural logarithm.</returns>
        public static Matrix Log(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Log(A.elements));
        }

        /// <summary>
        ///  Common (base 10) logarithm.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The Common (base 10) logarithm.</returns>
        public static Matrix Log10(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Log10(A.elements));
        }

        /// <summary>
        /// Sine of argument in radians.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The Sine of argument in radians.</returns>
        public static Matrix Sin(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Sin(A.elements));
        }

        /// <summary>
        /// Cosine of argument in radians.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The Cosine of argument in radians.</returns>
        public static Matrix Cos(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Cos(A.elements));
        }

        /// <summary>
        /// Tangent of argument in radians.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The tangent of argument in radians.</returns>
        public static Matrix Tan(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Tan(A.elements));
        }

        /// <summary>
        /// Sine of argument in degrees.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The sine of argument in degrees.</returns>
        public static Matrix Sind(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Sind(A.elements));
        }

        /// <summary>
        /// Cosine of argument in degrees.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The cosine of argument in degrees.</returns>
        public static Matrix Cosd(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Cosd(A.elements));
        }

        /// <summary>
        /// Tangent of argument in degrees.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The tangent of argument in degrees.</returns>
        public static Matrix Tand(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Tand(A.elements));
        }

        /// <summary>
        /// Arcsine function.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The arcsine of <paramref name="A"/>.</returns>
        public static Matrix Asin(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Asin(A.elements));
        }

        /// <summary>
        /// Arccosine function.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The arccossine of <paramref name="A"/>.</returns>
        public static Matrix Acos(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Acos(A.elements));
        }

        /// <summary>
        /// Arctangent function.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The arctangent of <paramref name="A"/>.</returns>
        public static Matrix Atan(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Atan(A.elements));
        }

        /// <summary>
        /// Hyperbolic sine.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The Hyperbolic sine of <paramref name="A"/>.</returns>
        public static Matrix Sinh(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Sinh(A.elements));
        }

        /// <summary>
        /// Hyperbolic cosine.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The Hyperbolic cosine of <paramref name="A"/>.</returns>
        public static Matrix Cosh(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Cosh(A.elements));
        }

        /// <summary>
        /// Hyperbolic tangent.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The Hyperbolic tangent of <paramref name="A"/>.</returns>
        public static Matrix Tanh(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Tanh(A.elements));
        }

        /// <summary>
        /// Round towards minus infinity.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The floor of <paramref name="A"/>.</returns>
        public static Matrix Floor(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Floor(A.elements));
        }

        /// <summary>
        /// Round towards plus infinity.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The ceiling of <paramref name="A"/>.</returns>
        public static Matrix Ceiling(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Ceiling(A.elements));
        }

        /// <summary>
        /// Round towards nearest integer.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The nearest integers of <paramref name="A"/>.</returns>
        public static Matrix Round(Matrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Round(A.elements));
        }

        #endregion

        #region Data Analysis

        /// <summary>
        ///  Largest component.Returns maximum elements and its index.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The maximum elements of <paramref name="A"/> and its index.</returns>
        public static Tuple<Matrix, Matrix> MaxEleInd(Matrix A)
        {
            Matrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix MaxInd = new Matrix(1, n);
            Matrix MaxEle = new Matrix(1, n);
            for (int j = 1; j <= n; j++)
            {
                MaxEle[1, j] = Vec[1, j];
                MaxInd[1, j] = 1;
                for (int i = 2; i <= m; i++)
                {
                    if (Vec[i, j] > MaxEle[1, j])
                    {
                        MaxEle[1, j] = Vec[i, j];
                        MaxInd[1, j] = i;
                    }
                }
            }
            return new Tuple<Matrix, Matrix>(MaxEle, MaxInd);
        }

        /// <summary>
        ///  Largest component.Returns maximum elements' index.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The maximum elements' index of <paramref name="A"/>.</returns>
        public static Matrix MaxInd(Matrix A)
        {
            Matrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix MaxInd = new Matrix(1, n);
            double MaxEle;
            for (int j = 1; j <= n; j++)
            {
                MaxEle = Vec[1, j];
                MaxInd[1, j] = 1;
                for (int i = 2; i <= m; i++)
                {
                    if (Vec[i, j] > MaxEle)
                    {
                        MaxEle = Vec[i, j];
                        MaxInd[1, j] = i;
                    }
                }
            }
            return MaxInd;
        }

        /// <summary>
        ///  Largest component.Returns maximum elements.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns> The maximum elements of <paramref name="A"/>.</returns>
        public static Matrix Max(Matrix A)
        {
            Matrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix MaxEle = new Matrix(1, n);
            for (int j = 1; j <= n; j++)
            {
                MaxEle[1, j] = Vec[1, j];
                for (int i = 2; i <= m; i++)
                    if (Vec[i, j] > MaxEle[1, j])
                        MaxEle[1, j] = Vec[i, j];
            }
            return MaxEle;
        }

        /// <summary>
        /// Smallest component.Returns minimum elements and its index.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The minimum elements of <paramref name="A"/> and its index.</returns>
        public static Tuple<Matrix, Matrix> MinEleInd(Matrix A)
        {
            Matrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix MinInd = new Matrix(1, n);
            Matrix MinEle = new Matrix(1, n);
            for (int j = 1; j <= n; j++)
            {
                MinEle[1, j] = Vec[1, j];
                MinInd[1, j] = 1;
                for (int i = 2; i <= m; i++)
                {
                    if (Vec[i, j] < MinEle[1, j])
                    {
                        MinEle[1, j] = Vec[i, j];
                        MinInd[1, j] = i;
                    }
                }
            }
            return new Tuple<Matrix, Matrix>(MinEle, MinInd);
        }

        /// <summary>
        /// Smallest component.Returns minimum elements' index.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The minimum elements' index of <paramref name="A"/>.</returns>
        public static Matrix MinInd(Matrix A)
        {
            Matrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix MinInd = new Matrix(1, n);
            double MinEle;
            for (int j = 1; j <= n; j++)
            {
                MinEle = Vec[1, j];
                MinInd[1, j] = 1;
                for (int i = 2; i <= m; i++)
                {
                    if (Vec[i, j] < MinEle)
                    {
                        MinEle = Vec[i, j];
                        MinInd[1, j] = i;
                    }
                }
            }
            return MinInd;
        }

        /// <summary>
        /// Smallest component.Returns minimum elements and its index.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The minimum elements of <paramref name="A"/>.</returns>
        public static Matrix Min(Matrix A)
        {
            Matrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix MinEle = new Matrix(1, n);
            for (int j = 1; j <= n; j++)
            {
                MinEle[1, j] = Vec[1, j];
                for (int i = 2; i <= m; i++)
                    if (Vec[i, j] < MinEle[1, j])
                        MinEle[1, j] = Vec[i, j];
            }
            return MinEle;
        }

        /// <summary>
        /// Sum of elements.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>Sum of elements.</returns>
        public static Matrix Sum(Matrix A)
        {

            Matrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix sm = Zeros(1, n);
            for (int j = 1; j <= n; j++)
                for (int i = 1; i <= m; i++)
                    sm[1, j] += Vec[i, j];
            return sm;
        }

        /// <summary>
        /// Product of elements.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns> Product of elements.</returns>
        public static Matrix Prod(Matrix A)
        {
            Matrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix pd = Ones(1, n);
            for (int j = 1; j <= n; j++)
                for (int i = 1; i <= m; i++)
                    pd[1, j] *= Vec[i, j];
            return pd;
        }

        /// <summary>
        /// Differences of elements.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The differences of elements.</returns>
        public static Matrix Diff(Matrix A)
        {
            Matrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix df = new Matrix(m - 1, n);
            for (int j = 1; j <= n; j++)
                for (int i = 1; i < m; i++)
                    df[i, j] = Vec[i + 1, j] - Vec[i, j];
            return df;
        }

        /// <summary>
        ///  Cumulative sum of elements.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The cumulative sum of elements.</returns>
        public static Matrix CumSum(Matrix A)
        {
            Matrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix cs = new Matrix(m, n);
            for (int j = 1; j <= n; j++)
            {
                cs[1, j] = Vec[1, j];
                for (int i = 2; i <= m; i++)
                    cs[i, j] = cs[i - 1, j] + Vec[i, j];
            }
            return cs;
        }

        /// <summary>
        ///  Cumulative product of elements.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The cumulative product of elements.</returns>
        public static Matrix CumProd(Matrix A)
        {
            Matrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix cs = new Matrix(m, n);
            for (int j = 1; j <= n; j++)
            {
                cs[1, j] = Vec[1, j];
                for (int i = 2; i <= m; i++)
                    cs[i, j] = cs[i - 1, j] * Vec[i, j];
            }
            return cs;
        }

        /// <summary>
        ///  Average of elements.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns> Average of elements.</returns>
        public static Matrix Mean(Matrix A)
        {
            Matrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix sm = Zeros(1, n);
            for (int j = 1; j <= n; j++)
                for (int i = 1; i <= m; i++)
                    sm[1, j] += Vec[i, j];
            return sm / m;
        }

        /// <summary>
        ///  Variance of elements.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns> Variance of elements.</returns>
        public static Matrix Var(Matrix A)
        {
            Matrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix Mu = Mean(Vec);
            Matrix V = Zeros(1, n);
            for (int j = 1; j <= n; j++)
            {
                for (int i = 1; i <= m; i++)
                    V[1, j] += Math.Pow((Vec[i, j] - Mu[1, j]), 2);
                V[1, j] = V[1, j] / (m - 1);
            }
            return V;
        }

        /// <summary>
        ///  Standard deviation.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns> Standard deviation of elements.</returns>
        public static Matrix Std(Matrix A)
        {
            Matrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix Mu = Mean(Vec);
            Matrix Stand = Zeros(1, n);
            for (int j = 1; j <= n; j++)
            {
                for (int i = 1; i <= m; i++)
                    Stand[1, j] += Math.Pow((Vec[i, j] - Mu[1, j]), 2);
                Stand[1, j] = Math.Sqrt(Stand[1, j] / (m - 1));
            }
            return Stand;
        }

        /// <summary>
        /// Sort in ascending or descending order. mode=1 ascending；mode==2，descending；
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <param name="mode">Sort mode,mode=1 ascending；mode==2，descending.</param>
        /// <returns>The sorted matrix.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="mode"/> is not 1 or 2.</exception>
        public static Matrix Sort(Matrix A, int mode)
        {
            Matrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            if (mode != 1 || mode != 2)
                throw new ArgumentException(Resources.Strings.Sort);

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix Acpy = new Matrix(Vec);
            for (int j = 1; j <= n; j++)
                Array.Sort(Acpy.elements, (j - 1) * m, m);
            if (mode == 2)
                for (int j = 1; j <= n; j++)
                    Array.Reverse(Acpy.elements, (j - 1) * m, m);

            return Acpy;
        }

        #endregion

        #region Matrix Manipulate

        /// <summary>
        /// Matrix transpose.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>Matrix transpose.</returns>
        public static Matrix Transpose(Matrix A)
        {
            int m = A.rows;
            int n = A.columns;
            Matrix At = new Matrix(n, m);
            for (int i = 1; i <= m; i++)
                for (int j = 1; j <= n; j++)
                    At[j, i] = A[i, j];
            return At;
        }

        /// <summary>
        /// Vectorize a matrix to a column matrix.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>A column matrix.</returns>
        public static Matrix Vectorize(Matrix A)
        {
            return new Matrix(A.rows * A.columns, 1, A.elements);
        }

        /// <summary>
        ///  Diagonal matrices.
        /// </summary>
        ///<param name="arr">An array.</param>
        /// <returns>A diagonal matrices whose diagonal elements is <paramref name="arr"/>.</returns>
        public static Matrix Diag(double[] arr)
        {
            int n = arr.Length;
            Matrix D = new Matrix(n);
            for (int i = 1; i <= n; i++)
                D[i, i] = arr[i - 1];
            return D;
        }

        /// <summary>
        ///  Diagonal matrices and diagonals of a matrix.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns> Diagonal matrices or diagonals of a matrix.</returns>
        public static Matrix Diag(Matrix A)
        {
            int min = A.rows;
            int max = A.columns;
            if (min > max)
            {
                min = A.columns;
                max = A.rows;
            }

            Matrix D;
            if (min == 1)
            {
                D = Zeros(max, max);
                for (int i = 1; i <= max; i++)
                    D[i, i] = A.elements[i - 1];
                return D;
            }

            D = new Matrix(min, 1);
            for (int i = 1; i <= min; i++)
                D.elements[i - 1] = A[i, i];
            return D;
        }

        /// <summary>
        /// Sum of diagonal elements.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The sum of diagonal elements.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        public static double Trace(Matrix A)
        {
            Checker.IsSquare(A);
            double tr = 0.0;
            for (int i = 1; i <= A.columns; i++)
                tr += A[i, i];
            return tr;
        }

        /// <summary>
        /// Concatenate matrix.Cat(1,A,B) is the same as [A;B]; Cat(2,A,B) is the same as [A,B].
        /// </summary>
        /// <param name="dim">dim=1,column concatenate;dim=2,row concatenate.</param>
        /// <param name="A">A matrix.</param>
        /// <param name="B">A matrix.</param>
        /// <returns>The concatenate matrix.</returns>
        /// <exception cref="ArgumentException">Throw if dimensions of matrices being concatenated are not consistent.</exception>
        public static Matrix Cat(int dim, Matrix A, Matrix B)
        {
            int m1 = A.rows;
            int n1 = A.columns;
            int m2 = B.rows;
            int n2 = B.columns;
            if (dim == 1)
            {
                if (n1 != n2)
                    throw new ArgumentException(Resources.Strings.Cat);
                Matrix connected = new Matrix(m1 + m2, n1);
                for (int j = 1; j <= n1; j++)
                {
                    for (int i = 1; i <= m1; i++)
                        connected[i, j] = A[i, j];
                    for (int i = m1 + 1; i <= m1 + m2; i++)
                        connected[i, j] = B[i - m1, j];
                }
                return connected;
            }
            else
            {
                if (m1 != m2)
                    throw new ArgumentException(Resources.Strings.Cat);
                Matrix Conected = new Matrix(m1, n1 + n2);
                for (int i = 1; i <= m1; i++)
                {
                    for (int j = 1; j <= n1; j++)
                        Conected[i, j] = A[i, j];
                    for (int j = n1 + 1; j <= n1 + n2; j++)
                        Conected[i, j] = B[i, j - n1];
                }
                return Conected;
            }
        }

        #endregion

        #region Basic Linear Algebra

        /// <summary>
        /// Reduced row echelon form.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns> The reduced row echelon form of <paramref name="A"/>.</returns>
        public static Matrix Rref(Matrix A)
        {
            int row = A.rows;
            int col = A.columns;
            Matrix temp = new Matrix(1, col);
            Matrix Acpy = new Matrix(A);

            int k;
            int jump = 0;
            int rr;

            for (int n = 1; n <= col; n++)
            {
                k = n - jump;
                if (k > row)
                    break;
                rr = k;

                for (int i = rr + 1; i <= row; i++)
                {
                    if (Math.Abs(Acpy[i, n]) > Math.Abs(Acpy[k, n]))
                        k = i;
                }
                if (Math.Abs(Acpy[k, n]) < LibData.Eps)
                {
                    jump++;
                    Acpy[k, n] = 0;
                    continue;
                }

                if (rr != k)
                    for (int j = n; j <= col; j++)
                    {
                        temp[1, j] = Acpy[rr, j];
                        Acpy[rr, j] = Acpy[k, j];
                        Acpy[k, j] = temp[1, j];
                    }

                for (int j = n + 1; j <= col; j++)
                    Acpy[rr, j] = Acpy[rr, j] / Acpy[rr, n];
                Acpy[rr, n] = 1.0;

                for (int i = rr + 1; i <= row; i++)
                    for (int j = n + 1; j <= col; j++)
                        Acpy[i, j] = -Acpy[i, n] * Acpy[rr, j] + Acpy[i, j];
                for (int i = rr + 1; i <= row; i++)
                    Acpy[i, n] = 0.0;

                for (int i = 1; i < rr; i++)
                    for (int j = n + 1; j <= col; j++)
                        Acpy[i, j] = -Acpy[i, n] * Acpy[rr, j] + Acpy[i, j];
                for (int i = 1; i < rr; i++)
                    Acpy[i, n] = 0.0;
            }
            return Acpy;
        }

        /// <summary>
        /// Matrix rank
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>An estimate of the number of linearly independent rows or columns of a matrix <paramref name="A"/>.</returns>
        public static int Rank(Matrix A)
        {
            int row = A.rows;
            int col = A.columns;
            Matrix temp = new Matrix(1, col);
            Matrix Acpy = new Matrix(A);

            int k;
            int jump = 0;
            int rr;

            for (int n = 1; n <= col; n++)
            {
                k = n - jump;
                if (k > row)
                    break;
                rr = k;

                for (int i = rr + 1; i <= row; i++)
                {
                    if (Math.Abs(Acpy[i, n]) > Math.Abs(Acpy[k, n]))
                        k = i;
                }
                if (Math.Abs(Acpy[k, n]) < LibData.Eps)
                {
                    jump++;
                    Acpy[k, n] = 0;
                    continue;
                }

                if (rr != k)
                    for (int j = n; j <= col; j++)
                    {
                        temp[1, j] = Acpy[rr, j];
                        Acpy[rr, j] = Acpy[k, j];
                        Acpy[k, j] = temp[1, j];
                    }

                for (int j = n + 1; j <= col; j++)
                    Acpy[rr, j] = Acpy[rr, j] / Acpy[rr, n];

                for (int i = rr + 1; i <= row; i++)
                    for (int j = n + 1; j <= col; j++)
                        Acpy[i, j] = -Acpy[i, n] * Acpy[rr, j] + Acpy[i, j];
            }
            int rank = col - jump;
            return rank < row ? rank : row;
        }

        private static Matrix Rref(Matrix A, out int rank)
        {
            int row = A.rows;
            int col = A.columns;
            Matrix temp = new Matrix(1, col);
            Matrix Acpy = new Matrix(A);

            int k;
            int jump = 0;
            int rr;

            for (int n = 1; n <= col; n++)
            {
                k = n - jump;
                if (k > row)
                    break;
                rr = k;

                for (int i = rr + 1; i <= row; i++)
                {
                    if (Math.Abs(Acpy[i, n]) > Math.Abs(Acpy[k, n]))
                        k = i;
                }
                if (Math.Abs(Acpy[k, n]) < LibData.Eps)
                {
                    jump++;
                    Acpy[k, n] = 0;
                    continue;
                }

                if (rr != k)
                    for (int j = n; j <= col; j++)
                    {
                        temp[1, j] = Acpy[rr, j];
                        Acpy[rr, j] = Acpy[k, j];
                        Acpy[k, j] = temp[1, j];
                    }

                for (int j = n + 1; j <= col; j++)
                    Acpy[rr, j] = Acpy[rr, j] / Acpy[rr, n];
                Acpy[rr, n] = 1.0;

                for (int i = rr + 1; i <= row; i++)
                    for (int j = n + 1; j <= col; j++)
                        Acpy[i, j] = -Acpy[i, n] * Acpy[rr, j] + Acpy[i, j];
                for (int i = rr + 1; i <= row; i++)
                    Acpy[i, n] = 0.0;

                for (int i = 1; i < rr; i++)
                    for (int j = n + 1; j <= col; j++)
                        Acpy[i, j] = -Acpy[i, n] * Acpy[rr, j] + Acpy[i, j];
                for (int i = 1; i < rr; i++)
                    Acpy[i, n] = 0.0;
            }

            rank = col - jump;
            rank = rank < row ? rank : row;
            return Acpy;
        }

        /// <summary>
        /// Computes Matrix rank use LU decomposition algorithm.Fast than <c>rank</c> method,but not very obvious.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>An estimate of the number of linearly independent rows or columns of a matrix <paramref name="A"/>.</returns>
        public static double RankLU(Matrix A)
        {
            int m = A.rows;
            int n = A.columns;

            Matrix temp = new Matrix(1, n);
            Matrix Acpy = new Matrix(A);

            int min = m < n ? m : n;
            int maxind;
            double md;
            double aii;
            int rank = min;

            for (int i = 1; i <= min; i++)
            {
                md = 0.0;
                for (int k = 1; k < i; k++)
                    md += Acpy[i, k] * Acpy[k, i];

                aii = Acpy[i, i] - md;
                maxind = i;

                for (int r = i + 1; r <= min; r++)
                {
                    md = 0.0;
                    for (int k = 1; k < i; k++)
                        md += Acpy[r, k] * Acpy[k, i];

                    md = Acpy[r, i] - md;
                    if (Math.Abs(md) > Math.Abs(aii))
                    {
                        maxind = r;
                        aii = md;
                    }
                }

                if (Math.Abs(aii) < LibData.Eps)
                    rank--;
                else
                {
                    if (i != maxind)
                    {
                        for (int j = 1; j <= n; j++)
                        {
                            temp[1, j] = Acpy[i, j];
                            Acpy[i, j] = Acpy[maxind, j];
                            Acpy[maxind, j] = temp[1, j];
                        }
                    }

                    Acpy[i, i] = aii;
                    for (int j = i + 1; j <= n; j++)
                    {
                        md = 0.0;
                        for (int k = 1; k < i; k++)
                            md += Acpy[i, k] * Acpy[k, j];
                        Acpy[i, j] = Acpy[i, j] - md;
                    }

                    for (int j = i + 1; j <= m; j++)
                    {
                        md = 0.0;
                        for (int k = 1; k < i; k++)
                            md += Acpy[j, k] * Acpy[k, i];
                        Acpy[j, i] = (Acpy[j, i] - md) / Acpy[i, i];
                    }
                }
            }
            return rank;
        }

        /// <summary>
        /// Matrix determinant.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The determinant of the square matrix <paramref name="A"/>.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        public static double Det(Matrix A)
        {
            Checker.IsSquare(A);

            int m = A.columns;
            double a;
            Matrix temp = new Matrix(1, m);
            Matrix Acpy = new Matrix(A);

            int k;
            double det = 1.0;
            int sign = 1;

            for (int n = 1; n <= m; n++)
            {
                k = n;
                for (int i = n + 1; i <= m; i++)
                {
                    if (Math.Abs(Acpy[i, n]) > Math.Abs(Acpy[k, n]))
                        k = i;
                }
                if (Acpy[k, n] == 0)
                    return 0.0;

                if (n != k)
                {
                    sign *= -1;
                    for (int j = n; j <= m; j++)
                    {
                        temp[1, j] = Acpy[n, j];
                        Acpy[n, j] = Acpy[k, j];
                        Acpy[k, j] = temp[1, j];
                    }
                }

                a = Acpy[n, n];
                det *= a;
                for (int j = n + 1; j <= m; j++)
                    Acpy[n, j] = Acpy[n, j] / a;

                for (int i = n + 1; i <= m; i++)
                    for (int j = n + 1; j <= m; j++)
                        Acpy[i, j] = -Acpy[i, n] * Acpy[n, j] + Acpy[i, j];
            }
            return sign * det;
        }

        /// <summary>
        /// Computes Matrix determinant use LU decomposition algorithm. Fast than <c>Det</c> method but not very obvious.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The determinant of the square matrix <paramref name="A"/>.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        public static double DetLU(Matrix A)
        {
            Checker.IsSquare(A);
            int n = A.rows;

            Matrix temp = new Matrix(1, n);
            Matrix Acpy = new Matrix(A);

            int maxind;
            double md;
            double aii;

            int sign = 1;
            double det = 1.0;

            for (int i = 1; i <= n; i++)
            {
                md = 0.0;
                for (int k = 1; k < i; k++)
                    md += Acpy[i, k] * Acpy[k, i];

                aii = Acpy[i, i] - md;
                maxind = i;

                for (int r = i + 1; r <= n; r++)
                {
                    md = 0.0;
                    for (int k = 1; k < i; k++)
                        md += Acpy[r, k] * Acpy[k, i];

                    md = Acpy[r, i] - md;
                    if (Math.Abs(md) > Math.Abs(aii))
                    {
                        maxind = r;
                        aii = md;
                    }
                }

                if (aii == 0)
                    return 0.0;

                else
                {
                    if (i != maxind)
                    {
                        sign *= -1;
                        for (int j = 1; j <= n; j++)
                        {
                            temp[1, j] = Acpy[i, j];
                            Acpy[i, j] = Acpy[maxind, j];
                            Acpy[maxind, j] = temp[1, j];
                        }
                    }

                    Acpy[i, i] = aii;
                    det *= Acpy[i, i];
                    for (int j = i + 1; j <= n; j++)
                    {
                        md = 0.0;
                        for (int k = 1; k < i; k++)
                            md += Acpy[i, k] * Acpy[k, j];
                        Acpy[i, j] = Acpy[i, j] - md;
                    }

                    for (int j = i + 1; j <= n; j++)
                    {
                        md = 0.0;
                        for (int k = 1; k < i; k++)
                            md += Acpy[j, k] * Acpy[k, i];
                        Acpy[j, i] = (Acpy[j, i] - md) / Acpy[i, i];
                    }
                }
            }
            return sign * det;
        }

        /// <summary>
        /// Matrix inverse.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns> The inverse of the square matrix <paramref name="A"/>.</returns>
        /// <exception cref="ArgumentException">Throw if matrix <paramref name="A"/> is not a square matrix.</exception>
        /// <exception cref="ArithmeticException">Throw if Matrix <paramref name="A"/> is badly scaled or nearly singular. </exception>
        public static Matrix Inv(Matrix A)
        {
            Checker.IsSquare(A);
            int m = A.rows;
            Matrix temp = new Matrix(1, 2 * m);
            Matrix AE = Cat(2, A, Eye(m));
            Matrix inv = new Matrix(m);
            int k;

            for (int n = 1; n <= m; n++)
            {
                k = n;
                for (int i = n + 1; i <= m; i++)
                {
                    if (Math.Abs(AE[i, n]) > Math.Abs(AE[k, n]))
                        k = i;
                }
                if (Math.Abs(AE[k, n]) < LibData.Eps)
                    throw new ArithmeticException(Resources.Strings.Singular);

                if (n != k)
                    for (int j = n; j <= 2 * m; j++)
                    {
                        temp[1, j] = AE[n, j];
                        AE[n, j] = AE[k, j];
                        AE[k, j] = temp[1, j];
                    }

                for (int j = n + 1; j <= 2 * m; j++)
                    AE[n, j] = AE[n, j] / AE[n, n];

                for (int i = n + 1; i <= m; i++)
                    for (int j = n + 1; j <= 2 * m; j++)
                        AE[i, j] = -AE[i, n] * AE[n, j] + AE[i, j];

                for (int i = 1; i < n; i++)
                    for (int j = n + 1; j <= 2 * m; j++)
                        AE[i, j] = -AE[i, n] * AE[n, j] + AE[i, j];
            }

            for (int i = 1; i <= m; i++)
                for (int j = 1; j <= m; j++)
                    inv[i, j] = AE[i, j + m];
            return inv;
        }

        ///<summary>
        /// Matrix Pseudoinverse. 
        ///</summary>
        ///<param name="A">A matrix.</param>
        ///<returns> The Pseudoinverse of matrix <paramref name="A"/>.</returns>
        public static Matrix Pinv(Matrix A)
        {
            int m = A.rows;
            int n = A.columns;
            int rk;
            Matrix Ar = Rref(A, out rk);
            Matrix buf;

            if (rk == m && m == n)
                return Inv(A);

            else if (rk == m)
            {
                buf = Transpose(A);
                return buf * Inv(A * buf);
            }

            else if (rk == n)
            {
                buf = Transpose(A);
                return Inv(buf * A) * buf;
            }

            else if (rk == 0)
                return Zeros(n, m);

            else
            {
                int ind = 0;
                Matrix B = new Matrix(m, rk);
                Matrix C;
                for (int i = 1; i <= m; i++)
                {
                    for (int j = i; j <= n; j++)
                        if (Ar[i, j] > 0.0)//first non zero number is 1.
                        {
                            ind++;
                            B[1, m, ind, ind] = A[1, m, j, j];
                            break;
                        }
                    if (ind == rk)
                        break;
                }
                C = Ar[1, rk, 1, n];
                buf = Transpose(B);
                Ar = Transpose(C);

                return (Ar * Inv(C * Ar)) * (Inv(buf * B) * buf);
            }
        }

        #endregion

        #region Normed Space Analysis

        /// <summary>
        /// Spectral radius of a square matrix.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The Spectral radius of square matrix <paramref name="A"/>.</returns>     
        public static double Sprad(Matrix A)
        {
            return Complex.Abs(EigValue(A)[0]);
        }

        /// <summary>
        /// Matrix norm.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <param name="p"><paramref name="p"/>=1 :the 1-th norm.<paramref name="p"/>=2 :the 2-th norm.
        /// <paramref name="p"/>='I' :the infinity norm.<paramref name="p"/>='F' :the Frobenius norm.
        /// </param>
        /// <returns>The <paramref name="p"/> norm of <paramref name="A"/>.</returns> 
        /// <exception cref="ArgumentException">Throw if <paramref name="p"/> is invalid arguments.</exception>
        public static double Norm(Matrix A, int p)
        {
            int m = A.rows;
            int n = A.columns;

            switch (p)
            {
                case 1:
                    return Sum(Abs(A)).elements.Max();
                case 2:
                    if (m == 1 || n == 1)
                        return A.elements.Norm();
                    return SV(A)[0];

                case 'I':
                    if (m == 1 || n == 1)
                        return MArray.NormInf(A.elements);
                    return Sum(Abs(Transpose(A))).elements.Max();
                case 'F':
                    return Math.Sqrt(MArray.Pow(A.elements, 2).Sum());
                default: throw new ArgumentException(Resources.Strings.Norm);
            }
        }

        /// <summary>
        ///  Condition number with respect to inversion.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <param name="p"><paramref name="p"/> = 1, 2, 'I', or 'F'. </param>
        /// <returns>The condition number of matrix <paramref name="A"/> in p-norm.</returns>    
        /// <exception cref="ArgumentException">Throw if <paramref name="p"/> is invalid arguments.or <paramref name="A"/> is not square(except <paramref name="p"/>=2).</exception>
        public static double Cond(Matrix A, int p)
        {
            switch (p)
            {
                case 1: return Norm(Inv(A), 1) * Norm(A, 1);
                case 2: double[] sv = SV(A); return sv.Max() / sv.Min();
                case 'I': return Norm(Inv(A), 'I') * Norm(A, 'I');
                case 'F': return Norm(Inv(A), 'F') * Norm(A, 'F');
                default: throw new ArgumentException(Resources.Strings.Norm);
            }
        }

        /// <summary>    
        ///  Reciprocal condition estimator. If A is well conditioned, Rcond(A)is near 1.0. else Rcond(A) is near 0.0.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns>The reciprocal condition of <paramref name="A"/>.</returns>
        public static double Rcond(Matrix A)
        {
            return 1.0 / Cond(A, 1);
        }

        #endregion

        #region System Of Linear Equations

        /// <summary>
        ///  Null space. Null(A) is an basis for the null space of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns>An basis for the null space of <paramref name="A"/>.</returns>
        /// <exception cref="ArithmeticException">Throw if <paramref name="A"/> is full rank.</exception>
        public static Matrix Null(Matrix A)
        {
            Matrix X = Eye(A.columns) - Pinv(A) * A;
            int rk = (int)Math.Round(Matrix.Trace(X));
            if (rk == 0)
                throw new ArithmeticException(string.Format(" Empty matrix: {0}-by-0.", A.rows));

            int k = 1;
            Matrix temp;
            Matrix N = new Matrix(A.columns, rk);
            for (int j = 1; j <= A.columns; j++)
            {
                if (k <= rk && X[1, A.columns, j, j].elements.NonZeroVector())
                {
                    temp = X[1, A.columns, j, j];
                    N[1, A.columns, k, k] = temp / temp.elements.Norm();
                    k++;
                }
            }
            return N;
        }

        /// <summary>
        ///  Linsolve Solve linear system <paramref name="A"/>*x=<paramref name="b"/>.It use pseudo-inverse to solve all kinds of linear equations.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <param name="b">A row or column matrix.</param>
        /// <returns>The solution of A*X=b. </returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not square or the dimensions of <paramref name="A"/> 
        /// and <paramref name="b"/> is not agree.</exception>
        public static Matrix LinSolve(Matrix A, Matrix b)
        {
            if (b.rows == 1)
                b = Vectorize(b);

            if (A.rows != b.rows)
                throw new ArgumentException(Resources.Strings.Solve);

            return Pinv(A) * b;
        }

        /// <summary>
        /// LUSolve use LU decomposition Solve linear system <paramref name="A"/>*x=<paramref name="b"/>. <paramref name="A"/> must be square and nonsingular.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <param name="b">A row or column matrix.</param>
        /// <returns>The solution of A*X=b. </returns>
        ///<exception cref="ArgumentException">Throw if <paramref name="A"/> is not square or the dimensions of <paramref name="A"/> 
        ///and <paramref name="b"/> is not agree.</exception>
        /// <exception cref="ArithmeticException">Throw if <paramref name="A"/> is singular. </exception>
        public static Matrix LUSolve(Matrix A, Matrix b)
        {
            Checker.IsSquare(A);

            Matrix vb = new Matrix(b);
            if (b.rows == 1)
                vb = Vectorize(b);

            int m = A.rows;
            if (m != vb.rows)
                throw new ArgumentException(Resources.Strings.Solve);

            Matrix[] LUP = LU(A);
            Matrix L = LUP[0];
            Matrix U = LUP[1];

            vb = LUP[2] * vb;
            Matrix x = new Matrix(m, 1);
            double md;
            for (int i = 1; i <= m; i++)
            {
                md = 0.0;
                for (int k = 1; k < i; k++)
                    md += L[i, k] * x[k, 1];
                x[i, 1] = vb[i, 1] - md;
            }
            for (int i = m; i > 0; i--)
            {
                md = 0.0;
                for (int k = i + 1; k <= m; k++)
                    md += U[i, k] * x[k, 1];

                if (Math.Abs(U[i, i]) < LibData.Eps)
                    throw new ArithmeticException(Resources.Strings.Singular);

                x[i, 1] = (x[i, 1] - md) / U[i, i];
            }
            return x;
        }

        /// <summary>
        /// CholSolve use Cholesky decomposition Solve linear system <paramref name="A"/>*x=<paramref name="b"/>. <paramref name="A"/> must be positive definite symmetric.
        /// </summary>
        /// <param name="A">A positive definite symmetric.</param>
        /// <param name="b">A row or column matrix.</param>
        /// <returns>The solution of A*X=b.</returns>
        /// <exception cref="ArgumentException">Throw if the dimensions of <paramref name="A"/> and <paramref name="b"/> is not agree.</exception>
        /// <exception cref="ArithmeticException">Throw if <paramref name="A"/> is not positive definite symmetric. </exception>
        public static Matrix CholSolve(Matrix A, Matrix b)
        {
            Checker.IsSymmetric(A);
            int m = A.rows;

            if (b.rows == 1)
                b = Vectorize(b);

            if (m != b.rows)
                throw new ArgumentException(Resources.Strings.Solve);

            Matrix[] LD = Chol(A);
            Matrix L = LD[0];
            Matrix D = LD[1];
            Matrix x = new Matrix(m, 1);
            double md;

            for (int i = 1; i <= m; i++)
            {
                md = 0.0;
                for (int k = 1; k < i; k++)
                    md += L[i, k] * x[k, 1];
                x[i, 1] = b[i, 1] - md;
            }
            for (int i = m; i > 0; i--)
            {
                md = 0.0;
                for (int k = i + 1; k <= m; k++)
                    md += L[k, i] * x[k, 1];

                if (Math.Abs(D[i, i]) < LibData.Eps)
                    throw new ArithmeticException(Resources.Strings.PosDef);

                x[i, 1] = x[i, 1] / D[i, i] - md;
            }
            return x;
        }

        /// <summary>
        /// FollowUpSolve use follow up method to Solve linear system A*X=b. A is a tridiagonal matrix.
        /// </summary>
        /// <param name="A">A tridiagonal matrix.</param>
        /// <param name="b">A column or row matrix.</param>
        /// <returns>The solution of A*X=b.</returns>
        /// <exception cref="ArgumentException">Throw if Matrix <paramref name="A"/> is not a tridiagonal matrix 
        /// or dementions of <paramref name="A"/> and <paramref name="b"/> is not agree.</exception>
        public static Matrix FollowUpSolve(Matrix A, Matrix b)
        {
            Checker.IsTridiag(A);
            int m = A.rows;

            if (b.rows == 1)
                b = Vectorize(b);

            if (m != b.rows)
                throw new ArgumentException(Resources.Strings.Solve);

            double[] L = new double[m];
            double[] U = new double[m];
            double[] x = new double[m];

            U[0] = A[1, 1];
            for (int i = 2; i <= m; i++)
            {
                L[i - 1] = A[i, i - 1] / U[i - 2];//////Maye divide by zero,later i will test to verify this.
                U[i - 1] = A[i, i] - A[i - 1, i] * L[i - 1];
            }

            x[0] = b[1, 1];
            for (int i = 2; i <= m; i++)
                x[i - 1] = b[i, 1] - L[i - 1] * x[i - 2];

            x[m - 1] = x[m - 1] / U[m - 1];//////Maye divide by zero,later i will test to verify this.
            for (int i = m - 1; i > 0; i--)
                x[i - 1] = (x[i - 1] - A[i, i + 1] * x[i]) / U[i - 1];

            return new Matrix(m, 1, x);
        }

        /// <summary>
        /// GSSolve use Gauss–Seidel iteration method to Solve linear system A*X=b. 
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <param name="b">A column or row matrix.</param>
        /// <returns>The solution of A*X=b.</returns>
        /// <exception cref="ArgumentException">Throw if Matrix <paramref name="A"/> is not a square matrix 
        /// or dementions of <paramref name="A"/> and <paramref name="b"/> is not agree.</exception>
        /// <exception cref="ArithmeticException">Throw if converge is not reached.</exception>
        public static Matrix GSSolve(Matrix A, Matrix b)
        {
            Checker.IsSquare(A);
            int n = A.columns;

            if (n != b.elements.Length)
                throw new ArgumentException(Resources.Strings.Solve);

            int k;
            double buf;
            double[] vb = new double[n];
            b.elements.CopyTo(vb, 0);
            Matrix Acpy = new Matrix(A);
            Matrix temp = new Matrix(1, n);

            for (int i = 1; i <= n; i++)
            {
                k = i;
                for (int r = i + 1; r <= n; r++)
                    if (Math.Abs(Acpy[r, i]) > Math.Abs(Acpy[k, i]))
                        k = r;
                if (Math.Abs(Acpy[k, i]) < LibData.Eps)
                    throw new ArithmeticException(Resources.Strings.Singular);

                if (i != k)
                {
                    for (int j = 1; j <= n; j++)
                    {
                        temp[1, j] = Acpy[i, j];
                        Acpy[i, j] = Acpy[k, j];
                        Acpy[k, j] = temp[1, j];
                    }

                    buf = vb[i - 1];
                    vb[i - 1] = vb[k - 1];
                    vb[k - 1] = buf;
                }
            }

            double[] xk = MArray.Rand(n);
            double[] xn = new double[n];
            bool it = true;

            while (it)
            {
                for (int i = 1; i <= n; i++)
                {
                    buf = 0.0;
                    for (int j = 1; j < i; j++)
                        buf += Acpy[i, j] * xk[j - 1];
                    for (int j = i + 1; j <= n; j++)
                        buf += Acpy[i, j] * xk[j - 1];

                    if (double.IsInfinity(buf))
                        throw new ArithmeticException(Resources.Strings.Converge);

                    xn[i - 1] = (vb[i - 1] - buf) / Acpy[i, i];
                }

                for (int i = 1; i <= n; i++)
                {
                    buf = 0.0;
                    for (int j = 1; j < i; j++)
                        buf += Acpy[i, j] * xn[j - 1];
                    for (int j = i + 1; j <= n; j++)
                        buf += Acpy[i, j] * xn[j - 1];
                    xk[i - 1] = (vb[i - 1] - buf) / Acpy[i, i];
                }

                for (int i = 0; i < n; i++)
                {
                    if (Math.Abs(xn[i] - xk[i]) > LibData.Eps)
                        break;
                    else
                        it = false;
                }
            }

            return new Matrix(n, 1, xn);
        }

        /// <summary>
        /// SORSolve use Successive over-relaxation method to Solve linear system A*X=b.
        /// <paramref name="w"/> is between 0 and 2 is called the relaxation factor.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <param name="b">A column or row matrix.</param>
        /// <param name="w">The relaxation factor,the range is 0~2.</param>
        /// <returns>The solution of A*X=b.</returns>
        /// <exception cref="ArgumentException">Throw if Matrix <paramref name="A"/> is not a square matrix 
        /// or dementions of,and if <paramref name="w"/> is not at the range of 0~2,<paramref name="A"/> and <paramref name="b"/> is not agree.</exception>
        /// <exception cref="ArithmeticException">Throw if converge is not reached or <paramref name="A"/> is singular.</exception>
        public static Matrix SORSolve(Matrix A, Matrix b, double w)
        {
            if (w < 0 || w > 2)
                throw new ArgumentException(Resources.Strings.RelFac);

            Checker.IsSquare(A);
            int n = A.columns;

            if (n != b.elements.Length)
                throw new ArgumentException(Resources.Strings.Solve);

            int k;
            double buf;
            double[] vb = new double[n];
            b.elements.CopyTo(vb, 0);
            Matrix Acpy = new Matrix(A);
            Matrix temp = new Matrix(1, n);

            for (int i = 1; i <= n; i++)
            {
                k = i;
                for (int r = i + 1; r <= n; r++)
                    if (Math.Abs(Acpy[r, i]) > Math.Abs(Acpy[k, i]))
                        k = r;
                if (Math.Abs(Acpy[k, i]) < LibData.Eps)
                    throw new ArithmeticException(Resources.Strings.Singular);

                if (i != k)
                {
                    for (int j = 1; j <= n; j++)
                    {
                        temp[1, j] = Acpy[i, j];
                        Acpy[i, j] = Acpy[k, j];
                        Acpy[k, j] = temp[1, j];
                    }

                    buf = vb[i - 1];
                    vb[i - 1] = vb[k - 1];
                    vb[k - 1] = buf;
                }
            }

            double[] xk = MArray.Rand(n);
            double[] xn = new double[n];
            bool it = true;

            while (it)
            {
                for (int i = 1; i <= n; i++)
                {
                    buf = 0.0;
                    for (int j = 1; j < i; j++)
                        buf += Acpy[i, j] * xk[j - 1];
                    for (int j = i + 1; j <= n; j++)
                        buf += Acpy[i, j] * xk[j - 1];

                    if (double.IsInfinity(buf))
                        throw new ArithmeticException(Resources.Strings.Converge);

                    xn[i - 1] = (1 - w) * xn[i - 1] + w * (vb[i - 1] - buf) / Acpy[i, i];
                }

                for (int i = 1; i <= n; i++)
                {
                    buf = 0.0;
                    for (int j = 1; j < i; j++)
                        buf += Acpy[i, j] * xn[j - 1];
                    for (int j = i + 1; j <= n; j++)
                        buf += Acpy[i, j] * xn[j - 1];
                    xk[i - 1] = (1 - w) * xk[i - 1] + w * (vb[i - 1] - buf) / Acpy[i, i];
                }

                for (int i = 0; i < n; i++)
                {
                    if (Math.Abs(xn[i] - xk[i]) > LibData.Eps)
                        break;
                    else
                        it = false;
                }
            }

            return new Matrix(n, 1, xn);
        }

        #endregion

        #region Matrix Transformation

        /// <summary>
        /// Householder transformation.  Atention: ColMat is a column matrix !
        /// Before use it,Check if ColMat is close to a zero vector.
        /// </summary>
        internal static Matrix House(Matrix ColMat)
        {
            int n = ColMat.rows;

            ColMat[1, 1] += ElMath.Sign(ColMat[1, 1]) * ColMat.elements.Norm();
            ColMat = ColMat / ColMat.elements.Norm();

            Matrix wwT = new Matrix(n);
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= i; j++)
                    wwT[i, j] = ColMat.elements[i - 1] * ColMat.elements[j - 1];

            for (int j = 1; j <= n; j++)
                for (int i = 1; i < j; i++)
                    wwT[i, j] = wwT[j, i];

            return Eye(n) - 2 * wwT;
        }

        private static Matrix House(Matrix ColMat, out double sign_norm)
        {
            int n = ColMat.rows;

            sign_norm = ElMath.Sign(ColMat[1, 1]) * ColMat.elements.Norm();
            ColMat[1, 1] += sign_norm;
            ColMat = ColMat / ColMat.elements.Norm();

            Matrix wwT = new Matrix(n);
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= i; j++)
                    wwT[i, j] = ColMat.elements[i - 1] * ColMat.elements[j - 1];

            for (int j = 1; j <= n; j++)
                for (int i = 1; i < j; i++)
                    wwT[i, j] = wwT[j, i];

            return Eye(n) - 2 * wwT;
        }

        /// <summary>
        /// Givens Transformation.
        /// </summary>
        internal static double[] Givens(double x, double y)
        {
            double a = ElMath.Hypot(x, y);
            if (a == 0)
                return new double[3];
            double c = x / a;
            double s = y / a;
            return new double[3] { c, s, a };
        }

        /// <summary>
        /// Bidiagonalization for the SVD use Householder reflection. B=U'*A*V,A is a matrix of m*n (m>n) 
        /// B is a Bidiagonal matrix,and U,V are orthogonal matries.
        /// </summary>
        /// <param name="A">A matrix that rows greater than columns.</param>
        /// <returns>The Bidiagonalized matrix.</returns>
        /// <exception cref=" ArgumentException">Throw if rows is less than columns.</exception>
        internal static Matrix[] Bidiag_house(Matrix A)
        {
            int m = A.rows;
            int n = A.columns;
            if (m < n)
                throw new ArgumentException(Resources.Strings.Bidiag);

            Matrix D = new Matrix(A);
            Matrix U = Eye(m);
            Matrix V = Eye(n);
            Matrix w;
            Matrix h;

            for (int i = 1; i <= n; i++)
            {
                w = D[i, m, i, i];

                if (w.elements.NonZeroVector())
                {
                    h = House(w);
                    D[i, m, 1, n] = h * D[i, m, 1, n];
                    U[1, m, i, m] *= h;
                }

                if (i < n - 1)
                {
                    w = Transpose(D[i, i, i + 1, n]);
                    if (w.elements.NonZeroVector())
                    {
                        h = House(w);
                        D[1, m, i + 1, n] = D[1, m, i + 1, n] * h;
                        V[1, n, i + 1, n] *= h;
                    }
                }
            }
            return new Matrix[3] { U, D, V };
        }

        /// <summary>
        /// Bidiagonalization for the SVD use Givens rptation. B=U'*A*V,A is a matrix of m*n (m>n) 
        /// B is a Bidiagonal matrix,and U,V are orthogonal matries.
        /// </summary>
        /// <param name="A">A matrix that rows greater than columns.</param>
        /// <returns>The Bidiagonalized matrix.</returns>
        /// <exception cref=" ArgumentException">Throw if rows is less than columns.</exception>
        public static Matrix[] Bidiag(Matrix A)
        {
            int m = A.rows;
            int n = A.columns;
            if (m < n)
                throw new ArgumentException(Resources.Strings.Bidiag);

            Matrix B = new Matrix(A);
            Matrix U = Eye(m);
            Matrix V = Eye(n);

            double md;
            double[] cs;

            for (int i = 1; i < n; i++)
            {
                for (int r = i + 1; r <= m; r++)
                    if (B[r, i] != 0)
                    {
                        cs = Givens(B[i, i], B[r, i]);
                        for (int k = i + 1; k <= n; k++)
                        {
                            md = B[i, k];
                            B[i, k] = cs[0] * md + cs[1] * B[r, k];
                            B[r, k] = -cs[1] * md + cs[0] * B[r, k];
                        }
                        B[i, i] = cs[2];
                        B[r, i] = 0;
                        for (int k = 1; k <= m; k++)
                        {
                            md = U[k, i];
                            U[k, i] = cs[0] * md + cs[1] * U[k, r];
                            U[k, r] = -cs[1] * md + cs[0] * U[k, r];
                        }
                    }

                for (int r = i + 2; r <= n; r++)
                    if (B[i, r] != 0)
                    {
                        cs = Givens(B[i, i + 1], B[i, r]);
                        for (int k = i + 1; k <= m; k++)
                        {
                            md = B[k, i + 1];
                            B[k, i + 1] = cs[0] * md + cs[1] * B[k, r];
                            B[k, r] = -cs[1] * md + cs[0] * B[k, r];
                        }
                        B[i, i + 1] = cs[2];
                        B[i, r] = 0;
                        for (int k = 1; k <= n; k++)
                        {
                            md = V[k, i + 1];
                            V[k, i + 1] = cs[0] * md + cs[1] * V[k, r];
                            V[k, r] = -cs[1] * md + cs[0] * V[k, r];
                        }
                    }
            }

            for (int r = n + 1; r <= m; r++)
                if (B[r, n] != 0)
                {
                    cs = Givens(B[n, n], B[r, n]);
                    B[r, n] = 0;
                    B[n, n] = cs[2];
                    for (int k = 1; k <= m; k++)
                    {
                        md = U[k, n];
                        U[k, n] = cs[0] * md + cs[1] * U[k, r];
                        U[k, r] = -cs[1] * md + cs[0] * U[k, r];
                    }
                }

            return new Matrix[3] { U, B, V };
        }


        /// <summary>
        /// Bidiagonalization for the SVD. Whithout calculate U and V.
        /// </summary>
        private static Matrix Bidiag_NoUV(Matrix A)
        {
            int m = A.rows;
            int n = A.columns;

            Matrix B = new Matrix(A);

            double md;
            double[] cs;

            for (int i = 1; i < n; i++)
            {
                for (int r = i + 1; r <= m; r++)
                    if (B[r, i] != 0)
                    {
                        cs = Givens(B[i, i], B[r, i]);
                        for (int k = i + 1; k <= n; k++)
                        {
                            md = B[i, k];
                            B[i, k] = cs[0] * md + cs[1] * B[r, k];
                            B[r, k] = -cs[1] * md + cs[0] * B[r, k];
                        }
                        B[i, i] = cs[2];
                        B[r, i] = 0;
                    }

                for (int r = i + 2; r <= n; r++)
                    if (B[i, r] != 0)
                    {
                        cs = Givens(B[i, i + 1], B[i, r]);
                        for (int k = i + 1; k <= m; k++)
                        {
                            md = B[k, i + 1];
                            B[k, i + 1] = cs[0] * md + cs[1] * B[k, r];
                            B[k, r] = -cs[1] * md + cs[0] * B[k, r];
                        }
                        B[i, i + 1] = cs[2];
                        B[i, r] = 0;
                    }
            }

            for (int r = n + 1; r <= m; r++)
                if (B[r, n] != 0)
                {
                    cs = Givens(B[n, n], B[r, n]);
                    B[r, n] = 0;
                    B[n, n] = cs[2];
                }

            return B;
        }

        /// <summary>
        /// Reduce to Upper Hessenberg form use Householder reflection. this routine is for QR iteration.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns>The Upper Hessenberg form.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        internal static Matrix[] Hess_house(Matrix A)
        {
            Checker.IsSquare(A);
            int n = A.rows;

            Matrix H = new Matrix(A);
            Matrix U = Eye(n);
            Matrix h;
            Matrix v;

            for (int i = 2; i < n; i++)
            {
                v = H[i, n, i - 1, i - 1];
                if (v.elements.NonZeroVector())
                {
                    h = House(v);
                    H[i, i - 1] = (h[1, 1, 1, h.rows] * H[i, n, i - 1, i - 1]).elements[0];
                    for (int k = i + 1; k <= n; k++)
                        H[k, i - 1] = 0.0;

                    H[1, i - 1, i, n] = H[1, i - 1, i, n] * h;
                    H[i, n, i, n] = h * H[i, n, i, n] * h;

                    U[1, n, i, n] *= h;
                }
            }
            return new Matrix[2] { U, H };
        }

        /// <summary>
        /// Reduce to Upper Hessenberg form use Givens rotation. this routine is for QR iteration.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns>The Upper Hessenberg form.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        public static Matrix[] Hess(Matrix A)
        {
            Checker.IsSquare(A);
            int n = A.rows;

            Matrix H = new Matrix(A);
            Matrix U = Eye(n);

            double[] cs;
            double md;

            for (int i = 2; i < n; i++)
                for (int r = i + 1; r <= n; r++)
                    if (H[r, i - 1] != 0)
                    {
                        cs = Givens(H[i, i - 1], H[r, i - 1]);

                        for (int k = i; k <= n; k++)
                        {
                            md = H[i, k];
                            H[i, k] = cs[0] * md + cs[1] * H[r, k];
                            H[r, k] = -cs[1] * md + cs[0] * H[r, k];
                        }
                        H[i, i - 1] = cs[2];
                        H[r, i - 1] = 0;
                        for (int k = 1; k <= n; k++)
                        {
                            md = H[k, i];
                            H[k, i] = cs[0] * md + cs[1] * H[k, r];
                            H[k, r] = -cs[1] * md + cs[0] * H[k, r];
                        }

                        for (int k = 1; k <= n; k++)
                        {
                            md = U[k, i];
                            U[k, i] = cs[0] * md + cs[1] * U[k, r];
                            U[k, r] = -cs[1] * md + cs[0] * U[k, r];
                        }
                    }

            return new Matrix[2] { U, H };
        }

        #endregion

        #region Matrix Decomposition

        /// <summary>
        /// LU decomposition with pivoting use Doolittle algorithm.
        /// P*A=L*U;L is an unit lower triangular matrix and U is an Upper triangular matrix,then P is a permutation matrix.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The decomposition of <paramref name="A"/>,returns L,U,P.</returns>
        ///<remarks>Warning: When i test this method,I found that when give a matrix that rows greater than columns,this method gives me a wrong solution.
        /// Here is aexample i found: Matrix A = new Matrix(6, 2, 4, 3, 8, 6, 6, 1, 7, 7, 2, 0, 5, 6). I don't know how to fix this LU algorithm until now.
        ///</remarks>
        public static Matrix[] LU(Matrix A)
        {
            int m = A.rows;
            int n = A.columns;

            Matrix L = Eye(m);
            Matrix U = new Matrix(m, n);
            Matrix P = Eye(m);
            Matrix temp = new Matrix(1, n);
            Matrix tP = new Matrix(1, m);
            Matrix Acpy = new Matrix(A);

            int min = m < n ? m : n;
            int maxind;
            double md;
            double aii;

            for (int i = 1; i <= min; i++)
            {
                md = 0.0;
                for (int k = 1; k < i; k++)
                    md += Acpy[i, k] * Acpy[k, i];

                aii = Acpy[i, i] - md;
                maxind = i;
                for (int r = i + 1; r <= min; r++)
                {
                    md = 0.0;
                    for (int k = 1; k < i; k++)
                        md += Acpy[r, k] * Acpy[k, i];

                    md = Acpy[r, i] - md;
                    if (Math.Abs(md) > Math.Abs(aii))
                    {
                        maxind = r;
                        aii = md;
                    }
                }

                if (Math.Abs(aii) > LibData.Eps)
                {
                    if (i != maxind)
                    {
                        for (int j = 1; j <= n; j++)
                        {
                            temp[1, j] = Acpy[i, j];
                            Acpy[i, j] = Acpy[maxind, j];
                            Acpy[maxind, j] = temp[1, j];
                        }

                        for (int j = 1; j <= m; j++)
                        {
                            tP[1, j] = P[i, j];
                            P[i, j] = P[maxind, j];
                            P[maxind, j] = tP[1, j];
                        }
                    }

                    Acpy[i, i] = aii;
                    for (int j = i + 1; j <= n; j++)
                    {
                        md = 0.0;
                        for (int k = 1; k < i; k++)
                            md += Acpy[i, k] * Acpy[k, j];
                        Acpy[i, j] = Acpy[i, j] - md;
                    }

                    for (int j = i + 1; j <= m; j++)
                    {
                        md = 0.0;
                        for (int k = 1; k < i; k++)
                            md += Acpy[j, k] * Acpy[k, i];
                        Acpy[j, i] = (Acpy[j, i] - md) / Acpy[i, i];
                    }
                }
            }

            for (int i = 1; i <= m; i++)
                for (int j = 1; j < i && j <= n; j++)
                    L[i, j] = Acpy[i, j];
            for (int j = 1; j <= n; j++)
                for (int i = 1; i <= j && i <= m; i++)
                    U[i, j] = Acpy[i, j];

            return new Matrix[3] { L, U, P };
        }

        /// <summary>
        /// LU decomposition without pivoting use Doolittle algorithm.
        /// P*A=L*U;L is an unit lower triangular matrix and U is an Upper triangular matrix.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The decomposition of <paramref name="A"/>,returns L,U.</returns>
        /// <exception cref="ArithmeticException">Throw when LU decomposition whithout pivoting does not exist.</exception>
        public static Matrix[] lu(Matrix A)
        {
            int m = A.rows;
            int n = A.columns;

            Matrix L = Eye(m);
            Matrix U = new Matrix(m, n);
            Matrix Acpy = new Matrix(A);

            int min = m < n ? m : n;
            double md;

            for (int i = 1; i <= min; i++)
            {
                for (int j = i; j <= n; j++)
                {
                    md = 0.0;
                    for (int k = 1; k < i; k++)
                        md += Acpy[i, k] * Acpy[k, j];
                    Acpy[i, j] = Acpy[i, j] - md;
                }

                if (Math.Abs(Acpy[i, i]) < LibData.Eps)
                    throw new ArithmeticException(Resources.Strings.LU);

                for (int j = i + 1; j <= m; j++)
                {
                    md = 0.0;
                    for (int k = 1; k < i; k++)
                        md += Acpy[j, k] * Acpy[k, i];
                    Acpy[j, i] = (Acpy[j, i] - md) / Acpy[i, i];
                }
            }

            for (int i = 1; i <= m; i++)
                for (int j = 1; j < i && j <= n; j++)
                    L[i, j] = Acpy[i, j];
            for (int j = 1; j <= n; j++)
                for (int i = 1; i <= j && i <= m; i++)
                    U[i, j] = Acpy[i, j];

            return new Matrix[2] { L, U };
        }

        /// <summary>
        ///  Cholesky factorization. 
        ///  A=L*D*L';L is an unit lower triangular matrix and D is a diagonal matrix.
        /// </summary>
        /// <param name="A">A positive definite matrix.</param>
        /// <returns>The Cholesky factorization of <paramref name="A"/>.returns L,D.</returns>
        /// <exception cref="ArithmeticException">Throw if <paramref name="A"/> is not a positive definite matrix.</exception>
        public static Matrix[] Chol(Matrix A)
        {
            Checker.IsSquare(A);
            int n = A.rows;

            Matrix D = Eye(n);
            Matrix L = new Matrix(n);
            double md = 0.0;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    if (A[i, j] != A[j, i])
                        throw new ArithmeticException(Resources.Strings.IsSymetric);

                    md = 0.0;
                    for (int k = 1; k < j; k++)
                        md += L[i, k] * L[j, k] / L[k, k];
                    L[i, j] = A[i, j] - md;
                }
                D[i, i] = L[i, i];
                if (Math.Abs(L[i, i]) < LibData.Eps)
                    throw new ArithmeticException(Resources.Strings.PosDef);
            }

            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= i; j++)
                    L[i, j] = L[i, j] / D[j, j];

            return new Matrix[2] { L, D };
        }

        /// <summary>
        ///  Cholesky factorization use square-root method.
        ///  A=L*L';L is an lower triangular matrix.
        /// </summary>
        /// <param name="A">A positive definite matrix.</param>
        /// <returns>The Cholesky factorization of <paramref name="A"/>,returns L.</returns>
        /// <exception cref="ArithmeticException">Throw if <paramref name="A"/> is not a positive definite matrix.</exception>    
        public static Matrix chol(Matrix A)
        {
            Checker.IsSquare(A);
            int n = A.rows;

            Matrix L = new Matrix(n);
            double md;

            for (int i = 1; i <= n; i++)
            {
                md = 0.0;
                for (int k = 1; k < i; k++)
                    md += L[i, k] * L[i, k];
                md = A[i, i] - md;
                if (md < LibData.Eps)
                    throw new ArithmeticException(Resources.Strings.PosDef);
                L[i, i] = Math.Sqrt(md);

                for (int j = i + 1; j <= n; j++)
                {
                    if (A[i, j] != A[j, i])
                        throw new ArithmeticException(Resources.Strings.IsSymetric);

                    md = 0.0;
                    for (int k = 1; k < i; k++)
                        md += L[i, k] * L[j, k];
                    L[j, i] = (A[i, j] - md) / L[i, i];
                }
            }
            return L;
        }

        /// <summary>
        /// Orthogonal-triangular(QR) decomposition use Householder method.
        /// where A is m-by-n, produces an m-by-n upper triangular matrix R and an m-by-m Orthogonal matrix Q so that A = Q*R.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>Orthogonal-triangular decomposition of <paramref name="A"/>,returns Q,R.</returns>
        public static Matrix[] QRhouse(Matrix A)
        {
            int row = A.rows;
            int col = A.columns;
            int min = row < col ? row : col;

            Matrix R = new Matrix(A);
            Matrix Q = Eye(row);
            Matrix v;

            Matrix h;

            double sign_norm;

            for (int i = 1; i <= min; i++)
            {
                v = R[i, row, i, i];
                if (v.elements.NonZeroVector())
                {
                    h = House(v, out sign_norm);

                    R[i, i] = -sign_norm;
                    for (int r = i + 1; r <= row; r++)
                        R[r, i] = 0.0;
                    for (int j = i + 1; j <= col; j++)
                        R[i, row, j, j] = h * R[i, row, j, j];

                    Q[1, row, i, row] *= h;
                }
            }
            return new Matrix[2] { Q, R };
        }

        /// <summary>
        /// Orthogonal-triangular(QR) decomposition use Givens method.
        /// where A is m-by-n, produces an m-by-n upper triangular matrix R and an m-by-m Orthogonal matrix Q so that A = Q*R.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>Orthogonal-triangular decomposition of <paramref name="A"/>,returns Q,R.</returns>
        public static Matrix[] QR(Matrix A)
        {
            int row = A.rows;
            int col = A.columns;
            int min = row < col ? row : col;

            Matrix R = new Matrix(A);
            Matrix Q = Eye(row);

            double[] cs;
            double md;

            for (int i = 1; i <= min; i++)
            {
                for (int r = i + 1; r <= row; r++)
                    if (R[r, i] != 0)
                    {
                        cs = Givens(R[i, i], R[r, i]);
                        for (int k = i + 1; k <= col; k++)
                        {
                            md = R[i, k];
                            R[i, k] = cs[0] * md + cs[1] * R[r, k];
                            R[r, k] = -cs[1] * md + cs[0] * R[r, k];
                        }
                        R[i, i] = cs[2];
                        R[r, i] = 0;
                        for (int k = 1; k <= row; k++)
                        {
                            md = Q[k, i];
                            Q[k, i] = cs[0] * md + cs[1] * Q[k, r];
                            Q[k, r] = -cs[1] * md + cs[0] * Q[k, r];
                        }
                    }
            }
            return new Matrix[2] { Q, R };
        }

        /// <summary>
        /// Full rank decomposition. 
        /// where A is m-by-n, produces an m-by-r column full rank matrix cf and an r-by-n row full rank matrix rf.
        /// so that A = cf*rf.r is the rank of matrix <paramref name="A"/>.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The full rank decomposition of <paramref name="A"/>,returns cf,rf.</returns>
        public static Matrix[] FRD(Matrix A)
        {
            int m = A.rows;
            int n = A.columns;
            int rk;
            int ind = 0;
            Matrix Ar = Rref(A, out rk);
            if (rk == 0)
                throw new ArgumentException(Resources.Strings.Singular + string.Format("\n Empty matrix: {0}-by-0 and 0-by-{1}.", m, n));
            Console.WriteLine(Ar);
            Matrix cf = new Matrix(m, rk);
            Matrix rf;
            for (int i = 1; i <= m; i++)
            {
                for (int j = i; j <= n; j++)
                    if (Ar[i, j] > 0.1)//first non zero number is 1.
                    {
                        ind++;
                        cf[1, m, ind, ind] = A[1, m, j, j];
                        break;
                    }
                if (ind == rk)
                    break;
            }
            rf = Ar[1, rk, 1, n];
            return new Matrix[2] { cf, rf };
        }

        /// <summary>
        /// Computes wikinson's shift.
        /// </summary>
        internal static double wikinson(double h11, double h12, double h22)
        {
            double mid = h12 * h12;
            double p = 0.5 * (h11 - h22);
            double r = Math.Sqrt(p * p + mid);

            p += ElMath.Sign(p) * r;
            if (p == 0)
                return 0.0;

            double mu = h22 - (mid / p);
            return mu;
        }

        /// <summary>
        /// Computes wikinson's shift.
        /// </summary>
        private static double wikinson(Matrix h)
        {
            double mid = h[1, 2] * h[2, 1];
            double p = 0.5 * (h[1, 1] - h[2, 2]);

            double mu = p * p + mid;
            if (mu < 0)
                return h[2, 2];
            double r = Math.Sqrt(mu);

            p += ElMath.Sign(p) * r;
            if (p == 0)
                return 0.0;

            mu = h[2, 2] - (mid / p);
            return mu;
        }
        /// <summary>
        /// SVD routine.Argument <paramref name="A"/> must be a bidiagnoal matrix.
        /// </summary>
        /// <param name="A"> A bidiagonal matrix.</param>
        /// <returns>The svd of <paramref name="A"/>.</returns>
        internal static Matrix[] svd(Matrix A)
        {
            double x;
            double y;
            double[] cs;
            double mu;
            double md;
            int ind = 1;
            int k = A.columns;
            int dim = k;

            Matrix b = A[1, k, 1, k];
            Matrix u = Eye(k);
            Matrix v = Eye(k);

            while (k - ind >= 1)
            {
                if (Math.Abs(b[k - 1, k]) < LibData.Eps)
                {
                    b[k - 1, k] = 0;
                    k--;
                    continue;
                }

                if (Math.Abs(b[ind, ind + 1]) < LibData.Eps)
                {
                    b[ind, ind + 1] = 0;
                    ind++;
                    continue;
                }

                for (int i = ind; i <= k; i++)
                    if (b[i, i] == 0)
                        b[i, i] = 1.1E-100;

                if (k > 2)
                    mu = wikinson(ElMath.Square(b[k - 1, k - 1], b[k - 2, k - 1]), b[k - 1, k - 1] * b[k - 1, k], ElMath.Square(b[k, k], b[k - 1, k]));
                else
                    mu = 0;
                x = b[ind, ind] * b[ind, ind] - mu;
                y = b[ind, ind] * b[ind, ind + 1];

                if (y != 0)
                {
                    cs = Givens(x, y);

                    for (int r = ind; r <= ind + 1; r++)
                    {
                        md = b[r, ind];
                        b[r, ind] = cs[0] * md + cs[1] * b[r, ind + 1];
                        b[r, ind + 1] = -cs[1] * md + cs[0] * b[r, ind + 1];
                    }

                    for (int r = 1; r <= dim; r++)
                    {
                        md = v[r, ind];
                        v[r, ind] = cs[0] * md + cs[1] * v[r, ind + 1];
                        v[r, ind + 1] = -cs[1] * md + cs[0] * v[r, ind + 1];
                    }
                }

                for (int i = ind; i < k; i++)
                {
                    x = b[i, i];
                    y = b[i + 1, i];

                    if (y != 0)
                    {
                        cs = Givens(x, y);

                        for (int j = i + 1; j <= dim; j++)
                        {
                            md = b[i, j];
                            b[i, j] = cs[0] * md + cs[1] * b[i + 1, j];
                            b[i + 1, j] = -cs[1] * md + cs[0] * b[i + 1, j];
                        }
                        b[i, i] = cs[2];
                        b[i + 1, i] = 0;
                        for (int j = 1; j <= dim; j++)
                        {
                            md = u[j, i];
                            u[j, i] = cs[0] * md + cs[1] * u[j, i + 1];
                            u[j, i + 1] = -cs[1] * md + cs[0] * u[j, i + 1];
                        }
                    }

                    if (i != k - 1)
                    {
                        x = b[i, i + 1];
                        y = b[i, i + 2];

                        if (y != 0)
                        {
                            cs = Givens(x, y);

                            for (int r = i + 1; r <= dim; r++)
                            {
                                md = b[r, i + 1];
                                b[r, i + 1] = cs[0] * md + cs[1] * b[r, i + 2];
                                b[r, i + 2] = -cs[1] * md + cs[0] * b[r, i + 2];
                            }
                            b[i, i + 1] = cs[2];
                            b[i, i + 2] = 0;
                            for (int r = 1; r <= dim; r++)
                            {
                                md = v[r, i + 1];
                                v[r, i + 1] = cs[0] * md + cs[1] * v[r, i + 2];
                                v[r, i + 2] = -cs[1] * md + cs[0] * v[r, i + 2];
                            }
                        }
                    }
                }

                if (ind == k && ind != dim)
                {
                    if (Math.Abs(b[ind, ind + 1]) > LibData.Eps)
                        ind--;
                    else
                        b[ind, ind + 1] = 0;
                }
            }

            for (int i = dim; i > 0; i--)
            {
                if (b[i, i] > LibData.Eps)
                    break;
                else if (Math.Abs(b[i, i]) < LibData.Eps)
                    b[i, i] = 0;
                else if (b[i, i] < 0)
                {
                    b[i, i] = -b[i, i];
                    u[1, dim, i, i] = -1.0 * u[1, dim, i, i];
                    break;
                }
            }

            Matrix temp;
            for (int i = 1; i < dim; i++)
            {
                k = i;
                for (int j = i + 1; j <= dim; j++)
                    if (b[j, j] > b[k, k])
                        k = j;
                if (k != i)
                {
                    md = b[i, i];
                    b[i, i] = b[k, k];
                    b[k, k] = md;

                    temp = u[1, dim, i, i];
                    u[1, dim, i, i] = u[1, dim, k, k];
                    u[1, dim, k, k] = temp;

                    temp = v[1, dim, i, i];
                    v[1, dim, i, i] = v[1, dim, k, k];
                    v[1, dim, k, k] = temp;
                }
            }
            return new Matrix[3] { u, b, v };

        }

        /// <summary>
        /// SVD，Singular Value Decomposition.svd(A) produces a diagonal matrix S of the same   
        /// dimension as <paramref name="A"/> and with nonnegative diagonal elements in decreasing order, 
        /// and orthogonal matrices U and V so that  A = U*S*V'.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The Singular Value Decomposition of <paramref name="A"/>,returns U,S,V.</returns>
        public static Matrix[] SVD(Matrix A)
        {
            Matrix[] USV;
            if (A.rows < A.columns)
                USV = Bidiag(Transpose(A));
            else
                USV = Bidiag(A);
            Matrix U = USV[0];
            Matrix S = USV[1];
            Matrix V = USV[2];

            USV = svd(S);
            int m = S.rows;
            int n = S.columns;
            if (m == n)
                return new Matrix[3] { U * USV[0], USV[1], V * USV[2] };

            U[1, m, 1, n] *= USV[0];
            S = new Matrix(m, n);
            for (int i = 1; i <= n; i++)
                S[i, i] = USV[1][i, i];
            V *= USV[2];
            if (A.rows > A.columns)
                return new Matrix[3] { U, S, V };
            else
                return new Matrix[3] { V, Transpose(S), U };
        }

        /// <summary>
        /// SV routine. Compute the Singular Value of a matrix. 
        /// </summary>
        /// <param name="A">A bidiagonal matrix.</param>
        /// <returns>Returns the Singular Value of <paramref name="A"/>. </returns>
        internal static double[] sv(Matrix A)
        {
            double x;
            double y;
            double[] cs;
            double mu;
            double md;
            int ind = 1;
            int k = A.columns;
            int dim = k;
            double[] sv = new double[dim];
            Matrix b = A[1, k, 1, k];

            while (k - ind >= 1)
            {
                if (Math.Abs(b[k - 1, k]) < LibData.Eps)
                {
                    b[k - 1, k] = 0;
                    k--;
                    continue;
                }

                if (Math.Abs(b[ind, ind + 1]) < LibData.Eps)
                {
                    b[ind, ind + 1] = 0;
                    ind++;
                    continue;
                }

                for (int i = ind; i <= k; i++)
                    if (b[i, i] == 0)
                        b[i, i] = 1.1E-100;

                if (k > 2)
                    mu = wikinson(ElMath.Square(b[k - 1, k - 1], b[k - 2, k - 1]), b[k - 1, k - 1] * b[k - 1, k], ElMath.Square(b[k, k], b[k - 1, k]));
                else
                    mu = 0;
                x = b[ind, ind] * b[ind, ind] - mu;
                y = b[ind, ind] * b[ind, ind + 1];

                if (y != 0)
                {
                    cs = Givens(x, y);

                    for (int r = ind; r <= ind + 1; r++)
                    {
                        md = b[r, ind];
                        b[r, ind] = cs[0] * md + cs[1] * b[r, ind + 1];
                        b[r, ind + 1] = -cs[1] * md + cs[0] * b[r, ind + 1];
                    }
                }

                for (int i = ind; i < k; i++)
                {
                    x = b[i, i];
                    y = b[i + 1, i];

                    if (y != 0)
                    {
                        cs = Givens(x, y);

                        for (int j = i + 1; j <= dim; j++)
                        {
                            md = b[i, j];
                            b[i, j] = cs[0] * md + cs[1] * b[i + 1, j];
                            b[i + 1, j] = -cs[1] * md + cs[0] * b[i + 1, j];
                        }
                        b[i, i] = cs[2];
                        b[i + 1, i] = 0;
                    }

                    if (i != k - 1)
                    {
                        x = b[i, i + 1];
                        y = b[i, i + 2];

                        if (y != 0)
                        {
                            cs = Givens(x, y);

                            for (int r = i + 1; r <= dim; r++)
                            {
                                md = b[r, i + 1];
                                b[r, i + 1] = cs[0] * md + cs[1] * b[r, i + 2];
                                b[r, i + 2] = -cs[1] * md + cs[0] * b[r, i + 2];
                            }
                            b[i, i + 1] = cs[2];
                            b[i, i + 2] = 0;
                        }
                    }
                }

                if (ind == k && ind != dim)
                {
                    if (Math.Abs(b[ind, ind + 1]) > LibData.Eps)
                        ind--;
                    else
                        b[ind, ind + 1] = 0;
                }
            }
            for (int i = 0; i < dim; i++)
                sv[i] = b[i + 1, i + 1];
            for (int i = dim - 1; i >= 0; i--)
            {
                if (sv[i] > LibData.Eps)
                    break;
                if (Math.Abs(sv[i]) < LibData.Eps)
                    sv[i] = 0;
                if (sv[i] < 0)
                    sv[i] = -sv[i];
            }
            Array.Sort(sv);
            Array.Reverse(sv);
            return sv;
        }

        /// <summary>
        ///  Compute the Singular Value of a matrix. 
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>Returns the Singular Value of <paramref name="A"/>. </returns>
        public static double[] SV(Matrix A)
        {
            Matrix S;
            if (A.rows < A.columns)
                S = Bidiag_NoUV(Transpose(A));
            else
                S = Bidiag_NoUV(A);
            return sv(S);
        }

        #endregion

        #region Eigenvalues And Eigenvectors

        /// <summary>
        /// Use the power method to find the largest magnitude eigenvalues and the corresponding eigenvectors.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns>The largest magnitude eigenvalues and the corresponding eigenvectors of <paramref name="A"/>.</returns>
        /// <exception cref="ArithmeticException">Throw if Convergence is not reached.</exception>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        public static Tuple<double, Matrix> PowEig(Matrix A)
        {
            Checker.IsSquare(A);
            Matrix u = Rand(A.rows, 1);
            double max_vn;
            double max_vk = 0.0;

            do
            {
                u = A * u;
                max_vn = u.elements.Max();
                if (max_vn == 0)
                    throw new ArithmeticException(Resources.Strings.Converge);
                u /= max_vn;

                u = A * u;
                max_vk = u.elements.Max();
                if (max_vk == 0)
                    throw new ArithmeticException(Resources.Strings.Converge);
                u /= max_vk;

            } while (Math.Abs(max_vn - max_vk) > LibData.Eps);
            return new Tuple<double, Matrix>(max_vk, u);
        }

        /// <summary>
        ///  Use inverse power method to find the minimum magnitude eigenvalues and the corresponding eigenvectors.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns>The minimum magnitude eigenvalues and the corresponding eigenvectors of <paramref name="A"/>.</returns>
        /// <exception cref="ArithmeticException">Throw if convergence is not reached.</exception>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        public static Tuple<double, Matrix> InvPowEig(Matrix A)
        {
            int m = A.rows;
            Matrix u = Rand(m, 1);
            double max_vn;
            double max_vk;

            do
            {
                u = LUSolve(A, u);
                max_vn = u.elements.Max();
                if (max_vn == 0)
                    throw new ArithmeticException(Resources.Strings.Converge);
                u /= max_vn;

                u = LUSolve(A, u);
                max_vk = u.elements.Max();
                if (max_vk == 0)
                    throw new ArithmeticException(Resources.Strings.Converge);
                u /= max_vk;
            } while (Math.Abs(max_vn - max_vk) > LibData.Eps);
            return new Tuple<double, Matrix>(1.0 / max_vk, u);
        }

        /// <summary>
        /// Find an approximate eigenvector when an approximation to a corresponding eigenvalue is already known. 
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <param name="p"><paramref name="p"/> is an estimate eigenvalue of </param>.
        /// <remarks>
        /// Warning: if <paramref name="p"/> is the exact the eigenvalue of <paramref name="A"/>,
        /// this method will gives the wrong solution.
        /// </remarks>
        /// <returns>An approximate eigenvector corresponding eigenvalue <paramref name="p"/></returns>
        /// <exception cref="ArithmeticException">Throw if convergence is not reached.</exception>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        public static Matrix InvPowEig(Matrix A, double p)
        {
            Checker.IsSquare(A);
            int n = A.columns;

            Matrix u = Rand(n, 1);
            double max_vn;
            double max_vk;

            Matrix AP = new Matrix(A);
            for (int i = 1; i <= n; i++)
                AP[i, i] -= p;

            do
            {
                u = LUSolve(AP, u);
                max_vn = u.elements.Max();
                if (max_vn == 0)
                    throw new ArithmeticException(Resources.Strings.Converge);
                u /= max_vn;

                u = LUSolve(AP, u);
                max_vk = u.elements.Max();
                if (max_vk == 0)
                    throw new ArithmeticException(Resources.Strings.Converge);
                u /= max_vk;
            } while (Math.Abs(max_vn - max_vk) > LibData.Eps);
            return u;
        }

        /// <summary>
        /// Jacobi Rotation Spectral factorization.
        /// </summary>
        private static double Jacobi(double x, double y, double z, out double c, out double s)
        {
            double t = 0.5 * (z - x) / y;
            if (t >= 0)
                t = 1 / (Math.Sqrt(1 + t * t) + t);
            else
                t = 1 / (Math.Sqrt(1 + t * t) - t);
            c = 1 / Math.Sqrt(1 + t * t);
            s = t * c;
            return t;
        }

        /// <summary>
        /// Spectral factorization using Jacobi eigenvalue algorithm. A=GDG'
        /// A is a Symmetric matrices.
        /// </summary>
        /// <param name="A">A symmetric matrix.</param>
        /// <returns>Spectral factorization of <paramref name="A"/>.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a symmetric matrix.</exception>
        [Obsolete("This method converges too slowly!")]
        internal static Matrix[] Jacobi(Matrix A)
        {
            Checker.IsSymmetric(A);
            int n = A.columns;
            Matrix D = new Matrix(A);
            Matrix G = Eye(n);

            double c;
            double s;
            double t;
            bool state = true;

            while (state)
            {
                state = false;
                for (int p = 1; p < n; p++)
                    for (int q = p + 1; q <= n; q++)
                        if (Math.Abs(D[p, q]) > LibData.Eps)
                        {
                            state = true;
                            t = Jacobi(D[p, p], D[p, q], D[q, q], out c, out s);
                            for (int j = 1; j <= n; j++)
                            {
                                t = D[p, j];
                                D[p, j] = c * t - s * D[q, j];
                                D[q, j] = s * t + c * D[q, j];
                            }

                            for (int i = 1; i <= n; i++)
                            {
                                t = D[i, p];
                                D[i, p] = c * t - s * D[i, q];
                                D[i, q] = s * t + c * D[i, q];
                            }
                            for (int i = 1; i <= n; i++)
                            {
                                t = G[i, p];
                                G[i, p] = c * t - s * G[i, q];
                                G[i, q] = s * t + c * G[i, q];
                            }
                        }
            }
            return new Matrix[] { G, D };
        }

        /// <summary>
        ///  Schur decomposition using a Francis Wilkinson shift(QR Algorithm). 
        ///  A = U*R*U'. U is unitary matrix and R is a real schur form.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns>The Schur decomposition of <paramref name="A"/>,returns U and R.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        public static Matrix[] Schur(Matrix A)
        {
            int n = A.columns;
            int dim = n;
            int k = 1;
            int m;

            Matrix[] UR = Hess(A);
            Matrix U = UR[0];
            Matrix R = UR[1];
            Matrix Q;
            double mu;
            double[] cs;
            double md;

            if (n != 1)
                while (n >= k)
                {
                    Q = Matrix.Eye(n - k + 1);
                    mu = wikinson(R[n - 1, n, n - 1, n]);
                    for (int i = k; i <= n; i++)
                        R[i, i] -= mu;

                    for (int i = k; i < n; i++)
                        if (R[i + 1, i] != 0)
                        {
                            cs = Givens(R[i, i], R[i + 1, i]);

                            for (int j = i + 1; j <= dim; j++)
                            {
                                md = R[i, j];
                                R[i, j] = cs[0] * md + cs[1] * R[i + 1, j];
                                R[i + 1, j] = -cs[1] * md + cs[0] * R[i + 1, j];
                            }
                            R[i, i] = cs[2];
                            R[i + 1, i] = 0;
                            for (int j = 1; j <= n - k + 1; j++)
                            {
                                m = i - k + 2;
                                md = Q[j, m - 1];
                                Q[j, m - 1] = cs[0] * md + cs[1] * Q[j, m];
                                Q[j, m] = -cs[1] * md + cs[0] * Q[j, m];
                            }
                        }
                    R[1, dim, k, n] *= Q;
                    U[1, dim, k, n] *= Q;

                    for (int i = k; i <= n; i++)
                        R[i, i] += mu;

                    if (Math.Abs(R[k + 1, k]) < LibData.Eps)
                    {
                        R[k + 1, k] = 0;
                        k++;
                    }
                    else if (k < dim - 1 && Math.Abs(R[k + 2, k + 1]) < LibData.Eps)
                    {
                        R[k + 2, k + 1] = 0;
                        md = R[k, k] - R[k + 1, k + 1];
                        if (Math.Sign(md * md + 4 * R[k + 1, k] * R[k, k + 1]) == -1)
                            k += 2;
                    }
                    else if (k == dim - 1)
                    {
                        md = R[k, k] - R[k + 1, k + 1];
                        if (Math.Sign(md * md + 4 * R[k + 1, k] * R[k, k + 1]) == -1)
                            k += 2;
                    }
                    if (Math.Abs(R[n, n - 1]) < LibData.Eps)
                    {
                        R[n, n - 1] = 0;
                        n--;
                    }

                }
            return new Matrix[2] { U, R };
        }

        /// <summary>
        ///  Schur decomposition using a Francis double shift(Francis Algorithm). 
        ///  A = U*R*U'. U is orthogonal matrix and R is a real schur form.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns>The Schur decomposition of <paramref name="A"/>,returns U and R.</returns>
        /// <exception cref="ArgumentException">Throw if A is not a square matrix.</exception>
        internal static Matrix[] schur(Matrix A)
        {
            int n = A.columns;
            int k = 1;
            int dim = n;
            Matrix[] UH = Hess(A);
            Matrix R = UH[1];
            Matrix U = UH[0];
            Matrix Q = new Matrix(3);

            double s;
            double c;
            double x;
            double y;
            double z;
            double md;

            while (k < n - 1)
            {
                s = R[n - 1, n - 1] + R[n, n];
                c = R[n - 1, n - 1] * R[n, n] - R[n - 1, n] * R[n, n - 1];

                x = R[k, k] * (R[k, k] - s) + R[k, k + 1] * R[k + 1, k] + c;
                y = R[k + 1, k] * (R[k, k] + R[k + 1, k + 1] - s);
                z = R[k + 1, k] * R[k + 2, k + 1];

                s = ElMath.Sign(x) * Math.Sqrt(x * x + y * y + z * z);
                md = s * (x + s);
                if ((y != 0 || z != 0) && s != 0 && md != 0)
                {
                    c = x / s;

                    Q[1, 1] = -c;
                    Q[2, 2] = c + z * z / md;
                    Q[3, 3] = c + y * y / md;
                    Q[1, 2] = -y / s; Q[2, 1] = Q[1, 2];
                    Q[1, 3] = -z / s; Q[3, 1] = Q[1, 3];
                    Q[2, 3] = -y * z / md; Q[3, 2] = Q[2, 3];

                    R[k, k + 2, 1, dim] = Q * R[k, k + 2, 1, dim];
                    R[1, n, k, k + 2] *= Q;
                    U[1, dim, k, k + 2] *= Q;
                }

                for (int i = k; i < n - 2; i++)
                {
                    x = R[i + 1, i];
                    y = R[i + 2, i];
                    z = R[i + 3, i];

                    s = ElMath.Sign(x) * Math.Sqrt(x * x + y * y + z * z);
                    md = s * (x + s);
                    if ((y != 0 || z != 0) && s != 0 && md != 0)
                    {
                        c = x / s;

                        Q[1, 1] = -c;
                        Q[2, 2] = c + z * z / md;
                        Q[3, 3] = c + y * y / md;
                        Q[1, 2] = -y / s; Q[2, 1] = Q[1, 2];
                        Q[1, 3] = -z / s; Q[3, 1] = Q[1, 3];
                        Q[2, 3] = -y * z / md; Q[3, 2] = Q[2, 3];

                        R[i + 1, i + 3, i, dim] = Q * R[i + 1, i + 3, i, dim];
                        R[1, n, i + 1, i + 3] *= Q; R[i + 2, i] = 0; R[i + 3, i] = 0;
                        U[1, dim, i + 1, i + 3] *= Q;
                    }
                }

                x = R[n - 1, n - 2];
                y = R[n, n - 2];
                md = ElMath.Hypot(x, y);
                if (Math.Abs(y) > LibData.Eps)
                {
                    c = x / md;
                    s = y / md;

                    for (int j = n - 3; j <= dim; j++)
                    {
                        z = R[n - 1, j];
                        R[n - 1, j] = c * z + s * R[n, j];
                        R[n, j] = -s * z + c * R[n, j];
                    }
                    for (int i = 1; i <= n; i++)
                    {
                        z = R[i, n - 1];
                        R[i, n - 1] = c * z + s * R[i, n];
                        R[i, n] = -s * z + c * R[i, n];
                    }
                    R[n - 1, n - 2] = md;
                    R[n, n - 2] = 0;
                    for (int i = 1; i <= dim; i++)
                    {
                        z = U[i, n - 1];
                        U[i, n - 1] = c * z + s * U[i, n];
                        U[i, n] = -s * z + c * U[i, n];
                    }
                }
                else
                    R[n, n - 2] = 0;

                if (Math.Abs(R[k + 1, k]) < LibData.Eps)
                {
                    R[k + 1, k] = 0;
                    k++;
                }
                else if (Math.Abs(R[k + 2, k + 1]) < LibData.Eps)
                {
                    R[k + 2, k + 1] = 0;
                    s = R[k, k] - R[k + 1, k + 1];
                    if (Math.Sign(s * s + 4 * R[k + 1, k] * R[k, k + 1]) == -1)
                        k += 2;
                }
                if (Math.Abs(R[n, n - 1]) < LibData.Eps)
                {
                    R[n, n - 1] = 0;
                    n--;
                }
            }

            if (n - k == 1 && Math.Abs(R[n, k]) > LibData.Eps)
            {
                s = R[k, k] - R[n, n];
                if (Math.Sign(s * s + 4 * R[n, k] * R[k, n]) != -1)
                {
                    while (Math.Abs(R[n, k]) > LibData.Eps)
                    {
                        x = R[k, k];
                        y = R[n, k];
                        md = ElMath.Hypot(x, y);
                        c = x / md;
                        s = y / md;

                        for (int j = 1; j <= dim; j++)
                        {
                            z = R[k, j];
                            R[k, j] = c * z + s * R[n, j];
                            R[n, j] = -s * z + c * R[n, j];
                        }
                        for (int i = 1; i <= n; i++)
                        {
                            z = R[i, k];
                            R[i, k] = c * z + s * R[i, n];
                            R[i, n] = -s * z + c * R[i, n];
                        }
                        for (int i = 1; i <= dim; i++)
                        {
                            z = U[i, k];
                            U[i, k] = c * z + s * U[i, n];
                            U[i, n] = -s * z + c * U[i, n];
                        }
                    }
                    R[n, k] = 0;

                    //UH = schur(R[k, n, k, n]);
                    //R[n, k] = 0; R[k, n] = UH[1][1, 2]; R[k, k] = UH[1][1, 1]; R[n, n] = UH[1][2, 2];
                    //U[1, dim, k, n] *= UH[0];
                }
            }
            return new Matrix[2] { U, R };
        }

        /// <summary>
        /// Returns a column complex array containing the sorted eigenvalues.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns>The sorted eigenvalues of a square matrix <paramref name="A"/>.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        public static Complex[] EigValue(Matrix A)
        {
            int n = A.columns;
            int k = 1;
            int dim = n;
            Matrix R = Hess(A)[1];
            Matrix Q = new Matrix(3);
            Matrix q = new Matrix(2, 2);

            double s;
            double c;
            double x;
            double y;
            double z;
            double md;
            Complex[] ev = new Complex[n];
            ev[0] = (Complex)R[1, 1];
            int m = 0;

            while (k < n - 1)
            {
                s = R[n - 1, n - 1] + R[n, n];
                c = R[n - 1, n - 1] * R[n, n] - R[n - 1, n] * R[n, n - 1];

                x = R[k, k] * (R[k, k] - s) + R[k, k + 1] * R[k + 1, k] + c;
                y = R[k + 1, k] * (R[k, k] + R[k + 1, k + 1] - s);
                z = R[k + 1, k] * R[k + 2, k + 1];

                s = ElMath.Sign(x) * Math.Sqrt(x * x + y * y + z * z);
                md = s * (x + s);
                if (s != 0 && md != 0)
                {
                    c = x / s;
                    Q[1, 1] = -c;
                    Q[2, 2] = c + z * z / md;
                    Q[3, 3] = c + y * y / md;
                    Q[1, 2] = -y / s; Q[2, 1] = Q[1, 2];
                    Q[1, 3] = -z / s; Q[3, 1] = Q[1, 3];
                    Q[2, 3] = -y * z / md; Q[3, 2] = Q[2, 3];

                    R[k, k + 2, 1, dim] = Q * R[k, k + 2, 1, dim];
                    R[1, n, k, k + 2] *= Q;
                }

                for (int i = k; i < n - 2; i++)
                {
                    x = R[i + 1, i];
                    y = R[i + 2, i];
                    z = R[i + 3, i];

                    s = ElMath.Sign(x) * Math.Sqrt(x * x + y * y + z * z);
                    md = s * (x + s);
                    if (s != 0 || md != 0)
                    {
                        c = x / s;
                        Q[1, 1] = -c;
                        Q[2, 2] = c + z * z / md;
                        Q[3, 3] = c + y * y / md;
                        Q[1, 2] = -y / s; Q[2, 1] = Q[1, 2];
                        Q[1, 3] = -z / s; Q[3, 1] = Q[1, 3];
                        Q[2, 3] = -y * z / md; Q[3, 2] = Q[2, 3];

                        R[i + 1, i + 3, i, dim] = Q * R[i + 1, i + 3, i, dim];
                        R[1, n, i + 1, i + 3] *= Q;
                    }
                }

                x = R[n - 1, n - 2];
                y = R[n, n - 2];
                md = ElMath.Hypot(x, y);
                if (md != 0)
                {
                    c = x / md;
                    s = y / md;

                    for (int j = 1; j <= dim; j++)
                    {
                        z = R[n - 1, j];
                        R[n - 1, j] = c * z + s * R[n, j];
                        R[n, j] = -s * z + c * R[n, j];
                    }
                    for (int i = 1; i <= n; i++)
                    {
                        z = R[i, n - 1];
                        R[i, n - 1] = c * z + s * R[i, n];
                        R[i, n] = -s * z + c * R[i, n];
                    }
                }

                if (Math.Abs(R[k + 1, k]) < LibData.Eps)
                {
                    ev[m] = (Complex)R[k, k];
                    m++;
                    k++;
                }
                else if (Math.Abs(R[k + 2, k + 1]) < LibData.Eps)
                {
                    s = R[k, k] - R[k + 1, k + 1];
                    md = s * s + 4 * R[k + 1, k] * R[k, k + 1];
                    if (Math.Sign(md) == -1)
                    {
                        ev[m] = 0.5 * new Complex(R[k, k] + R[k + 1, k + 1], -Math.Sqrt(-md));
                        ev[m + 1] = Complex.Conj(ev[m]);
                        m += 2;
                        k += 2;
                    }
                }
                if (Math.Abs(R[n, n - 1]) < LibData.Eps)
                {
                    ev[m] = (Complex)R[n, n];
                    m++;
                    n--;
                }
            }

            if (n - k == 1 && Math.Abs(R[n, k]) > LibData.Eps)
            {
                s = R[k, k] - R[n, n];
                md = s * s + 4 * R[n, k] * R[k, n];
                if (Math.Sign(md) != -1)
                {
                    s = R[k, k] + R[n, n];
                    md = Math.Sqrt(md);
                    ev[m] = (Complex)(0.5 * (s + md));
                    ev[m + 1] = (Complex)(0.5 * (s - md));
                    m += 2;
                }
                else
                {
                    ev[m] = 0.5 * new Complex(R[k, k] + R[n, n], -Math.Sqrt(-md));
                    ev[m + 1] = Complex.Conj(ev[m]);
                    m += 2;
                }
            }
            Array.Sort(ev);
            Array.Reverse(ev);
            return ev;
        }

        #endregion

        #region Explicit Operator Converter

        /// <summary>
        /// Convert a double type array to a Matrix.
        /// </summary>
        /// <param name="arr">A double type array.</param>
        /// <returns>A matrix.</returns>
        public static explicit operator Matrix(double[] arr)
        {
            return new Matrix(arr.Length, 1, arr);
        }

        #endregion

        #region Overide System.Object Methods

        #region ToString Helper Method

        private bool IsIntMatrix()
        {
            double[] ele = this.elements;
            for (int i = 0; i < ele.Length; i++)
                if (ele[i].ToString().Contains('.') || Math.Abs(ele[i]) >= 1E9)
                    return false;
            return true;
        }

        private double max()
        {
            double[] ele = this.elements;
            double max = 0;
            for (int i = 0; i < ele.Length; i++)
                if (Math.Abs(ele[i]) > Math.Abs(max))
                {
                    if (!(double.IsPositiveInfinity(ele[i]) || double.IsNegativeInfinity(ele[i])))
                        max = ele[i];
                }
            return max;
        }

        private StringBuilder ToString(StringBuilder strout)
        {
            double md;
            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= columns; j++)
                {
                    md = Math.Abs(this[i, j]);
                    if (md == 0)
                    {
                        strout.Append("     ");
                        for (int k = 0; k < LibData.Digits; k++)
                            strout.Append(" ");
                        strout.Append("0");
                    }
                    else if (double.IsNaN(md))
                    {
                        strout.Append("   ");
                        for (int k = 0; k < LibData.Digits; k++)
                            strout.Append(" ");
                        strout.Append("NaN");
                    }

                    else if (double.IsPositiveInfinity(this[i, j]))
                    {
                        strout.Append("   ");
                        for (int k = 0; k < LibData.Digits; k++)
                            strout.Append(" ");
                        strout.Append("Inf");
                    }

                    else if (double.IsNegativeInfinity(this[i, j]))
                    {
                        strout.Append("  ");
                        for (int k = 0; k < LibData.Digits; k++)
                            strout.Append(" ");
                        strout.Append("-Inf");
                    }

                    else
                    {
                        if (md < 10)
                            strout.Append("   ");
                        else if (md < 100)
                            strout.Append("  ");
                        else
                            strout.Append(" ");

                        if (ElMath.Sign(this[i, j]) == 1)
                            strout.Append(" ");
                        else
                            strout.Append("-");
                        strout.AppendFormat(LibData.Format, md);
                    }
                }
                strout.AppendLine();
            }
            return strout;
        }

        #endregion

        /// <summary>
        /// Returns the equivalent string representation of this matrix.
        /// </summary>
        /// <returns>The string representation of this matrix.</returns>
        public override string ToString()
        {
            StringBuilder strout = new StringBuilder();
            if (this.IsIntMatrix())
            {
                for (int i = 1; i <= rows; i++)
                {
                    for (int j = 1; j <= columns; j++)
                    {
                        if (double.IsNaN(this[i, j]))
                            strout.AppendFormat("{0,10}", "NaN");
                        else if (double.IsPositiveInfinity(this[i, j]))
                            strout.AppendFormat("{0,10}", "Inf");
                        else if (double.IsNegativeInfinity(this[i, j]))
                            strout.AppendFormat("{0,10}", "-Inf");
                        else
                            strout.AppendFormat("{0,10}", this[i, j]);
                    }
                    strout.AppendLine();
                }

                return strout.ToString();
            }

            int n = int.Parse(Math.Abs(this.max()).ToString("E0").Substring(2));
            if (n > 2)
            {
                strout.AppendFormat("   1e{0} *", n);
                strout.AppendLine();
                strout.AppendLine();
                (this / Math.Pow(10, n)).ToString(strout);
            }

            else if (-n > 2)
            {
                strout.AppendFormat("   1e{0} *", n);
                strout.AppendLine();
                strout.AppendLine();

                (this * Math.Pow(10, -n)).ToString(strout);
            }

            else
                this.ToString(strout);

            return strout.ToString();
        }

        /// <summary>
        /// Indicates whether this instance and a specific object are equals.
        /// </summary>
        /// <param name="obj">An object.</param>
        /// <returns>A boolean value indicates whether this instance and a specific object are equals.</returns>
        public override bool Equals(object obj)
        {
            Matrix A = obj as Matrix;
            if (A == null)
                return false;
            if (this.columns != A.columns || this.rows != A.rows)
                return false;
            for (int i = 1; i <= A.rows; i++)
                for (int j = 1; j <= A.columns; j++)
                {
                    if (!this[i, j].Equals(A[i, j]))
                        return false;
                }
            return true;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A int value reprents the hashcode.</returns>
        public override int GetHashCode()
        {
            return (int)Norm(this, 1);
        }

        #endregion
    }
}