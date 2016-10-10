using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarTax.Dto;

namespace CarTax.UnitTest
{
    [TestClass]
    public class CalculationTaxTest
    {
        private double delta = 0.00001;
        [TestMethod]
        public void EuroCarTest()
        {
            var cal = new TaxCalculator();
            var euroCar = new CarInput { Name = "Benz G65", CarFrom = Area.Euro, Capacity = 6.0, Price = 217900 };
            double expectedPrice = 34410768;
            Assert.AreEqual(expectedPrice, cal.GetUserPrice(euroCar), delta);
        }
        [TestMethod]
        public void JapanCarTest()
        {
            var cal = new TaxCalculator();
            var car = new CarInput { Name = "Honda Jazzy", CarFrom = Area.Japan, Capacity = 1.5, Price = 19490 };
            double expectedPrice = 1744121.12;
            Assert.AreEqual(expectedPrice, cal.GetUserPrice(car), delta);
        }
        [TestMethod]
        public void UsaCarTest()
        {
            var cal = new TaxCalculator();
            var car = new CarInput { Name = "Jeep wrangler", CarFrom = Area.Usa, Capacity = 3.6, Price = 36995 };
            double expectedPrice = 3700091.92;
            Assert.AreEqual(expectedPrice, cal.GetUserPrice(car), delta);
        }

        public void EuroCar_Capacity_Greater_Than_6_Test()
        {
            var cal = new TaxCalculator();
            var euroCar = new CarInput { Name = "Benz G65", CarFrom = Area.Euro, Capacity = 16.0, Price = 217900 };
            double expectedPrice = 34410768;
            Assert.AreEqual(expectedPrice, cal.GetUserPrice(euroCar), delta);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.AreaNotSupportException))]
        public void AreaNotSupportTest()
        {
            var cal = new TaxCalculator();
            var car = new CarInput { Name = "Chery QQ", CarFrom = Area.Other, Capacity = 1.0, Price = 6000 };
            cal.GetUserPrice(car);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InvalidInputCarException))]
        public void InvalidInput_Capacity_Less_Then_Zero_Test()
        {
            var cal = new TaxCalculator();
            var car = new CarInput { Name = "Jeep wrangler", CarFrom = Area.Usa, Capacity = -2, Price = 36995 };
            cal.GetUserPrice(car);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InvalidInputCarException))]
        public void InvalidInput_Capacity_Equals_Zero_Test()
        {
            var cal = new TaxCalculator();
            var car = new CarInput { Name = "Jeep wrangler", CarFrom = Area.Usa, Capacity = 0, Price = 36995 };
            cal.GetUserPrice(car);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InvalidInputCarException))]
        public void InvalidInput_Price_Less_Then_Zero_Test()
        {
            var cal = new TaxCalculator();
            var car = new CarInput { Name = "Jeep wrangler", CarFrom = Area.Usa, Capacity = 6, Price = -2 };
            cal.GetUserPrice(car);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InvalidInputCarException))]
        public void InvalidInput_Capacity_Price_Less_Then_Zero_Test()
        {
            var cal = new TaxCalculator();
            var car = new CarInput { Name = "Jeep wrangler", CarFrom = Area.Usa, Capacity = -1, Price = -2 };
            cal.GetUserPrice(car);
        }
    }
}
