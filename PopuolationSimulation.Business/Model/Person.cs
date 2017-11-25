using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationSimulation.Business.Model
{
    public enum Sex {
        Male,
        Female
    }

    class Person
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public DateTime BirtDate { get; set; }
        public Sex Sex { get; set; }

        public HealthStatus HealthStatus { get;set;}


    }
}
