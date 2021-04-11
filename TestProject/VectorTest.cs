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
    /// Zusammenfassungsbeschreibung für VectorTest
    /// </summary>
    [TestClass]
    public class VectorTest
    {
        public VectorTest()
        {
            //
            // TODO: Konstruktorlogik hier hinzufügen
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Ruft den Textkontext mit Informationen über
        ///den aktuellen Testlauf sowie Funktionalität für diesen auf oder legt diese fest.
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

        #region Zusätzliche Testattribute
        //
        // Sie können beim Schreiben der Tests folgende zusätzliche Attribute verwenden:
        //
        // Verwenden Sie ClassInitialize, um vor Ausführung des ersten Tests in der Klasse Code auszuführen.
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Verwenden Sie ClassCleanup, um nach Ausführung aller Tests in einer Klasse Code auszuführen.
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen. 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestAdd()
        {
            Vector v1 = new Vector(1, 2);
            Vector v2 = new Vector(3, 4);

            Vector v3 = v1 + v2;
            Vector exp = new Vector(4, 6);
            Assert.AreEqual<Vector>(exp, v3);
        }

        [TestMethod]
        public void TestSub()
        {
            Vector v1 = new Vector(5, 8);
            Vector v2 = new Vector(3, 4);

            Vector v3 = v1 - v2;
            Vector exp = new Vector(2, 4);
            Assert.AreEqual<Vector>(exp, v3);
        }

        [TestMethod]
        public void TestSkalarMult()
        {
            Vector v1 = new Vector(1, 3);
            Vector v2 = new Vector(4, 6);

            double act = v1 * v2;
            double exp = 22;
            Assert.AreEqual<double>(exp, act);
        }

        [TestMethod]
        public void TestLength()
        {
            Vector v1 = new Vector(3, 4);
            Assert.AreEqual<double>(5, v1.Length);
        }

        [TestMethod]
        public void TestMult()
        {
            Vector v1 = new Vector(2, 3);
            Vector v2 = v1 * 2;
            Vector v3 = 2 * v1;

            Vector exp = new Vector(4, 6);
            Assert.AreEqual<Vector>(exp, v2);
            Assert.AreEqual<Vector>(exp, v3);
        }

        [TestMethod]
        public void TestDiv()
        {
            Vector v1 = new Vector(4, 8);
            Vector v2 = v1 / 2;

            Vector exp = new Vector(2, 4);
            Assert.AreEqual<Vector>(exp, v2);
        }

        [TestMethod]
        public void TestAbst()
        {
            Vector ap = new Vector(1, 3);
            Vector ep = new Vector(5, 1);
            Vector rv = ep - ap;
            Vector p = new Vector(4, 3);

            double lambda = (p - ap) * rv / (rv * rv);
            Vector c = p - (ap + lambda * rv);

            double exp = Math.Sqrt(9.0 / 5.0);
            Assert.AreEqual<double>(exp, c.Length);
        }

        [TestMethod]
        public void TestDistance()
        {
            Vector ap = new Vector(1, 3);
            Vector ep = new Vector(5, 1);
            Vector rv = ep - ap;
            Vector p = new Vector(4, 3);

            double dist = Vector.DistanceLinePoint(ap, rv, p);

            double exp = Math.Sqrt(9.0 / 5.0);
            Assert.AreEqual<double>(exp, dist);
        }

    }
}
