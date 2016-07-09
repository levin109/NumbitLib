using NumBitLib;
using NumBitLib.LinearAlgebra;
using System;
using System.Diagnostics;

namespace TestProject
{
    class Test
    {
        static void Main(string[] args)
        {

            //   Console.WindowWidth = 150;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Matrix mat = new Matrix(2, 3, 1, 2, 3, 4, 5, 6);//inits a matrix:mat=[1,2,3;4,5,6]
            CMatrix cmat = new CMatrix(1, 2, new Complex(1, 2), new Complex(3, 4));//inits a complex matrix:cmat=[1+2i,3+4i]
            CMatrix cat = new CMatrix(Matrix.Rand(3, 3), Matrix.Rand(3, 3));//inits a complex matrix use two random matrix
            Matrix.Hilb(5);//Hilbert matrix
            Matrix.Rank(mat);//matrix inverse
            CMatrix.Det(cat);//matrix determinant
            Matrix.QR(mat);
            CMatrix.SVD(cmat);//svd decomposition
            Console.WriteLine(sw.Elapsed);
        }
    }
}
