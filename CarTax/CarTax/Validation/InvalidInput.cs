using CarTax.Dto;
namespace CarTax.Validation
{
    public class InvalidInput: ValidationInput
    {
        public override double GetUserPrice(CarInput car)
        {
            throw new Exceptions.InvalidInputCarException();
        }
    }
}
