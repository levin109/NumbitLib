
/*********************************************************************************************************************************************
 * *
 * *        File Name                : Polynomial.cs
 * *        Creator                  : Fuhua Lai
 * *        Date Modified            : 2013-6-12 15:09:35
 * *        Functional Description   : Performs Polynomial computations.
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

using NumBitLib.LinearAlgebra;
using System;

namespace NumBitLib.Functions
{
    /// <summary>
    /// Provides some static method to solve polynomial problems.
    /// A polynomial is represented by an array. The polynomial coefficients are in descending powers.
    /// </summary>
    public static class Polynomial
    {
        #region Arithmetic Operator

        /// <summary>
        /// Polynomial addition.
        /// </summary>
        /// <param name="P">Left-hand operand.</param>
        /// <param name="Q">Right-hand operand.</param>
        /// <returns>The addition of <paramref name="P"/> and <paramref name="Q"/>.</returns>
        public static double[] Add(double[] P, params double[] Q)
        {
            int n;
            double[] buf;
            if (P.Length > Q.Length)
            {
                buf = new double[P.Length];
                n = P.Length - Q.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = P[i];
                for (int i = n; i < P.Length; i++)
                    buf[i] = P[i] + Q[i - n];
            }
            else
            {
                buf = new double[Q.Length];
                n = Q.Length - P.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = Q[i];
                for (int i = n; i < Q.Length; i++)
                    buf[i] = Q[i] + P[i - n];
            }
            return ZeroLeadCheck(buf);
        }

        /// <summary>
        /// Polynomial addition.
        /// </summary>
        /// <param name="P">Left-hand operand.</param>
        /// <param name="Q">Right-hand operand.</param>
        /// <returns>The addition of <paramref name="P"/> and <paramref name="Q"/>.</returns>
        public static Complex[] Add(Complex[] P, params Complex[] Q)
        {
            int n;
            Complex[] buf;
            if (P.Length > Q.Length)
            {
                buf = new Complex[P.Length];
                n = P.Length - Q.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = P[i];
                for (int i = n; i < P.Length; i++)
                    buf[i] = P[i] + Q[i - n];
            }
            else
            {
                buf = new Complex[Q.Length];
                n = Q.Length - P.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = Q[i];
                for (int i = n; i < Q.Length; i++)
                    buf[i] = Q[i] + P[i - n];
            }
            return ZeroLeadCheck(buf);
        }

        /// <summary>
        /// Polynomial addition.
        /// </summary>
        /// <param name="P">Left-hand operand.</param>
        /// <param name="Q">Right-hand operand.</param>
        /// <returns>The addition of <paramref name="P"/> and <paramref name="Q"/>.</returns>
        public static Complex[] Add(Complex[] P, params double[] Q)
        {
            int n;
            Complex[] buf;
            if (P.Length > Q.Length)
            {
                buf = new Complex[P.Length];
                n = P.Length - Q.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = P[i];
                for (int i = n; i < P.Length; i++)
                    buf[i] = P[i] + Q[i - n];
            }
            else
            {
                buf = new Complex[Q.Length];
                n = Q.Length - P.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = (Complex)Q[i];
                for (int i = n; i < Q.Length; i++)
                    buf[i] = Q[i] + P[i - n];
            }
            return ZeroLeadCheck(buf);
        }

        /// <summary>
        /// Polynomial addition.
        /// </summary>
        /// <param name="P">Left-hand operand.</param>
        /// <param name="Q">Right-hand operand.</param>
        /// <returns>The addition of <paramref name="P"/> and <paramref name="Q"/>.</returns>
        public static Complex[] Add(double[] P, params Complex[] Q)
        {
            int n;
            Complex[] buf;
            if (P.Length > Q.Length)
            {
                buf = new Complex[P.Length];
                n = P.Length - Q.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = (Complex)P[i];
                for (int i = n; i < P.Length; i++)
                    buf[i] = P[i] + Q[i - n];
            }
            else
            {
                buf = new Complex[Q.Length];
                n = Q.Length - P.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = Q[i];
                for (int i = n; i < Q.Length; i++)
                    buf[i] = Q[i] + P[i - n];
            }
            return ZeroLeadCheck(buf);
        }

        /// <summary>
        /// Polynomial subtraction.
        /// </summary>
        /// <param name="P">Left-hand operand.</param>
        /// <param name="Q">Right-hand operand.</param>
        /// <returns>The subtraction of <paramref name="P"/> and <paramref name="Q"/>.</returns>
        public static double[] Sub(double[] P, params double[] Q)
        {
            int n;
            double[] buf;
            if (P.Length > Q.Length)
            {
                buf = new double[P.Length];
                n = P.Length - Q.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = P[i];
                for (int i = n; i < P.Length; i++)
                    buf[i] = P[i] - Q[i - n];
            }
            else
            {
                buf = new double[Q.Length];
                n = Q.Length - P.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = -Q[i];
                for (int i = n; i < Q.Length; i++)
                    buf[i] = P[i - n] - Q[i];
            }
            return ZeroLeadCheck(buf);
        }

        /// <summary>
        /// Polynomial subtraction.
        /// </summary>
        /// <param name="P">Left-hand operand.</param>
        /// <param name="Q">Right-hand operand.</param>
        /// <returns>The subtraction of <paramref name="P"/> and <paramref name="Q"/>.</returns>
        public static Complex[] Sub(Complex[] P, params Complex[] Q)
        {
            int n;
            Complex[] buf;
            if (P.Length > Q.Length)
            {
                buf = new Complex[P.Length];
                n = P.Length - Q.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = P[i];
                for (int i = n; i < P.Length; i++)
                    buf[i] = P[i] - Q[i - n];
            }
            else
            {
                buf = new Complex[Q.Length];
                n = Q.Length - P.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = -Q[i];
                for (int i = n; i < Q.Length; i++)
                    buf[i] = P[i - n] - Q[i];
            }
            return ZeroLeadCheck(buf);
        }

        /// <summary>
        /// Polynomial subtraction.
        /// </summary>
        /// <param name="P">Left-hand operand.</param>
        /// <param name="Q">Right-hand operand.</param>
        /// <returns>The subtraction of <paramref name="P"/> and <paramref name="Q"/>.</returns>
        public static Complex[] Sub(Complex[] P, params double[] Q)
        {
            int n;
            Complex[] buf;
            if (P.Length > Q.Length)
            {
                buf = new Complex[P.Length];
                n = P.Length - Q.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = P[i];
                for (int i = n; i < P.Length; i++)
                    buf[i] = P[i] - Q[i - n];
            }
            else
            {
                buf = new Complex[Q.Length];
                n = Q.Length - P.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = -(Complex)Q[i];
                for (int i = n; i < Q.Length; i++)
                    buf[i] = P[i - n] - Q[i];
            }
            return ZeroLeadCheck(buf);
        }

        /// <summary>
        /// Polynomial subtraction.
        /// </summary>
        /// <param name="P">Left-hand operand.</param>
        /// <param name="Q">Right-hand operand.</param>
        /// <returns>The subtraction of <paramref name="P"/> and <paramref name="Q"/>.</returns>
        public static Complex[] Sub(double[] P, params Complex[] Q)
        {
            int n;
            Complex[] buf;
            if (P.Length > Q.Length)
            {
                buf = new Complex[P.Length];
                n = P.Length - Q.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = (Complex)P[i];
                for (int i = n; i < P.Length; i++)
                    buf[i] = P[i] - Q[i - n];
            }
            else
            {
                buf = new Complex[Q.Length];
                n = Q.Length - P.Length;

                for (int i = 0; i < n; i++)
                    buf[i] = -Q[i];
                for (int i = n; i < Q.Length; i++)
                    buf[i] = P[i - n] - Q[i];
            }
            return ZeroLeadCheck(buf);
        }

        /// <summary>
        /// Polynomial multiplication.
        /// </summary>
        /// <param name="P">Left-hand operand.</param>
        /// <param name="Q">Right-hand operand.</param>
        /// <returns>The multiplication of <paramref name="P"/> and <paramref name="Q"/>.</returns>
        public static double[] Conv(double[] P, params double[] Q)
        {
            int m = P.Length;
            int n = Q.Length;
            int len = m + n - 1;
            double[] pqx = new double[len];
            for (int k = 0; k < len; k++)
                for (int i = 0; i <= k; i++)
                {
                    if (i < m && k - i < n)
                        pqx[k] += P[i] * Q[k - i];
                }
            return ZeroLeadCheck(pqx);
        }

        /// <summary>
        /// Polynomial multiplication.
        /// </summary>
        /// <param name="P">Left-hand operand.</param>
        /// <param name="Q">Right-hand operand.</param>
        /// <returns>The multiplication of <paramref name="P"/> and <paramref name="Q"/>.</returns>
        public static Complex[] Conv(double[] P, params Complex[] Q)
        {
            int m = P.Length;
            int n = Q.Length;
            int len = m + n - 1;
            Complex[] pqx = new Complex[len];
            for (int k = 0; k < len; k++)
                for (int i = 0; i <= k; i++)
                {
                    if (i < m && k - i < n)
                        pqx[k] += P[i] * Q[k - i];
                }
            return ZeroLeadCheck(pqx);
        }

        /// <summary>
        /// Polynomial multiplication.
        /// </summary>
        /// <param name="P">Left-hand operand.</param>
        /// <param name="Q">Right-hand operand.</param>
        /// <returns>The multiplication of <paramref name="P"/> and <paramref name="Q"/>.</returns>
        public static Complex[] Conv(Complex[] P, params Complex[] Q)
        {
            int m = P.Length;
            int n = Q.Length;
            int len = m + n - 1;
            Complex[] pqx = new Complex[len];
            for (int k = 0; k < len; k++)
                for (int i = 0; i <= k; i++)
                {
                    if (i < m && k - i < n)
                        pqx[k] += P[i] * Q[k - i];
                }
            return ZeroLeadCheck(pqx);
        }

        /// <summary>
        /// Polynomial division.
        /// </summary>
        /// <param name="P">Left-hand operand.</param>
        /// <param name="Q">Right-hand operand.</param>
        /// <returns>The division of <paramref name="P"/> and <paramref name="Q"/>.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="Q"/> is zero Polynomial.</exception>
        public static double[][] Deconv(double[] P, params double[] Q)
        {
            double[] fx = ZeroLeadCheck(P);
            double[] gx = ZeroLeadCheck(Q);
            int m = fx.Length;
            int n = gx.Length;
            if (n == 1 && gx[0] == 0)
                throw new ArgumentException(Resources.Strings.Deconv);

            if (m < n)
                return new double[][] { new double[1], P };

            int k = m - n + 1;
            int i = 0;
            int jump = 0;
            double[] q = new double[k];
            double[] temp;
            do
            {
                q[i] = fx[0] / gx[0];
                temp = new double[m - 1];
                for (int j = 1; j < n; j++)
                    temp[j - 1] = fx[j] - fx[0] / gx[0] * gx[j];
                for (int j = n; j < m; j++)
                    temp[j - 1] = fx[j];
                fx = ZeroLeadCheck(temp);
                jump = temp.Length - fx.Length;
                if (jump != 0)
                {
                    for (int j = 1; j <= jump; j++)
                        if (i + j < q.Length)
                            q[i + j] = 0;
                    i += jump + 1;
                }
                else
                    i++;
                m = fx.Length;
            } while (m >= n);
            return new double[][] { q, fx.Length == 0 ? new double[1] : fx };
        }

        /// <summary>
        /// Polynomial division.
        /// </summary>
        /// <param name="P">Left-hand operand.</param>
        /// <param name="Q">Right-hand operand.</param>
        /// <returns>The division of <paramref name="P"/> and <paramref name="Q"/>.</returns>
        public static Complex[][] Deconv(Complex[] P, params Complex[] Q)
        {
            Complex[] fx = ZeroLeadCheck(P);
            Complex[] gx = ZeroLeadCheck(Q);
            int m = fx.Length;
            int n = gx.Length;

            if (n == 1 && gx[0] == Complex.Zero)
                throw new ArgumentException("Division by zero Polynomial.");
            if (m < n)
                return new Complex[][] { new Complex[1], P };

            int k = m - n + 1;
            int i = 0;
            int jump = 0;
            Complex[] q = new Complex[k];
            Complex[] temp;
            do
            {
                q[i] = fx[0] / gx[0];
                temp = new Complex[m - 1];
                for (int j = 1; j < n; j++)
                    temp[j - 1] = fx[j] - fx[0] / gx[0] * gx[j];
                for (int j = n; j < m; j++)
                    temp[j - 1] = fx[j];
                fx = ZeroLeadCheck(temp);
                jump = temp.Length - fx.Length;
                if (jump != 0)
                {
                    for (int j = 1; j <= jump; j++)
                        if (i + j < q.Length)
                            q[i + j] = Complex.Zero;
                    i += jump + 1;
                }
                else
                    i++;
                m = fx.Length;
            } while (m >= n);
            return new Complex[][] { q, fx.Length == 0 ? new Complex[1] : fx };
        }

        #endregion

        #region Polynomial Evaluation

        /// <summary>
        /// Returns the value of a polynomial P evaluated at a number.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <param name="x">A real number.</param>
        /// <returns>The value of a polynomial <paramref name="P"/> evaluated at <paramref name="x"/>.</returns>
        public static double Polyval(double[] P, double x)
        {
            double pval;
            pval = P[0];
            for (int i = 1; i < P.Length; i++)
                pval = pval * x + P[i];
            return pval;
        }

        /// <summary>
        /// Returns the value of a polynomial P evaluated at a number.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <param name="x">A real number.</param>
        /// <returns>The value of a polynomial <paramref name="P"/> evaluated at <paramref name="x"/>.</returns>
        public static Complex Polyval(double[] P, Complex x)
        {
            Complex pval;
            pval = (Complex)P[0];
            for (int i = 1; i < P.Length; i++)
                pval = pval * x + P[i];
            return pval;
        }

        /// <summary>
        /// Returns the value of a polynomial P evaluated at a number.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <param name="x">A real number.</param>
        /// <returns>The value of a polynomial <paramref name="P"/> evaluated at <paramref name="x"/>.</returns>
        public static Complex Polyval(Complex[] P, double x)
        {
            Complex pval;
            pval = P[0];
            for (int i = 1; i < P.Length; i++)
                pval = pval * x + P[i];
            return pval;
        }

        /// <summary>
        /// Returns the value of a polynomial P evaluated at a number.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <param name="x">A real number.</param>
        /// <returns>The value of a polynomial <paramref name="P"/> evaluated at <paramref name="x"/>.</returns>
        public static Complex Polyval(Complex[] P, Complex x)
        {
            Complex pval;
            pval = P[0];
            for (int i = 1; i < P.Length; i++)
                pval = pval * x + P[i];
            return pval;
        }

        /// <summary>
        /// Returns the value of a polynomial P evaluated at an array.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <param name="X">An array.</param>
        /// <returns>The value of a polynomial <paramref name="P"/> evaluated at <paramref name="X"/>.</returns>
        public static double[] Polyval(double[] P, double[] X)
        {
            double[] pval = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
                pval[i] = Polyval(P, X[i]);
            return pval;
        }

        /// <summary>
        /// Returns the value of a polynomial P evaluated at an array.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <param name="X">An array.</param>
        /// <returns>The value of a polynomial <paramref name="P"/> evaluated at <paramref name="X"/>.</returns>
        public static Complex[] Polyval(double[] P, Complex[] X)
        {
            Complex[] pval = new Complex[X.Length];
            for (int i = 0; i < X.Length; i++)
                pval[i] = Polyval(P, X[i]);
            return pval;
        }

        /// <summary>
        /// Returns the value of a polynomial P evaluated at an array.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <param name="X">An array.</param>
        /// <returns>The value of a polynomial <paramref name="P"/> evaluated at <paramref name="X"/>.</returns>
        public static Complex[] Polyval(Complex[] P, Complex[] X)
        {
            Complex[] pval = new Complex[X.Length];
            for (int i = 0; i < X.Length; i++)
                pval[i] = Polyval(P, X[i]);
            return pval;
        }

        /// <summary>
        /// Returns the value of a polynomial P evaluated at an array.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <param name="X">An array.</param>
        /// <returns>The value of a polynomial <paramref name="P"/> evaluated at <paramref name="X"/>.</returns>
        public static Complex[] Polyval(Complex[] P, double[] X)
        {
            Complex[] pval = new Complex[X.Length];
            for (int i = 0; i < X.Length; i++)
                pval[i] = Polyval(P, X[i]);
            return pval;
        }

        #endregion

        #region Polynomial Calculus

        /// <summary>
        /// Differentiate polynomial.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <returns>The derivative of the polynomial whose coefficients are the elements of <paramref name="P"/>.</returns>
        public static double[] Polyder(params double[] P)
        {
            int n = P.Length - 1;
            double[] der = new double[n];
            for (int i = 0; i < n; i++)
                der[i] = (n - i) * P[i];
            return ZeroLeadCheck(der);
        }

        /// <summary>
        /// Differentiate polynomial.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <returns>The derivative of the polynomial whose coefficients are the elements of <paramref name="P"/>.</returns>
        public static Complex[] Polyder(params Complex[] P)
        {
            int n = P.Length - 1;
            Complex[] der = new Complex[n];
            for (int i = 0; i < n; i++)
                der[i] = (n - i) * P[i];
            return ZeroLeadCheck(der);
        }

        /// <summary>
        /// Differentiate polynomial.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <param name="n">The derivative order.</param>
        /// <returns>The n-th derivative of the polynomial whose coefficients are the elements of <paramref name="P"/>.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="n"/> is negative.</exception>
        public static double[] Polyder(double[] P, int n)
        {
            if (n >= P.Length)
                return new double[1];
            if (n < 0)
                throw new ArgumentException(Resources.Strings.Polyder);
            if (n == 0)
                return P;
            int k = P.Length - n;
            int md;
            double[] dern = new double[k];
            for (int i = 0; i < k; i++)
            {
                md = P.Length - 1 - i;
                dern[i] = 1;
                for (int j = 0; j < n; j++)
                    dern[i] *= (md - j);
                dern[i] *= P[i];
            }
            return ZeroLeadCheck(dern);
        }

        /// <summary>
        /// Differentiate polynomial.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <param name="n">The derivative order.</param>
        /// <returns>The n-th derivative of the polynomial whose coefficients are the elements of <paramref name="P"/>.</returns>
        /// <exception cref="ArgumentException">Throw if <paramref name="n"/> is negative.</exception>
        public static Complex[] Polyder(Complex[] P, int n)
        {
            if (n >= P.Length)
                return new Complex[1];
            if (n < 0)
                throw new ArgumentException(Resources.Strings.Polyder);
            if (n == 0)
                return P;
            int k = P.Length - n;
            int md;
            Complex[] dern = new Complex[k];
            for (int i = 0; i < k; i++)
            {
                md = P.Length - 1 - i;
                dern[i] = Complex.One;
                for (int j = 0; j < n; j++)
                    dern[i] *= (md - j);
                dern[i] *= P[i];
            }
            return ZeroLeadCheck(dern);
        }

        /// <summary>
        ///  Integrate polynomial.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <returns>The integrate of the polynomial whose coefficients are the elements of <paramref name="P"/>.</returns>
        public static double[] Polyint(params double[] P)
        {
            double[] pint = new double[P.Length + 1];
            for (int i = 0; i < P.Length; i++)
                pint[i] = P[i] / (P.Length - i);
            return ZeroLeadCheck(pint);
        }

        /// <summary>
        ///  Integrate polynomial.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <returns>The integrate of the polynomial whose coefficients are the elements of <paramref name="P"/>.</returns>
        public static Complex[] Polyint(params Complex[] P)
        {
            Complex[] pint = new Complex[P.Length + 1];
            for (int i = 0; i < P.Length; i++)
                pint[i] = P[i] / (P.Length - i);
            return ZeroLeadCheck(pint);
        }

        #endregion

        #region Polynomial Roots

        /// <summary>
        /// Find the roots of a polynomial.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <returns>The roots of a polynomial.</returns>
        public static Complex[] Roots(params double[] P)
        {
            double[] CheckedP = ZeroLeadCheck(P);
            int n = CheckedP.Length - 1;
            if (n == 0)
                throw new ArgumentException(Resources.Strings.Roots);

            Matrix FriendMatrix = new Matrix(n);
            for (int i = 1; i < n; i++)
                FriendMatrix[i + 1, i] = 1.0;
            for (int i = 1; i <= n; i++)
                FriendMatrix[i, n] = -CheckedP[n - i + 1] / CheckedP[0];
            return Matrix.EigValue(FriendMatrix);
        }

        /// <summary>
        /// Find the roots of a polynomial.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <returns>The roots of a polynomial.</returns>
        public static Complex[] Roots(params Complex[] P)
        {
            Complex[] CheckedP = ZeroLeadCheck(P);
            int n = CheckedP.Length - 1;
            if (n == 0)
                throw new ArgumentException(Resources.Strings.Roots);

            CMatrix FriendMatrix = new CMatrix(n);
            for (int i = 1; i < n; i++)
                FriendMatrix[i + 1, i] = Complex.One;
            for (int i = 1; i <= n; i++)
                FriendMatrix[i, n] = -CheckedP[n - i + 1] / CheckedP[0];
            return CMatrix.EigValue(FriendMatrix);
        }

        #endregion

        #region Helper Method

        /// <summary>
        /// Check if the first index of elements is zero.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <returns>The checked Polynomial.</returns>
        private static double[] ZeroLeadCheck(double[] P)
        {
            int count = 0;
            for (int i = 0; i < P.Length; i++)
                if (P[i] == 0)
                    count++;
                else
                    break;
            if (count == 0)
                return P;
            if (count == P.Length)
                return new double[1];

            double[] CheckedP = new double[P.Length - count];
            for (int i = 0; i < CheckedP.Length; i++)
                CheckedP[i] = P[i + count];
            return CheckedP;
        }

        /// <summary>
        /// Check if the first index of elements is zero.
        /// </summary>
        /// <param name="P">A Polynomial is represented by an array.</param>
        /// <returns>The checked Polynomial.</returns>
        private static Complex[] ZeroLeadCheck(params Complex[] P)
        {
            int count = 0;
            for (int i = 0; i < P.Length; i++)
                if (Complex.Abs(P[i]) < double.Epsilon)
                    count++;
                else
                    break;
            if (count == 0)
                return P;
            if (count == P.Length)
                return new Complex[1];

            Complex[] CheckedP = new Complex[P.Length - count];
            for (int i = 0; i < CheckedP.Length; i++)
                CheckedP[i] = P[i + count];
            return CheckedP;
        }

        /// <summary>
        /// Normalizes the polynomial.
        /// </summary>
        /// <param name="P">A double type array.</param>
        /// <returns>The normalized polynomial.</returns>
        internal static double[] Normalize(double[] P)
        {
            double[] p = ZeroLeadCheck(P);
            return MArray.Div(p, p[0]);
        }

        /// <summary>
        /// Normalizes the polynomial.
        /// </summary>
        /// <param name="P">A complex array.</param>
        /// <returns>The normalized polynomial.</returns>
        public static Complex[] Normalize(Complex[] P)
        {
            Complex[] p = ZeroLeadCheck(P);
            return MArray.Div(p, p[0]);
        }

        #endregion
    }
}
