using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToolBox;

namespace TestProject
{
    /// <summary>
    /// Summary description for MatrixTest
    /// </summary>
    [TestClass]
    public class MatrixTest
    {
        public MatrixTest()
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

        [TestMethod]
        public void TestAdd()
        {
            Matrix m1 = new Matrix(2, 3);
            Matrix m2 = new Matrix(2, 3);
            for (uint i = 0; i < m1.Rows; i++)
            {
                for (uint j = 0; j < m1.Columns; j++)
                {
                    m1[i, j] = 1;
                    m2[i, j] = 2;
                }
            }
            Matrix m3 = m1 + m2;
            for (uint i = 0; i < m1.Rows; i++)
            {
                for (uint j = 0; j < m1.Columns; j++)
                {
                    Assert.AreEqual(3, m3[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestSub()
        {
            Matrix m1 = new Matrix(2, 3);
            Matrix m2 = new Matrix(2, 3);
            for (uint i = 0; i < m1.Rows; i++)
            {
                for (uint j = 0; j < m1.Columns; j++)
                {
                    m1[i, j] = 3;
                    m2[i, j] = 1;
                }
            }
            Matrix m3 = m1 - m2;
            for (uint i = 0; i < m1.Rows; i++)
            {
                for (uint j = 0; j < m1.Columns; j++)
                {
                    Assert.AreEqual(2, m3[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestSkalarMulR()
        {
            Matrix m1 = new Matrix(2, 3);
            Matrix m2 = new Matrix(2, 3);
            for (uint i = 0; i < m1.Rows; i++)
            {
                for (uint j = 0; j < m1.Columns; j++)
                {
                    m1[i, j] = 3;
                }
            }
            Matrix m3 = m1 * 3;
            for (uint i = 0; i < m1.Rows; i++)
            {
                for (uint j = 0; j < m1.Columns; j++)
                {
                    Assert.AreEqual(9, m3[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestSkalarMulL()
        {
            Matrix m1 = new Matrix(2, 3);
            Matrix m2 = new Matrix(2, 3);
            for (uint i = 0; i < m1.Rows; i++)
            {
                for (uint j = 0; j < m1.Columns; j++)
                {
                    m1[i, j] = 3;
                }
            }
            Matrix m3 = 2 * m1;
            for (uint i = 0; i < m1.Rows; i++)
            {
                for (uint j = 0; j < m1.Columns; j++)
                {
                    Assert.AreEqual(6, m3[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestSkalarDiv()
        {
            Matrix m1 = new Matrix(2, 3);
            for (uint i = 0; i < m1.Rows; i++)
            {
                for (uint j = 0; j < m1.Columns; j++)
                {
                    m1[i, j] = 6;
                }
            }
            Matrix m3 = m1 / 2;
            for (uint i = 0; i < m1.Rows; i++)
            {
                for (uint j = 0; j < m1.Columns; j++)
                {
                    Assert.AreEqual(3, m3[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestMul()
        {
            Matrix m1 = new Matrix(4, 2);
            for (uint i = 0; i < m1.Rows; i++)
            {
                for (uint j = 0; j < m1.Columns; j++)
                {
                    m1[i, j] = i * m1.Columns + j;
                }
            }
            Matrix m2 = new Matrix(2, 3);
            for (uint i = 0; i < m2.Rows; i++)
            {
                for (uint j = 0; j < m2.Columns; j++)
                {
                    m2[i, j] = i * m2.Columns + j;
                }
            }
            Matrix m3 = m1 * m2;
            Assert.AreEqual(m1.Rows, m3.Rows, "Outer dimension");
            Assert.AreEqual(m2.Columns, m3.Columns, "Outer dimension");
            for (uint i = 0; i < m1.Rows; i++)
            {
                for (uint j = 0; j < m1.Columns; j++)
                {
                    //Assert.AreEqual(3, m3[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestUnity()
        {
            Matrix I = Matrix.Unity(3);
            Assert.AreEqual((uint)3, I.Rows);
            Assert.AreEqual((uint)3, I.Columns);

        }

        [TestMethod]
        public void TestTranspose()
        {
            Matrix mat = new Matrix(3, 3);
            for (uint i = 0; i < mat.Rows; i++)
            {
                mat[i, 0] = 5;
            }
            Matrix trans = mat.Transpose();
            Assert.AreEqual(mat.Rows, trans.Columns);
            Assert.AreEqual(mat.Columns, trans.Rows);
            for (uint i = 0; i < trans.Columns; i++)
            {
                Assert.AreEqual(5, trans[0, i]);
            }
        }

        [TestMethod]
        public void TestArrayConstructor()
        {
            Matrix mat = new Matrix(new[] { new double[] { 3, 5, 6 }, new double[] { 7, 2, 3 }, new double[] { 8, 3, 7 } });


        }

        /// <summary>
        ///A test for Determinant
        ///</summary>
        [TestMethod()]
        public void DeterminantTest()
        {
            Matrix target = Matrix.Unity(3);
            double actual = target.Determinant;
            double expected = 1;
            Assert.AreEqual(expected, actual);

        }
    }
}
