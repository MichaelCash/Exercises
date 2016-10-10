using System;

namespace CarTax.Car
{
    public abstract class BaseCar
    {
        private const double VATRate = 0.12;
        private const int MaximumCapacity = 6;
        private const double ExchangeRate = 47;

        protected double[] ImportTaxRates { get; set; }

        private double ImportTaxRate(double capacity)
        {
            return ImportTaxRates[ Math.Min((int)Math.Ceiling(capacity), MaximumCapacity) /3];
        }

        public virtual double GetUserPrice(double capacity, double price)
        {
            return (price * (1 + ImportTaxRate(capacity)) * (1 + VATRate)) * ExchangeRate;
        }
    }
}
