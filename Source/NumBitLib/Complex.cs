
/*********************************************************************************************************************************************
 * *
 * *        File Name                : Complex.cs
 * *        Creator                  : Fuhua Lai
 * *        Date Modified            : 2013-6-5 20:09:55
 * *        Functional Description   : Performs Complex operation.
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
namespace NumBitLib
{
    /// <summary>
    /// Represents a Complex number.
    /// </summary>
    /// <remarks>
    /// A complex number is a number that can be expressed in the form a + bi, where a and b are real numbers and i is the imaginary unit,
    /// that is i^2 = −1.In this expression, a is the real part and b is the imaginary part of the complex number.
    /// <para>
    /// Complex numbers extend the idea of the one-dimensional number line to the two-dimensional complex plane by using the horizontal axis 
    /// for the real part and the vertical axis for the imaginary part. The complex number a + bi can be identified with the point (a, b) in 
    /// the complex plane.
    /// </para>
    /// <para>
    /// The complex numbers contain the ordinary real numbers while extending them in order to solve problems that cannot be solved with real
    /// numbers alone. The Italian mathematician Gerolamo Cardano is the first known to have introduced complex numbers. He called them "fictitious"
    /// during his attempts to find solutions to cubic equations in the 16th century, but complex numbers are no more or less "fictitious" or 
    /// "imaginary" than any other kind of number.
    /// </para>
    /// </remarks>
    public struct Complex : IComparable, IFormattable
    {
        #region Fields，Properties

        /// <summary>
        /// Complex number of Zero.
        /// </summary>
        public static readonly Complex Zero = new Complex(0.0, 0.0);
        /// <summary>
        /// Complex number of One.
        /// </summary>
        public static readonly Complex One = new Complex(1.0, 0.0);
        /// <summary>
        /// Imaginary unit.
        /// </summary>
        public static readonly Complex I = new Complex(0.0, 1.0);

        /// <summary>
        ///  The real part of a Complex number.
        /// </summary>
        private double real;
        /// <summary>
        ///  The imaginary part of a Complex number.
        /// </summary>
        private double imaginary;

        /// <summary>
        /// Gets or sets the real part of this Complex number.
        /// </summary>
        public double Real
        {
            get { return real; }
            set { real = value; }
        }
        /// <summary>
        /// Gets or sets the imaginary part of this Complex number.
        /// </summary>
        public double Imaginary
        {
            get { return imaginary; }
            set { imaginary = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Complex struct by specified its real and imaginary part.
        /// </summary>
        /// <param name="real">The real part of this Complex number.</param>
        /// <param name="imaginary">The imaginary part of this Complex number.</param>
        ///<example>Create a <see cref="Complex"/> instance.
        ///<code>
        ///Complex cp=new Complex(1,2);
        ///</code>
        ///</example>
        public Complex(double real, double imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

       

        public Complex(double real)
            : this(real, 0.0)
        {
        }

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="cp">A complex number.</param>
        ///<example>Use copy constructor.
        ///<code>
        ///Complex cp=new Complex(1,2);
        ///Complex copy=new Complex(cp);
        ///</code></example>
        public Complex(Complex cp)
        {
            this.real = cp.real;
            this.imaginary = cp.imaginary;
        }

        #endregion

        #region Operator Overloading

        /// <summary>
        /// Complex addition.
        /// </summary>
        /// <param name="cp1">The left-hand operand.</param>
        /// <param name="cp2">The right-hand operand.</param>
        /// <returns>The sum of two Complex number.</returns>
        public static Complex operator +(Complex cp1, Complex cp2)
        {
            return new Complex(cp1.real + cp2.real, cp1.imaginary + cp2.imaginary);
        }

        /// <summary>
        /// Complex addition.
        /// </summary>
        /// <param name="a">The left-hand operand that is a real number.</param>
        /// <param name="cp">The right-hand operand that is a Complex number.</param>
        /// <returns>The sum of the real number and the Complex number.</returns>
        public static Complex operator +(double a, Complex cp)
        {
            return new Complex(a + cp.real, cp.imaginary);
        }

        /// <summary>
        /// Complex addition.
        /// </summary>
        /// <param name="cp">The left-hand operand that is a Complex number.</param>
        /// <param name="a">The right-hand operand that is a real number.</param>
        /// <returns>The sum of the Complex number and the real number.</returns>
        public static Complex operator +(Complex cp, double a)
        {
            return new Complex(a + cp.real, cp.imaginary);
        }

        /// <summary>
        /// Complex subtraction.
        /// </summary>
        /// <param name="cp1">The left-hand operand that is a Complex number.</param>
        /// <param name="cp2">The right-hand operand that is a Complex number.</param>
        /// <returns>The subtraction of two Complex number.</returns>
        public static Complex operator -(Complex cp1, Complex cp2)
        {
            return new Complex(cp1.real - cp2.real, cp1.imaginary - cp2.imaginary);
        }

        /// <summary>
        /// Complex subtraction.
        /// </summary>
        /// <param name="a">The left-hand operand that is a real number.</param>
        /// <param name="cp">The right-hand operand that is a Complex number.</param>
        /// <returns>The subtraction of the real number and the Complex number.</returns>
        public static Complex operator -(double a, Complex cp)
        {
            return new Complex(a - cp.real, -cp.imaginary);
        }

        /// <summary>
        /// Complex subtraction.
        /// </summary>
        /// <param name="cp">The left-hand operand that is a Complex number.</param>
        /// <param name="a">The right-hand operand that is a real number.</param>
        /// <returns>The subtraction of the Complex number and the real number.</returns>
        public static Complex operator -(Complex cp, double a)
        {
            return new Complex(cp.real - a, cp.imaginary);
        }

        /// <summary>
        /// Complex negative number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>    
        /// <returns>The negative number of <paramref name="cp"/>.</returns>
        public static Complex operator -(Complex cp)
        {
            return new Complex(-cp.real, -cp.imaginary);
        }

        /// <summary>
        /// Complex multiplication.
        /// </summary>
        /// <param name="cp1">The left-hand operand that is a Complex number.</param>
        /// <param name="cp2">The right-hand operand that is a Complex number.</param>
        /// <returns>The multiplication of two Complex number.</returns>
        public static Complex operator *(Complex cp1, Complex cp2)
        {
            double a = cp1.real;
            double b = cp1.imaginary;
            double c = cp2.real;
            double d = cp2.imaginary;
            return new Complex((a * c - b * d), (a * d + b * c));
        }

        /// <summary>
        /// Complex multiplication.
        /// </summary>
        /// <param name="a">The left-hand operand that is a real number.</param>
        /// <param name="cp">The right-hand operand that is a Complex number.</param>
        /// <returns>The multiplication of the real number and the Complex number.</returns>
        public static Complex operator *(double a, Complex cp)
        {
            return new Complex(a * cp.real, a * cp.imaginary);
        }

        /// <summary>
        /// Complex multiplication.
        /// </summary>
        /// <param name="cp">The left-hand operand that is a Complex number.</param>
        /// <param name="a">The right-hand operand that is a real number.</param>
        /// <returns>The multiplication of the Complex number and the real number.</returns>
        public static Complex operator *(Complex cp, double a)
        {
            return new Complex(a * cp.real, a * cp.imaginary);
        }

        /// <summary>
        /// Complex division.
        /// </summary>
        /// <param name="cp1">The left-hand operand that is a Complex number.</param>
        /// <param name="cp2">The right-hand operand that is a Complex number.</param>
        /// <returns>The division of two Complex number.</returns>
        public static Complex operator /(Complex cp1, Complex cp2)
        {
            double a = cp1.real;
            double b = cp1.imaginary;
            double c = cp2.real;
            double d = cp2.imaginary;
            if (Math.Abs(d) < Math.Abs(c))
            {
                double p = d / c;
                return new Complex((a + b * p) / (c + d * p), (b - a * p) / (c + d * p));
            }
            double q = c / d;
            return new Complex((b + a * q) / (d + c * q), (-a + b * q) / (d + c * q));
        }

        /// <summary>
        /// Complex division.
        /// </summary>
        /// <param name="a">The left-hand operand that is a real number.</param>
        /// <param name="cp">The right-hand operand that is a Complex number.</param>
        /// <returns>The division of the real number and the Complex number.</returns>
        public static Complex operator /(double a, Complex cp)
        {
            double c = cp.real;
            double d = cp.imaginary;
            if (Math.Abs(d) < Math.Abs(c))
            {
                double p = d / c;
                return new Complex(a / (c + d * p), -a * p / (c + d * p));
            }
            double q = c / d;
            return new Complex(a * q / (d + c * q), -a / (d + c * q));
        }

        /// <summary>
        /// Complex division.
        /// </summary>
        /// <param name="cp">The left-hand operand that is a Complex number.</param>
        /// <param name="a">The right-hand operand that is a real number.</param>
        /// <returns>The division of the Complex number and the real number.</returns>
        public static Complex operator /(Complex cp, double a)
        {
            return new Complex(cp.real / a, cp.imaginary / a);
        }

        /// <summary>
        /// Complex equality.
        /// </summary>
        /// <param name="cp1">The left-hand operand that is a Complex number.</param>
        /// <param name="cp2">The right-hand operand that is a Complex number.</param>
        /// <returns>The equality of two Complex number.</returns>
        public static bool operator ==(Complex cp1, Complex cp2)
        {
            return (cp1.real == cp2.real) && (cp1.imaginary == cp2.imaginary);
        }

        /// <summary>
        /// Complex inequality.
        /// </summary>
        /// <param name="cp1">The left-hand operand that is a Complex number.</param>
        /// <param name="cp2">The right-hand operand that is a Complex number.</param>
        /// <returns>The inequality of two Complex number.</returns>
        public static bool operator !=(Complex cp1, Complex cp2)
        {
            return (cp1.real != cp2.real) || (cp1.imaginary != cp2.imaginary);
        }

        #endregion

        #region Basic functions

        /// <summary>
        /// Returns the modulus or absolute value of a Complex number. 
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <remarks>Interpretation about Absolute value from wikipedia:
        /// <para>Since the complex numbers are not ordered, the definition for the real absolute value cannot be directly generalised for
        /// a complex number. However the geometric interpretation of the absolute value of a real number as its distance from 0 can be generalised.
        /// The absolute value of a complex number is defined as its distance in the complex plane from the origin using the Pythagorean theorem.
        /// More generally the absolute value of the difference of two complex numbers is equal to the distance between those two complex numbers.
        /// For any complex numberz = x + iy,where x and y are real numbers, the absolute value or modulus of z is denoted | z | and is given by
        /// |z| = sqrt(x^2 + y^2).</para>
        /// </remarks>
        /// <returns>The absolute value of <paramref name="cp"/>.</returns>
        public static double Abs(Complex cp)
        {
            double a = cp.real;
            double b = cp.imaginary;
            double r;
            if (Math.Abs(a) > Math.Abs(b))
            {
                r = b / a;
                return Math.Sqrt(a * a * (1.0 + r * r));
            }
            else if (b != 0)
            {
                r = a / b;
                return Math.Sqrt(b * b * (1.0 + r * r));
            }
            else
                return 0.0;
        }

        /// <summary>
        /// Returns the argument of a Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The argument of <paramref name="cp"/>.</returns>
        /// <remarks> 
        /// In mathematics, arg is a function operating on complex numbers (visualized in a complex plane). 
        /// It gives the angle between the line joining the point to the origin and the positive real axis.
        /// </remarks>
        public static double Angle(Complex cp)
        {
            return Math.Atan2(cp.imaginary, cp.real);
        }

        /// <summary>
        /// Returns the conjugate of a Complex number. 
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The conjugate of <paramref name="cp"/>.</returns>
        /// <remarks>In mathematics, complex conjugates are a pair of complex numbers, both having the same real part,
        /// but with imaginary parts of equal magnitude and opposite signs. For example, 3 + 4i and 3 − 4i are complex conjugates.
        /// <para> Geometrically, the conjugate of z(a complex number) is the "reflection" of z about the real axis.
        /// In particular, conjugating twice gives the original complex number.</para>
        /// </remarks>
        public static Complex Conj(Complex cp)
        {
            return new Complex(cp.real, -cp.imaginary);
        }

        /// <summary>
        /// Returns a Complex number from it's modulus and argument.
        /// </summary>
        /// <param name="modulus">modulus</param>
        /// <param name="argument">argument</param>
        /// <returns>A Complex number.</returns>
        /// <remarks>
        /// An alternative way of defining a point P in the complex plane, other than using the x- and y-coordinates, 
        /// is to use the distance of the point from O, the point whose coordinates are (0, 0) (the origin), 
        /// together with the angle between the line through P and O and the (horizontal) line which is the positive part of the real axis. 
        /// This idea leads to the polar form of complex numbers.
        /// </remarks>
        public static Complex Polar(double modulus, double argument)
        {
            return new Complex(modulus * Math.Cos(argument), modulus * Math.Sin(argument));
        }

        /// <summary>
        /// Returns the reciprocal of a Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The reciprocal of <paramref name="cp"/>.</returns>
        public static Complex Inv(Complex cp)
        {
            double c = cp.real;
            double d = cp.imaginary;
            double e = c * c + d * d;
            return new Complex(c / e, -d / e);
        }

        /// <summary>
        /// Signum function of a Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The Signum of <paramref name="cp"/>.</returns>
        /// <remarks>
        /// The signum function can be generalized to complex numbers as sign(z) =z/|z| for any z ∈C except z = 0. 
        /// The signum of a given complex number z is the point on the unit circle of the complex plane that is nearest to z. 
        /// </remarks>
        public static Complex Sign(Complex cp)
        {
            if (cp == Zero)
                return One;
            else
                return cp / Abs(cp);
        }

        internal double ConjMul()
        {
            return ElMath.Square(this.real, this.imaginary);
        }

        #endregion

        #region Elementary Analytic Functions

        /// <summary>
        /// Returns the Complex exponent.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The exponent of <paramref name="cp"/>.</returns>
        public static Complex Exp(Complex cp)
        {
            double a = cp.real;
            double b = cp.imaginary;
            return Math.Exp(a) * new Complex((Math.Cos(b)), (Math.Sin(cp.imaginary)));
        }

        /// <summary>
        /// Returns the Complex natural logarithm.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The natural logarithm of <paramref name="cp"/>.</returns>
        public static Complex Log(Complex cp)
        {
            return new Complex(Math.Log(Abs(cp)), Angle(cp));
        }

        /// <summary>
        /// Returns the Complex logarithm of the base 10.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The logarithm of the base 10 of <paramref name="cp"/>.</returns>
        public static Complex Log10(Complex cp)
        {
            return Log(cp) / Math.Log(10);
        }

        /// <summary>
        /// Returns a specified Complex number raise to the specified power.
        /// </summary>
        /// <param name="cp">The base</param>
        /// <param name="n">The specified power</param>
        /// <returns><paramref name="cp"/> of the power <paramref name="n"/>.</returns>
        public static Complex Pow(Complex cp, int n)
        {
            double arg = Angle(cp);
            double mid = Math.Pow(Abs(cp), n);
            return new Complex(mid * Math.Cos(n * arg), mid * Math.Sin(n * arg));
        }

        /// <summary>
        /// Returns a specified Complex number raise to the specified power.
        /// </summary>
        /// <param name="cp">The base</param>
        /// <param name="x">The specified power</param>
        ///  <returns><paramref name="cp"/> of the power <paramref name="x"/>.</returns>
        public static Complex Pow(Complex cp, double x)
        {
            return Exp(x * Log(cp));
        }

        /// <summary>
        /// Returns a specified Complex number raise to the specified power.
        /// </summary>
        /// <param name="x">The base.</param>
        /// <param name="cp">The specified power.</param>
        ///  <returns><paramref name="x"/> of the power <paramref name="cp"/>.</returns>
        public static Complex Pow(double x, Complex cp)
        {
            return Exp(cp * Math.Log(x));
        }

        /// <summary>
        /// Returns a specified Complex number raise to the specified power.
        /// </summary>
        /// <param name="cp1">The Complex number.</param>
        /// <param name="cp2">The specified Complex power.</param>
        ///  <returns><paramref name="cp1"/> of the power <paramref name="cp2"/>.</returns>
        public static Complex Pow(Complex cp1, Complex cp2)
        {
            return Exp(cp2 * Log(cp1));
        }

        /// <summary>
        ///  Returns the square root of a Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The square root of <paramref name="cp"/>.</returns>
        public static Complex Sqrt(Complex cp)
        {
            return Polar(Math.Sqrt(Abs(cp)), Angle(cp) / 2.0);
        }

        /// <summary>
        /// Returns a n-th Root of a Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <param name="n">The specified root.</param>
        /// <returns> A <paramref name="n"/>-th Root of <paramref name="cp"/>.</returns>
        public static Complex Root(Complex cp, int n)
        {
            double arg = Angle(cp) / n;
            return Math.Pow(Abs(cp), 1.0 / n) * new Complex(Math.Cos(arg), Math.Sin(arg));
        }

        /// <summary>
        /// Returns all of the n-th Roots of a Complex number.
        /// </summary>
        /// <param name="cp">A Complex number</param>
        /// <param name="n">The specified root.</param>
        /// <returns>All of the <paramref name="n"/>-th Roots of <paramref name="cp"/>.</returns>
        /// <example>
        /// <code>
        /// Complex cp = new Complex(1, 2);
        ///   Complex[] roots = Complex.AllRoots(cp, 3);
        ///   //ToStringExtend() is a extend method for Complex[].
        ///Console.WriteLine(roots.ToStringExtend());
        /// </code>
        /// </example>
        /// <remarks>Every complex number has n different nth roots in the complex plane. </remarks>
        public static Complex[] AllRoots(Complex cp, int n)
        {
            Complex[] roots = new Complex[n];
            double arg = Complex.Angle(cp);
            double p = 2 * Math.PI;
            double r = Math.Pow(Abs(cp), 1.0 / n);
            double mid;
            for (int i = 0; i < n; i++)
            {
                mid = (arg + p * i) / n;
                roots[i] = r * new Complex(Math.Cos(mid), Math.Sin(mid));
            }
            return roots;
        }

        /// <summary>
        /// Returns the sine of the specified Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The sine of <paramref name="cp"/>.</returns>
        public static Complex Sin(Complex cp)
        {
            double a = cp.real;
            double b = cp.imaginary;
            return new Complex(Math.Sin(a) * Math.Cosh(b), Math.Cos(a) * Math.Sinh(b));
        }

        /// <summary>
        ///  Returns the cosine of the specified Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The cosine of <paramref name="cp"/>.</returns>
        public static Complex Cos(Complex cp)
        {
            double real = cp.real;
            double imaginary = cp.imaginary;
            return new Complex(Math.Cos(real) * Math.Cosh(imaginary), -(Math.Sin(real) * Math.Sinh(imaginary)));
        }

        /// <summary>
        ///  Returns the tangent of the specified Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The tangent of <paramref name="cp"/>.</returns>
        public static Complex Tan(Complex cp)
        {
            return Sin(cp) / Cos(cp);
        }

        /// <summary>
        ///  Returns the arcsine of the specified Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The arcsine of <paramref name="cp"/>.</returns>
        public static Complex Asin(Complex cp)
        {
            return (-I * Log((I * cp) + Sqrt(1.0 - (cp * cp))));
        }

        /// <summary>
        ///  Returns the arccosine of the specified Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The arccosine of <paramref name="cp"/>.</returns>
        public static Complex Acos(Complex cp)
        {
            return (-I * Log(cp + (I * Sqrt(1.0 - (cp * cp)))));
        }

        /// <summary>
        ///  Returns the arctangent of the specified Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The arctangent of <paramref name="cp"/>.</returns>
        public static Complex Atan(Complex cp)
        {
            return ((I / 2.0) * (Log(1.0 - (I * cp)) - Log(1.0 + (I * cp))));
        }

        /// <summary>
        ///  Returns the hyperbolic sine of the specified Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The hyperbolic sine of <paramref name="cp"/>.</returns>
        public static Complex Sinh(Complex cp)
        {
            double a = cp.real;
            double b = cp.imaginary;
            return new Complex(Math.Sinh(a) * Math.Cos(b), Math.Cosh(a) * Math.Sin(b));
        }

        /// <summary>
        ///  Returns the hyperbolic cosine of the specified Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The hyperbolic cosine of <paramref name="cp"/>.</returns>
        public static Complex Cosh(Complex cp)
        {
            double a = cp.real;
            double b = cp.imaginary;
            return new Complex(Math.Cosh(a) * Math.Cos(b), Math.Sinh(a) * Math.Sin(b));
        }

        /// <summary>
        ///  Returns the hyperbolic tangent of the specified Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The hyperbolic tangent of <paramref name="cp"/>.</returns>
        public static Complex Tanh(Complex cp)
        {
            return (Sinh(cp) / Cosh(cp));
        }
        #endregion

        #region Rounding Functions

        /// <summary>
        ///  Returns the largest Complex with integer parts less than or equal to the specified Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The largest Complex with integer parts less than or equal to <paramref name="cp"/>.</returns>
        public static Complex Floor(Complex cp)
        {
            return new Complex(Math.Floor(cp.real), Math.Floor(cp.imaginary));
        }

        /// <summary>
        /// Returns the smallest Complex number with integral parts that is greater than or equal to the specified Complex number.
        /// </summary>
        /// <param name="cp">A Complex number.</param>
        /// <returns>The smallest Complex number with integral parts that is greater than or equal to <paramref name="cp"/>.</returns>
        public static Complex Ceiling(Complex cp)
        {
            return new Complex(Math.Ceiling(cp.real), Math.Ceiling(cp.imaginary));
        }

        /// <summary>
        /// Rounds a Complex number to the nearest Complex number with integral parts.
        /// </summary> 
        /// <param name="cp">A Complex number.</param>
        /// <returns>The nearest Complex number with integral parts to <paramref name="cp"/>.</returns>
        public static Complex Round(Complex cp)
        {
            return new Complex(Math.Round(cp.real), Math.Round(cp.imaginary));
        }

        internal bool CloseToZero()
        {
            if (Math.Abs(this.real) < LibData.Eps && Math.Abs(this.imaginary) < LibData.Eps)
                return true;
            return false;
        }
        #endregion

        #region Conversions

        /// <summary>
        /// Explicit conversion from double to Complex.
        /// </summary>
        /// <param name="a">A real number.</param>
        /// <returns>A Complex number whose real part is <paramref name="a"/> and imaginary part is 0.</returns>
        public static explicit operator Complex(double a)
        {
            return new Complex(a, 0.0);
        }

        #endregion

        #region Overide & implement

        /// <summary>
        ///  Converts this instance to its equivalent string representation.
        /// </summary>
        /// <returns>A string represent a Complex number.</returns>
        public override string ToString()
        {
            string strout;
            string sign = "";
            if (imaginary > 0)
                sign = "+";

            if (real != 0.0)
            {
                if (imaginary == 0.0)
                    strout = string.Format("{0,0:G5}", real);
                else
                    strout = string.Format("{0,0:G5}", real) + sign + string.Format("{0,0:G5}", imaginary) + "i";
            }
            else
            {
                if (imaginary != 0.0)
                    strout = string.Format("{0,0:G5}", imaginary) + "i";
                else if (this == Complex.Zero)
                    strout = "0";
                else


                    strout = string.Format("{0,0:G5}", real) + sign + string.Format("{0,0:G5}", imaginary) + "i";//NaN
            }
            return strout;
        }

        /// <summary>
        /// Converts this instance to its equivalent string representation by using the specified LibData.Format.
        /// </summary>
        /// <param name="format">A numeric LibData.Format string.</param>
        /// <returns>A string represent a Complex number.</returns>
        public string ToString(string format)
        {
            string strout = "";
            string sign = "";
            if (imaginary > 0)
                sign = "+";

            if (real != 0.0)
            {
                if (imaginary == 0.0)
                    strout = string.Format(format, real);
                else
                    strout = string.Format(format, real) + sign + string.Format(format, imaginary) + "i";
            }
            else
            {
                if (imaginary != 0.0)
                    strout = string.Format(format, imaginary) + "i";
                else if (this == Complex.Zero)
                    strout = "0";
                else
                    strout = string.Format(format, real) + sign + string.Format(format, imaginary) + "i";//NaN
            }
            return strout;
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation
        /// by using the specified LibData.Format and culture-specific LibData.Format information.
        /// </summary>
        /// <param name="format"> A numeric LibData.Format string. </param>
        /// <param name="formatProvider"> An System.IFormatProvider that supplies culture-specific formatting information.</param>
        /// <returns>A string represent a Complex number.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            string re;
            string im;

            if (double.IsNaN(this.real))
            {
                re = "   ";
                for (int k = 0; k < LibData.Digits; k++)
                    re += " ";
                re += "NaN";
            }

            else if (double.IsPositiveInfinity(this.real))
            {
                re = "   ";
                for (int k = 0; k < LibData.Digits; k++)
                    re += " ";
                re += "Inf";
            }

            else if (double.IsNegativeInfinity(this.real))
            {
                re = "  ";
                for (int k = 0; k < LibData.Digits; k++)
                    re += " ";
                re += "-Inf";
            }

            else
            {
                if (Math.Abs(this.real) < 10)
                    re = "   ";
                else
                    re = "  ";

                if (ElMath.Sign(this.real) == 1)
                    re += " " + Math.Abs(this.real).ToString(format, formatProvider);
                else
                    re += "-" + Math.Abs(this.real).ToString(format, formatProvider);


            }

            if (double.IsNaN(this.imaginary))
            {
                im = " + ";
                for (int k = 0; k < LibData.Digits - 1; k++)
                    im += " ";
                im += "NaNi";
            }

            else if (double.IsPositiveInfinity(this.imaginary))
            {
                im = "  + ";
                for (int k = 0; k < LibData.Digits - 1; k++)
                    im += " ";
                im += "Infi";
            }

            else if (double.IsNegativeInfinity(this.imaginary))
            {
                im = " - ";
                for (int k = 0; k < LibData.Digits - 2; k++)
                    im += " ";
                im += "-Infi";
            }

            else
            {
                if (Math.Sign(this.imaginary) == -1)
                {
                    if (Math.Abs(this.imaginary) < 10)
                        im = " - " + Math.Abs(this.imaginary).ToString(format, formatProvider) + "i";
                    else
                        im = " -" + Math.Abs(this.imaginary).ToString(format, formatProvider) + "i";
                }

                else
                {
                    if (Math.Abs(this.imaginary) < 10)
                        im = " + " + Math.Abs(this.imaginary).ToString(format, formatProvider) + "i";
                    else
                        im = " +" + Math.Abs(this.imaginary).ToString(format, formatProvider) + "i";
                }
            }

            return re + im;
        }

        /// <summary>
        /// Indicates whether this instance and a specific object are equals.
        /// </summary>
        /// <param name="obj">An object(Complex).</param>
        /// <returns>An boolean value indicates whether this instance and a specific object are equals.</returns>
        public override bool Equals(object obj)
        {
            return ((obj is Complex) && (this == ((Complex)obj)));
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A integer value reprents the hashcode.</returns>
        public override int GetHashCode()
        {
            int mid = 0x5f5e0fd;
            int a = real.GetHashCode() % mid;
            int b = imaginary.GetHashCode();
            return (a ^ b);
        }

        /// <summary>
        /// Quantitative Comparison.
        /// </summary>
        /// <param name="obj">An Complex number.</param> 
        /// <returns>
        /// returns -1: less than the argument;  returns 1: more than the argument; returns 0: equals to the argument;
        /// </returns>
        /// <remarks> This method is used to implement IComparable interface.</remarks>
        /// <exception cref="ArgumentException">if <paramref name="obj"/> is not a Complex number.</exception>
        public int CompareTo(object obj)
        {
            if (obj is Complex)
            {
                Complex cp = (Complex)obj;
                double a = Abs(this);
                double b = Abs(cp);
                if (a < b)
                    return -1;
                if (a > b)
                    return 1;
                else
                    return 0;
            }
            throw new ArgumentException(Resources.Strings.CompareTo);
        }
        #endregion
    }
}