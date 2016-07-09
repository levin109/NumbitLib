
/*********************************************************************************************************************************************
 * *
 * *        File Name                : Marray.cs
 * *        Creator                  : Fuhua Lai
 * *        Date Modified            : 2013-6-11 22:51:30
 * *        Functional Description   : Performs Array operation.
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

namespace NumBitLib
{
    /// <summary>
    /// Provides static method for array operation.
    /// </summary>
    public static class MArray
    {
        #region Array Generator

        /// <summary>
        /// Generate an array whose elements are all 0.0.
        /// </summary>
        /// <param name="length">The length of this array.</param>
        /// <returns>Returns an array with elements are all 0.0.</returns>
        public static double[] Zeros(int length)
        {
            return new double[length];
        }

        /// <summary>
        /// Generate an array whose elements are all 1.0.
        /// </summary>
        /// <param name="length">The length of this array.</param>
        /// <returns>Returns an array with elements are all 1.0.</returns>
        public static double[] Ones(int length)
        {
            double[] rd = new double[length];
            for (int i = 0; i < length; i++)
                rd[i] = 1.0;
            return rd;
        }

        /// <summary>
        /// Generate an array whose elements are all 1.0.
        /// </summary>
        /// <param name="num">A real number.</param>
        /// <param name="length">The length of this array.</param>
        /// <returns>Returns an array with elements are all <paramref name="num"/>.</returns>
        public static double[] Scalar(double num, int length)
        {
            double[] rd = new double[length];
            for (int i = 0; i < length; i++)
                rd[i] = num;
            return rd;
        }

        /// <summary>
        /// Generate a random array whose elements are between 0.0 and 1.0.
        /// </summary>
        ///  <param name="length">The length of this array.</param>
        /// <returns>Returns a random array with elements between 0.0 and 1.0.</returns>
        public static double[] Rand(int length)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            double[] rd = new double[length];
            for (int i = 0; i < length; i++)
                rd[i] = r.NextDouble();
            return rd;
        }

        /// <summary>
        /// Generate a random array whose elements are between <paramref name="start"/> and <paramref name="end"/>.
        /// </summary>  
        ///  <param name="length">The length of this array.</param>
        /// <param name="start">Define the minimum elements in this array.</param>
        /// <param name="end">Define the maxmum elements in this array.</param>
        /// <returns>Returns a random array with elements between <paramref name="start"/> and <paramref name="end"/>.</returns>
        public static double[] Rand(int length, double start, double end)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            double[] rd = new double[length];
            end -= start;
            for (int i = 0; i < length; i++)
                rd[i] = start + end * r.NextDouble();
            return rd;
        }

        /// <summary>
        /// Generate a random array whose elements are all integers and its range is 0~10.
        /// </summary>
        /// <param name="length">The length of this array.</param>
        /// <returns>A random array whose elements are all integers and its range is 0~10.</returns>
        public static double[] Randi(int length)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            double[] rd = new double[length];
            for (int i = 0; i < length; i++)
                rd[i] = r.Next(0, 10);
            return rd;
        }

        /// <summary>
        /// Linearly spaced double[].
        /// </summary>
        /// <param name="start">The start element.</param>
        /// <param name="end">The end element.</param>
        /// <param name="n">The number of points.</param>
        /// <returns><paramref name="n"/> points between <paramref name="start"/> and <paramref name="end"/>.</returns>
        /// <exception cref="IndexOutOfRangeException">Throws if <paramref name="n"/> is not positive.</exception>
        public static double[] LinSpace(double start, double end, Int64 n)
        {
            end = (end - start) / (n - 1);
            double[] LS = new double[n];
            LS[0] = start;
            for (Int64 i = 1; i < n; i++)
                LS[i] = LS[i - 1] + end;
            return LS;
        }

        #endregion

        #region Arithmetic Operation

        #region Array Addition

        /// <summary>
        /// Array addition.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The sum of two array.</returns>
        public static double[] Add(double[] a, double[] b)
        {
            double[] c = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] + b[i];
            return c;
        }

        /// <summary>
        /// Array addition.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The sum of array <paramref name="a"/> and real number <paramref name="b"/>.</returns>
        public static double[] Add(double[] a, double b)
        {
            double[] c = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] + b;
            return c;
        }

        /// <summary>
        /// Array addition.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The sum of array <paramref name="a"/> and Complex number <paramref name="b"/>.</returns>
        public static Complex[] Add(double[] a, Complex b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] + b;
            return c;
        }

        /// <summary>
        /// Array addition.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The sum of two array.</returns>
        public static Complex[] Add(double[] a, Complex[] b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] + b[i];
            return c;
        }

        /// <summary>
        /// Array addition.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The sum of two array.</returns>
        public static Complex[] Add(Complex[] a, Complex[] b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] + b[i];
            return c;
        }

        /// <summary>
        /// Array addition.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The sum of array <paramref name="a"/> and Complex number <paramref name="b"/>.</returns>
        public static Complex[] Add(Complex[] a, Complex b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] + b;
            return c;
        }

        /// <summary>
        /// Array addition.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The sum of two array.</returns>
        public static Complex[] Add(Complex[] a, double[] b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] + b[i];
            return c;
        }

        /// <summary>
        /// Array addition.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The sum of array <paramref name="a"/> and real number <paramref name="b"/>.</returns>
        public static Complex[] Add(Complex[] a, double b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] + b;
            return c;
        }

        #endregion

        #region  Array Subtraction

        /// <summary>
        /// Array subtraction.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The subtraction of two array.</returns>
        public static double[] Sub(double[] a, double[] b)
        {
            double[] c = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] - b[i];
            return c;
        }

        /// <summary>
        /// Array subtraction.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The subtraction of array <paramref name="a"/> and real number <paramref name="b"/>.</returns>
        public static double[] Sub(double[] a, double b)
        {
            double[] c = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] - b;
            return c;
        }

        /// <summary>
        /// Array subtraction.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The subtraction of array <paramref name="a"/> and Complex number <paramref name="b"/>.</returns>
        public static Complex[] Sub(double[] a, Complex b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] - b;
            return c;
        }

        /// <summary>
        /// Array subtraction.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The subtraction of two array.</returns>
        public static Complex[] Sub(double[] a, Complex[] b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] - b[i];
            return c;
        }

        /// <summary>
        /// Array subtraction.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The subtraction of two array.</returns>
        public static Complex[] Sub(Complex[] a, Complex[] b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] - b[i];
            return c;
        }

        /// <summary>
        /// Array subtraction.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The subtraction of array <paramref name="a"/> and Complex number <paramref name="b"/>.</returns>
        public static Complex[] Sub(Complex[] a, Complex b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] - b;
            return c;
        }

        /// <summary>
        /// Array subtraction.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The subtraction of two array.</returns>
        public static Complex[] Sub(Complex[] a, double[] b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] - b[i];
            return c;
        }

        /// <summary>
        /// Array subtraction.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The subtraction of array <paramref name="a"/> and real number <paramref name="b"/>.</returns>
        public static Complex[] Sub(Complex[] a, double b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] - b;
            return c;
        }

        /// <summary>
        /// Array subtraction.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The subtraction of real number <paramref name="a"/> and array <paramref name="b"/>.</returns>
        public static double[] Sub(double a, double[] b)
        {
            double[] c = new double[b.Length];
            for (int i = 0; i < b.Length; i++)
                c[i] = a - b[i];
            return c;
        }

        /// <summary>
        /// Array subtraction.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The subtraction of Complex number <paramref name="a"/> and array <paramref name="b"/>.</returns>
        public static Complex[] Sub(double a, Complex[] b)
        {
            Complex[] c = new Complex[b.Length];
            for (int i = 0; i < b.Length; i++)
                c[i] = a - b[i];
            return c;
        }

        /// <summary>
        /// Array subtraction.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The subtraction of Complex number <paramref name="a"/> and array <paramref name="b"/>.</returns>
        public static Complex[] Sub(Complex a, double[] b)
        {
            Complex[] c = new Complex[b.Length];
            for (int i = 0; i < b.Length; i++)
                c[i] = a - b[i];
            return c;
        }

        /// <summary>
        /// Array subtraction.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The subtraction of Complex number <paramref name="a"/> and array <paramref name="b"/>.</returns>
        public static Complex[] Sub(Complex a, Complex[] b)
        {
            Complex[] c = new Complex[b.Length];
            for (int i = 0; i < b.Length; i++)
                c[i] = a - b[i];
            return c;
        }

        #endregion

        #region Array Multiplication

        /// <summary>
        /// Array multiplication.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The multiplication of two array.</returns>
        public static double[] Mul(double[] a, double[] b)
        {
            double[] c = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] * b[i];
            return c;
        }

        /// <summary>
        /// Array multiplication.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The multiplication of array <paramref name="a"/> and real number <paramref name="b"/>.</returns>
        public static double[] Mul(double[] a, double b)
        {
            double[] c = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] * b;
            return c;
        }

        /// <summary>
        /// Array multiplication.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The multiplication of array <paramref name="a"/> and Complex number <paramref name="b"/>.</returns>
        public static Complex[] Mul(double[] a, Complex b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] * b;
            return c;
        }
        /// <summary>
        /// Array multiplication.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The multiplication of two array.</returns>
        public static Complex[] Mul(double[] a, Complex[] b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] * b[i];
            return c;
        }

        /// <summary>
        /// Array multiplication.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The multiplication of two array.</returns>
        public static Complex[] Mul(Complex[] a, Complex[] b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] * b[i];
            return c;
        }

        /// <summary>
        /// Array multiplication.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The multiplication of array <paramref name="a"/> and Complex number <paramref name="b"/>.</returns>
        public static Complex[] Mul(Complex[] a, Complex b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] * b;
            return c;
        }

        /// <summary>
        /// Array multiplication.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The multiplication of two array.</returns>
        public static Complex[] Mul(Complex[] a, double[] b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] * b[i];
            return c;
        }

        /// <summary>
        /// Array multiplication.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The multiplication of array <paramref name="a"/> and real number <paramref name="b"/>.</returns>
        public static Complex[] Mul(Complex[] a, double b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] * b;
            return c;
        }

        #endregion

        #region Array Division

        /// <summary>
        /// Array division.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The division of two array.</returns>
        public static double[] Div(double[] a, double[] b)
        {
            double[] c = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] / b[i];
            return c;
        }

        /// <summary>
        /// Array division.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The division of array <paramref name="a"/> and real number <paramref name="b"/>.</returns>
        public static double[] Div(double[] a, double b)
        {
            double[] c = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] / b;
            return c;
        }

        /// <summary>
        /// Array division.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The division of array <paramref name="a"/> and Complex number <paramref name="b"/>.</returns>
        public static Complex[] Div(double[] a, Complex b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] / b;
            return c;
        }
        /// <summary>
        /// Array division.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The division of two array.</returns>
        public static Complex[] Div(double[] a, Complex[] b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] / b[i];
            return c;
        }

        /// <summary>
        /// Array division.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The division of two array.</returns>
        public static Complex[] Div(Complex[] a, Complex[] b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] / b[i];
            return c;
        }

        /// <summary>
        /// Array division.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The division of array <paramref name="a"/> and Complex number <paramref name="b"/>.</returns>
        public static Complex[] Div(Complex[] a, Complex b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] / b;
            return c;
        }

        /// <summary>
        /// Array division.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The division of two array.</returns>
        public static Complex[] Div(Complex[] a, double[] b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] / b[i];
            return c;
        }

        /// <summary>
        /// Array division.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The division of array <paramref name="a"/> and real number <paramref name="b"/>.</returns>
        public static Complex[] Div(Complex[] a, double b)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i] / b;
            return c;
        }

        /// <summary>
        /// Array division.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The division of real number <paramref name="a"/> and array <paramref name="b"/>.</returns>
        public static double[] Div(double a, double[] b)
        {
            double[] c = new double[b.Length];
            for (int i = 0; i < b.Length; i++)
                c[i] = a / b[i];
            return c;
        }

        /// <summary>
        /// Array division.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The division of Complex number <paramref name="a"/> and array <paramref name="b"/>.</returns>
        public static Complex[] Div(double a, Complex[] b)
        {
            Complex[] c = new Complex[b.Length];
            for (int i = 0; i < b.Length; i++)
                c[i] = a / b[i];
            return c;
        }

        /// <summary>
        /// Array division.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The division of Complex number <paramref name="a"/> and array <paramref name="b"/>.</returns>
        public static Complex[] Div(Complex a, double[] b)
        {
            Complex[] c = new Complex[b.Length];
            for (int i = 0; i < b.Length; i++)
                c[i] = a / b[i];
            return c;
        }

        /// <summary>
        /// Array division.
        /// </summary>
        /// <param name="a">The left-hand operand.</param>
        /// <param name="b">The right-hand operand.</param>
        /// <returns>The division of Complex number <paramref name="a"/> and array <paramref name="b"/>.</returns>
        public static Complex[] Div(Complex a, Complex[] b)
        {
            Complex[] c = new Complex[b.Length];
            for (int i = 0; i < b.Length; i++)
                c[i] = a / b[i];
            return c;
        }

        #endregion

        #endregion

        #region Basic Functions

        /// <summary>
        /// Differences of elements.
        /// </summary>
        /// <param name="arr">A double array.</param>
        /// <returns>The differences of elements.</returns>
        public static double[] Diff(double[] arr)
        {
            int n = arr.Length - 1;
            double[] buf = new double[n];
            for (int i = 0; i < n; i++)
                buf[i] = arr[i + 1] - arr[i];
            return buf;
        }

        /// <summary>
        /// Differences of elements.
        /// </summary>
        /// <param name="arr">A Complex array.</param>
        /// <returns>The differences of elements.</returns>
        public static Complex[] Diff(Complex[] arr)
        {
            int n = arr.Length - 1;
            Complex[] buf = new Complex[n];
            for (int i = 0; i < n; i++)
                buf[i] = arr[i + 1] - arr[i];
            return buf;
        }

        /// <summary>
        ///  Product of elements.
        /// </summary>
        /// <param name="arr">A Complex array.</param>
        /// <returns>The product of elements.</returns>
        public static Complex Prod(Complex[] arr)
        {
            Complex buf = arr[0];
            for (int i = 1; i < arr.Length; i++)
                buf *= arr[i];
            return buf;
        }

        /// <summary>
        /// Cumulative sum of elements.
        /// </summary>
        /// <param name="arr">A double array.</param>
        /// <returns>Cumulative sum of array <paramref name="arr"/>.</returns>
        public static double[] CumSum(double[] arr)
        {
            double[] cusum = new double[arr.Length];
            cusum[0] = arr[0];
            for (int i = 1; i < arr.Length; i++)
                cusum[i] = arr[i] + cusum[i - 1];
            return cusum;
        }

        /// <summary>
        /// Cumulative sum of elements.
        /// </summary>
        /// <param name="arr">A Complpex array.</param>
        /// <returns>Cumulative sum of array <paramref name="arr"/>.</returns>
        public static Complex[] CumSum(Complex[] arr)
        {
            Complex[] cusum = new Complex[arr.Length];
            cusum[0] = arr[0];
            for (int i = 1; i < arr.Length; i++)
                cusum[i] = arr[i] + cusum[i - 1];
            return cusum;
        }

        /// <summary>
        /// Cumulative product of elements.
        /// </summary>
        /// <param name="arr">A double array.</param>
        /// <returns>Cumulative product of array <paramref name="arr"/>.</returns>
        public static double[] CumProd(double[] arr)
        {
            double[] cusum = new double[arr.Length];
            cusum[0] = arr[0];
            for (int i = 1; i < arr.Length; i++)
                cusum[i] = arr[i] * cusum[i - 1];
            return cusum;
        }

        /// <summary>
        /// Cumulative product of elements.
        /// </summary>
        /// <param name="arr">A Complex array.</param>
        /// <returns>Cumulative product of array <paramref name="arr"/>.</returns>
        public static Complex[] CumProd(Complex[] arr)
        {
            Complex[] cusum = new Complex[arr.Length];
            cusum[0] = arr[0];
            for (int i = 1; i < arr.Length; i++)
                cusum[i] = arr[i] * cusum[i - 1];
            return cusum;
        }

        #endregion

        #region Mathematical Functions

        #region Double[]

        /// <summary>
        ///  Absolute value for each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The Absolute value for each element in <paramref name="arr"/>.</returns>
        public static double[] Abs(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Abs(arr[i]);
            return ans;
        }

        /// <summary>
        ///  Square root for each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The Square root for each element in <paramref name="arr"/>.</returns>
        public static double[] Sqrt(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Sqrt(arr[i]);
            return ans;
        }

        /// <summary>
        ///  Power value for each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <param name="p">The specified power.</param>
        /// <returns>The Power value for each element in <paramref name="arr"/>.</returns>
        public static double[] Pow(double[] arr, double p)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Pow(arr[i], p);
            return ans;
        }

        /// <summary>
        /// Exponential value for each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The Exponential value for each element in <paramref name="arr"/>.</returns>
        public static double[] Exp(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Exp(arr[i]);
            return ans;
        }

        /// <summary>
        ///  Natural logarithm for each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The natural logarithm for each element in <paramref name="arr"/>.</returns>
        public static double[] Log(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Log(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The Common logarithm for each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The Common logarithm for each element in <paramref name="arr"/>.</returns>
        public static double[] Log10(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Log10(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The sine of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The sine of each element in <paramref name="arr"/>.</returns>
        public static double[] Sin(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Sin(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The cosine of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The cosine of each element in <paramref name="arr"/>.</returns>
        public static double[] Cos(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Cos(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The tangent of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The tangent of each element in <paramref name="arr"/>.</returns>
        public static double[] Tan(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Tan(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The sine of each elements in an array in degrees.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The sine of each element in <paramref name="arr"/> in degrees.</returns>
        public static double[] Sind(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Sin(0.017453292519943296 * arr[i]);
            return ans;
        }

        /// <summary>
        ///  The cosine of each elements in an array in degrees.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The cosine of each element in <paramref name="arr"/> in degrees.</returns>
        public static double[] Cosd(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Cos(0.017453292519943296 * arr[i]);
            return ans;
        }

        /// <summary>
        ///  The tangent of each elements in an array in degrees.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The tangent of each element in <paramref name="arr"/> in degrees.</returns>
        public static double[] Tand(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Tan(0.017453292519943296 * arr[i]);
            return ans;
        }

        /// <summary>
        ///  The arcsine of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The arcsine of each element in <paramref name="arr"/>.</returns>
        public static double[] Asin(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Asin(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The arccosine of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The arccosine of each element in <paramref name="arr"/>.</returns>
        public static double[] Acos(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Acos(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The arctangent of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The arctangent of each element in <paramref name="arr"/>.</returns>
        public static double[] Atan(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Atan(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The hyperbolic sine of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The hyperbolic sine of each element in <paramref name="arr"/>.</returns>
        public static double[] Sinh(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Sinh(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The hyperbolic cosine of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The hyperbolic cosine of each element in <paramref name="arr"/>.</returns>
        public static double[] Cosh(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Cosh(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The tangent of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The hyperbolic tangent of each element in <paramref name="arr"/>.</returns>
        public static double[] Tanh(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Tanh(arr[i]);
            return ans;
        }

        /// <summary>
        /// Floor value for each elements in a double[].
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The floor value of each element in <paramref name="arr"/>.</returns>
        public static double[] Floor(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Floor(arr[i]);
            return ans;
        }

        /// <summary>
        /// Ceiling value for each elements in a double[].
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The Ceiling value of each element in <paramref name="arr"/>.</returns>
        public static double[] Ceiling(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Ceiling(arr[i]);
            return ans;
        }

        /// <summary>
        /// Round value for each elements in a double[].
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The round value of each element in <paramref name="arr"/>.</returns>
        public static double[] Round(double[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Math.Round(arr[i]);
            return ans;
        }

        #endregion

        #region Complex[]

        internal static double[] ConjMul(Complex[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = arr[i].ConjMul();
            return ans;
        }

        /// <summary>
        ///  Absolute value for each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The Absolute value for each element in <paramref name="arr"/>.</returns>
        public static double[] Abs(Complex[] arr)
        {
            double[] ans = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Abs(arr[i]);
            return ans;
        }

        /// <summary>
        ///  Square root for each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The Square root for each element in <paramref name="arr"/>.</returns>
        public static Complex[] Sqrt(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Sqrt(arr[i]);
            return ans;
        }

        /// <summary>
        ///  Power value for each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <param name="p">The specified power.</param>
        /// <returns>The Power value for each element in <paramref name="arr"/>.</returns>
        public static Complex[] Pow(Complex[] arr, double p)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Pow(arr[i], p);
            return ans;
        }

        /// <summary>
        ///  Power value for each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <param name="p">The specified power.</param>
        /// <returns>The Power value for each element in <paramref name="arr"/>.</returns>
        public static Complex[] Pow(Complex[] arr, Complex p)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Pow(arr[i], p);
            return ans;
        }

        /// <summary>
        ///  Power value for each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <param name="p">The specified power.</param>
        /// <returns>The Power value for each element in <paramref name="arr"/>.</returns>
        public static Complex[] Pow(double[] arr, Complex p)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Pow(arr[i], p);
            return ans;
        }

        /// <summary>
        /// Exponential value for each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The Exponential value for each element in <paramref name="arr"/>.</returns>
        public static Complex[] Exp(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Exp(arr[i]);
            return ans;
        }

        /// <summary>
        ///  Natural logarithm for each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The natural logarithm for each element in <paramref name="arr"/>.</returns>
        public static Complex[] Log(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Log(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The Common logarithm for each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The Common logarithm for each element in <paramref name="arr"/>.</returns>
        public static Complex[] Log10(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Log10(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The sine of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The sine of each element in <paramref name="arr"/>.</returns>
        public static Complex[] Sin(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Sin(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The cosine of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The cosine of each element in <paramref name="arr"/>.</returns>
        public static Complex[] Cos(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Cos(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The tangent of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The tangent of each element in <paramref name="arr"/>.</returns>
        public static Complex[] Tan(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Tan(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The sine of each elements in an array in degrees.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The sine of each element in <paramref name="arr"/> in degrees.</returns>
        public static Complex[] Sind(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Sin(0.017453292519943296 * arr[i]);
            return ans;
        }

        /// <summary>
        ///  The cosine of each elements in an array in degrees.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The cosine of each element in <paramref name="arr"/> in degrees.</returns>
        public static Complex[] Cosd(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Cos(0.017453292519943296 * arr[i]);
            return ans;
        }

        /// <summary>
        ///  The tangent of each elements in an array in degrees.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The tangent of each element in <paramref name="arr"/> in degrees.</returns>
        public static Complex[] Tand(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Tan(0.017453292519943296 * arr[i]);
            return ans;
        }

        /// <summary>
        ///  The arcsine of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The arcsine of each element in <paramref name="arr"/>.</returns>
        public static Complex[] Asin(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Asin(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The arccosine of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The arccosine of each element in <paramref name="arr"/>.</returns>
        public static Complex[] Acos(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Acos(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The arctangent of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The arctangent of each element in <paramref name="arr"/>.</returns>
        public static Complex[] Atan(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Atan(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The hyperbolic sine of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The hyperbolic sine of each element in <paramref name="arr"/>.</returns>
        public static Complex[] Sinh(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Sinh(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The hyperbolic cosine of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The hyperbolic cosine of each element in <paramref name="arr"/>.</returns>
        public static Complex[] Cosh(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Cosh(arr[i]);
            return ans;
        }

        /// <summary>
        ///  The tangent of each elements in an array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The hyperbolic tangent of each element in <paramref name="arr"/>.</returns>
        public static Complex[] Tanh(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Tanh(arr[i]);
            return ans;
        }

        /// <summary>
        /// Floor value for each elements in a double[].
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The floor value of each element in <paramref name="arr"/>.</returns>
        public static Complex[] Floor(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Floor(arr[i]);
            return ans;
        }

        /// <summary>
        /// Ceiling value for each elements in a double[].
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The Ceiling value of each element in <paramref name="arr"/>.</returns>
        public static Complex[] Ceiling(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Ceiling(arr[i]);
            return ans;
        }

        /// <summary>
        /// Round value for each elements in a double[].
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The round value of each element in <paramref name="arr"/>.</returns>
        public static Complex[] Round(Complex[] arr)
        {
            Complex[] ans = new Complex[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                ans[i] = Complex.Round(arr[i]);
            return ans;
        }


        #endregion

        #endregion

        #region Vector Norm

        /// <summary>
        /// returns the 1-norm of <paramref name="arr"/>.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The 1-norm of <paramref name="arr"/>.</returns>
        public static double Norm1(double[] arr)
        {
            return Abs(arr).Sum();
        }

        /// <summary>
        /// returns the infinity norm of <paramref name="arr"/>.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The infinity norm of <paramref name="arr"/>.</returns>
        public static double NormInf(double[] arr)
        {
            return Abs(arr).Max();
        }

        /// <summary>
        /// returns the 1-norm of <paramref name="arr"/>.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The 1-norm of <paramref name="arr"/>.</returns>
        public static double Norm1(Complex[] arr)
        {
            return Abs(arr).Sum();
        }

        /// <summary>
        /// returns the infinity norm of <paramref name="arr"/>.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The infinity norm of <paramref name="arr"/>.</returns>
        public static double NormInf(Complex[] arr)
        {
            return Abs(arr).Max();
        }

        /// <summary>
        /// Calculate the p-Norm of this array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <param name="p">p-norm.</param>
        /// <returns>the p-Norm of <paramref name="arr"/>.</returns>
        public static double Norm(double[] arr, int p)
        {
            if (p < 1)
            {
                throw new ArgumentException(Resources.Strings.PosInt);
            }

            double sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += Math.Pow(Math.Abs(arr[i]), p);
            }
            return Math.Pow(sum, 1.0 / p);
        }

        /// <summary>
        /// Calculate the p-Norm of this array.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <param name="p">p-norm.</param>
        /// <returns>the p-Norm of <paramref name="arr"/>.</returns>
        public static double Norm(Complex[] arr, int p)
        {
            if (p < 1)
            {
                throw new ArgumentException(Resources.Strings.PosInt);
            }

            double sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += Math.Pow(Complex.Abs(arr[i]), p);
            }
            return Math.Pow(sum, 1.0 / p);
        }

        /// <summary>
        /// Normalizes this array to a unit array with respect to the Eucliden 2-Norm.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The normalized array.</returns>
        public static double[] Normalize(double[] arr)
        {
            return MArray.Div(arr, arr.Norm());
        }

        /// <summary>
        /// Normalizes this array to a unit array with respect to the Eucliden 2-Norm.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The normalized array.</returns>
        public static Complex[] Normalize(Complex[] arr)
        {
            return MArray.Div(arr, arr.Norm());
        }

        #endregion

        #region Vector Space Analysis

        #region Double[]

        /// <summary>
        /// Returns a Dot Product or call Inner Product .
        /// </summary>
        /// <param name="a">An array.</param>
        /// <param name="b">An array.</param>
        /// <returns>The dot Product of <paramref name="a"/> and <paramref name="b"/>.</returns>
        public static double DotProd(double[] a, double[] b)
        {
            return MArray.Mul(a, b).Sum();
        }


        /// <summary>
        ///  Returns a Cross Product.
        /// </summary>
        /// <param name="a">An array.</param>
        /// <param name="b">An array.</param>
        /// <returns>The Cross Product of <paramref name="a"/> and <paramref name="b"/>.</returns>  
        /// <exception cref="ArithmeticException">Throw if the length of <paramref name="a"/> or <paramref name="b"/> is not 3.</exception>
        /// <remarks>It can only compute the Cross Product in three dimension.</remarks>     
        public static double[] CrossProd(double[] a, double[] b)
        {
            if (a.Length != 3 && b.Length != 3)
                throw new ArithmeticException(Resources.Strings.CrossProd);

            return new double[3] { a[2] * b[3] - a[3] * b[2], a[3] * b[1] - a[1] * b[3], a[1] * b[2] - a[2] * b[1] };
        }

        /// <summary>
        /// Returns the angle.
        /// </summary>
        /// <param name="a">An array.</param>
        /// <param name="b">A array.</param>
        /// <returns> The angle between <paramref name="a"/> and <paramref name="b"/>.</returns>
        public static double Angle(double[] a, double[] b)
        {
            return Math.Acos(DotProd(a, b) / (a.Norm() * b.Norm()));
        }

        #endregion

        #region Complex[]

        /// <summary>
        /// Returns a Dot Product or call Inner Product .
        /// </summary>
        /// <param name="a">An array.</param>
        /// <param name="b">An array.</param>
        /// <returns>The dot Product of <paramref name="a"/> and <paramref name="b"/>.</returns>
        public static Complex DotProd(Complex[] a, Complex[] b)
        {
            return MArray.Mul(a, b.Conj()).Sum();
        }

        /// <summary>
        ///  Returns a Cross Product.
        /// </summary>
        /// <param name="a">An array.</param>
        /// <param name="b">An array.</param>
        /// <returns>The Cross Product of <paramref name="a"/> and <paramref name="b"/>.</returns>  
        /// <exception cref="ArithmeticException">Throw if the length of <paramref name="a"/> or <paramref name="b"/> is not 3.</exception>
        /// <remarks>It can only compute the Cross Product in three dimension.</remarks>     
        public static Complex[] CrossProd(Complex[] a, Complex[] b)
        {
            if (a.Length != 3 && b.Length != 3)
                throw new ArithmeticException(Resources.Strings.CrossProd);

            return new Complex[3] { a[2] * b[3] - a[3] * b[2], a[3] * b[1] - a[1] * b[3], a[1] * b[2] - a[2] * b[1] };
        }

        /// <summary>
        /// Returns the angle.
        /// </summary>
        /// <param name="a">An array.</param>
        /// <param name="b">A array.</param>
        /// <returns> The angle between <paramref name="a"/> and <paramref name="b"/>.</returns>
        public static double Angle(Complex[] a, Complex[] b)
        {
            return Math.Acos(Complex.Abs(DotProd(a, b)) / (a.Norm() * b.Norm()));
        }

        #endregion

        #endregion
    }
}
