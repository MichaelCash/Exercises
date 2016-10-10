namespace CarTax.Car
{
    public class OtherCar : BaseCar
    {
        public override double GetUserPrice(double capacity, double price)
        {
            throw new Exceptions.AreaNotSupportException();
        }
    }
}
