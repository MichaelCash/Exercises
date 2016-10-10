using CarTax.Validation;

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
