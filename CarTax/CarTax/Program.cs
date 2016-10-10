using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarTax.Car;
using CarTax.Dto;
using CarTax.Validation;
namespace CarTax
{
    class Program
    {
        // NO IF - ELSE
        // NO WHERE
        // VALIDATION
        // UNIT TEST

        static void Main(string[] args)
        {
            var cal = new TaxCalculator();
            var cars = GetDummyData();
            foreach (var car in cars)
            {
                try
                {
                    Console.Write(string.Format("Car = {0}, From = {1}, Capacity = {2}, Price={3:#,###.##} USD => User Price = ", car.Name, ((Area)car.CarFrom).ToString(), car.Capacity, car.Price.ToString()));
                    Console.WriteLine(string.Format("{0:#,###.##} PHP", cal.GetUserPrice(car)));
                }
                catch (Exceptions.TaxCalculatorException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }


            Console.ReadLine();
        }
        public static List<CarInput> GetDummyData()
        {
            return new List<CarInput>() {
                new CarInput {Name= "Benz G65", CarFrom = Area.Euro, Capacity = 6.0, Price = 217900 },
                new CarInput {Name= "Honda Jazzy", CarFrom = Area.Japan, Capacity = 1.5, Price = 19490 },
                new CarInput {Name= "Jeep wrangler", CarFrom = Area.Usa, Capacity = 3.6, Price = 36995 },
                new CarInput {Name= "Chery QQ", CarFrom = Area.Other, Capacity = 1.0, Price = 6000 },
                new CarInput {Name= "BMW X5", CarFrom = Area.Euro, Capacity = 4.0, Price = 0 }
            };
        }
    }
}
