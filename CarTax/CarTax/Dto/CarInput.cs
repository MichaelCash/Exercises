namespace CarTax.Dto
{
    public class CarInput
    {
        public Area CarFrom { get; set; }
        public double Capacity { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
    }

    public enum Area {
        Other = 0,
        Euro = 1,
        Usa = 2,
        Japan = 3
    }
}
