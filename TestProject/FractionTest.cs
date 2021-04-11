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
    /// Zusammenfassungsbeschreibung für FractionText
    /// </summary>
    [TestClass]
    public class FractionTest
    {
        public FractionTest()
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
            Fraction a = new Fraction(1, 3);
            Fraction b = new Fraction(3, 5);
            Fraction c = a + b;

            Assert.AreEqual(new Fraction(14, 15), c);

            c = a + 3;
            Assert.AreEqual(new Fraction(10, 3), c);

            c = 2 + b;
            Assert.AreEqual(new Fraction(13, 5), c);
        }

        [TestMethod]
        public void TestSub()
        {
            Fraction a = new Fraction(14, 15);
            Fraction b = new Fraction(3, 5);
            Fraction c = a - b;
            Assert.AreEqual(new Fraction(1, 3), c);

            a = new Fraction(16, 15);
            c = a - 1;
            Assert.AreEqual(new Fraction(1, 15), c);

            a = new Fraction(3, 15);
            c = 1 - a;
            Assert.AreEqual(new Fraction(12, 15), c);
        }

        [TestMethod]
        public void TestMul()
        {
            Fraction a = new Fraction(1, 3);
            Fraction b = new Fraction(4, 5);
            Fraction c = a * b;

            Assert.AreEqual(new Fraction(4, 15), c);
        }

        [TestMethod]
        public void TestDiv()
        {
            Fraction a = new Fraction(4, 15);
            Fraction b = new Fraction(4, 5);
            Fraction c = a / b;

            Assert.AreEqual(new Fraction(1, 3), c);
        }

        [TestMethod]
        public void TestNeg()
        {
            Fraction a = new Fraction(4, 15);
            Fraction c = -a;

            Assert.AreEqual(new Fraction(-4, 15), c);
        }

        [TestMethod]
        public void TestToString()
        {
            Fraction a = new Fraction(14, 15);
            Console.WriteLine(a.ToString());

            Assert.AreEqual("14/15", a.ToString());
        }

        [TestMethod]
        public void TestEquals()
        {
            Fraction a = new Fraction(6, 10);
            Fraction b = new Fraction(3, 5);

            Assert.AreEqual(true, a.Equals(b));

            a = new Fraction(6);
            Assert.AreEqual(true, a.Equals(6));
        }

        [TestMethod]
        public void TestRational()
        {
            //double pi = Math.PI;
            //Fraction a = Fraction.MakeRational(Math.PI,1e-13);
            //double diff = a - pi;
            //Console.WriteLine(a.ToString());
            Fraction a = new Fraction(-0.7284);

            Assert.AreEqual(-1821, a.Numerator);
            Assert.AreEqual(2500, a.Denominator);
        }

        [TestMethod]
        public void TestCopy()
        {
            Fraction a = new Fraction(5,8);
            Fraction b = new Fraction(a);
            //b.Denominator = 9;
            //b.Numerator = 6;

            Assert.AreEqual(5, a.Numerator);
            Assert.AreEqual(8, a.Denominator);
            //Assert.AreEqual(6, b.Numerator);
            //Assert.AreEqual(9, b.Denominator);
        }

    }
}
