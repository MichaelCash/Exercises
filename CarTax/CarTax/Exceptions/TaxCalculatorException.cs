using CarTax.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
