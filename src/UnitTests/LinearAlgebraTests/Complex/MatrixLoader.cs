// <copyright file="MatrixLoader.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
// http://mathnetnumerics.codeplex.com
//
// Copyright (c) 2009-2013 Math.NET
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using MathNet.Numerics.Random;
using NUnit.Framework;

namespace MathNet.Numerics.UnitTests.LinearAlgebraTests.Complex
{
    using System.Numerics;

    /// <summary>
    /// Base class for matrix tests.
    /// </summary>
    public abstract class MatrixLoader
    {
        /// <summary>
        /// Gets or sets test matrices values to use.
        /// </summary>
        protected Dictionary<string, Complex[,]> TestData2D { get; set; }

        /// <summary>
        /// Gets or sets test matrices instances to use.
        /// </summary>
        protected Dictionary<string, Matrix<Complex>> TestMatrices { get; set; }

        /// <summary>
        /// Creates a matrix for the given number of rows and columns.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <returns>A matrix with the given dimensions.</returns>
        protected abstract Matrix<Complex> CreateMatrix(int rows, int columns);

        /// <summary>
        /// Creates a matrix from a 2D array.
        /// </summary>
        /// <param name="data">The 2D array to create this matrix from.</param>
        /// <returns>A matrix with the given values.</returns>
        protected abstract Matrix<Complex> CreateMatrix(Complex[,] data);

        /// <summary>
        /// Creates a vector of the given size.
        /// </summary>
        /// <param name="size">The size of the vector to create.
        /// </param>
        /// <returns>The new vector. </returns>
        protected abstract Vector<Complex> CreateVector(int size);

        /// <summary>
        /// Creates a vector from an array.
        /// </summary>
        /// <param name="data">The array to create this vector from.</param>
        /// <returns>The new vector. </returns>
        protected abstract Vector<Complex> CreateVector(Complex[] data);

        /// <summary>
        /// Setup test matrices.
        /// </summary>
        [SetUp]
        public virtual void SetupMatrices()
        {
            TestData2D = new Dictionary<string, Complex[,]>
                {
                    {"Singular3x3", new[,] {{new Complex(1.0, 1), new Complex(1.0, 1), new Complex(2.0, 1)}, {new Complex(1.0, 1), new Complex(1.0, 1), new Complex(2.0, 1)}, {new Complex(1.0, 1), new Complex(1.0, 1), new Complex(2.0, 1)}}},
                    {"Square3x3", new[,] {{new Complex(-1.1, 1), new Complex(-2.2, 1), new Complex(-3.3, 1)}, {Complex.Zero, new Complex(1.1, 1), new Complex(2.2, 1)}, {new Complex(-4.4, 1), new Complex(5.5, 1), new Complex(6.6, 1)}}},
                    {"Square4x4", new[,] {{new Complex(-1.1, 1), new Complex(-2.2, 1), new Complex(-3.3, 1), new Complex(-4.4, 1)}, {Complex.Zero, new Complex(1.1, 1), new Complex(2.2, 1), new Complex(3.3, 1)}, {new Complex(1.0, 1), new Complex(2.1, 1), new Complex(6.2, 1), new Complex(4.3, 1)}, {new Complex(-4.4, 1), new Complex(5.5, 1), new Complex(6.6, 1), new Complex(-7.7, 1)}}},
                    {"Singular4x4", new[,] {{new Complex(-1.1, 1), new Complex(-2.2, 1), new Complex(-3.3, 1), new Complex(-4.4, 1)}, {new Complex(-1.1, 1), new Complex(-2.2, 1), new Complex(-3.3, 1), new Complex(-4.4, 1)}, {new Complex(-1.1, 1), new Complex(-2.2, 1), new Complex(-3.3, 1), new Complex(-4.4, 1)}, {new Complex(-1.1, 1), new Complex(-2.2, 1), new Complex(-3.3, 1), new Complex(-4.4, 1)}}},
                    {"Tall3x2", new[,] {{new Complex(-1.1, 1), new Complex(-2.2, 1)}, {Complex.Zero, new Complex(1.1, 1)}, {new Complex(-4.4, 1), new Complex(5.5, 1)}}},
                    {"Wide2x3", new[,] {{new Complex(-1.1, 1), new Complex(-2.2, 1), new Complex(-3.3, 1)}, {Complex.Zero, new Complex(1.1, 1), new Complex(2.2, 1)}}},
                    {"Symmetric3x3", new[,] {{Complex.One, 2.0, 3.0}, {2.0, 2.0, 0.0}, {3.0, 0.0, 3.0}}}
                };

            TestMatrices = new Dictionary<string, Matrix<Complex>>();
            foreach (var name in TestData2D.Keys)
            {
                TestMatrices.Add(name, CreateMatrix(TestData2D[name]));
            }
        }

        public static Matrix<Complex> GenerateRandomDenseMatrix(int row, int col)
        {
            return DenseMatrix.CreateRandom(row, col, new Normal(new MersenneTwister(1)));
        }

        public static Matrix<Complex> GenerateRandomPositiveDefiniteHermitianDenseMatrix(int order)
        {
            var a = DenseMatrix.CreateRandom(order, order, new Normal(new MersenneTwister(1)));
            return a.ConjugateTranspose()*a;
        }

        public static Vector<Complex> GenerateRandomDenseVector(int order)
        {
            return DenseVector.CreateRandom(order, new Normal(new MersenneTwister(1)));
        }

        public static Matrix<Complex> GenerateRandomUserDefinedMatrix(int row, int col)
        {
            return new UserDefinedMatrix(GenerateRandomDenseMatrix(row, col).ToArray());
        }

        public static Matrix<Complex> GenerateRandomPositiveDefiniteHermitianUserDefinedMatrix(int order)
        {
            return new UserDefinedMatrix(GenerateRandomPositiveDefiniteHermitianDenseMatrix(order).ToArray());
        }

        public static Vector<Complex> GenerateRandomUserDefinedVector(int order)
        {
            return new UserDefinedVector(GenerateRandomDenseVector(order).ToArray());
        }
    }
}
