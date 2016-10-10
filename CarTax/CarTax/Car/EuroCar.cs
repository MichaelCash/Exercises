namespace CarTax.Car
{
    public class EuroCar : BaseCar
    {
        public EuroCar() 
        {
            ImportTaxRates = new double[] {1, 1.2, 2 };
        }
    }
}
