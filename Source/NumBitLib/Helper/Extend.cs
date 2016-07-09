
/*********************************************************************************************************************************************
 * *
 * *        File Name                : Extend.cs
 * *        Creator                  : fuhua lai
 * *        Date Modified            : 2013-6-6 11:50:50
 * *        Functional Description   : Extend Method for Array class.
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

using System;
using System.Linq;
using System.Text;

namespace NumBitLib.Helper
{
    /// <summary>
    /// Extend method for array.
    /// </summary>
    public static class Extend
    {
        #region Unary Operation

        /// <summary>
        /// Array opposite.
        /// </summary>
        /// <param name="a">An array.</param>      
        /// <returns>The opposite of each element in array <paramref name="a"/>.</returns>
        public static double[] Opp(this double[] a)
        {
            double[] c = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = -a[i];
            return c;
        }

        /// <summary>
        /// Array conjugate.
        /// </summary>
        /// <param name="a">An array.</param>      
        /// <returns>The conjugate of each element in array <paramref name="a"/>.</returns>
        public static Complex[] Conj(this Complex[] a)
        {
            Complex[] c = new Complex[a.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = Complex.Conj(a[i]);
            return c;
        }

        #endregion

        #region Array Operation

        /// <summary>
        /// Array concatenate .
        /// </summary>
        /// <typeparam name="T">Some basic data type.</typeparam>
        /// <param name="arr">An array to be concatenated.</param>
        /// <param name="StartIndex">The start index to concatenate.</param>
        /// <param name="EndIndex">The end index to concatenate.</param>
        /// <returns>An new array.</returns>
        public static T[] Cut<T>(this  T[] arr, int StartIndex, int EndIndex)
        {
            T[] buf = new T[EndIndex - StartIndex + 1];
            for (int i = StartIndex; i <= EndIndex; i++)
                buf[i - StartIndex] = arr[i];
            return buf;
        }

        /// <summary>
        /// Array merge.
        /// </summary>
        /// <typeparam name="T">Some basic data type.</typeparam>
        /// <param name="arr1">An array to be merged.</param>
        /// <param name="arr2">Another array to be merged.</param>
        /// <returns>An new array.</returns>
        public static T[] Merge<T>(T[] arr1, T[] arr2)
        {
            int n = arr1.Length + arr2.Length;
            T[] buf = new T[n];
            for (int i = 0; i < arr1.Length; i++)
                buf[i] = arr1[i];
            for (int i = arr1.Length; i < n; i++)
                buf[i] = arr2[i - arr1.Length];
            return buf;
        }

        #endregion

        #region Basic Data Analysis

        /// <summary>
        /// Sum of elements.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The sum of elements in <paramref name="arr"/>.</returns>
        public static Complex Sum(this Complex[] arr)
        {
            Complex sum = arr[0];
            for (int i = 1; i < arr.Length; i++)
                sum += arr[i];
            return sum;
        }

        /// <summary>
        /// Average of elements.
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The Average of elements in <paramref name="arr"/>.</returns>
        public static Complex Average(this Complex[] arr)
        {
            Complex sum = arr[0];
            for (int i = 1; i < arr.Length; i++)
                sum += arr[i];
            return sum / arr.Length;
        }

        /// <summary>
        ///  Product of elements.
        /// </summary>
        /// <param name="arr">A double array.</param>
        /// <returns>The product of elements.</returns>
        public static double Prod(this double[] arr)
        {
            double buf = arr[0];
            for (int i = 1; i < arr.Length; i++)
                buf *= arr[i];
            return buf;
        }

        /// <summary>
        ///  Product of elements.
        /// </summary>
        /// <param name="arr">A Complex array.</param>
        /// <returns>The product of elements.</returns>
        public static Complex Prod(this Complex[] arr)
        {
            Complex buf = arr[0];
            for (int i = 1; i < arr.Length; i++)
                buf *= arr[i];
            return buf;
        }

        /// <summary>
        /// Returns the variance of this array.Var normalizes this array by n-1 if n>1, where n is the sample size. 
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The variance of this array.</returns>
        public static double Var(this double[] arr)
        {
            if (arr.Length == 1)
                return 0.0;
            double[] vc = MArray.Sub(arr, arr.Average());
            return MArray.Mul(vc, vc).Sum() / (arr.Length - 1);
        }
        ///<summary>
        /// Returns the standard deviation of this array.Var normalizes this array by n-1 if n>1, where n is the sample size. 
        /// </summary>
        /// <param name="arr">An array.</param>
        /// <returns>The standard deviation of this array.</returns>
        public static double Std(this double[] arr)
        {
            if (arr.Length == 1)
                return 0.0;
            double[] vc = MArray.Sub(arr, arr.Average());
            return Math.Sqrt(MArray.Mul(vc, vc).Sum() / (arr.Length - 1));
        }

        /// <summary>
        /// Array norm.
        /// </summary>
        /// <param name="arr">An double array.</param>
        /// <returns>The norm of this double array.</returns>
        public static double Norm(this double[] arr)
        {
            double buf = arr[0] * arr[0];
            for (int i = 1; i < arr.Length; i++)
                buf += arr[i] * arr[i];
            return buf;
        }

        /// <summary>
        /// Array norm.
        /// </summary>
        /// <param name="arr">An double array.</param>
        /// <returns>The norm of this double array.</returns>
        public static double Norm(this Complex[] arr)
        {
            double buf = ElMath.Square(arr[0].Real, arr[0].Imaginary);
            for (int i = 1; i < arr.Length; i++)
                buf += ElMath.Square(arr[i].Real, arr[i].Imaginary);
            return Math.Sqrt(buf);
        }

        #endregion

        #region Array Type

        /// <summary>
        /// Check if an array is zero vector.
        /// </summary>
        internal static bool NonZeroVector(this double[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] != 0)
                    return true;
            return false;
        }

        /// <summary>
        /// Check if an array is zero vector.
        /// </summary>
        internal static bool NonZeroVector(this Complex[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i]!=Complex.Zero)
                    return true;
            return false;
        }

        #endregion

        #region TostringExtend()

        /// <summary>
        /// Converts the array to its equivalent string representation.
        /// </summary>
        /// <typeparam name="T">Value type.</typeparam>
        /// <param name="arr">An array.</param>
        /// <returns>The equivalent string representation of this array.</returns>
        public static string ToString<T>(this T[] arr)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                sb.AppendFormat(LibData.Format, arr[i]);
                sb.Append(" ");
            }
            return sb.ToString();
        }

        #endregion
    }
}
