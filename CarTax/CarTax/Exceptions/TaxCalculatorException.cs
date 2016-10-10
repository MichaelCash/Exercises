using System;

namespace CarTax.Exceptions
{
    public class TaxCalculatorException: Exception
    {
        public TaxCalculatorException(string message)
        : base(message)
        {
        }
    }
}
