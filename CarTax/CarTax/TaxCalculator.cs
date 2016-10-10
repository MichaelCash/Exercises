using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarTax.Validation;
using CarTax.Dto;
namespace CarTax
{
    public class TaxCalculator
    {
        private List<ValidationInput> validations = new List<ValidationInput>() { new InvalidInput(), new ValidInput() };
        public double GetUserPrice(CarInput car)
        {
            int index = (int)Math.Min(Math.Max(Math.Ceiling(Math.Min(car.Capacity, car.Price)), 0), 1);
            return validations[index].GetUserPrice(car);
        }
    }
}
