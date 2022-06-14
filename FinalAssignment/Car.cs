using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment
{
    internal class Car
    {
       public string carName { get; set; }
       public DateTime startDate { get; set; }
       
       public DateTime endDate { get; set; }

       public string day { get; set; }

       public Car(string carName, DateTime startDate, DateTime endDate, string day)
        {
            this.carName = carName;
            this.startDate = startDate;
            this.endDate = endDate;
            this.day = day;
        }

        public double getPeriod()
        {
            double days = endDate.Subtract(startDate).Days;
            return days;
        }
    }
}
