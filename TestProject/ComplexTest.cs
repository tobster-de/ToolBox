using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToolBox;
using ToolBox.Algebra;

namespace TestProject
{
    /// <summary>
    /// Summary description for ComplexTest
    /// </summary>
    [TestClass]
    public class ComplexTest
    {
        public ComplexTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        ///A test for Absolute value
        ///</summary>
        [TestMethod]
        public void AbsoluteTest()
        {
            Complex target = new Complex(4, 3);
            Fraction expected = new Fraction(5);

            Assert.AreEqual(expected, target.Absolute);

            target = new Complex(5, 4);
            double expAbs = Math.Sqrt(5 * 5 + 4 * 4);

            Assert.IsTrue(Math.Abs(expAbs- target.Absolute.Value) <= double.Epsilon);
        }

        /// <summary>
        ///A test for Real
        ///</summary>
        [TestMethod]
        public void RealTest()
        {
            Fraction expected = new Fraction(99);
            Complex target = new Complex(expected);

            Assert.AreEqual(expected, target.Real);
        }

        /// <summary>
        ///A test for Imaginary
        ///</summary>
        [TestMethod]
        public void ImaginaryTest()
        {
            Fraction expected = new Fraction(99);
            Complex target = new Complex(2, expected);

            Assert.AreEqual(expected, target.Imaginary);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest()
        {
            Complex target = new Complex(3, 3);
            string expected = "3+3i";
            Assert.AreEqual(expected, target.ToString());

            target = new Complex(4, -5);
            expected = "4-5i";
            Assert.AreEqual(expected, target.ToString());

            target = new Complex(-2, 6);
            expected = "-2+6i";
            Assert.AreEqual(expected, target.ToString());

            target = new Complex(-8, -3);
            expected = "-8-3i";
            Assert.AreEqual(expected, target.ToString());

            target = new Complex(0, -1);
            expected = "-i";
            Assert.AreEqual(expected, target.ToString());

            target = new Complex(0, 1);
            expected = "i";
            Assert.AreEqual(expected, target.ToString());

            target = new Complex(7, 0);
            expected = "7";
            Assert.AreEqual(expected, target.ToString());

            target = new Complex(-9, 0);
            expected = "-9";
            Assert.AreEqual(expected, target.ToString());
        }

        /// <summary>
        ///A test for op_Subtraction
        ///</summary>
        [TestMethod]
        public void op_SubtractionTest1()
        {
            Complex a = new Complex(9, 7);
            Complex b = new Complex(4, 4);
            Complex expected = new Complex(5, 3);
            Complex actual = (a - b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Subtraction
        ///</summary>
        [TestMethod]
        public void op_SubtractionTest()
        {
            Complex comp = new Complex(7, 8);
            int value = 3;
            Complex expected = new Complex(4, 8);
            Complex actual = (comp - value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Multiply
        ///</summary>
        [TestMethod]
        public void op_MultiplyTest1()
        {
            Complex comp = new Complex(4, 7);
            int value = 2;
            Complex expected = new Complex(8, 14);
            Complex actual = (comp * value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Multiply
        ///</summary>
        [TestMethod]
        public void op_MultiplyTest()
        {
            Complex a = new Complex(4, 2);
            Complex b = new Complex(5, 3);
            Complex expected = new Complex(14, 22); // TODO: Initialize to an appropriate value
            Complex actual = (a * b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Division
        ///</summary>
        [TestMethod]
        public void op_DivisionTest1()
        {
            Complex a = new Complex(25, 25);
            Complex b = new Complex(3, 4);
            Complex expected = new Complex(7, -1); // TODO: Initialize to an appropriate value
            Complex actual = (a / b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Division
        ///</summary>
        [TestMethod]
        public void op_DivisionTest()
        {
            Complex comp = new Complex(4, 6);
            int value = 2;
            Complex expected = new Complex(2, 3);
            Complex actual = (comp / value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Addition
        ///</summary>
        [TestMethod]
        public void op_AdditionTest()
        {
            Complex a = new Complex(3, 5);
            Complex b = new Complex(2, 4);
            Complex expected = new Complex(5, 9);
            Complex actual = (a + b);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Addition
        ///</summary>
        [TestMethod]
        public void op_AdditionTest_Int()
        {
            Complex comp = new Complex(3, 5);
            int value = 7;
            Complex expected = new Complex(10, 5);
            Complex actual = (comp + value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        [TestMethod]
        public void GetHashCodeTest()
        {
            int real = 5;
            int imag = 2;
            Complex target = new Complex(real, imag);
            int expected = ((real * 397 ^ 1) * 397) ^ (imag * 397 ^ 1);
            int actual = target.GetHashCode();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            Complex target = new Complex(3, 7);
            object obj = new Complex(3, 7);
            bool expected = true;
            bool actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            obj = new Complex(6, 4);
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            obj = new Fraction(3, 4);
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Conjugation
        /// </summary>
        [TestMethod]
        public void ConjugationTest()
        {
            Complex comp = new Complex(4, 6);
            Complex expected = new Complex(4, -6);

            Assert.AreEqual(expected, comp.Conjugation);
        }

        /// <summary>
        ///A test for Complex Constructor
        ///</summary>
        [TestMethod]
        public void ConstructorTest_RealImagAsFraction()
        {
            Fraction real = new Fraction(7);
            Fraction imag = new Fraction(3);
            Complex target = new Complex(real, imag);

            Assert.AreEqual(new Fraction(7.0), target.Real);
            Assert.AreEqual(new Fraction(3.0), target.Imaginary);
        }

        /// <summary>
        ///A test for Complex Constructor
        ///</summary>
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorTest_RealImagAsFractionDefault()
        {
            Fraction real = default;
            Fraction imag = default;
            Complex target = new Complex(real, imag);
        }

        /// <summary>
        ///A test for Complex Constructor
        ///</summary>
        [TestMethod]
        public void ConstructorTest_RealImagAsDouble()
        {
            double real = 8.0;
            double imag = 9.0;
            Complex target = new Complex(real, imag);

            Assert.AreEqual(new Fraction(8), target.Real);
            Assert.AreEqual(new Fraction(9), target.Imaginary);
        }

        /// <summary>
        ///A test for Complex Constructor
        ///</summary>
        [TestMethod]
        public void ConstructorTest_RealImagAsDouble_Zero()
        {
            double real = 0F;
            double imag = 0F;
            Complex target = new Complex(real, imag);

            Assert.AreEqual(new Fraction(0), target.Real);
            Assert.AreEqual(new Fraction(0), target.Imaginary);
        }

        /// <summary>
        ///A test for Complex Constructor
        ///</summary>
        [TestMethod]
        public void ConstructorTest_RealImagAsInt()
        {
            int real = 6;
            int imag = 3;
            Complex target = new Complex(real, imag);
            Assert.AreEqual(new Fraction(real), target.Real);
            Assert.AreEqual(new Fraction(imag), target.Imaginary);
        }

        /// <summary>
        ///A test for Complex Constructor
        ///</summary>
        [TestMethod]
        public void ConstructorTest_RealImagAsInt_Zero()
        {
            int real = 0;
            int imag = 0;
            Complex target = new Complex(real, imag);
            Assert.AreEqual(new Fraction(0), target.Real);
            Assert.AreEqual(new Fraction(0), target.Imaginary);
        }

        /// <summary>
        ///A test for Complex Constructor
        ///</summary>
        [TestMethod]
        public void ConstructorTest_RealAsInt()
        {
            int real = 8;
            Complex target = new Complex(real);
            Assert.AreEqual(new Fraction(real), target.Real);
            Assert.AreEqual(new Fraction(0), target.Imaginary);
        }

        /// <summary>
        ///A test for Complex Constructor
        ///</summary>
        [TestMethod]
        public void ConstructorTest_RealAsInt_Zero()
        {
            int real = 0;
            Complex target = new Complex(real);
            Assert.AreEqual(new Fraction(0), target.Real);
            Assert.AreEqual(new Fraction(0), target.Imaginary);
        }

        /// <summary>
        ///A test for Complex Constructor
        ///</summary>
        [TestMethod]
        public void ConstructorTest_Copy()
        {
            int real = 6;
            int imag = 3;
            Complex original = new Complex(real, imag);

            Complex target = new Complex(original);

            Assert.AreEqual(original.Real, target.Real);
            Assert.AreEqual(original.Imaginary, target.Imaginary);
        }

        /// <summary>
        ///A test for Complex Constructor
        ///</summary>
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorTest_RealAsFractionDefault()
        {
            Fraction real = default;
            Complex target = new Complex(real);
        }

        /// <summary>
        ///A test for Complex Constructor
        ///</summary>
        [TestMethod]
        public void ConstructorTest_RealAsFraction()
        {
            Fraction real = new Fraction(8);
            Complex target = new Complex(real);

            Assert.AreEqual(new Fraction(8.0), target.Real);
        }

        /// <summary>
        ///A test for Complex Constructor
        ///</summary>
        [TestMethod]
        public void ConstructorTest_RealAsDouble()
        {
            double real = 5.0;
            Complex target = new Complex(real);

            Assert.AreEqual(new Fraction(5.0), target.Real);
            Assert.AreEqual(new Fraction(0), target.Imaginary);
        }

        /// <summary>
        ///A test for Complex Constructor
        ///</summary>
        [TestMethod]
        public void ConstructorTest_RealAsDouble_Zero()
        {
            double real = 0.0;
            Complex target = new Complex(real);

            Assert.AreEqual(new Fraction(0), target.Real);
            Assert.AreEqual(new Fraction(0), target.Imaginary);
        }
    }
}
