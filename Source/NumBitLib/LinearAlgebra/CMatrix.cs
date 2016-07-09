
/*********************************************************************************************************************************************
 * *
 * *        File Name                : CMatrix.cs
 * *        Creator                  : Fuhua Lai
 * *        Date Modified            : 2013-6-19 10:58:07
 * *        Functional Description   : Performs Complex Matrix operation.
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
    /// Represents a Complex Matrix.
    /// </summary>
    public class CMatrix
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
        protected Complex[] elements;

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
        /// Gets the elements of this CMatrix.
        /// </summary>
        public Complex[] Elements
        {
            get { return this.elements; }
        }

        /// <summary>
        /// Gets or set the real part elements of this Matrix.
        /// </summary>
        public Matrix Real
        {
            get
            {
                Matrix real = new Matrix(this.rows, this.columns);
                for (int i = 0; i < this.elements.Length; i++)
                    real.Elements[i] = this.elements[i].Real;
                return real;
            }
            set
            {
                for (int i = 0; i < this.elements.Length; i++)
                    this.elements[i].Real = value.Elements[i];
            }
        }

        /// <summary>
        /// Gets or set the imaginary part elements of this Matrix.
        /// </summary>
        public Matrix Imaginary
        {
            get
            {
                Matrix img = new Matrix(this.rows, this.columns);
                for (int i = 0; i < this.elements.Length; i++)
                    img.Elements[i] = this.elements[i].Imaginary;
                return img;
            }
            set
            {
                for (int i = 0; i < this.elements.Length; i++)
                    this.elements[i].Imaginary = value.Elements[i];
            }
        }

        /// <summary>
        /// Gets or sets the specified index of the element.
        /// </summary>
        /// <param name="i">Row index.</param>
        /// <param name="j">Column index.</param>
        /// <returns>An elements of this Matrix.</returns>
        /// <exception cref="IndexOutOfRangeException">Throw if index out of range.</exception>
        public Complex this[int i, int j]
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
        public CMatrix this[int i]
        {
            get
            {
                i--;
                CMatrix R = new CMatrix(1, columns);
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
        public CMatrix this[int RowStart, int RowEnd, int ColStart, int ColEnd]
        {
            get
            {
                CMatrix MatChild = new CMatrix(RowEnd - RowStart + 1, ColEnd - ColStart + 1);
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
        public CMatrix()
        {
            rows = 1;
            columns = 1;
            elements = new Complex[1];
        }

        /// <summary>
        /// Initializes a new instance of Complex Matrix with specified size.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <exception cref="ArgumentException">Throw if <paramref name="rows"/> or <paramref name="columns"/> is negative.</exception>
        public CMatrix(int rows, int columns)
        {
            if (rows < 0 || columns < 0)
                throw new ArgumentException(Resources.Strings.MatrixCtor);
            this.rows = rows;
            this.columns = columns;
            elements = new Complex[rows * columns];
        }

        /// <summary>
        /// Initializes a new instance of Complex Matrix with specified size and elements.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <param name="data">The array.</param>
        /// <exception cref="ArgumentException">Throw if matrix dimensions don't consistent with the elements number.</exception>
        public CMatrix(int rows, int columns, params Complex[] data)
            : this(rows, columns)
        {
            if (data.Length != rows * columns)
                throw new ArgumentException(Resources.Strings.MatrixCtor1);
            data.CopyTo(this.elements, 0);
        }

        /// <summary>
        /// Initializes a Square Complex Matrix with specified size.
        /// </summary>
        /// <param name="size">The size of the Square Matrix. </param>
        /// <exception cref="ArgumentException">Throw if <paramref name="size"/> is negative.</exception>
        public CMatrix(int size)
        {
            if (size < 0)
                throw new ArgumentException(Resources.Strings.MatrixCtor2);
            rows = size;
            columns = size;
            elements = new Complex[size * size];
        }

        /// <summary>
        ///  Initializes a Square Complex Matrix with specified size and elements.
        /// </summary>
        /// <param name="size">The size of the Square Matrix.</param>
        /// <param name="data">The array.</param>
        /// <exception cref="ArgumentException">Throw if matrix dimensions don't consistent with the elements number.</exception>
        public CMatrix(int size, params Complex[] data)
            : this(size)
        {
            if (data.Length != size * size)
                throw new ArgumentException(Resources.Strings.MatrixCtor1);
            data.CopyTo(this.elements, 0);
        }

        /// <summary>
        /// Construct a row Complex Matrix use an array.
        /// </summary>
        /// <param name="data">The array.</param>
        public CMatrix(params Complex[] data)
        {
            this.rows = 1;
            this.columns = data.Length;
            data.CopyTo(this.elements, 0);
        }

        /// <summary>
        /// Construct a row Complex Matrix use an array.
        /// </summary>
        /// <param name="data">The array.</param>
        public CMatrix(params double[] data)
        {
            this.rows = 1;
            this.columns = data.Length;
            data.CopyTo(this.elements, 0);
        }

        /// <summary>
        ///  Initializes a new instance of Complex Matrix with specified size an elements.
        /// </summary>
        /// <param name="size">The size of the Square Matrix.</param>
        /// <param name="data">The array.</param>
        public CMatrix(int size, params double[] data)
            : this(size)
        {
            if (data.Length != size * size)
                throw new ArgumentException(Resources.Strings.MatrixCtor1);
            for (int i = 0; i < data.Length; i++)
                this.elements[i].Real = data[i];
        }

        /// <summary>
        /// Initializes a new instance of Complex Matrix with specified size an elements.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <param name="data">The double type array.</param>
        public CMatrix(int rows, int columns, params double[] data)
            : this(rows, columns)
        {
            if (data.Length != rows * columns)
                throw new ArgumentException(Resources.Strings.MatrixCtor1);
            for (int i = 0; i < data.Length; i++)
                this.elements[i].Real = data[i];
        }

        /// <summary>
        /// Initializes a new instance of Complex Matrix by a real Matrix.
        /// </summary>
        /// <param name="A">A real Matrix.</param>
        public CMatrix(Matrix A)
        {
            this.rows = A.Rows;
            this.columns = A.Columns;
            this.elements = new Complex[rows * columns];
            this.Real = A;
        }

        /// <summary>
        /// Initializes a new instance of Complex Matrix by two real matrix.
        /// </summary>
        /// <param name="A">The real part of this Complex Matrix.</param>
        /// <param name="B">The imaginary part of this Complex Matrix.</param>
        public CMatrix(Matrix A, Matrix B)
        {
            int m = A.Rows;
            int n = A.Columns;
            this.rows = m;
            this.columns = n;
            elements = new Complex[m * n];
            for (int i = 1; i <= m; i++)
                for (int j = 1; j <= n; j++)
                    this[i, j] = new Complex(A[i, j], B[i, j]);
        }

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="Cpy">The Matrix to be copied</param>
        public CMatrix(CMatrix Cpy)
        {
            rows = Cpy.rows;
            columns = Cpy.columns;
            this.elements = new Complex[rows * columns];
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
        public static CMatrix Zeros(int rows, int columns)
        {
            return new CMatrix(rows, columns);
        }

        /// <summary>
        /// Ones matrix.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <returns>Ones matrix.</returns>
        public static CMatrix Ones(int rows, int columns)
        {
            return new CMatrix(rows, columns, MArray.Ones(rows * columns));
        }

        /// <summary>
        /// Identity matrix.
        /// </summary>
        ///<param name="size">The size of this matrix.</param>
        /// <returns>Identity matrix.</returns>
        public static CMatrix Eye(int size)
        {
            CMatrix E = new CMatrix(size);
            for (int i = 1; i <= size; i++)
                E[i, i] = new Complex(1.0, 0.0);
            return E;
        }

        /// <summary>
        /// Scalar matrix.
        /// </summary>
        ///<param name="size">The size of this matrix.</param>
        ///<param name="num">The element.</param>
        /// <returns>Scalar matrix.</returns>
        public static CMatrix Scalar(int size, Complex num)
        {
            CMatrix S = new CMatrix(size);
            for (int i = 0; i < S.elements.Length; i++)
                S.elements[i] = num;
            return S;
        }

        /// <summary>
        ///  Uniformly distributed pseudorandom numbers.The interval is [0.0,1.0].
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <returns>A matrix with Uniformly distributed pseudorandom numbers.</returns>
        public static CMatrix Rand(int rows, int columns)
        {
            return new CMatrix(Matrix.Rand(rows, columns), Matrix.Rand(rows, columns));
        }

        /// <summary>
        /// Generate values from the uniform distribution on the interval [a, b].
        /// </summary>
        /// <param name="rows">Rows</param>
        /// <param name="columns">Columns</param>
        /// <param name="a">The start position of a interval.</param>
        /// <param name="b">The end position of a interval.</param>
        /// <returns>A matrix with Uniformly distributed pseudorandom numbers,the interval is between <paramref name="a"/> and <paramref name="b"/>.</returns>
        public static CMatrix Rand(int rows, int columns, double a, double b)
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
        public static CMatrix Randi(int rows, int columns)
        {
            CMatrix R = new CMatrix(rows, columns);
            Random ra = new Random(DateTime.Now.Millisecond);
            for (int i = 1; i <= rows; i++)
                for (int j = 1; j <= columns; j++)
                    R[i, j] = new Complex(ra.Next(1, 10), ra.Next(1, 10));
            return R;
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
        public bool IsHermitian()
        {
            if (this.IsSquare())
            {
                for (int i = 1; i <= this.Rows; i++)
                    for (int j = 1; j < i; j++)
                        if (this[i, j] != Complex.Conj(this[j, i]))
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
                        if (this[i, j] != Complex.Zero || this[j, i] != Complex.Zero)
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
                Complex md;
                for (int i = 1; i <= this.Rows; i++)
                    for (int j = 1; j <= i; j++)
                    {
                        md = Complex.Zero;
                        for (int k = 1; k <= this.rows; k++)
                            md += Complex.Conj(this[k, i]) * this[k, j];
                        for (int k = 1; k <= this.rows; k++)
                            md -= this[i, k] * Complex.Conj(this[j, k]);
                        if (!md.CloseToZero())
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
        public static CMatrix operator +(CMatrix A, CMatrix B)
        {
            Checker.MatrixMatch(A, B);
            return new CMatrix(A.rows, A.columns, MArray.Add(A.elements, B.elements));
        }

        /// <summary>
        /// Matrix subtraction.
        /// </summary>
        /// <param name="A">The left-hand operand.</param>
        /// <param name="B">The right-hand operand.</param>
        /// <returns>The subtion of two Matrix.</returns>
        /// <exception cref="ArgumentException">Throw if the size of matrix <paramref name="A"/> and <paramref name="B"/> don't match.</exception>
        public static CMatrix operator -(CMatrix A, CMatrix B)
        {
            Checker.MatrixMatch(A, B);
            return new CMatrix(A.rows, A.columns, MArray.Sub(A.elements, B.elements));
        }

        /// <summary>
        /// Matrix multiplication.
        /// </summary>
        /// <param name="A">The left-hand operand.</param>
        /// <param name="B">The right-hand operand.</param>
        /// <returns>The multiplication of two Matrix.</returns>
        /// <exception cref="ArgumentException">Throw if the Inner matrix dimensions don't agree.</exception>
        public static CMatrix operator *(CMatrix A, CMatrix B)
        {
            int m = A.rows;
            int k = A.columns;
            int n = B.columns;
            if (k != B.rows)
                throw new ArithmeticException(Resources.Strings.MatrixMul);
            CMatrix Mul = new CMatrix(m, n);

            if (m > 30)
            {
                Parallel.For(1, m + 1, i =>
                {
                    for (int j = 1; j <= n; j++)
                    {
                        Complex temp = Complex.Zero;
                        for (int t = 1; t <= k; t++)
                        {
                            temp += A[i, t] * B[t, j];
                        }
                        Mul[i, j] = temp;
                    }
                });
                return Mul;
            }

            Complex tem;
            for (int i = 1; i <= m; i++)
                for (int j = 1; j <= n; j++)
                {
                    tem = Complex.Zero;
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
        public static CMatrix operator /(CMatrix A, CMatrix B)
        {
            return A * CMatrix.Pinv(B);
        }

        #endregion

        #region Hybrid Operation

        /// <summary>
        /// Matrix addition.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <param name="B">A Matrix.</param>
        /// <returns>The addition of <paramref name="A"/> and <paramref name="B"/>.</returns>
        public static CMatrix operator +(Matrix A, CMatrix B)
        {
            Checker.MatrixMatch(A, B);
            return new CMatrix(A.Rows, A.Columns, MArray.Add(A.Elements, B.elements));
        }

        /// <summary>
        /// Matrix addition.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <param name="B">A Matrix.</param>
        /// <returns>The addition of <paramref name="A"/> and <paramref name="B"/>.</returns>
        public static CMatrix operator +(CMatrix A, Matrix B)
        {
            Checker.MatrixMatch(A, B);
            return new CMatrix(A.Rows, A.Columns, MArray.Add(A.elements, B.Elements));
        }

        /// <summary>
        /// Matrix subtraction.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <param name="B">A Matrix.</param>
        /// <returns>The subtraction of <paramref name="A"/> and <paramref name="B"/>.</returns>
        public static CMatrix operator -(CMatrix A, Matrix B)
        {
            Checker.MatrixMatch(A, B);
            return new CMatrix(A.Rows, A.Columns, MArray.Sub(A.elements, B.Elements));
        }

        /// <summary>
        /// Matrix subtraction.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <param name="B">A Matrix.</param>
        /// <returns>The subtraction of <paramref name="A"/> and <paramref name="B"/>.</returns>
        public static CMatrix operator -(Matrix A, CMatrix B)
        {
            Checker.MatrixMatch(A, B);
            return new CMatrix(A.Rows, A.Columns, MArray.Sub(A.Elements, B.elements));
        }

        /// <summary>
        /// Matrix multiplication.
        /// </summary>
        /// <param name="A">The left-hand operand.</param>
        /// <param name="B">The right-hand operand.</param>
        /// <returns>The multiplication of two Matrix.</returns>
        /// <exception cref="ArgumentException">Throw if the Inner matrix dimensions don't agree.</exception>
        public static CMatrix operator *(CMatrix A, Matrix B)
        {
            int m = A.rows;
            int k = A.columns;
            int n = B.Columns;
            if (k != B.Rows)
                throw new ArithmeticException(Resources.Strings.MatrixMul);
            CMatrix Mul = new CMatrix(m, n);

            if (m > 30)
            {
                Parallel.For(1, m + 1, i =>
                {
                    for (int j = 1; j <= n; j++)
                    {
                        Complex temp = Complex.Zero;
                        for (int t = 1; t <= k; t++)
                        {
                            temp += A[i, t] * B[t, j];
                        }
                        Mul[i, j] = temp;
                    }
                });
                return Mul;
            }

            Complex tem;
            for (int i = 1; i <= m; i++)
                for (int j = 1; j <= n; j++)
                {
                    tem = Complex.Zero;
                    for (int t = 1; t <= k; t++)
                        tem += A[i, t] * B[t, j];
                    Mul[i, j] = tem;
                }
            return Mul;
        }

        /// <summary>
        /// Matrix multiplication.
        /// </summary>
        /// <param name="A">The left-hand operand.</param>
        /// <param name="B">The right-hand operand.</param>
        /// <returns>The multiplication of two Matrix.</returns>
        /// <exception cref="ArgumentException">Throw if the Inner matrix dimensions don't agree.</exception>
        public static CMatrix operator *(Matrix A, CMatrix B)
        {
            int m = A.Rows;
            int k = A.Columns;
            int n = B.Columns;
            if (k != B.Rows)
                throw new ArithmeticException(Resources.Strings.MatrixMul);
            CMatrix Mul = new CMatrix(m, n);

            if (m > 30)
            {
                Parallel.For(1, m + 1, i =>
                {
                    for (int j = 1; j <= n; j++)
                    {
                        Complex temp = Complex.Zero;
                        for (int t = 1; t <= k; t++)
                        {
                            temp += A[i, t] * B[t, j];
                        }
                        Mul[i, j] = temp;
                    }
                });
                return Mul;
            }

            Complex tem;
            for (int i = 1; i <= m; i++)
                for (int j = 1; j <= n; j++)
                {
                    tem = Complex.Zero;
                    for (int t = 1; t <= k; t++)
                        tem += A[i, t] * B[t, j];
                    Mul[i, j] = tem;
                }
            return Mul;
        }

        /// <summary>
        /// Matrix division.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <param name="B">A Matrix.</param>
        /// <returns>The division of <paramref name="A"/> and <paramref name="B"/>.</returns>
        public static CMatrix operator /(Matrix A, CMatrix B)
        {
            return A * Pinv(B);
        }

        /// <summary>
        /// Matrix division.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <param name="B">A Matrix.</param>
        /// <returns>The division of <paramref name="A"/> and <paramref name="B"/>.</returns>
        public static CMatrix operator /(CMatrix A, Matrix B)
        {
            return A * Matrix.Pinv(B);
        }

        /// <summary>
        /// Convert a double type array to a Matrix.
        /// </summary>
        /// <param name="arr">An double type array.</param>
        /// <returns>A Complex matrix.</returns>
        public static explicit operator CMatrix(double[] arr)
        {
            return new CMatrix(1, arr.Length, arr);
        }

        /// <summary>
        /// Convert a Complex type array to a Matrix.
        /// </summary>
        /// <param name="arr">An Complex type array.</param>
        /// <returns>A Complex matrix.</returns>
        public static explicit operator CMatrix(Complex[] arr)
        {
            return new CMatrix(1, arr.Length, arr);
        }

        /// <summary>
        /// Convert a Matrix object to a Complex Matrix.
        /// </summary>
        /// <param name="matrix">A matrix to be convert to a Complex Matrix.</param>
        /// <returns>A Complex Matrix.</returns>
        public static explicit operator CMatrix(Matrix matrix)
        {
            return new CMatrix(matrix);
        }

        #endregion

        #region Array Operation

        /// <summary>
        /// Matrix addition.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The addition of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator +(double a, CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Add(A.elements, a));
        }

        /// <summary>
        /// Matrix addition.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The addition of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator +(CMatrix A, double a)
        {
            return new CMatrix(A.rows, A.columns, MArray.Add(A.elements, a));
        }

        /// <summary>
        /// Matrix addition.
        /// </summary>
        /// <param name="a">A Complex number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The addition of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator +(Complex a, CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Add(A.elements, a));
        }

        /// <summary>
        /// Matrix addition.
        /// </summary>
        /// <param name="a">A Complex number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The addition of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator +(CMatrix A, Complex a)
        {
            return new CMatrix(A.rows, A.columns, MArray.Add(A.elements, a));
        }

        /// <summary>
        /// Matrix subtraction.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The subtraction of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator -(double a, CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Sub(a, A.elements));
        }

        /// <summary>
        /// Matrix subtraction.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The subtraction of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator -(CMatrix A, double a)
        {
            return new CMatrix(A.rows, A.columns, MArray.Sub(A.elements, a));
        }

        /// <summary>
        /// Matrix subtraction.
        /// </summary>
        /// <param name="a">A Complex number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The subtraction of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator -(Complex a, CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Sub(a, A.elements));
        }

        /// <summary>
        /// Matrix subtraction.
        /// </summary>
        /// <param name="a">A Complex number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The subtraction of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator -(CMatrix A, Complex a)
        {
            return new CMatrix(A.rows, A.columns, MArray.Sub(A.elements, a));
        }

        /// <summary>
        /// Matrix multiplication.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The multiplication of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator *(double a, CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Mul(A.elements, a));
        }

        /// <summary>
        /// Matrix multiplication.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The multiplication of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator *(CMatrix A, double a)
        {
            return new CMatrix(A.rows, A.columns, MArray.Mul(A.elements, a));
        }

        /// <summary>
        /// Matrix multiplication.
        /// </summary>
        /// <param name="a">A Complex number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The multiplication of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator *(Complex a, CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Mul(A.elements, a));
        }

        /// <summary>
        /// Matrix multiplication.
        /// </summary>
        /// <param name="a">A Complex number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The multiplication of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator *(CMatrix A, Complex a)
        {
            return new CMatrix(A.rows, A.columns, MArray.Mul(A.elements, a));
        }

        /// <summary>
        /// Matrix division.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The division of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator /(double a, CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Div(a, A.elements));
        }

        /// <summary>
        /// Matrix division.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The division of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator /(CMatrix A, double a)
        {
            return new CMatrix(A.rows, A.columns, MArray.Div(A.elements, a));
        }

        /// <summary>
        /// Matrix division.
        /// </summary>
        /// <param name="a">A Complex number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The division of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator /(Complex a, CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Div(a, A.elements));
        }

        /// <summary>
        /// Matrix division.
        /// </summary>
        /// <param name="a">A Complex number.</param>
        /// <param name="A">A matrix.</param>
        /// <returns>The division of <paramref name="a"/> and <paramref name="A"/>.</returns>
        public static CMatrix operator /(CMatrix A, Complex a)
        {
            return new CMatrix(A.rows, A.columns, MArray.Div(A.elements, a));
        }

        /// <summary>
        /// Array multiplication.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <param name="B">A Mtarix.</param>
        /// <returns>The array multiplication of <paramref name="A"/> and <paramref name="B"/>.</returns>
        public static CMatrix Mul(CMatrix A, CMatrix B)
        {
            Checker.MatrixMatch(A, B);
            return new CMatrix(A.rows, A.columns, MArray.Mul(A.elements, B.elements));
        }

        /// <summary>
        /// Array division.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <param name="B">A Mtarix.</param>
        /// <returns>The array division of <paramref name="A"/> and <paramref name="B"/>.</returns>
        public static CMatrix Div(CMatrix A, CMatrix B)
        {
            Checker.MatrixMatch(A, B);
            return new CMatrix(A.rows, A.columns, MArray.Div(A.elements, B.elements));
        }

        #endregion

        #region Mathematical Functions

        /// <summary>
        /// Array conjugate.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The array conjugate.</returns>
        public static CMatrix Conj(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, A.elements.Conj());
        }

        /// <summary>
        /// Absolute value.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The absolute value.</returns>
        public static Matrix Abs(CMatrix A)
        {
            return new Matrix(A.rows, A.columns, MArray.Abs(A.elements));
        }

        /// <summary>
        /// Square root.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The Squqre root.</returns>
        public static CMatrix Sqrt(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Sqrt(A.elements));
        }

        /// <summary>
        /// Array power.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <param name="p">The specified power.</param>
        /// <returns>The array power root.</returns>
        public static CMatrix Pow(CMatrix A, int p)
        {
            return new CMatrix(A.rows, A.columns, MArray.Pow(A.elements, p));
        }

        /// <summary>
        /// Array power.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <param name="p">The specified power.</param>
        /// <returns>The array power root.</returns>
        public static CMatrix Pow(CMatrix A, double p)
        {
            return new CMatrix(A.rows, A.columns, MArray.Pow(A.elements, p));
        }

        /// <summary>
        /// Array exponent.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The array exponent root.</returns>
        public static CMatrix Exp(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Exp(A.elements));
        }

        /// <summary>
        /// Natural logarithm.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The natural logarithm.</returns>
        public static CMatrix Log(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Log(A.elements));
        }

        /// <summary>
        ///  Common (base 10) logarithm.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The Common (base 10) logarithm.</returns>
        public static CMatrix Log10(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Log10(A.elements));
        }

        /// <summary>
        /// Sine of argument in radians.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The Sine of argument in radians.</returns>
        public static CMatrix Sin(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Sin(A.elements));
        }

        /// <summary>
        /// Cosine of argument in radians.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The Cosine of argument in radians.</returns>
        public static CMatrix Cos(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Cos(A.elements));
        }

        /// <summary>
        /// Tangent of argument in radians.
        /// </summary>
        /// <param name="A">A Matrix.</param>
        /// <returns>The tangent of argument in radians.</returns>
        public static CMatrix Tan(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Tan(A.elements));
        }

        /// <summary>
        /// Sine of argument in degrees.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The sine of argument in degrees.</returns>
        public static CMatrix Sind(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Sind(A.elements));
        }

        /// <summary>
        /// Cosine of argument in degrees.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The cosine of argument in degrees.</returns>
        public static CMatrix Cosd(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Cosd(A.elements));
        }

        /// <summary>
        /// Tangent of argument in degrees.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The tangent of argument in degrees.</returns>
        public static CMatrix Tand(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Tand(A.elements));
        }

        /// <summary>
        /// Arcsine function.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The arcsine of <paramref name="A"/>.</returns>
        public static CMatrix Asin(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Asin(A.elements));
        }

        /// <summary>
        /// Arccosine function.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The arccossine of <paramref name="A"/>.</returns>
        public static CMatrix Acos(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Acos(A.elements));
        }

        /// <summary>
        /// Arctangent function.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The arctangent of <paramref name="A"/>.</returns>
        public static CMatrix Atan(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Atan(A.elements));
        }

        /// <summary>
        /// Hyperbolic sine.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The Hyperbolic sine of <paramref name="A"/>.</returns>
        public static CMatrix Sinh(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Sinh(A.elements));
        }

        /// <summary>
        /// Hyperbolic cosine.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The Hyperbolic cosine of <paramref name="A"/>.</returns>
        public static CMatrix Cosh(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Cosh(A.elements));
        }

        /// <summary>
        /// Hyperbolic tangent.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The Hyperbolic tangent of <paramref name="A"/>.</returns>
        public static CMatrix Tanh(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Tanh(A.elements));
        }

        /// <summary>
        /// Round towards minus infinity.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The floor of <paramref name="A"/>.</returns>
        public static CMatrix Floor(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Floor(A.elements));
        }

        /// <summary>
        /// Round towards plus infinity.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The ceiling of <paramref name="A"/>.</returns>
        public static CMatrix Ceiling(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Ceiling(A.elements));
        }

        /// <summary>
        /// Round towards nearest integer.
        /// </summary>
        /// <param name="A">A Matrix</param>
        /// <returns>The nearest integers of <paramref name="A"/>.</returns>
        public static CMatrix Round(CMatrix A)
        {
            return new CMatrix(A.rows, A.columns, MArray.Round(A.elements));
        }

        #endregion

        #region Data Analysis

        /// <summary>
        ///  Largest component.Returns maximum elements and its index.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The maximum elements of <paramref name="A"/> and its index.</returns>
        public static Tuple<CMatrix, Matrix> MaxEleInd(CMatrix A)
        {
            CMatrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix MaxInd = new Matrix(1, n);
            CMatrix MaxEle = new CMatrix(1, n);
            for (int j = 1; j <= n; j++)
            {
                MaxEle[1, j] = Vec[1, j];
                MaxInd[1, j] = 1;
                for (int i = 2; i <= m; i++)
                {
                    if (Complex.Abs(Vec[i, j]) > Complex.Abs(MaxEle[1, j]))
                    {
                        MaxEle[1, j] = Vec[i, j];
                        MaxInd[1, j] = i;
                    }
                }
            }
            return new Tuple<CMatrix, Matrix>(MaxEle, MaxInd);
        }

        /// <summary>
        ///  Largest component.Returns maximum elements' index.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The maximum elements' index of <paramref name="A"/>.</returns>
        public static Matrix MaxInd(CMatrix A)
        {
            CMatrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix MaxInd = new Matrix(1, n);
            Complex MaxEle;
            for (int j = 1; j <= n; j++)
            {
                MaxEle = Vec[1, j];
                MaxInd[1, j] = 1;
                for (int i = 2; i <= m; i++)
                {
                    if (Complex.Abs(Vec[i, j]) > Complex.Abs(MaxEle))
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
        public static CMatrix Max(CMatrix A)
        {
            CMatrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            CMatrix MaxEle = new CMatrix(1, n);
            for (int j = 1; j <= n; j++)
            {
                MaxEle[1, j] = Vec[1, j];
                for (int i = 2; i <= m; i++)
                    if (Complex.Abs(Vec[i, j]) > Complex.Abs(MaxEle[1, j]))
                        MaxEle[1, j] = Vec[i, j];
            }
            return MaxEle;
        }

        /// <summary>
        /// Smallest component.Returns minimum elements and its index.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The minimum elements of <paramref name="A"/> and its index.</returns>
        public static Tuple<CMatrix, Matrix> MinEleInd(CMatrix A)
        {
            CMatrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix MinInd = new Matrix(1, n);
            CMatrix MinEle = new CMatrix(1, n);
            for (int j = 1; j <= n; j++)
            {
                MinEle[1, j] = Vec[1, j];
                MinInd[1, j] = 1;
                for (int i = 2; i <= m; i++)
                {
                    if (Complex.Abs(Vec[i, j]) < Complex.Abs(MinEle[1, j]))
                    {
                        MinEle[1, j] = Vec[i, j];
                        MinInd[1, j] = i;
                    }
                }
            }
            return new Tuple<CMatrix, Matrix>(MinEle, MinInd);
        }

        /// <summary>
        /// Smallest component.Returns minimum elements' index.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The minimum elements' index of <paramref name="A"/>.</returns>
        public static Matrix MinInd(CMatrix A)
        {
            CMatrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            Matrix MinInd = new Matrix(1, n);
            Complex MinEle;
            for (int j = 1; j <= n; j++)
            {
                MinEle = Vec[1, j];
                MinInd[1, j] = 1;
                for (int i = 2; i <= m; i++)
                {
                    if (Complex.Abs(Vec[i, j]) < Complex.Abs(MinEle))
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
        public static CMatrix Min(CMatrix A)
        {
            CMatrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            CMatrix MinEle = new CMatrix(1, n);
            for (int j = 1; j <= n; j++)
            {
                MinEle[1, j] = Vec[1, j];
                for (int i = 2; i <= m; i++)
                    if (Complex.Abs(Vec[i, j]) < Complex.Abs(MinEle[1, j]))
                        MinEle[1, j] = Vec[i, j];
            }
            return MinEle;
        }

        /// <summary>
        /// Sum of elements.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>Sum of elements.</returns>
        public static CMatrix Sum(CMatrix A)
        {

            CMatrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            CMatrix sm = Zeros(1, n);
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
        public static CMatrix Prod(CMatrix A)
        {
            CMatrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            CMatrix pd = Ones(1, n);
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
        public static CMatrix Diff(CMatrix A)
        {
            CMatrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            CMatrix df = new CMatrix(m - 1, n);
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
        public static CMatrix CumSum(CMatrix A)
        {
            CMatrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            CMatrix cs = new CMatrix(m, n);
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
        public static CMatrix CumProd(CMatrix A)
        {
            CMatrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            CMatrix cs = new CMatrix(m, n);
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
        public static CMatrix Mean(CMatrix A)
        {
            CMatrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            CMatrix sm = Zeros(1, n);
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
        public static Matrix Var(CMatrix A)
        {
            CMatrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            CMatrix Mu = Mean(Vec);
            Matrix V = Matrix.Zeros(1, n);
            for (int j = 1; j <= n; j++)
            {
                for (int i = 1; i <= m; i++)
                    V[1, j] += (Vec[i, j] - Mu[1, j]).ConjMul();
                V[1, j] = V[1, j] / (m - 1);
            }
            return V;
        }

        /// <summary>
        ///  Standard deviation.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns> Standard deviation of elements.</returns>
        public static Matrix Std(CMatrix A)
        {
            CMatrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            int m = Vec.rows;
            int n = Vec.columns;
            CMatrix Mu = Mean(Vec);
            Matrix Stand = Matrix.Zeros(1, n);
            for (int j = 1; j <= n; j++)
            {
                for (int i = 1; i <= m; i++)
                    Stand[1, j] += (Vec[i, j] - Mu[1, j]).ConjMul();
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
        public static CMatrix Sort(CMatrix A, int mode)
        {
            CMatrix Vec;
            if (A.rows == 1)
                Vec = Vectorize(A);
            else
                Vec = A;

            if (mode != 1 || mode != 2)
                throw new ArgumentException(Resources.Strings.Sort);

            int m = Vec.rows;
            int n = Vec.columns;
            CMatrix Acpy = new CMatrix(Vec);
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
        public static CMatrix Transpose(CMatrix A)
        {
            int m = A.rows;
            int n = A.columns;
            CMatrix At = new CMatrix(n, m);
            for (int i = 1; i <= m; i++)
                for (int j = 1; j <= n; j++)
                    At[j, i] = Complex.Conj(A[i, j]);
            return At;
        }

        /// <summary>
        /// Matrix transpose without conjugate.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>Non-conjugate Matrix transpose.</returns> 
        public static CMatrix TransposeDot(CMatrix A)
        {
            int m = A.rows;
            int n = A.columns;
            CMatrix At = new CMatrix(n, m);
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
        public static CMatrix Vectorize(CMatrix A)
        {
            return new CMatrix(A.rows * A.columns, 1, A.elements);
        }

        /// <summary>
        ///  Diagonal matrices.
        /// </summary>
        ///<param name="arr">An array.</param>
        /// <returns>A diagonal matrices whose diagonal elements is <paramref name="arr"/>.</returns>
        public static CMatrix Diag(double[] arr)
        {
            int n = arr.Length;
            CMatrix D = new CMatrix(n);
            for (int i = 1; i <= n; i++)
                D[i, i] = (Complex)arr[i - 1];
            return D;
        }

        /// <summary>
        ///  Diagonal matrices.
        /// </summary>
        ///<param name="arr">An array.</param>
        /// <returns>A diagonal matrices whose diagonal elements is <paramref name="arr"/>.</returns>
        public static CMatrix Diag(Complex[] arr)
        {
            int n = arr.Length;
            CMatrix D = new CMatrix(n);
            for (int i = 1; i <= n; i++)
                D[i, i] = arr[i - 1];
            return D;
        }

        /// <summary>
        ///  Diagonal matrices and diagonals of a matrix.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns> Diagonal matrices or diagonals of a matrix.</returns>
        public static CMatrix Diag(CMatrix A)
        {
            int min = A.rows;
            int max = A.columns;
            if (min > max)
            {
                min = A.columns;
                max = A.rows;
            }

            CMatrix D;
            if (min == 1)
            {
                D = Zeros(max, max);
                for (int i = 1; i <= max; i++)
                    D[i, i] = A.elements[i - 1];
                return D;
            }

            D = new CMatrix(min, 1);
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
        public static Complex Trace(CMatrix A)
        {
            Checker.IsSquare(A);
            Complex tr = Complex.Zero;
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
        public static CMatrix Cat(int dim, CMatrix A, CMatrix B)
        {
            int m1 = A.rows;
            int n1 = A.columns;
            int m2 = B.rows;
            int n2 = B.columns;
            if (dim == 1)
            {
                if (n1 != n2)
                    throw new ArgumentException(Resources.Strings.Cat);
                CMatrix connected = new CMatrix(m1 + m2, n1);
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
                CMatrix Conected = new CMatrix(m1, n1 + n2);
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
        public static CMatrix Rref(CMatrix A)
        {
            int row = A.rows;
            int col = A.columns;
            CMatrix temp = new CMatrix(1, col);
            CMatrix Acpy = new CMatrix(A);

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
                    if (Complex.Abs(Acpy[i, n]) > Complex.Abs(Acpy[k, n]))
                        k = i;
                }
                if (Acpy[k, n].CloseToZero())
                {
                    Acpy[k, n] = Complex.Zero;
                    jump++;
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
                Acpy[rr, n] = Complex.One;

                for (int i = rr + 1; i <= row; i++)
                    for (int j = n + 1; j <= col; j++)
                        Acpy[i, j] = -Acpy[i, n] * Acpy[rr, j] + Acpy[i, j];
                for (int i = rr + 1; i <= row; i++)
                    Acpy[i, n] = Complex.Zero;

                for (int i = 1; i < rr; i++)
                    for (int j = n + 1; j <= col; j++)
                        Acpy[i, j] = -Acpy[i, n] * Acpy[rr, j] + Acpy[i, j];
                for (int i = 1; i < rr; i++)
                    Acpy[i, n] = Complex.Zero;
            }
            return Acpy;
        }

        /// <summary>
        /// Matrix rank
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>An estimate of the number of linearly independent rows or columns of a matrix <paramref name="A"/>.</returns>
        public static int Rank(CMatrix A)
        {
            int row = A.rows;
            int col = A.columns;
            CMatrix temp = new CMatrix(1, col);
            CMatrix Acpy = new CMatrix(A);

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
                    if (Complex.Abs(Acpy[i, n]) > Complex.Abs(Acpy[k, n]))
                        k = i;
                }
                if (Acpy[k, n].CloseToZero())
                {
                    jump++;
                    Acpy[k, n] = Complex.Zero;
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

        private static CMatrix Rref(CMatrix A, out int rank)
        {
            int row = A.rows;
            int col = A.columns;
            CMatrix temp = new CMatrix(1, col);
            CMatrix Acpy = new CMatrix(A);

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
                    if (Complex.Abs(Acpy[i, n]) > Complex.Abs(Acpy[k, n]))
                        k = i;
                }
                if (Acpy[k, n].CloseToZero())
                {
                    jump++;
                    Acpy[k, n] = Complex.Zero;
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
                Acpy[rr, n] = Complex.One;

                for (int i = rr + 1; i <= row; i++)
                    for (int j = n + 1; j <= col; j++)
                        Acpy[i, j] = -Acpy[i, n] * Acpy[rr, j] + Acpy[i, j];
                for (int i = rr + 1; i <= row; i++)
                    Acpy[i, n] = Complex.Zero;

                for (int i = 1; i < rr; i++)
                    for (int j = n + 1; j <= col; j++)
                        Acpy[i, j] = -Acpy[i, n] * Acpy[rr, j] + Acpy[i, j];
                for (int i = 1; i < rr; i++)
                    Acpy[i, n] = Complex.Zero;
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
        public static double RankLU(CMatrix A)
        {
            int m = A.rows;
            int n = A.columns;

            CMatrix temp = new CMatrix(1, n);
            CMatrix Acpy = new CMatrix(A);

            int min = m < n ? m : n;
            int maxind;
            Complex md;
            Complex aii;
            int rank = min;

            for (int i = 1; i <= min; i++)
            {
                md = Complex.Zero;
                for (int k = 1; k < i; k++)
                    md += Acpy[i, k] * Acpy[k, i];

                aii = Acpy[i, i] - md;
                maxind = i;

                for (int r = i + 1; r <= min; r++)
                {
                    md = Complex.Zero;
                    for (int k = 1; k < i; k++)
                        md += Acpy[r, k] * Acpy[k, i];

                    md = Acpy[r, i] - md;
                    if (Complex.Abs(md) > Complex.Abs(aii))
                    {
                        maxind = r;
                        aii = md;
                    }
                }

                if (aii.CloseToZero())
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
                        md = Complex.Zero;
                        for (int k = 1; k < i; k++)
                            md += Acpy[i, k] * Acpy[k, j];
                        Acpy[i, j] = Acpy[i, j] - md;
                    }

                    for (int j = i + 1; j <= m; j++)
                    {
                        md = Complex.Zero;
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
        public static Complex Det(CMatrix A)
        {
            Checker.IsSquare(A);

            int m = A.columns;
            Complex a;
            CMatrix temp = new CMatrix(1, m);
            CMatrix Acpy = new CMatrix(A);

            int k;
            Complex det = Complex.One;
            int sign = 1;

            for (int n = 1; n <= m; n++)
            {
                k = n;
                for (int i = n + 1; i <= m; i++)
                {
                    if (Complex.Abs(Acpy[i, n]) > Complex.Abs(Acpy[k, n]))
                        k = i;
                }
                if (Acpy[k, n] == Complex.Zero)
                    return Complex.Zero;

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
        public static Complex DetLU(CMatrix A)
        {
            Checker.IsSquare(A);
            int n = A.rows;

            CMatrix temp = new CMatrix(1, n);
            CMatrix Acpy = new CMatrix(A);

            int maxind;
            Complex md;
            Complex aii;

            int sign = 1;
            Complex det = Complex.One;

            for (int i = 1; i <= n; i++)
            {
                md = Complex.Zero;
                for (int k = 1; k < i; k++)
                    md += Acpy[i, k] * Acpy[k, i];

                aii = Acpy[i, i] - md;
                maxind = i;

                for (int r = i + 1; r <= n; r++)
                {
                    md = Complex.Zero;
                    for (int k = 1; k < i; k++)
                        md += Acpy[r, k] * Acpy[k, i];

                    md = Acpy[r, i] - md;
                    if (Complex.Abs(md) > Complex.Abs(aii))
                    {
                        maxind = r;
                        aii = md;
                    }
                }

                if (aii == Complex.Zero)
                    return Complex.Zero;

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
                        md = Complex.Zero;
                        for (int k = 1; k < i; k++)
                            md += Acpy[i, k] * Acpy[k, j];
                        Acpy[i, j] = Acpy[i, j] - md;
                    }

                    for (int j = i + 1; j <= n; j++)
                    {
                        md = Complex.Zero;
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
        public static CMatrix Inv(CMatrix A)
        {
            Checker.IsSquare(A);
            int m = A.rows;
            CMatrix temp = new CMatrix(1, 2 * m);
            CMatrix AE = Cat(2, A, Eye(m));
            CMatrix inv = new CMatrix(m);
            int k;

            for (int n = 1; n <= m; n++)
            {
                k = n;

                for (int i = n + 1; i <= m; i++)
                {
                    if (Complex.Abs(AE[i, n]) > Complex.Abs(AE[k, n]))
                        k = i;
                }
                if (AE[k, n].CloseToZero())
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
        public static CMatrix Pinv(CMatrix A)
        {
            int m = A.rows;
            int n = A.columns;
            int rk;
            CMatrix Ar = Rref(A, out rk);
            CMatrix buf;

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
                CMatrix B = new CMatrix(m, rk);
                CMatrix C;
                for (int i = 1; i <= m; i++)
                {
                    for (int j = i; j <= n; j++)
                        if (Ar[i, j].Real > 0.0)//first non zero number is 1.
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

        #region  Normed Space Analysis

        /// <summary>
        /// Spectral radius of a square matrix.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The Spectral radius of square matrix <paramref name="A"/>.</returns>     
        public static double Sprad(CMatrix A)
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
        public static double Norm(CMatrix A, int p)
        {
            int m = A.rows;
            int n = A.columns;

            switch (p)
            {
                case 1:
                    return Matrix.Sum(Abs(A)).Elements.Max();
                case 2:
                    if (m == 1 || n == 1)
                        return A.elements.Norm();
                    return SV(A)[0];

                case 'I':
                    if (m == 1 || n == 1)
                        return MArray.NormInf(A.elements);
                    return Matrix.Sum(Abs(Transpose(A))).Elements.Max();
                case 'F':
                    return Math.Sqrt(MArray.ConjMul(A.elements).Sum());
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
        public static double Cond(CMatrix A, int p)
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
        public static double Rcond(CMatrix A)
        {
            return 1.0 / Cond(A, 1);
        }

        #endregion

        #region System Of Linear Equations

        /// <summary>
        ///  Null space. Null(A) is an basis for the null space of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>An basis for the null space of <paramref name="A"/>.</returns>
        /// <exception cref="ArithmeticException">Throw if <paramref name="A"/> is full rank.</exception>
        public static CMatrix Null(CMatrix A)
        {
            int rk;
            CMatrix X = Eye(A.columns) - Pinv(A) * A;
            rk = (int)Math.Round(Matrix.Trace(X.Real));
            if (rk == 0)
                throw new ArithmeticException(string.Format(" Empty matrix: {0}-by-0.", A.rows));

            int k = 1;
            CMatrix temp;
            CMatrix N = new CMatrix(A.columns, rk);
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
        public static CMatrix LinSolve(CMatrix A, CMatrix b)
        {
            if (b.rows == 1)
                b = Vectorize(b);

            if (A.rows != b.rows)
                throw new ArgumentException(Resources.Strings.Solve);

            return Pinv(A) * b;
        }

        /// <summary>
        ///  Linsolve Solve linear system <paramref name="A"/>*x=<paramref name="b"/>.It use pseudo-inverse to solve all kinds of linear equations.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <param name="b">A row or column matrix.</param>
        /// <returns>The solution of A*X=b. </returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not square or the dimensions of <paramref name="A"/> 
        /// and <paramref name="b"/> is not agree.</exception>
        public static CMatrix LinSolve(CMatrix A, Matrix b)
        {
            if (b.Rows == 1)
                b = Matrix.Vectorize(b);

            if (A.rows != b.Rows)
                throw new ArgumentException(Resources.Strings.Solve);

            return Pinv(A) * b;
        }

        /// <summary>
        ///  Linsolve Solve linear system <paramref name="A"/>*x=<paramref name="b"/>.It use pseudo-inverse to solve all kinds of linear equations.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <param name="b">A row or column matrix.</param>
        /// <returns>The solution of A*X=b. </returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not square or the dimensions of <paramref name="A"/> 
        /// and <paramref name="b"/> is not agree.</exception>
        public static CMatrix LinSolve(Matrix A, CMatrix b)
        {
            if (b.rows == 1)
                b = Vectorize(b);

            if (A.Rows != b.rows)
                throw new ArgumentException(Resources.Strings.Solve);

            return Matrix.Pinv(A) * b;
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
        public static CMatrix LUSolve(CMatrix A, CMatrix b)
        {
            Checker.IsSquare(A);
            CMatrix vb = new CMatrix(b);
            if (b.rows == 1)
                vb = Vectorize(b);

            int m = A.rows;
            if (m != vb.rows)
                throw new ArgumentException(Resources.Strings.Solve);

            CMatrix[] LUP = LU(A);
            CMatrix L = LUP[0];
            CMatrix U = LUP[1];

            vb = LUP[2] * vb;
            CMatrix x = new CMatrix(m, 1);
            Complex md;
            for (int i = 1; i <= m; i++)
            {
                md = Complex.Zero;
                for (int k = 1; k < i; k++)
                    md += L[i, k] * x[k, 1];
                x[i, 1] = vb[i, 1] - md;
            }
            for (int i = m; i > 0; i--)
            {
                md = Complex.Zero;
                for (int k = i + 1; k <= m; k++)
                    md += U[i, k] * x[k, 1];

                if (U[i, i].CloseToZero())
                    throw new ArithmeticException(Resources.Strings.Singular);

                x[i, 1] = (x[i, 1] - md) / U[i, i];
            }
            return x;
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
        public static CMatrix LUSolve(CMatrix A, Matrix b)
        {
            Checker.IsSquare(A);
            int m = A.rows;

            Matrix vb = new Matrix(b);
            if (b.Rows == 1)
                vb = Matrix.Vectorize(b);

            if (m != vb.Rows)
                throw new ArgumentException(Resources.Strings.Solve);

            CMatrix[] LUP = LU(A);
            CMatrix L = LUP[0];
            CMatrix U = LUP[1];

            vb = LUP[2].Real * vb;
            CMatrix x = new CMatrix(m, 1);
            Complex md;
            for (int i = 1; i <= m; i++)
            {
                md = Complex.Zero;
                for (int k = 1; k < i; k++)
                    md += L[i, k] * x[k, 1];
                x[i, 1] = vb[i, 1] - md;
            }
            for (int i = m; i > 0; i--)
            {
                md = Complex.Zero;
                for (int k = i + 1; k <= m; k++)
                    md += U[i, k] * x[k, 1];

                if (U[i, i].CloseToZero())
                    throw new ArithmeticException(Resources.Strings.Singular);

                x[i, 1] = (x[i, 1] - md) / U[i, i];
            }
            return x;
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
        public static CMatrix LUSolve(Matrix A, CMatrix b)
        {
            Checker.IsSquare(A);
            int m = A.Rows;

            CMatrix vb = new CMatrix(b);
            if (b.rows == 1)
                vb = Vectorize(b);

            if (m != vb.rows)
                throw new ArgumentException(Resources.Strings.Solve);

            Matrix[] LUP = Matrix.LU(A);
            Matrix L = LUP[0];
            Matrix U = LUP[1];

            vb = LUP[2] * vb;
            CMatrix x = new CMatrix(m, 1);
            Complex md;
            for (int i = 1; i <= m; i++)
            {
                md = Complex.Zero;
                for (int k = 1; k < i; k++)
                    md += L[i, k] * x[k, 1];
                x[i, 1] = vb[i, 1] - md;
            }
            for (int i = m; i > 0; i--)
            {
                md = Complex.Zero;
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
        public static CMatrix CholSolve(CMatrix A, CMatrix b)
        {
            Checker.IsSymmetric(A);
            int m = A.rows;

            if (b.rows == 1)
                b = Vectorize(b);

            if (m != b.rows)
                throw new ArgumentException(Resources.Strings.Solve);

            CMatrix[] LD = Chol(A);
            CMatrix L = LD[0];
            CMatrix D = LD[1];
            CMatrix x = new CMatrix(m, 1);
            Complex md;

            for (int i = 1; i <= m; i++)
            {
                md = Complex.Zero;
                for (int k = 1; k < i; k++)
                    md += L[i, k] * x[k, 1];
                x[i, 1] = b[i, 1] - md;
            }
            for (int i = m; i > 0; i--)
            {
                md = Complex.Zero;
                for (int k = i + 1; k <= m; k++)
                    md += L[k, i] * x[k, 1];

                if (D[i, i].CloseToZero())
                    throw new ArithmeticException(Resources.Strings.PosDef);

                x[i, 1] = x[i, 1] / D[i, i] - md;
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
        public static CMatrix CholSolve(CMatrix A, Matrix b)
        {
            Checker.IsSymmetric(A);
            int m = A.rows;

            if (b.Rows == 1)
                b = Matrix.Vectorize(b);

            if (m != b.Rows)
                throw new ArgumentException(Resources.Strings.Solve);

            CMatrix[] LD = Chol(A);
            CMatrix L = LD[0];
            CMatrix D = LD[1];
            CMatrix x = new CMatrix(m, 1);
            Complex md;

            for (int i = 1; i <= m; i++)
            {
                md = Complex.Zero;
                for (int k = 1; k < i; k++)
                    md += L[i, k] * x[k, 1];
                x[i, 1] = b[i, 1] - md;
            }
            for (int i = m; i > 0; i--)
            {
                md = Complex.Zero;
                for (int k = i + 1; k <= m; k++)
                    md += L[k, i] * x[k, 1];

                if (D[i, i].CloseToZero())
                    throw new ArithmeticException(Resources.Strings.PosDef);

                x[i, 1] = x[i, 1] / D[i, i] - md;
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
        public static CMatrix CholSolve(Matrix A, CMatrix b)
        {
            Checker.IsSymmetric(A);
            int m = A.Rows;

            if (b.rows == 1)
                b = Vectorize(b);

            if (m != b.rows)
                throw new ArgumentException(Resources.Strings.Solve);

            Matrix[] LD = Matrix.Chol(A);
            Matrix L = LD[0];
            Matrix D = LD[1];
            CMatrix x = new CMatrix(m, 1);
            Complex md;

            for (int i = 1; i <= m; i++)
            {
                md = Complex.Zero;
                for (int k = 1; k < i; k++)
                    md += L[i, k] * x[k, 1];
                x[i, 1] = b[i, 1] - md;
            }
            for (int i = m; i > 0; i--)
            {
                md = Complex.Zero;
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
        public static CMatrix FollowUpSolve(CMatrix A, CMatrix b)
        {
            Checker.IsTridiag(A);
            int m = A.rows;

            if (b.rows == 1)
                b = Vectorize(b);

            if (m != b.rows)
                throw new ArgumentException(Resources.Strings.Solve);

            Complex[] L = new Complex[m];
            Complex[] U = new Complex[m];
            Complex[] x = new Complex[m];

            U[0] = A[1, 1];
            for (int i = 2; i <= m; i++)
            {
                L[i - 1] = A[i, i - 1] / U[i - 2];
                U[i - 1] = A[i, i] - A[i - 1, i] * L[i - 1];
            }

            x[0] = b[1, 1];
            for (int i = 2; i <= m; i++)
                x[i - 1] = b[i, 1] - L[i - 1] * x[i - 2];

            x[m - 1] = x[m - 1] / U[m - 1];
            for (int i = m - 1; i > 0; i--)
                x[i - 1] = (x[i - 1] - A[i, i + 1] * x[i]) / U[i - 1];

            return new CMatrix(m, 1, x);
        }

        /// <summary>
        /// FollowUpSolve use follow up method to Solve linear system A*X=b. A is a tridiagonal matrix.
        /// </summary>
        /// <param name="A">A tridiagonal matrix.</param>
        /// <param name="b">A column or row matrix.</param>
        /// <returns>The solution of A*X=b.</returns>
        /// <exception cref="ArgumentException">Throw if Matrix <paramref name="A"/> is not a tridiagonal matrix 
        /// or dementions of <paramref name="A"/> and <paramref name="b"/> is not agree.</exception>
        public static CMatrix FollowUpSolve(CMatrix A, Matrix b)
        {
            Checker.IsTridiag(A);
            int m = A.rows;

            if (b.Rows == 1)
                b = Matrix.Vectorize(b);

            if (m != b.Rows)
                throw new ArgumentException(Resources.Strings.Solve);

            Complex[] L = new Complex[m];
            Complex[] U = new Complex[m];
            Complex[] x = new Complex[m];

            U[0] = A[1, 1];
            for (int i = 2; i <= m; i++)
            {
                L[i - 1] = A[i, i - 1] / U[i - 2];
                U[i - 1] = A[i, i] - A[i - 1, i] * L[i - 1];
            }

            x[0] = (Complex)b[1, 1];
            for (int i = 2; i <= m; i++)
                x[i - 1] = b[i, 1] - L[i - 1] * x[i - 2];

            x[m - 1] = x[m - 1] / U[m - 1];
            for (int i = m - 1; i > 0; i--)
                x[i - 1] = (x[i - 1] - A[i, i + 1] * x[i]) / U[i - 1];

            return new CMatrix(m, 1, x);
        }


        /// <summary>
        /// FollowUpSolve use follow up method to Solve linear system A*X=b. A is a tridiagonal matrix.
        /// </summary>
        /// <param name="A">A tridiagonal matrix.</param>
        /// <param name="b">A column or row matrix.</param>
        /// <returns>The solution of A*X=b.</returns>
        /// <exception cref="ArgumentException">Throw if Matrix <paramref name="A"/> is not a tridiagonal matrix 
        /// or dementions of <paramref name="A"/> and <paramref name="b"/> is not agree.</exception>
        public static CMatrix FollowUpSolve(Matrix A, CMatrix b)
        {
            Checker.IsTridiag(A);
            int m = A.Rows;

            if (b.rows == 1)
                b = Vectorize(b);

            if (m != b.rows)
                throw new ArgumentException(Resources.Strings.Solve);

            Complex[] L = new Complex[m];
            Complex[] U = new Complex[m];
            Complex[] x = new Complex[m];

            U[0] = (Complex)A[1, 1];
            for (int i = 2; i <= m; i++)
            {
                L[i - 1] = A[i, i - 1] / U[i - 2];
                U[i - 1] = A[i, i] - A[i - 1, i] * L[i - 1];
            }

            x[0] = b[1, 1];
            for (int i = 2; i <= m; i++)
                x[i - 1] = b[i, 1] - L[i - 1] * x[i - 2];

            x[m - 1] = x[m - 1] / U[m - 1];
            for (int i = m - 1; i > 0; i--)
                x[i - 1] = (x[i - 1] - A[i, i + 1] * x[i]) / U[i - 1];

            return new CMatrix(m, 1, x);
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
        public static CMatrix GSSolve(CMatrix A, CMatrix b)
        {
            Checker.IsSquare(A);
            int n = A.columns;

            if (n != b.elements.Length)
                throw new ArgumentException(Resources.Strings.Solve);

            int k;
            Complex buf;
            Complex[] vb = new Complex[n];
            b.elements.CopyTo(vb, 0);
            CMatrix Acpy = new CMatrix(A);
            CMatrix temp = new CMatrix(1, n);

            for (int i = 1; i <= n; i++)
            {
                k = i;
                for (int r = i + 1; r <= n; r++)
                    if (Complex.Abs(Acpy[r, i]) > Complex.Abs(Acpy[k, i]))
                        k = r;
                if (Acpy[k, i].CloseToZero())
                    throw new ArgumentException(Resources.Strings.Singular);

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

            Complex[] xk = new Complex[n]; xk[0] = (Complex)DateTime.Now.Millisecond;
            Complex[] xn = new Complex[n];
            bool it = true;

            while (it)
            {
                for (int i = 1; i <= n; i++)
                {
                    buf = Complex.Zero;
                    for (int j = 1; j <= n; j++)
                        if (i != j)
                            buf += Acpy[i, j] * xk[j - 1];

                    if (double.IsInfinity(buf.Real) || double.IsInfinity(buf.Imaginary))
                        throw new ArgumentException(Resources.Strings.Converge);

                    xn[i - 1] = (vb[i - 1] - buf) / Acpy[i, i];
                }

                for (int i = 1; i <= n; i++)
                {
                    buf = Complex.Zero;
                    for (int j = 1; j < i; j++)
                        buf += Acpy[i, j] * xn[j - 1];
                    for (int j = i + 1; j <= n; j++)
                        buf += Acpy[i, j] * xn[j - 1];
                    xk[i - 1] = (vb[i - 1] - buf) / Acpy[i, i];
                }

                for (int i = 0; i < n; i++)
                {
                    if (Complex.Abs(xn[i] - xk[i]) > LibData.Eps)
                        break;
                    else
                        it = false;
                }
            }

            return new CMatrix(n, 1, xn);
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
        public static CMatrix GSSolve(CMatrix A, Matrix b)
        {
            Checker.IsSquare(A);
            int n = A.columns;

            if (n != b.Elements.Length)
                throw new ArgumentException(Resources.Strings.Solve);

            int k;
            double buf;
            double[] vb = new double[n];
            b.Elements.CopyTo(vb, 0);
            CMatrix Acpy = new CMatrix(A);
            CMatrix temp = new CMatrix(1, n);

            for (int i = 1; i <= n; i++)
            {
                k = i;
                for (int r = i + 1; r <= n; r++)
                    if (Complex.Abs(Acpy[r, i]) > Complex.Abs(Acpy[k, i]))
                        k = r;
                if (Acpy[k, i].CloseToZero())
                    throw new ArgumentException(Resources.Strings.Singular);

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

            Complex[] xk = new Complex[n]; xk[0] = (Complex)DateTime.Now.Millisecond;
            Complex[] xn = new Complex[n];
            Complex md;
            bool it = true;

            while (it)
            {
                for (int i = 1; i <= n; i++)
                {
                    md = Complex.Zero;
                    for (int j = 1; j <= n; j++)
                        if (i != j)
                            md += Acpy[i, j] * xk[j - 1];

                    if (double.IsInfinity(md.Real) || double.IsInfinity(md.Real))
                        throw new ArgumentException(Resources.Strings.Converge);

                    xn[i - 1] = (vb[i - 1] - md) / Acpy[i, i];
                }

                for (int i = 1; i <= n; i++)
                {
                    md = Complex.Zero;
                    for (int j = 1; j < i; j++) if (i != j)
                            md += Acpy[i, j] * xn[j - 1];
                    for (int j = i + 1; j <= n; j++) if (i != j)
                            md += Acpy[i, j] * xn[j - 1];

                    xk[i - 1] = (vb[i - 1] - md) / Acpy[i, i];
                }

                for (int i = 0; i < n; i++)
                {
                    if (Complex.Abs(xn[i] - xk[i]) > LibData.Eps)
                        break;
                    else
                        it = false;
                }

            }

            return new CMatrix(n, 1, xn);
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
        public static CMatrix GSSolve(Matrix A, CMatrix b)
        {
            Checker.IsSquare(A);
            int n = A.Columns;

            if (n != b.elements.Length)
                throw new ArgumentException(Resources.Strings.Solve);

            int k;
            Complex buf;
            Complex[] vb = new Complex[n];
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
                    throw new ArgumentException(Resources.Strings.Singular);

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

            Complex[] xk = new Complex[n]; xk[0] = (Complex)DateTime.Now.Millisecond;
            Complex[] xn = new Complex[n];
            bool it = true;

            while (it)
            {
                for (int i = 1; i <= n; i++)
                {
                    buf = Complex.Zero;
                    for (int j = 1; j <= n; j++)
                        if (i != j)
                            buf += Acpy[i, j] * xk[j - 1];

                    if (double.IsInfinity(buf.Real) || double.IsInfinity(buf.Imaginary))
                        throw new ArgumentException(Resources.Strings.Converge);

                    xn[i - 1] = (vb[i - 1] - buf) / Acpy[i, i];
                }

                for (int i = 1; i <= n; i++)
                {
                    buf = Complex.Zero;
                    for (int j = 1; j < i; j++)
                        buf += Acpy[i, j] * xn[j - 1];
                    for (int j = i + 1; j <= n; j++)
                        buf += Acpy[i, j] * xn[j - 1];
                    xk[i - 1] = (vb[i - 1] - buf) / Acpy[i, i];
                }

                for (int i = 0; i < n; i++)
                {
                    if (Math.Abs(xn[i].Real - xk[i].Real) > LibData.Eps || Math.Abs(xn[i].Imaginary - xk[i].Imaginary) > LibData.Eps)
                        break;
                    else
                        it = false;
                }

            }
            return new CMatrix(n, 1, xn);
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
        public static CMatrix SORSolve(CMatrix A, CMatrix b, double w)
        {
            if (w < 0 || w > 2)
                throw new ArgumentException(Resources.Strings.RelFac);

            Checker.IsSquare(A);
            int n = A.columns;

            if (n != b.elements.Length)
                throw new ArgumentException(Resources.Strings.Solve);

            int k;
            Complex buf;
            Complex[] vb = new Complex[n];
            b.elements.CopyTo(vb, 0);
            CMatrix Acpy = new CMatrix(A);
            CMatrix temp = new CMatrix(1, n);

            for (int i = 1; i <= n; i++)
            {
                k = i;
                for (int r = i + 1; r <= n; r++)
                    if (Complex.Abs(Acpy[r, i]) > Complex.Abs(Acpy[k, i]))
                        k = r;
                if (Acpy[k, i].CloseToZero())
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

            Complex[] xk = new Complex[n]; xk[0] = (Complex)DateTime.Now.Millisecond;
            Complex[] xn = new Complex[n];
            bool it = true;

            while (it)
            {
                for (int i = 1; i <= n; i++)
                {
                    buf = Complex.Zero;
                    for (int j = 1; j < i; j++)
                        buf += Acpy[i, j] * xk[j - 1];
                    for (int j = i + 1; j <= n; j++)
                        buf += Acpy[i, j] * xk[j - 1];

                    if (double.IsInfinity(buf.Real) || double.IsInfinity(buf.Imaginary))
                        throw new ArithmeticException(Resources.Strings.Converge);

                    xn[i - 1] = (1 - w) * xn[i - 1] + w * (vb[i - 1] - buf) / Acpy[i, i];
                }

                for (int i = 1; i <= n; i++)
                {
                    buf = Complex.Zero;
                    for (int j = 1; j < i; j++)
                        buf += Acpy[i, j] * xn[j - 1];
                    for (int j = i + 1; j <= n; j++)
                        buf += Acpy[i, j] * xn[j - 1];
                    xk[i - 1] = (1 - w) * xk[i - 1] + w * (vb[i - 1] - buf) / Acpy[i, i];
                }

                for (int i = 0; i < n; i++)
                {
                    if (Math.Abs(xn[i].Real - xk[i].Real) > LibData.Eps || Math.Abs(xn[i].Imaginary - xk[i].Imaginary) > LibData.Eps)
                        break;
                    else
                        it = false;
                }
            }

            return new CMatrix(n, 1, xn);
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
        public static CMatrix SORSolve(CMatrix A, Matrix b, double w)
        {
            if (w < 0 || w > 2)
                throw new ArgumentException(Resources.Strings.RelFac);

            Checker.IsSquare(A);
            int n = A.columns;

            if (n != b.Elements.Length)
                throw new ArgumentException(Resources.Strings.Solve);

            int k;
            double buf;
            double[] vb = new double[n];
            b.Elements.CopyTo(vb, 0);
            CMatrix Acpy = new CMatrix(A);
            CMatrix temp = new CMatrix(1, n);

            for (int i = 1; i <= n; i++)
            {
                k = i;
                for (int r = i + 1; r <= n; r++)
                    if (Complex.Abs(Acpy[r, i]) > Complex.Abs(Acpy[k, i]))
                        k = r;
                if (Acpy[k, i].CloseToZero())
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

            Complex md;
            Complex[] xk = new Complex[n]; xk[0] = (Complex)DateTime.Now.Millisecond;
            Complex[] xn = new Complex[n];
            bool it = true;

            while (it)
            {
                for (int i = 1; i <= n; i++)
                {
                    md = Complex.Zero;
                    for (int j = 1; j < i; j++)
                        md += Acpy[i, j] * xk[j - 1];
                    for (int j = i + 1; j <= n; j++)
                        md += Acpy[i, j] * xk[j - 1];

                    if (double.IsInfinity(md.Real) || double.IsInfinity(md.Imaginary))
                        throw new ArithmeticException(Resources.Strings.Converge);

                    xn[i - 1] = (1 - w) * xn[i - 1] + w * (vb[i - 1] - md) / Acpy[i, i];
                }

                for (int i = 1; i <= n; i++)
                {
                    md = Complex.Zero;
                    for (int j = 1; j < i; j++)
                        md += Acpy[i, j] * xn[j - 1];
                    for (int j = i + 1; j <= n; j++)
                        md += Acpy[i, j] * xn[j - 1];
                    xk[i - 1] = (1 - w) * xk[i - 1] + w * (vb[i - 1] - md) / Acpy[i, i];
                }

                for (int i = 0; i < n; i++)
                {
                    if (Math.Abs(xn[i].Real - xk[i].Real) > LibData.Eps || Math.Abs(xn[i].Imaginary - xk[i].Imaginary) > LibData.Eps)
                        break;
                    else
                        it = false;
                }
            }

            return new CMatrix(n, 1, xn);
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
        public static CMatrix SORSolve(Matrix A, Matrix b, double w)
        {
            if (w < 0 || w > 2)
                throw new ArgumentException(Resources.Strings.RelFac);

            Checker.IsSquare(A);
            int n = A.Columns;

            if (n != b.Elements.Length)
                throw new ArgumentException(Resources.Strings.Solve);

            int k;
            double buf;
            double[] vb = new double[n];
            b.Elements.CopyTo(vb, 0);
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

            Complex md;
            Complex[] xk = new Complex[n]; xk[0] = (Complex)DateTime.Now.Millisecond;
            Complex[] xn = new Complex[n];
            bool it = true;

            while (it)
            {
                for (int i = 1; i <= n; i++)
                {
                    md = Complex.Zero;
                    for (int j = 1; j < i; j++)
                        md += Acpy[i, j] * xk[j - 1];
                    for (int j = i + 1; j <= n; j++)
                        md += Acpy[i, j] * xk[j - 1];

                    if (double.IsInfinity(md.Real) || double.IsInfinity(md.Imaginary))
                        throw new ArithmeticException(Resources.Strings.Converge);

                    xn[i - 1] = (1 - w) * xn[i - 1] + w * (vb[i - 1] - md) / Acpy[i, i];
                }

                for (int i = 1; i <= n; i++)
                {
                    md = Complex.Zero;
                    for (int j = 1; j < i; j++)
                        md += Acpy[i, j] * xn[j - 1];
                    for (int j = i + 1; j <= n; j++)
                        md += Acpy[i, j] * xn[j - 1];
                    xk[i - 1] = (1 - w) * xk[i - 1] + w * (vb[i - 1] - md) / Acpy[i, i];
                }

                for (int i = 0; i < n; i++)
                {
                    if (Math.Abs(xn[i].Real - xk[i].Real) > LibData.Eps || Math.Abs(xn[i].Imaginary - xk[i].Imaginary) > LibData.Eps)
                        break;
                    else
                        it = false;
                }
            }

            return new CMatrix(n, 1, xn);
        }

        #endregion

        #region Matrix Transformation

        /// <summary>
        /// Householder transformation.  Atention: ColMat is a column matrix !
        /// Before use it,Check if ColMat is close to a zero vector.
        /// </summary>
        private static CMatrix House(CMatrix ColMat)
        {
            int n = ColMat.rows;

            ColMat[1, 1] += Complex.Sign(ColMat[1, 1]) * ColMat.elements.Norm();
            ColMat = ColMat / ColMat.elements.Norm();

            CMatrix wwT = new CMatrix(n);
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= i; j++)
                    wwT[i, j] = ColMat.elements[i - 1] * Complex.Conj(ColMat.elements[j - 1]);

            for (int j = 1; j <= n; j++)
                for (int i = 1; i < j; i++)
                    wwT[i, j] = Complex.Conj(wwT[j, i]);

            return Eye(n) - 2 * wwT;
        }

        private static CMatrix House(CMatrix ColMat, out Complex sign_norm)
        {
            int n = ColMat.rows;

            sign_norm = Complex.Sign(ColMat[1, 1]) * ColMat.elements.Norm();
            ColMat[1, 1] += sign_norm;
            ColMat = ColMat / ColMat.elements.Norm();

            CMatrix wwT = new CMatrix(n);
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= i; j++)
                    wwT[i, j] = ColMat.elements[i - 1] * Complex.Conj(ColMat.elements[j - 1]);

            for (int j = 1; j <= n; j++)
                for (int i = 1; i < j; i++)
                    wwT[i, j] = Complex.Conj(wwT[j, i]);

            return Eye(n) - 2 * wwT;
        }

        /// <summary>
        /// Givens Rotation
        /// </summary>
        internal static Complex[] Givens(Complex x, Complex y)
        {
            Complex sign = Complex.Sign(x);
            double r = Math.Sqrt(x.Real * x.Real + x.Imaginary * x.Imaginary + y.Real * y.Real + y.Imaginary * y.Imaginary);
            if (r == 0)
                return new Complex[3];
            double c = Complex.Abs(x) / r;
            Complex s = sign * Complex.Conj(y) / r;
            return new Complex[3] { (Complex)c, s, sign * r };
        }

        /// <summary>
        /// Variant Givens Rotation
        /// </summary>
        private static Complex[] Givens_variant(Complex x, Complex y)
        {
            double r = Math.Sqrt(x.Real * x.Real + x.Imaginary * x.Imaginary + y.Real * y.Real + y.Imaginary * y.Imaginary);
            if (r == 0)
                return new Complex[3];
            return new Complex[3] { Complex.Conj(x) / r, Complex.Conj(y) / r, (Complex)r };
        }

        /// <summary>
        /// Bidiagonalization for the SVD use Householder reflection. B=U'*A*V,A is a matrix of m*n (m>n) 
        /// B is a Bidiagonal matrix,and U,V are unitary matries.
        /// </summary>
        /// <param name="A">A matrix that rows greater than columns.</param>
        /// <returns>The Bidiagonalized matrix.</returns>
        /// <exception cref=" ArgumentException">Throw if rows is less than columns.</exception>
        internal static CMatrix[] Bidiag_house(CMatrix A)
        {
            int m = A.rows;
            int n = A.columns;
            int min = m < n ? m : n;

            CMatrix D = new CMatrix(A);
            CMatrix U = Eye(m);
            CMatrix V = Eye(n);
            CMatrix w;
            CMatrix h;

            for (int i = 1; i <= min; i++)
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
            return new CMatrix[3] { U, D, V };
        }

        /// <summary>
        /// Bidiagonalization for the SVD use Givens rptation. B=U'*A*V,A is a matrix of m*n (m>n) 
        /// B is a real Bidiagonal matrix,and U,V are unitary matries.
        /// </summary>
        /// <param name="A">A matrix that rows greater than columns.</param>
        /// <returns>The Bidiagonalized matrix.</returns>
        /// <exception cref=" ArgumentException">Throw if rows is less than columns.</exception>
        public static Tuple<CMatrix, Matrix, CMatrix> Bidiag(CMatrix A)
        {
            int m = A.rows;
            int n = A.columns;
            if (m < n)
                throw new ArgumentException(Resources.Strings.Bidiag);

            CMatrix B = new CMatrix(A);
            CMatrix U = Eye(m);
            CMatrix V = Eye(n);

            Complex md;
            Complex[] cs;

            for (int i = 1; i < n; i++)
            {
                for (int r = i + 1; r <= m; r++)
                    if (B[r, i] != Complex.Zero)
                    {
                        cs = Givens_variant(B[i, i], B[r, i]);
                        for (int k = i + 1; k <= n; k++)
                        {
                            md = B[i, k];
                            B[i, k] = cs[0] * md + cs[1] * B[r, k];
                            B[r, k] = -Complex.Conj(cs[1]) * md + Complex.Conj(cs[0]) * B[r, k];
                        }
                        B[i, i] = cs[2];
                        B[r, i] = Complex.Zero;
                        for (int k = 1; k <= m; k++)
                        {
                            md = U[k, i];
                            U[k, i] = Complex.Conj(cs[0]) * md + Complex.Conj(cs[1]) * U[k, r];
                            U[k, r] = -cs[1] * md + cs[0] * U[k, r];
                        }
                    }

                for (int r = i + 2; r <= n; r++)
                    if (B[i, r] != Complex.Zero)
                    {
                        cs = Givens_variant(B[i, i + 1], B[i, r]);
                        for (int k = i + 1; k <= m; k++)
                        {
                            md = B[k, i + 1];
                            B[k, i + 1] = cs[0] * md + cs[1] * B[k, r];
                            B[k, r] = -Complex.Conj(cs[1]) * md + Complex.Conj(cs[0]) * B[k, r];
                        }
                        B[i, i + 1] = cs[2];
                        B[i, r] = Complex.Zero;
                        for (int k = 1; k <= n; k++)
                        {
                            md = V[k, i + 1];
                            V[k, i + 1] = cs[0] * md + cs[1] * V[k, r];
                            V[k, r] = -Complex.Conj(cs[1]) * md + Complex.Conj(cs[0]) * V[k, r];
                        }
                    }
            }

            for (int r = n + 1; r <= m; r++)
                if (B[r, n] != Complex.Zero)
                {
                    cs = Givens_variant(B[n, n], B[r, n]);
                    B[r, n] = Complex.Zero;
                    B[n, n] = cs[2];

                    for (int k = 1; k <= m; k++)
                    {
                        md = U[k, n];
                        U[k, n] = Complex.Conj(cs[0]) * md + Complex.Conj(cs[1]) * U[k, r];
                        U[k, r] = -cs[1] * md + cs[0] * U[k, r];
                    }
                }

            if (n > 1)
            {
                double buf;
                Complex buf2;

                buf = Complex.Abs(B[n - 1, n]);
                buf2 = Complex.Conj(B[n - 1, n]) / buf;
                for (int i = 1; i <= n; i++)
                    V[i, n] *= buf2;
                B[n, n] *= buf2;
                B[n - 1, n] = (Complex)buf;

                buf = Complex.Abs(B[n, n]);
                for (int i = 1; i <= m; i++)
                    U[i, n] *= B[n, n] / buf;
                B[n, n] = (Complex)buf;
            }
            return new Tuple<CMatrix, Matrix, CMatrix>(U, B.Real, V);
        }

        /// <summary>
        /// Bidiagonalization for the SVD. U'*A*V=B,A: matrix of m*n(m>n) B :Bidiagonal matrix;U,V:unitary matrix.
        /// This method has not caculate U and V,this method is for compute singular value only.
        /// </summary>
        private static Matrix Bidiag_NoUV(CMatrix A)
        {
            int m = A.rows;
            int n = A.columns;

            CMatrix B = new CMatrix(A);

            Complex md;
            Complex[] cs;

            for (int i = 1; i < n; i++)
            {
                for (int r = i + 1; r <= m; r++)
                    if (B[r, i] != Complex.Zero)
                    {
                        cs = Givens_variant(B[i, i], B[r, i]);
                        for (int k = i + 1; k <= n; k++)
                        {
                            md = B[i, k];
                            B[i, k] = cs[0] * md + cs[1] * B[r, k];
                            B[r, k] = -Complex.Conj(cs[1]) * md + Complex.Conj(cs[0]) * B[r, k];
                        }
                        B[i, i] = cs[2];
                        B[r, i] = Complex.Zero;
                    }

                for (int r = i + 2; r <= n; r++)
                    if (B[i, r] != Complex.Zero)
                    {
                        cs = Givens_variant(B[i, i + 1], B[i, r]);
                        for (int k = i + 1; k <= m; k++)
                        {
                            md = B[k, i + 1];
                            B[k, i + 1] = cs[0] * md + cs[1] * B[k, r];
                            B[k, r] = -Complex.Conj(cs[1]) * md + Complex.Conj(cs[0]) * B[k, r];
                        }
                        B[i, i + 1] = cs[2];
                        B[i, r] = Complex.Zero;
                    }
            }

            for (int r = n + 1; r <= m; r++)
                if (B[r, n] != Complex.Zero)
                {
                    cs = Givens_variant(B[n, n], B[r, n]);
                    B[r, n] = Complex.Zero;
                    B[n, n] = cs[2];
                }

            if (n > 1)
            {
                double buf;
                Complex buf2;

                buf = Complex.Abs(B[n - 1, n]);
                buf2 = Complex.Conj(B[n - 1, n]) / buf;
                B[n, n] *= buf2;
                B[n - 1, n] = (Complex)buf;

                buf = Complex.Abs(B[n, n]);
                B[n, n] = (Complex)buf;
            }
            return B.Real;
        }

        /// <summary>
        /// Reduce to Upper Hessenberg form use Householder reflection. this routine is for QR iteration.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns>The Upper Hessenberg form.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        internal static CMatrix[] Hess_house(CMatrix A)
        {
            Checker.IsSquare(A);
            int n = A.rows;

            CMatrix H = new CMatrix(A);
            CMatrix U = Eye(n);
            CMatrix h;
            CMatrix v;

            for (int i = 2; i < n; i++)
            {
                v = H[i, n, i - 1, i - 1];
                if (v.elements.NonZeroVector())
                {
                    h = House(v);
                    H[i, i - 1] = (h[1, 1, 1, h.rows] * H[i, n, i - 1, i - 1]).elements[0];
                    for (int k = i + 1; k <= n; k++)
                        H[k, i - 1] = Complex.Zero;

                    H[1, i - 1, i, n] = H[1, i - 1, i, n] * h;
                    H[i, n, i, n] = h * H[i, n, i, n] * h;

                    U[1, n, i, n] *= h;
                }
            }
            return new CMatrix[2] { U, H };
        }

        /// <summary>
        /// Reduce to Upper Hessenberg form use Givens rotation. this routine is for QR iteration.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns>The Upper Hessenberg form.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        public static CMatrix[] Hess(CMatrix A)
        {
            Checker.IsSquare(A);
            int n = A.rows;

            CMatrix H = new CMatrix(A);
            CMatrix U = Eye(n);

            Complex[] cs;
            Complex md;

            for (int i = 2; i < n; i++)
                for (int r = i + 1; r <= n; r++)
                    if (H[r, i - 1] != Complex.Zero)
                    {
                        cs = Givens(H[i, i - 1], H[r, i - 1]);

                        for (int k = i; k <= n; k++)
                        {
                            md = H[i, k];
                            H[i, k] = cs[0] * md + cs[1] * H[r, k];
                            H[r, k] = -Complex.Conj(cs[1]) * md + cs[0] * H[r, k];
                        }
                        H[i, i - 1] = cs[2];
                        H[r, i - 1] = Complex.Zero;

                        for (int k = 1; k <= n; k++)
                        {
                            md = H[k, i];
                            H[k, i] = cs[0] * md + Complex.Conj(cs[1]) * H[k, r];
                            H[k, r] = -cs[1] * md + cs[0] * H[k, r];
                        }

                        for (int k = 1; k <= n; k++)
                        {
                            md = U[k, i];
                            U[k, i] = cs[0] * md + Complex.Conj(cs[1]) * U[k, r];
                            U[k, r] = -cs[1] * md + cs[0] * U[k, r];
                        }
                    }

            return new CMatrix[2] { U, H };
        }

        #endregion

        #region Matrix Decompostion

        /// <summary>
        /// LU decomposition with pivoting use Doolittle algorithm.
        /// P*A=L*U;L is an unit lower triangular matrix and U is an Upper triangular matrix,then P is a permutation matrix.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The decomposition of <paramref name="A"/>,returns L,U,P.</returns>
        public static CMatrix[] LU(CMatrix A)
        {
            int m = A.rows;
            int n = A.columns;

            CMatrix L = Eye(m);
            CMatrix U = new CMatrix(m, n);
            CMatrix P = Eye(m);
            CMatrix temp = new CMatrix(1, n);
            CMatrix tP = new CMatrix(1, m);
            CMatrix Acpy = new CMatrix(A);

            int min = m < n ? m : n;
            int maxind;
            Complex md;
            Complex aii;

            for (int i = 1; i <= min; i++)
            {
                md = Complex.Zero;
                for (int k = 1; k < i; k++)
                    md += Acpy[i, k] * Acpy[k, i];

                aii = Acpy[i, i] - md;
                maxind = i;
                for (int r = i + 1; r <= min; r++)
                {
                    md = Complex.Zero;
                    for (int k = 1; k < i; k++)
                        md += Acpy[r, k] * Acpy[k, i];

                    md = Acpy[r, i] - md;
                    if (Complex.Abs(md) > Complex.Abs(aii))
                    {
                        maxind = r;
                        aii = md;
                    }
                }

                if (!aii.CloseToZero())
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
                        md = Complex.Zero;
                        for (int k = 1; k < i; k++)
                            md += Acpy[i, k] * Acpy[k, j];
                        Acpy[i, j] = Acpy[i, j] - md;
                    }

                    for (int j = i + 1; j <= m; j++)
                    {
                        md = Complex.Zero;
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

            return new CMatrix[3] { L, U, P };
        }

        /// <summary>
        /// LU decomposition without pivoting use Doolittle algorithm.
        /// P*A=L*U;L is an unit lower triangular matrix and U is an Upper triangular matrix.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The decomposition of <paramref name="A"/>,returns L,U.</returns>
        /// <exception cref="ArithmeticException">Throw when LU decomposition whithout pivoting does not exist.</exception>
        public static CMatrix[] lu(CMatrix A)
        {
            int m = A.rows;
            int n = A.columns;

            CMatrix L = Eye(m);
            CMatrix U = new CMatrix(m, n);
            CMatrix Acpy = new CMatrix(A);

            int min = m < n ? m : n;
            Complex md;

            for (int i = 1; i <= min; i++)
            {
                for (int j = i; j <= n; j++)
                {
                    md = Complex.Zero;
                    for (int k = 1; k < i; k++)
                        md += Acpy[i, k] * Acpy[k, j];
                    Acpy[i, j] = Acpy[i, j] - md;
                }

                if (Acpy[i, i].CloseToZero())
                    throw new ArithmeticException(Resources.Strings.LU);

                for (int j = i + 1; j <= m; j++)
                {
                    md = Complex.Zero;
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

            return new CMatrix[2] { L, U };
        }

        /// <summary>
        ///  Cholesky factorization. 
        ///  A=L*D*L';L is an unit lower triangular matrix and D is a diagonal matrix.
        /// </summary>
        /// <param name="A">A positive definite matrix.</param>
        /// <returns>The Cholesky factorization of <paramref name="A"/>.returns L,D.</returns>
        /// <exception cref="ArithmeticException">Throw if <paramref name="A"/> is not a positive definite matrix.</exception>
        public static CMatrix[] Chol(CMatrix A)
        {
            Checker.IsSquare(A);
            int n = A.rows;

            CMatrix D = Eye(n);
            CMatrix L = new CMatrix(n);
            Complex md = Complex.Zero;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    if (A[i, j] != Complex.Conj(A[j, i]))
                        throw new ArithmeticException(Resources.Strings.Hermitian);

                    md = Complex.Zero;
                    for (int k = 1; k < j; k++)
                        md += L[i, k] * Complex.Conj(L[j, k]) / L[k, k];
                    L[i, j] = A[i, j] - md;
                }
                D[i, i] = L[i, i];
                if (L[i, i].CloseToZero())
                    throw new ArithmeticException(Resources.Strings.PosDef);
            }

            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= i; j++)
                    L[i, j] = L[i, j] / D[j, j];

            return new CMatrix[2] { L, D };
        }

        /// <summary>
        ///  Cholesky factorization use square-root method.
        ///  A=L*L';L is an lower triangular matrix.
        /// </summary>
        /// <param name="A">A positive definite matrix.</param>
        /// <returns>The Cholesky factorization of <paramref name="A"/>,returns L.</returns>
        /// <exception cref="ArithmeticException">Throw if <paramref name="A"/> is not a positive definite matrix.</exception>     
        public static CMatrix chol(CMatrix A)
        {
            Checker.IsSquare(A);
            int n = A.rows;

            CMatrix L = new CMatrix(n);
            double md;
            Complex cp;

            for (int i = 1; i <= n; i++)
            {
                if (A[i, i].Imaginary != 0)
                    throw new ArithmeticException(Resources.Strings.Hermitian);

                md = 0.0;
                for (int k = 1; k < i; k++)
                    md += L[i, k].ConjMul();
                md = A[i, i].Real - md;

                if (md <= LibData.Eps)
                    throw new ArithmeticException(Resources.Strings.PosDef);

                L[i, i] = (Complex)Math.Sqrt(md);
                for (int j = i + 1; j <= n; j++)
                {
                    if (A[i, j] != Complex.Conj(A[j, i]))
                        throw new ArithmeticException(Resources.Strings.Hermitian);

                    cp = Complex.Zero;
                    for (int k = 1; k < i; k++)
                        cp += L[i, k] * L[j, k];
                    L[j, i] = (A[i, j] - cp) / L[i, i];
                }
            }
            return L;
        }

        /// <summary>
        /// Orthogonal-triangular(QR) decomposition use Householder method.
        /// where A is m-by-n, produces an m-by-n upper triangular matrix R and an m-by-m unitary matrix Q so that A = Q*R.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>Orthogonal-triangular decomposition of <paramref name="A"/>,returns Q,R.</returns>
        public static CMatrix[] QRhouse(CMatrix A)
        {
            int row = A.rows;
            int col = A.columns;
            int min = row < col ? row : col;

            CMatrix R = new CMatrix(A);
            CMatrix Q = Eye(row);
            CMatrix v;

            CMatrix h;

            Complex sign_norm;

            for (int i = 1; i <= min; i++)
            {
                v = R[i, row, i, i];
                if (v.elements.NonZeroVector())
                    continue;

                h = House(v, out sign_norm);

                R[i, i] = -sign_norm;
                for (int r = i + 1; r <= row; r++)
                    R[r, i] = Complex.Zero;
                for (int j = i + 1; j <= col; j++)
                    R[i, row, j, j] = h * R[i, row, j, j];

                Q[1, row, i, row] *= h;
            }
            return new CMatrix[2] { Q, R };
        }

        /// <summary>
        /// Orthogonal-triangular(QR) decomposition use Givens method.
        /// where A is m-by-n, produces an m-by-n upper triangular matrix R and an m-by-m unitary matrix Q so that A = Q*R.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>Orthogonal-triangular decomposition of <paramref name="A"/>,returns Q,R.</returns>
        public static CMatrix[] QR(CMatrix A)
        {
            int row = A.rows;
            int col = A.columns;
            int min = row < col ? row : col;

            CMatrix R = new CMatrix(A);
            CMatrix Q = Eye(row);

            Complex[] cs;
            Complex md;

            for (int i = 1; i <= min; i++)
                for (int r = i + 1; r <= row; r++)
                    if (R[r, i] != Complex.Zero)
                    {
                        cs = Givens(R[i, i], R[r, i]);

                        for (int k = i + 1; k <= col; k++)
                        {
                            md = R[i, k];
                            R[i, k] = cs[0] * md + cs[1] * R[r, k];
                            R[r, k] = -Complex.Conj(cs[1]) * md + cs[0] * R[r, k];
                        }
                        R[i, i] = cs[2];
                        R[r, i] = Complex.Zero;
                        for (int k = 1; k <= row; k++)
                        {
                            md = Q[k, i];
                            Q[k, i] = cs[0] * md + Complex.Conj(cs[1]) * Q[k, r];
                            Q[k, r] = -cs[1] * md + cs[0] * Q[k, r];
                        }
                    }
            return new CMatrix[2] { Q, R };
        }

        /// <summary>
        /// Full rank decomposition. 
        /// where A is m-by-n, produces an m-by-r column full rank matrix cf and an r-by-n row full rank matrix rf.
        /// so that A = cf*rf.r is the rank of matrix <paramref name="A"/>.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The full rank decomposition of <paramref name="A"/>,returns cf,rf.</returns>
        public static CMatrix[] FRD(CMatrix A)
        {
            int m = A.rows;
            int n = A.columns;
            int rk;
            int ind = 0;
            CMatrix Ar = Rref(A, out rk);


            if (rk == 0)
                throw new ArgumentException(Resources.Strings.Singular + string.Format("\n Empty matrix: {0}-by-0 and 0-by-{1}.", m, n));

            CMatrix cf = new CMatrix(m, rk);
            CMatrix rf;
            for (int i = 1; i <= m; i++)
            {
                for (int j = i; j <= n; j++)
                    if (Ar[i, j].Real > 0.0)//first non zero number is 1.
                    {
                        ind++;
                        cf[1, m, ind, ind] = A[1, m, j, j];
                        break;
                    }
                if (ind == rk)
                    break;
            }
            rf = Ar[1, rk, 1, n];
            return new CMatrix[2] { cf, rf };
        }

        /// <summary>
        /// SVD，Singular Value Decomposition.svd(A) produces a diagonal matrix S of the same   
        /// dimension as <paramref name="A"/> and with nonnegative diagonal elements in decreasing order, 
        /// and unitary matrices U and V so that  A = U*S*V'.
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>The Singular Value Decomposition of <paramref name="A"/>,returns U,S,V.</returns>
        public static Tuple<CMatrix, Matrix, CMatrix> SVD(CMatrix A)
        {
            Tuple<CMatrix, Matrix, CMatrix> UBV;
            if (A.rows < A.columns)
                UBV = Bidiag(Transpose(A));
            else
                UBV = Bidiag(A);
            CMatrix U = UBV.Item1;
            Matrix S = UBV.Item2;
            CMatrix V = UBV.Item3;

            Matrix[] USV = Matrix.svd(S);
            int m = S.Rows;
            int n = S.Columns;
            if (m == n)
                return new Tuple<CMatrix, Matrix, CMatrix>(U * USV[0], USV[1], V * USV[2]);

            U[1, m, 1, n] *= USV[0];
            S = new Matrix(m, n);
            for (int i = 1; i <= n; i++)
                S[i, i] = USV[1][i, i];
            V *= USV[2];
            if (A.rows > A.columns)
                return new Tuple<CMatrix, Matrix, CMatrix>(U, S, V);
            else
                return new Tuple<CMatrix, Matrix, CMatrix>(V, Matrix.Transpose(S), U);
        }

        /// <summary>
        ///  Compute the Singular Value of a matrix. 
        /// </summary>
        /// <param name="A">A matrix.</param>
        /// <returns>Returns the Singular Value of <paramref name="A"/>. </returns>
        public static double[] SV(CMatrix A)
        {
            Matrix S;
            if (A.rows < A.columns)
                S = Bidiag_NoUV(CMatrix.Transpose(A));
            else
                S = Bidiag_NoUV(A);
            return Matrix.sv(S);
        }

        #endregion

        #region Eigenvalues And Eigenvectors

        /// <summary>
        ///  Use the power method to find the largest magnitude eigenvalues and the corresponding eigenvectors.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns>The largest magnitude eigenvalues and the corresponding eigenvectors of <paramref name="A"/>.</returns>
        /// <exception cref="ArithmeticException">Throw if Convergence is not reached.</exception>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        public static Tuple<Complex, CMatrix> PowEig(CMatrix A)
        {
            Checker.IsSquare(A);
            CMatrix u = Rand(A.rows, 1);
            Complex max_vn;
            Complex max_vk = Complex.Zero;

            do
            {
                u = A * u;
                max_vn = u.elements.Max();
                if (max_vn == Complex.Zero)
                    throw new ArithmeticException(Resources.Strings.Converge);
                u /= max_vn;

                u = A * u;
                max_vk = u.elements.Max();
                if (max_vk == Complex.Zero)
                    throw new ArithmeticException(Resources.Strings.Converge);
                u /= max_vk;

                max_vn -= max_vk;
            } while (Math.Abs(max_vn.Real) > LibData.Eps || Math.Abs(max_vn.Imaginary) > LibData.Eps);
            return new Tuple<Complex, CMatrix>(max_vk, u);
        }

        /// <summary>
        ///  Use inverse power method to find the minimum magnitude eigenvalues and the corresponding eigenvectors.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns>The minimum magnitude eigenvalues and the corresponding eigenvectors of <paramref name="A"/>.</returns>
        /// <exception cref="ArithmeticException">Throw if convergence is not reached.</exception>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        public static Tuple<Complex, CMatrix> InvPowEig(CMatrix A)
        {
            int m = A.rows;
            CMatrix u = Rand(m, 1);
            Complex max_vn;
            Complex max_vk = Complex.Zero;

            do
            {
                u = LUSolve(A, u);
                max_vn = u.Elements.Max();
                if (max_vn == Complex.Zero)
                    throw new ArithmeticException(Resources.Strings.Converge);
                u /= max_vn;

                u = LUSolve(A, u);
                max_vk = u.elements.Max();
                if (max_vk == Complex.Zero)
                    throw new ArithmeticException(Resources.Strings.Converge);
                u /= max_vk;
                max_vn -= max_vk;
            } while (Math.Abs(max_vn.Real) > LibData.Eps || Math.Abs(max_vn.Imaginary) > LibData.Eps);
            return new Tuple<Complex, CMatrix>(1.0 / max_vk, u);
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
        public static CMatrix InvPowEig(CMatrix A, Complex p)
        {
            Checker.IsSquare(A);
            int n = A.columns;

            CMatrix u = Rand(n, 1);
            Complex max_vn;
            Complex max_vk;

            CMatrix AP = new CMatrix(A);
            for (int i = 1; i <= n; i++)
                AP[i, i] -= p;

            do
            {
                u = LUSolve(AP, u);
                max_vn = u.Elements.Max();
                if (max_vn == Complex.Zero)
                    throw new ArithmeticException(Resources.Strings.Converge);
                u /= max_vn;
                u = LUSolve(AP, u);
                max_vk = u.elements.Max();
                if (max_vk == Complex.Zero)
                    throw new ArithmeticException(Resources.Strings.Converge);
                u /= max_vk;
                max_vn -= max_vk;
            } while (Math.Abs(max_vn.Real) > LibData.Eps || Math.Abs(max_vn.Imaginary) > LibData.Eps);
            return u / u.elements.Norm();
        }

        /// <summary>
        /// Spectral factorization using Jacobi eigenvalue algorithm. A=GDG'
        /// A is a Symmetric matrices.
        /// </summary>
        /// <param name="A">A symmetric matrix.</param>
        /// <returns>Spectral factorization of <paramref name="A"/>.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a symmetric matrix.</exception>
        [Obsolete("This method converges too slowly!")]
        public static CMatrix[] Jacobi(CMatrix A)
        {
            Checker.IsSymmetric(A);
            int n = A.Columns;
            CMatrix D = new CMatrix(A);
            CMatrix G = Eye(n);

            Complex c;
            Complex s;
            Complex t;
            bool state = true;

            while (state)
            {
                state = false;
                for (int p = 1; p < n; p++)
                    for (int q = p + 1; q <= n; q++)
                        if (!D[p, q].CloseToZero())
                        {
                            state = true;
                            t = 0.5 * (D[p, p] - D[q, q]) / Complex.Conj(D[p, q]);
                            t = t + Complex.Sqrt(t * t + Complex.Sign(t));
                            c = (Complex)1 / Math.Sqrt(1 + ElMath.Square(t.Real, t.Imaginary));
                            s = Complex.Sign(t) * t * c;

                            for (int j = 1; j <= n; j++)
                            {
                                t = D[p, j];
                                D[p, j] = c * t - s * D[q, j];
                                D[q, j] = Complex.Conj(s) * t + c * D[q, j];
                            }

                            for (int i = 1; i <= n; i++)
                            {
                                t = D[i, p];
                                D[i, p] = c * t - Complex.Conj(s) * D[i, q];
                                D[i, q] = s * t + c * D[i, q];
                            }
                            for (int i = 1; i <= n; i++)
                            {
                                t = G[i, p];
                                G[i, p] = c * t - Complex.Conj(s) * G[i, q];
                                G[i, q] = s * t + c * G[i, q];
                            }
                        }
            }
            return new CMatrix[] { G, D };
        }

        /// <summary>
        /// Computes the Wilkinson shift. h is a 2*2 complex matrix.
        /// </summary>
        internal static Complex Wilkinson(CMatrix h)
        {
            Complex p = 0.5 * (h[1, 1] - h[2, 2]);
            Complex r = Complex.Sqrt(p * p + h[1, 2] * h[2, 1]);
            Complex mu = (Complex.Abs(p + r) > Complex.Abs(p - r) ? (p + r) : (p - r));
            if (mu == Complex.Zero)
                return mu;
            return h[2, 2] - h[1, 2] * h[2, 1] / mu;
        }

        /// <summary>
        ///  Schur decomposition using a Francis Wilkinson shift(QR Algorithm). 
        ///  A = U*R*U'. U is unitary matrix and R is a real schur form.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns>The Schur decomposition of <paramref name="A"/>,returns U and R.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        public static CMatrix[] Schur(CMatrix A)
        {
            int n = A.columns;
            int dim = n;
            int k = 1;
            int m;

            CMatrix[] UR = Hess(A);
            CMatrix U = UR[0];
            CMatrix R = UR[1];
            CMatrix Q;
            Complex mu;
            Complex[] cs;
            Complex md;

            if (n != 1)
                while (n >= k)
                {
                    Q = CMatrix.Eye(n - k + 1);
                    mu = Wilkinson(R[n - 1, n, n - 1, n]);
                    for (int i = k; i <= n; i++)
                        R[i, i] -= mu;

                    for (int i = k; i < n; i++)
                        if (R[i + 1, i] != Complex.Zero)
                        {
                            cs = Givens(R[i, i], R[i + 1, i]);

                            for (int j = i + 1; j <= dim; j++)
                            {
                                md = R[i, j];
                                R[i, j] = cs[0] * md + cs[1] * R[i + 1, j];
                                R[i + 1, j] = -Complex.Conj(cs[1]) * md + cs[0] * R[i + 1, j];
                            }
                            R[i, i] = cs[2];
                            R[i + 1, i] = Complex.Zero;
                            for (int j = 1; j <= n - k + 1; j++)
                            {
                                m = i - k + 2;
                                md = Q[j, m - 1];
                                Q[j, m - 1] = cs[0] * md + Complex.Conj(cs[1]) * Q[j, m];
                                Q[j, m] = -cs[1] * md + cs[0] * Q[j, m];
                            }
                        }
                    R[1, dim, k, n] *= Q;
                    U[1, dim, k, n] *= Q;

                    for (int i = k; i <= n; i++)
                        R[i, i] += mu;

                    while (k < n)
                        if (Math.Abs(R[k + 1, k].Real) < LibData.Eps && Math.Abs(R[k + 1, k].Imaginary) < LibData.Eps)
                        {
                            R[k + 1, k] = Complex.Zero;
                            k++;
                        }
                        else
                            break;
                    while (n > 1)
                        if (Math.Abs(R[n, n - 1].Real) < LibData.Eps && Math.Abs(R[n, n - 1].Imaginary) < LibData.Eps)
                        {
                            R[n, n - 1] = Complex.Zero;
                            n--;
                        }
                        else
                            break;
                }
            return new CMatrix[2] { U, R };
        }

        /// <summary>
        /// Returns a column complex array containing the sorted eigenvalues.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <returns>The sorted eigenvalues of a square matrix <paramref name="A"/>.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="A"/> is not a square matrix.</exception>
        public static Complex[] EigValue(CMatrix A)
        {
            int n = A.columns;
            int dim = n;
            int k = 1;
            int m;

            CMatrix R = Hess(A)[1];
            CMatrix Q;

            Complex mu;
            Complex[] cs;
            Complex md;
            Complex[] ev = new Complex[n];
            int t = 0;

            if (n != 1)
                while (n >= k)
                {
                    Q = CMatrix.Eye(n - k + 1);
                    mu = Wilkinson(R[n - 1, n, n - 1, n]);
                    for (int i = k; i <= n; i++)
                        R[i, i] -= mu;

                    for (int i = k; i < n; i++)
                        if (Math.Abs(R[i + 1, i].Real) > LibData.Eps || Math.Abs(R[i + 1, i].Imaginary) > LibData.Eps)
                        {
                            cs = Givens(R[i, i], R[i + 1, i]);

                            for (int j = i + 1; j <= dim; j++)
                            {
                                md = R[i, j];
                                R[i, j] = cs[0] * md + cs[1] * R[i + 1, j];
                                R[i + 1, j] = -Complex.Conj(cs[1]) * md + cs[0] * R[i + 1, j];
                            }
                            R[i, i] = cs[2];
                            R[i + 1, i] = Complex.Zero;
                            for (int j = 1; j <= n - k + 1; j++)
                            {
                                m = i - k + 2;
                                md = Q[j, m - 1];
                                Q[j, m - 1] = cs[0] * md + Complex.Conj(cs[1]) * Q[j, m];
                                Q[j, m] = -cs[1] * md + cs[0] * Q[j, m];
                            }
                        }
                    R[1, dim, k, n] *= Q;

                    for (int i = k; i <= n; i++)
                        R[i, i] += mu;

                    while (k < n)
                        if (Math.Abs(R[k + 1, k].Real) < LibData.Eps && Math.Abs(R[k + 1, k].Imaginary) < LibData.Eps)
                        {
                            ev[t] = R[k, k];
                            t++;
                            k++;
                        }
                        else
                            break;
                    while (n > 1)
                        if (Math.Abs(R[n, n - 1].Real) < LibData.Eps && Math.Abs(R[n, n - 1].Imaginary) < LibData.Eps)
                        {
                            if (t < dim)
                            {
                                ev[t] = R[n, n];
                                t++;
                            }
                            n--;
                        }
                        else
                            break;
                }
            Array.Sort(ev);
            Array.Reverse(ev);
            return ev;
        }

        /// <summary>
        /// Produces a diagonal matrix D of eigenvalues 
        /// and a full matrix V whose columns are the corresponding eigenvectors so that A*V = V*D.if <paramref name="A"/> 
        /// can't diagonalized,than V is not nonsingular.
        /// </summary>
        /// <param name="A">A square matrix.</param>
        /// <param name="Diagonalizable">Indicates whether A is diagonalizable.</param>
        /// <returns>A diagonal matrix D of eigenvalues and a full matrix V whose columns are the corresponding eigenvectors.</returns>
        public static CMatrix[] Eig(CMatrix A, out bool Diagonalizable)
        {
            Diagonalizable = true;
            if (A.IsHermitian() || A.IsNormal())
                return Schur(A);

            Complex[] val = EigValue(A);
            int n = A.columns;
            CMatrix B = new CMatrix(A);
            CMatrix N;
            CMatrix V = new CMatrix(n);
            int k;
            for (int i = 0; i < n; )
            {
                k = 1;
                for (int j = 1; j < n - i; j++)
                    if (Math.Abs(val[i].Real - val[i + j].Real) < LibData.Eps && Math.Abs(val[i].Real - val[i + j].Real) < LibData.Eps)
                        k++;
                for (int j = 1; j <= n; j++)
                    B[j, j] = A[j, j] - val[i];

                N = Null(B);
                if (k != N.columns)
                {
                    Diagonalizable = false;
                    for (int j = 0; j <= k - N.columns; j++)
                        V[1, n, i + 1 + j * N.columns, i + (j + 1) * N.columns] = N;
                }
                else
                    V[1, n, i + 1, i + N.columns] = N;
                i = i + k;

            }
            return new CMatrix[] { V, Diag(val) };
        }

        #endregion

        #region Overide System.Object Methods

        private double max()
        {
            Complex[] ele = this.elements;
            double max = 0;
            for (int i = 0; i < ele.Length; i++)
                if (Math.Abs(ele[i].Real) > Math.Abs(max))
                {
                    if (Math.Abs(ele[i].Real) > Math.Abs(ele[i].Imaginary))
                        if (!(double.IsPositiveInfinity(ele[i].Real) || double.IsNegativeInfinity(ele[i].Real)))
                            max = ele[i].Real;
                        else
                            if (!(double.IsPositiveInfinity(ele[i].Imaginary) || double.IsNegativeInfinity(ele[i].Imaginary)))
                                max = ele[i].Imaginary;
                }
            return max;
        }

        /// <summary>
        /// Returns the equivalent string representation of this matrix.
        /// </summary>
        /// <returns>The string representation of this matrix.</returns>
        public override string ToString()
        {
            int n = int.Parse(Math.Abs(this.max()).ToString("E0").Substring(2));
            double md;
            StringBuilder strout = new StringBuilder();
            if (n > 1)
            {
                md = Math.Pow(10, n);
                strout.AppendFormat("    1e+{0} *", n);
                strout.AppendLine();
                strout.AppendLine();
                for (int i = 1; i <= rows; i++)
                {
                    for (int j = 1; j <= columns; j++)
                        strout.AppendFormat(LibData.Format, (this[i, j] / md));
                    strout.AppendLine();
                }
            }

            else if (-n > 1)
            {
                md = Math.Pow(10, -n);
                strout.AppendFormat("    1e{0} *", n);
                strout.AppendLine();
                strout.AppendLine();
                for (int i = 1; i <= rows; i++)
                {
                    for (int j = 1; j <= columns; j++)
                        strout.AppendFormat(LibData.Format, (this[i, j] * md));
                    strout.AppendLine();
                }
            }

            else
                for (int i = 1; i <= rows; i++)
                {
                    for (int j = 1; j <= columns; j++)
                        strout.AppendFormat(LibData.Format, this[i, j]);
                    strout.AppendLine();
                }

            return strout.ToString();
        }

        /// <summary>
        /// Indicates whether this instance and a specific object are equals.
        /// </summary>
        /// <param name="obj">An object.</param>
        /// <returns>A boolean value indicates whether this instance and a specific object are equals.</returns>
        public override bool Equals(object obj)
        {
            CMatrix A = obj as CMatrix;
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
