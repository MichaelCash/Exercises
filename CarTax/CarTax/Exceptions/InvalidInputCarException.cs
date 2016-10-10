using CarTax.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTax.Exceptions
{
    public class InvalidInputCarException : TaxCalculatorException
    {
        public InvalidInputCarException()
        : base(ErrorMessage.InvalidInput)
        {
        }
    }
}
