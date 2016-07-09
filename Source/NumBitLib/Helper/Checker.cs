
/*********************************************************************************************************************************************
 * *
 * *        File Name                : Checker.cs
 * *        Creator                  : Fuhua Lai
 * *        Date Modified            : 2013-6-11 23:17:37
 * *        Functional Description   : Performs dimention Checks.
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

namespace NumBitLib.Helper
{
    /// <summary>
    /// Dimention Checks,throw exception if condition is false.
    /// </summary>
    internal static class Checker
    {
        #region Array Checker

        internal static void ArrayMatch(Array a1, Array a2)
        {
            if (a1.Length != a2.Length)
                throw new ArgumentException(Resources.Strings.ArrayMatch);
        }

        #endregion

        #region Matrix Checker

        internal static void MatrixMatch(Matrix A, Matrix B)
        {
            if (A.Rows != B.Rows || A.Columns != B.Columns)
                throw new ArgumentException(Resources.Strings.MatrixMatch);
        }

        internal static void MatrixMatch(CMatrix A, CMatrix B)
        {
            if (A.Rows != B.Rows || A.Columns != B.Columns)
                throw new ArgumentException(Resources.Strings.MatrixMatch);
        }

        internal static void MatrixMatch(CMatrix A, Matrix B)
        {
            if (A.Rows != B.Rows || A.Columns != B.Columns)
                throw new ArgumentException(Resources.Strings.MatrixMatch);
        }

        internal static void MatrixMatch(Matrix A, CMatrix B)
        {
            if (A.Rows != B.Rows || A.Columns != B.Columns)
                throw new ArgumentException(Resources.Strings.MatrixMatch);
        }

        internal static void IsSquare(Matrix A)
        {
            if (A.Rows != A.Columns)
                throw new ArgumentException(Resources.Strings.IsSquare);
        }

        internal static void IsSquare(CMatrix A)
        {
            if (A.Rows != A.Columns)
                throw new ArgumentException(Resources.Strings.IsSquare);
        }

        internal static void IsSymmetric(Matrix A)
        {
            IsSquare(A);
            for (int i = 1; i <= A.Rows; i++)
                for (int j = 1; j < i; j++)
                    if (A[i, j] != A[j, i])
                        throw new ArgumentException(Resources.Strings.IsSymetric);
        }

        internal static void IsSymmetric(CMatrix A)
        {
            IsSquare(A);
            for (int i = 1; i <= A.Rows; i++)
                for (int j = 1; j <= i; j++)
                    if (A[i, j] != Complex.Conj(A[j, i]))
                        throw new ArgumentException(Resources.Strings.IsSymetric);
        }

        internal static void IsTridiag(Matrix A)
        {
            IsSquare(A);
            for (int i = 3; i <= A.Rows; i++)
                for (int j = 1; j < i - 1; j++)
                    if (A[i, j] != 0 || A[j, i] != 0)
                        throw new ArgumentException(Resources.Strings.IsTridiag);
        }

        internal static void IsTridiag(CMatrix A)
        {
            IsSquare(A);
            for (int i = 3; i <= A.Rows; i++)
                for (int j = 1; j < i - 1; j++)
                    if (A[i, j] != Complex.Zero || A[j, i] != Complex.Zero)
                        throw new ArgumentException(Resources.Strings.IsTridiag);
        }

        #endregion
    }
}
