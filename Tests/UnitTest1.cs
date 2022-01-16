using NUnit.Framework;
using CalcClass;

namespace Tests
{
    [TestFixture]
    public class InitializationTests
    {
        private CalcClass.CalcClass _calcClass;
        
        [SetUp]
        public void Setup()
        {
            _calcClass = new CalcClass.CalcClass();
        }

        [Test]
        public void DefaultValueTest()
        {
            Assert.AreEqual(0, _calcClass.calcValue);
        }
        
        [Test]
        public void DefaultSystemTest()
        {
            Assert.AreEqual(CalcSystems.SystemDec, _calcClass.CalcSystem);
        }
        
        [Test]
        public void DefaultDTypeTest()
        {
            Assert.AreEqual(CalcDTypes.TypeQWord64, _calcClass.CalcDType);
        }

        [Test]
        public void DefaultBinaryArrayTest()
        {
            Assert.AreEqual(new int[64], _calcClass.binArray);
        }
    }
    
    [TestFixture]
    public class BinInsertingTest
    {
        private CalcClass.CalcClass _calcClass;
        
        [SetUp]
        public void Setup()
        {
            _calcClass = new CalcClass.CalcClass
            {
                CalcSystem = CalcSystems.SystemBin
            };
        }

        [Test]
        public void InsertValidValues()
        {
            _calcClass.insert('1');
            _calcClass.insert('0');
            _calcClass.insert('1');
            Assert.AreEqual(5, _calcClass.calcValue);
        }
        
        [Test]
        public void InsertInvalidValues()
        {
            _calcClass.insert('1');
            _calcClass.insert('w');
            _calcClass.insert('4');
            _calcClass.insert('1');
            Assert.AreEqual(3, _calcClass.calcValue);
        }
    }

    [TestFixture]
    public class DecInsertingTest
    {
        private CalcClass.CalcClass _calcClass;
        
        [SetUp]
        public void Setup()
        {
            _calcClass = new CalcClass.CalcClass
            {
                CalcSystem = CalcSystems.SystemDec
            };
        }

        [Test]
        public void InsertValidValues()
        {
            _calcClass.insert('5');
            _calcClass.insert('0');
            _calcClass.insert('7');
            _calcClass.insert('2');
            Assert.AreEqual(5072, _calcClass.calcValue);
        }
        
        [Test]
        public void InsertInvalidValues()
        {
            _calcClass.insert('5');
            _calcClass.insert('q');
            _calcClass.insert('2');
            Assert.AreEqual(52, _calcClass.calcValue);
        }
    }
    
    [TestFixture]
    public class OctInsertingTest
    {
        private CalcClass.CalcClass _calcClass;
        
        [SetUp]
        public void Setup()
        {
            _calcClass = new CalcClass.CalcClass
            {
                CalcSystem = CalcSystems.SystemOct
            };
        }

        [Test]
        public void InsertValidValues()
        {
            _calcClass.insert('1');
            _calcClass.insert('4');
            _calcClass.insert('2');
            Assert.AreEqual(98, _calcClass.calcValue);
        }
        
        [Test]
        public void InsertInvalidValues()
        {
            _calcClass.insert('4');
            _calcClass.insert('q');
            _calcClass.insert('9');
            _calcClass.insert('7');
            Assert.AreEqual(39, _calcClass.calcValue);
        }
    }
    
    [TestFixture]
    public class HexInsertingTest
    {
        private CalcClass.CalcClass _calcClass;
        
        [SetUp]
        public void Setup()
        {
            _calcClass = new CalcClass.CalcClass
            {
                CalcSystem = CalcSystems.SystemHex
            };
        }

        [Test]
        public void InsertValidValues()
        {
            _calcClass.insert('6');
            _calcClass.insert('1');
            _calcClass.insert('d');
            _calcClass.insert('A');
            Assert.AreEqual(25050, _calcClass.calcValue);
        }
        
        [Test]
        public void InsertInvalidValues()
        {
            _calcClass.insert('5');
            _calcClass.insert('q');
            _calcClass.insert('T');
            _calcClass.insert('b');
            Assert.AreEqual(91, _calcClass.calcValue);
        }
    }

    [TestFixture]
    public class SystemPrintingTest
    {
        private CalcClass.CalcClass _calcClass;
        [SetUp]
        public void Setup()
        {
            _calcClass = new CalcClass.CalcClass();
        }

        [Test]
        public void DecPrinting()
        {
            _calcClass.insert('8');
            _calcClass.insert('2');
            _calcClass.insert('7');
            Assert.AreEqual("827", _calcClass.calcValuesInActiveSystem);
        }

        [Test]
        public void BinPrinting()
        {
            _calcClass.insert('4');
            _calcClass.insert('1');
            _calcClass.insert('7');
            _calcClass.CalcSystem = CalcSystems.SystemBin;
            Assert.AreEqual("110100001", _calcClass.calcValuesInActiveSystem);
        }
        
        [Test]
        public void OctPriting()
        {
            _calcClass.insert('6');
            _calcClass.insert('6');
            _calcClass.insert('6');
            _calcClass.CalcSystem = CalcSystems.SystemOct;
            Assert.AreEqual("1232", _calcClass.calcValuesInActiveSystem);
        }

