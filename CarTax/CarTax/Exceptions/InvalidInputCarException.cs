using CarTax.Validation;

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
