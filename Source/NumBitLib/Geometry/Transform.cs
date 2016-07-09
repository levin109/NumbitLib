
/*********************************************************************************************************************************************
 * *
 * *        File Name                : Transform.cs
 * *        Creator                  : Fuhua Lai
 * *        Date Modified            : 2013-6-12 15:13:36
 * *        Functional Description   : Performs Math Transformation.
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

namespace NumBitLib.Geometry
{
    /// <summary>
    ///  Performs Math Transformation.
    /// </summary>
    public class Transform
    {
        #region Coordinate Transformation

        /// <summary>
        ///  Transform polar to Cartesian coordinates.
        /// </summary>
        /// <param name="r">polar coordinates radius.</param>
        /// <param name="theta">polar coordinates Angle.</param>
        /// <returns>The Cartesian coordinates,returns x,y.</returns>
        public static double[] Pol2Cart(double r, double theta)
        {
            return new double[] { r * Math.Cos(theta), r * Math.Sin(theta) };
        }

        /// <summary>
        ///  Transform Cartesian to polar coordinates.
        /// </summary>
        /// <param name="x">Cartesian coordinates <paramref name="x"/>.</param>
        /// <param name="y">Cartesian coordinates <paramref name="y"/>.</param>
        /// <returns>The Polar coordinates,returns radius and angle.</returns>
        public static double[] Cart2Pol(double x, double y)
        {
            return new double[] { Math.Sqrt(ElMath.Square(x, y)), Math.Atan2(y, x) };
        }

        /// <summary>
        /// Transform Cartesian to spherical coordinates.
        /// </summary>
        /// <param name="r">spherical coordinates radius.</param>
        /// <param name="theta">spherical coordinates azimuth.</param>
        /// <param name="phi">spherical coordinates elevation.</param>
        /// <returns>The spherical coordinates,returns x,y,z.</returns>
        public static double[] Pol2Sph(double r, double theta, double phi)
        {
            double mid = r * Math.Sin(theta);
            return new double[] { mid * Math.Cos(phi), mid * Math.Sin(phi), r * Math.Cos(theta) };
        }

        /// <summary>
        /// Transform spherical to Cartesian coordinates.
        /// </summary>
        /// <param name="x">Cartesian coordinates <paramref name="x"/>.</param>
        /// <param name="y">Cartesian coordinates <paramref name="y"/>.</param>
        /// <param name="z">Cartesian coordinates <paramref name="z"/>.</param>
        /// <returns>The Cartesian coordinates.
        /// Returns spherical coordinates radius,spherical coordinates azimuth,spherical coordinates elevation.</returns>
        public static double[] Sph2Cart(double x, double y, double z)
        {
            double r = Math.Sqrt(x * x + y * y + z * z);
            return new double[] { Math.Acos(z / r), Math.Atan2(y, x) };
        }

        #endregion

        #region Angular Transformation

        /// <summary>
        ///  Convert degree to radian.
        /// </summary>
        /// <param name="degree">Degrees.</param>
        /// <returns>The radian form.</returns>
        public static double AngleToRadian(double degree)
        {
            return 0.017453292519943296 * degree;
        }

        /// <summary>
        ///  Convert radianto degree .
        /// </summary>
        /// <param name="radian">radian.</param>
        /// <returns>The degree form.</returns>
        public static double RadianToAngle(double radian)
        {
            return 57.295779513082323 * radian;
        }

        #endregion
    }
}
