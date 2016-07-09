
/*********************************************************************************************************************************************
 * *
 * *        File Name                : ElMath.cs
 * *        Creator                  : Fuhua Lai
 * *        Date Modified            : 2013-6-6 10:06:38
 * *        Functional Description   : Performs some basic mathematical computation.
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
using System.Collections.Generic;
using System.Numerics;

namespace NumBitLib
{
    /// <summary>
    /// Performs some basic mathematical computation.
    /// </summary>
    public class ElMath
    {
        #region Elementary Algebra

        /// <summary>
        /// Returns a boolean value indicates wether a integer number is odd.
        /// </summary>
        /// <param name="n">An integer number.</param>
        /// <returns>A boolean value indicates whether <paramref name="n"/> is odd.</returns>
        public static bool IsOdd(Int64 n)
        {
            return Convert.ToBoolean(n & 1);
        }

        /// <summary>
        /// Returns a boolean value indicates wether a integer number is odd.
        /// </summary>
        /// <param name="n">An integer number.</param>
        /// <returns>A boolean value indicates whether <paramref name="n"/> is odd.</returns>
        public static bool IsEven(Int64 n)
        {
            return n % 2 == 0;
        }
        /// <summary>
        /// Returns a boolean value reprents wether a integer number is prime.
        /// </summary>
        /// <param name="n">An integer number.</param>
        /// <returns>A boolean value indicates whether <paramref name="n"/> is prime.</returns>
        /// <exception cref="ArgumentException">Throw exception if Argument is a negative integer.</exception>
        public static bool IsPrime(Int64 n)
        {
            if (n < 0)
                throw new ArgumentException(Resources.Strings.NonNegInt);
            if (n == 2)
                return true;
            if (n == 1 || n % 2 == 0)
                return false;
            for (int i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        ///  Generate a list of prime numbers less than or equal to a specified integer number.
        /// </summary>
        /// <param name="n">An integer number.</param>
        /// <returns>An integer array contains prime numbers less than or equal to <paramref name="n"/>. </returns>
        /// <exception cref="ArgumentException">Throw exception if Argument is a negative integer.</exception>
        public static Int64[] Primes(Int64 n)
        {
            if (n < 0)
                throw new ArgumentException(Resources.Strings.NonNegInt);
            List<Int64> lst = new List<Int64>();
            for (int i = 2; i <= n; i++)
                if (IsPrime(i))
                    lst.Add(i);
            return lst.ToArray();
        }

        /// <summary>
        /// Integer factorization. 
        /// </summary>
        /// <param name="n">An integer number.</param>
        /// <returns>An integer array contains the prime factors of <paramref name="n"/>.</returns>
        /// <exception cref="ArgumentException">Throw exception if Argument is a negative integer.</exception>
        public static Int64[] Factor(Int64 n)
        {
            if (n < 0)
                throw new ArgumentException(Resources.Strings.NonNegInt);
            List<Int64> primes = new List<Int64>();
            if (IsPrime(n))
                return new Int64[1] { n };
            for (int i = 2; i <= n; )
            {
                if (IsPrime(i) && (n % i == 0))
                {
                    primes.Add(i);
                    n /= i;
                }
                else
                {
                    i++;
                }
            }
            return primes.ToArray();
        }

        /// <summary>
        /// Returns the product of all the integers from 1 to n. 
        /// </summary>
        /// <param name="n">An integer number.</param>
        /// <returns>The factorial of <paramref name="n"/>.</returns>
        /// <exception cref="ArgumentException">Throw exception if Argument is a negative integer.</exception>
        public static Int64 Factorial(int n)
        {
            if (n < 0)
                throw new ArgumentException(Resources.Strings.NonNegInt);
            else if (n > 1)
            {
                Int64 fac = n;
                for (int i = n - 1; i > 1; i--)
                    fac *= i;
                return fac;
            }
            else
                return 1;
        }

        /// <summary>
        /// Returns the product of all the integers from 1 to n.this method use <see cref="BigInteger"/> to against integer overflow.
        /// </summary>
        /// <param name="n">An integer number.</param>
        /// <returns>The factorial of <paramref name="n"/>.</returns>
        /// <exception cref="ArgumentException">Throw exception if Argument is a negative integer.</exception>
        public static BigInteger FactorialBig(Int64 n)
        {
            if (n < 0)
                throw new ArgumentException(Resources.Strings.NonNegInt);
            else if (n > 1)
            {
                BigInteger fac = n;
                for (Int64 i = n - 1; i > 1; i--)
                    fac *= i;
                return fac;
            }
            else
                return 1;
        }

        /// <summary>
        /// Combinatorial number.
        /// </summary>
        /// <param name="n">An integer number.</param>
        /// <param name="k">An integer number.</param>
        /// <exception cref="ArgumentException">Throw exception if Argument <paramref name="n"/> is a negative integer,
        /// and if argument <paramref name="n"/> less equal <paramref name="k"/>.</exception>
        public static Int64 Nchoosek(int n, int k)
        {
            if (n < 0)
                throw new ArgumentException(Resources.Strings.NonNegInt);
            if (k > n)
                throw new ArgumentException(Resources.Strings.Nchoosek);
            else if (n == 0)
                return 1;
            else
            {
                Int64 Cnk = n;
                for (int i = n - 1; i > n - k; i--)
                    Cnk *= i;
                return Cnk / Factorial(k);
            }
        }

        /// <summary>
        ///  Greatest Common Divisor.
        /// </summary>
        /// <param name="a">An integer number.</param>
        /// <param name="b">An integer number.</param>
        /// <returns>The Greatest Common Divisor of <paramref name="a"/> and <paramref name="b"/>.</returns>
        public static int GCD(int a, int b)
        {
            int max = Math.Abs(Math.Max(a, b));
            int min = Math.Abs(Math.Min(a, b));

            if (min == 0)
                return max;
            int mid = max % min;
            if (mid != 0)
                return GCD(min, mid);
            return min;
        }

        /// <summary>
        ///  Least Common Multiple
        /// </summary>
        /// <param name="a">An integer number.</param>
        /// <param name="b">An integer number.</param>
        /// <returns>The Least Common Multiple of <paramref name="a"/> and <paramref name="b"/>.</returns>
        /// <exception cref="ArgumentException">Throw exception if Argument <paramref name="a"/> or <paramref name="b"/> is not a positive integer.</exception>
        public static int LCM(int a, int b)
        {
            if (a < 1 || b < 1)
                throw new ArgumentException(Resources.Strings.PosInt);
            int temp_lcm;
            temp_lcm = a * b / GCD(a, b);
            return temp_lcm;
        }

        #endregion

        #region Basic Math Calculation

        /// <summary>
        ///  Robust computation of the sum of squares.The result is <paramref name="a"/>^2+<paramref name="b"/>^2.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <param name="b">A real number.</param>
        /// <returns>The square sum of <paramref name="a"/> and <paramref name="b"/>.</returns>
        /// <remarks>This method is design to avoid errors arising due to limited-precision calculations performed on computers.</remarks>
        public static double Square(double a, double b)
        {
            double r;
            if (Math.Abs(a) > Math.Abs(b))
            {
                r = b / a;
                return a * a * (1.0 + r * r);
            }
            else if (b != 0)
            {
                r = a / b;
                return b * b * (1.0 + r * r);
            }
            else
                return 0.0;
        }

        /// <summary>
        /// Robust computation of the square root of the sum of squares.The result is sqrt(<paramref name="x"/>^2+<paramref name="y"/>^2).
        /// </summary>
        /// <param name="x">A number of double type.</param>
        /// <param name="y">A number of double type.</param>
        /// <returns>The square root of the sum of squares.</returns>
        /// <remarks> Hypot is a mathematical function defined to calculate the length of the hypotenuse of a right-angle triangle. 
        /// It is designed to avoid errors arising due to limited-precision calculations performed on computers.</remarks>
        public static double Hypot(double x, double y)
        {
            double t;
            x = Math.Abs(x);
            y = Math.Abs(y);
            if (x < y)
            {
                t = x;
                x = y;
            }
            else
                t = y;
            if (x == 0)
                return y;
            t = t / x;
            return x * Math.Sqrt(1 + t * t);
        }


        /// <summary>
        /// Returns -1 if x is negative;else returns 1.
        /// </summary>
        /// <param name="x">A number of double type.</param>
        /// <returns>The sign of <paramref name="x"/>.</returns>
        public static int Sign(double x)
        {
            if (Math.Sign(x) == -1)
                return -1;
            else
                return 1;
        }

        #endregion
    }
}
