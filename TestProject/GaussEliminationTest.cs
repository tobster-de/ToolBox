using ToolBox;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToolBox.Algebra;

namespace TestProject
{


    /// <summary>
    ///This is a test class for GaussEliminationTest and is intended
    ///to contain all GaussEliminationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GaussEliminationTest
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for SolveEquation
        ///</summary>
        [TestMethod()]
        public void SolveEquationTest()
        {
            Matrix A = new Matrix(3, 3);
            A[0, 0] = 1; A[0, 1] = 2; A[0, 2] = 3;
            A[1, 0] = -1; A[1, 1] = 1; A[1, 2] = 0;
            A[2, 0] = 1; A[2, 1] = 1; A[2, 2] = 2;

            Vector b = new Vector(0, 0, 0);
            Vector expected = null;
            Vector actual;
            actual = GaussElimination.SolveEquation(A, b);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InvertMatrix
        ///</summary>
        [TestMethod()]
        public void InvertMatrixTest()
        {
            Matrix A = new Matrix(3, 3);
            A[0, 0] = 1; A[0, 1] = 2; A[0, 2] = 0;
            A[1, 0] = 2; A[1, 1] = 3; A[1, 2] = 0;
            A[2, 0] = 3; A[2, 1] = 4; A[2, 2] = 1;

            Matrix Ai = new Matrix(3, 3);
            Ai[0, 0] = -3; Ai[0, 1] = 2; Ai[0, 2] = 0;
            Ai[1, 0] = 2; Ai[1, 1] = -1; Ai[1, 2] = 0;
            Ai[2, 0] = 1; Ai[2, 1] = -2; Ai[2, 2] = 1;

            Matrix actual;
            actual = GaussElimination.InvertMatrix(A);
            Assert.AreEqual(Ai, actual);
        }
    }
}