        [Test]
        public void HexPrinting()
        {
            _calcClass.insert('6');
            _calcClass.insert('9');
            _calcClass.insert('4');
            _calcClass.insert('2');
            _calcClass.insert('0');
            _calcClass.CalcSystem = CalcSystems.SystemHex;
            Assert.AreEqual("10f2c", _calcClass.calcValuesInActiveSystem);
        }
    }

    [TestFixture]
    public class BinaryArrayReturnTest
    {
        private CalcClass.CalcClass _calcClass;

        [SetUp]
        public void Setup()
        {
            _calcClass = new CalcClass.CalcClass();
        }
    }

    [TestFixture]
    public class BasicArithmeticOperations
    {
        private CalcClass.CalcClass _calcClass;

        [SetUp]
        public void Setup()
        {
            _calcClass = new CalcClass.CalcClass();
        }

        [Test]
        public void SumTest()
        {
            _calcClass.insert('5');
            _calcClass.insert('8');
            _calcClass.insert('+');
            _calcClass.insert('3');
            _calcClass.insert('7');
            _calcClass.insert('=');
            Assert.AreEqual(95, _calcClass.calcValue);
        }

        [Test]
        public void DiffTest()
        {
            _calcClass.insert('8');
            _calcClass.insert('1');
            _calcClass.insert('-');
            _calcClass.insert('5');
            _calcClass.insert('7');
            _calcClass.insert('=');
            Assert.AreEqual(24, _calcClass.calcValue);
        }

        [Test]
        public void MulTest()
        {
            _calcClass.insert('1');
            _calcClass.insert('3');
            _calcClass.insert('*');
            _calcClass.insert('2');
            _calcClass.insert('1');
            _calcClass.insert('=');
            Assert.AreEqual(273, _calcClass.calcValue);
        }

        [Test]
        public void DivTest()
        {
            _calcClass.insert('5');
            _calcClass.insert('0');
            _calcClass.insert('/');
            _calcClass.insert('1');
            _calcClass.insert('0');
            _calcClass.insert('=');
            Assert.AreEqual(5, _calcClass.calcValue);
        }

        [Test]
        public void PowTest()
        {
            _calcClass.insert('4');
            _calcClass.insert('p');
            _calcClass.insert('3');
            _calcClass.insert('=');
            Assert.AreEqual(64, _calcClass.calcValue);
        }

        [Test]
        public void ModuloTest()
        {
            _calcClass.insert('8');
            _calcClass.insert('3');
            _calcClass.insert('%');
            _calcClass.insert('1');
            _calcClass.insert('4');
            _calcClass.insert('=');
            Assert.AreEqual(13, _calcClass.calcValue);
        }
        
        [Test]
        public void MixedOperationsTest()
        {
            _calcClass.insert('4');
            _calcClass.insert('*');
            _calcClass.insert('6');
            _calcClass.insert('-');
            _calcClass.insert('8');
            _calcClass.insert('/');
            _calcClass.insert('4');
            _calcClass.insert('=');
            Assert.AreEqual(4, _calcClass.calcValue);
        }
        
        [Test]
        public void MixedOperationsTest2()
        {
            _calcClass.insert('1');
            _calcClass.insert('5');
            _calcClass.insert('/');
            _calcClass.insert('5');
            _calcClass.insert('+');
            _calcClass.insert('4');
            _calcClass.insert('7');
            _calcClass.insert('/');
            _calcClass.insert('2');
            _calcClass.insert('=');
            Assert.AreEqual(25, _calcClass.calcValue);
        }

        [Test]
        public void ChangeOperationTest()
        {
            _calcClass.insert('4');
            _calcClass.insert('2');
            _calcClass.insert('+');
            _calcClass.insert('-');
            _calcClass.insert('1');
            _calcClass.insert('6');
            _calcClass.insert('=');
            Assert.AreEqual(26, _calcClass.calcValue);
        }
    }

    [TestFixture]
    public class BinaryOperationsTests
    {
        private CalcClass.CalcClass _calcClass;

        [SetUp]
        public void Setup()
        {
            _calcClass = new CalcClass.CalcClass();
        }

        [Test]
        public void AndOperatorTest()
        {
            _calcClass.insert('1');
            _calcClass.insert('5');
            _calcClass.insert('3'); //128 + 16 + 8 + 1
            _calcClass.insert('&');
            _calcClass.insert('3');
            _calcClass.insert('0'); // 16 + 8 + 4 + 2
            _calcClass.insert('=');
            Assert.AreEqual(24, _calcClass.calcValue);
        }

        [Test]
        public void OrOperatorTest()
        {
            _calcClass.insert('7');
            _calcClass.insert('3'); // 64 + 8 + 1
            _calcClass.insert('|');
            _calcClass.insert('4');
            _calcClass.insert('8'); // 32 + 16
            _calcClass.insert('=');
            Assert.AreEqual(121, _calcClass.calcValue);
        }

        [Test]
        public void XorOperationTest()
        {
            _calcClass.insert('4');
            _calcClass.insert('9'); // 32 + 16 + 1
            _calcClass.insert('^');
            _calcClass.insert('4');
            _calcClass.insert('5'); // 32 + 8 + 4 + 1
            _calcClass.insert('=');
            Assert.AreEqual(28, _calcClass.calcValue);
        }

        [Test]
        public void NotOperationTest()
        {
            _calcClass.insert('8');
            _calcClass.insert('1'); // 64 + 16 + 1
            _calcClass.insert('!');
            Assert.AreEqual(long.MaxValue-81, _calcClass.calcValue);
        }
    }
}