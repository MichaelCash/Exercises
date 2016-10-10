using System.Collections.Generic;
using CarTax.Car;
using CarTax.Dto;
namespace CarTax.Validation
{
    public class ValidInput : ValidationInput
    {
        private static List<BaseCar> carImports = new List<BaseCar>() { new OtherCar(), new EuroCar(), new UsaCar(), new JapanCar() };

        public override double GetUserPrice(CarInput input)
        {
            return carImports[(int)input.CarFrom].GetUserPrice(input.Capacity, input.Price);
        }
    }
}
