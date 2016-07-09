
/*********************************************************************************************************************************************
 * *
 * *        File Name                : LibData.cs
 * *        Creator                  : Fuhua Lai
 * *        Date Modified            : 2013-6-5 20:06:55
 * *        Functional Description   : Provides some basic data in common use.
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
 * *        Copyright(c)2012-2013  NumBit work studio.  All rights reserved. 
 * *
 * ********************************************************************************************************************************************/

using System;

namespace NumBitLib
{
    /// <summary>
    /// A base class provides some basic data in common use.
    /// </summary>
    /// <remarks>This is a base library may be inherited by other class,it provides some basic data in common use.</remarks>
    public class LibData
    {
        /// <summary>
        ///  Variable precision digits.
        /// </summary>
        private static int digits = 3;

        /// <summary>
        /// Arithmetic Operational precision.
        /// </summary>
        protected static double eps = 1E-10;
        /// <summary>
        /// A string that specified display LibData.Format.
        /// </summary>
        protected static string format = "{0,0:F3}";

        /// <summary>
        /// initialize a new instance of <c>LibData</c>.
        /// </summary>
        /// <param name="n">Variable precision digits.</param>
        public LibData(int n)
        {
            digits = n;
            eps = Math.Pow(10, -n - 2);
          format = format.Replace(format[6], n.ToString()[0]);
        }

        /// <summary>
        /// Get variable precision digits.
        /// </summary>
        public static int Digits
        {
            get { return digits; }
        }

        /// <summary>
        ///  Get the Arithmetic Operational precision.
        /// </summary>
        public static double Eps
        {
            get { return eps; }
        }

        /// <summary>
        ///  Get the Display LibData.Format. 
        /// </summary>
        public static string Format
        {
            get { return format; }
        }
    }
}
