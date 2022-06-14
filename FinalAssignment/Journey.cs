using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment
{
    internal class Journey
    {
        public string name { get; set; }   

        public Car car { get; set; }

        public int distance { get; set; }

        public int days { get; set; }
     
        public Journey(string name,Car car, int distance, int days)
        {
            this.car = car;
            this.name = name;
            this.distance = distance;
            this.days = days;
        }

        public double getPrice()
        {
            double total = 0;
            int day = 0;

            if (distance / 100 != 1)
            {
                total = 0.18 * distance;
            }
            total += total + days * 50;

            return total;
        }

        public double getPeriod()
        {
            return car.getPeriod();
        }

    }
}
