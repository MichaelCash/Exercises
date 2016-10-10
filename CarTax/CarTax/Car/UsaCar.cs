namespace CarTax.Car
{
    public class UsaCar : BaseCar
    {
        public UsaCar() 
        {
            ImportTaxRates = new double[] {0.75, 0.9, 1.5 };
        }
    }
}
