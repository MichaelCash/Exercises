namespace CarTax.Car
{
    public class JapanCar : BaseCar
    {
        public JapanCar() 
        {
            ImportTaxRates = new double[] {0.7, 0.8, 1.35 };
        }
    }
}
