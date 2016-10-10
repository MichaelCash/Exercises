using CarTax.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTax.Exceptions
{
    public class AreaNotSupportException: TaxCalculatorException
    {
        public AreaNotSupportException()
        : base(ErrorMessage.UnsupportArea)
        {
        }
    }
}
