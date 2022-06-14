using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment
{
    internal class Company
    {
        private List<Car> cars = new List<Car>();
        private List<Journey> journeys = new List<Journey>();

        public Company() { }

        public List<Car> Cars { get { return cars; } }
        public List<Journey> Journeys { get { return journeys;} }

        public void addJourney(Journey journey)
        {
            journeys.Add(journey);
        }
        public void addCar(Car car)
        {
            cars.Add(car);
        }
        public double totalIncome()
        {
            double totalIncome = 0;
            foreach(Journey journey in journeys)
            {
                totalIncome += journey.getPrice();
            }
            return totalIncome;
        }
        public double averageDistance()
        {
            double averageDistance = 0;
            foreach(Journey journey in journeys)
            {
                averageDistance += journey.distance;
            }
            averageDistance /= journeys.Count();
            return averageDistance;
        }
        public double longestPeriod()
        {
            double longestPeriod = 0;      
            {
                foreach(Journey journey in journeys)
                {
                    if(journey.car.getPeriod() > longestPeriod)
                    {
                        longestPeriod = journey.car.getPeriod();
                    }
                }
            }
            return longestPeriod;
        }
    }
}
