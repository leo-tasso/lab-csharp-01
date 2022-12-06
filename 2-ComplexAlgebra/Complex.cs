using System;

namespace ComplexAlgebra
{
    /// <summary>
    /// A type for representing Complex numbers.
    /// </summary>
    ///
    /// TODO: Model Complex numbers in an object-oriented way and implement this class.
    /// TODO: In other words, you must provide a means for:
    /// TODO: * instantiating complex numbers
    /// TODO: * accessing a complex number's real, and imaginary parts
    /// TODO: * accessing a complex number's modulus, and phase
    /// TODO: * complementing a complex number
    /// TODO: * summing up or subtracting two complex numbers
    /// TODO: * representing a complex number as a string or the form Re +/- iIm
    /// TODO:     - e.g. via the ToString() method
    /// TODO: * checking whether two complex numbers are equal or not
    /// TODO:     - e.g. via the Equals(object) method
    public class Complex
    {


        public Complex(double real, double imm)
        {
            Real = real;
            Imaginary = imm;
        }

        // TODO: fill this class\



        public double Real { get; }
        public double Imaginary { get; }

        public double Modulus => Math.Sqrt(Real * Real + Imaginary * Imaginary);

        public Complex Complement() => new Complex(Real, -Imaginary);

        public Complex Plus(Complex c) => new Complex(c.Real + this.Real, c.Imaginary + this.Imaginary);

        public Complex Minus(Complex c) => new Complex(this.Real - c.Real, this.Imaginary - c.Imaginary);

        public double Phase => Math.Atan2(Imaginary, Real);

        public override string ToString() => $"{this.Real}+ i{this.Imaginary}";

        public override bool Equals(object obj)
        {
            return obj is Complex complex &&
                   Real == complex.Real &&
                   Imaginary == complex.Imaginary;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Real, Imaginary);
        }
    }
}