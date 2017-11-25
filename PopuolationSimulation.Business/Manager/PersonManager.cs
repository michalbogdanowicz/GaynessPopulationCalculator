using PopulationSimulation.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationSimulation.Business.Manager
{
    class PersonManager
    {

        public List<Person> GenerateFirstPeople (int number, decimal maleToFemalRatio) {
            List<Person> newListOfPeople = new List<Person>();

            // use some logic to generate some first people by using the paramentsr
            // this might be moved to another class caleed People genrator if this gets too big.
            throw new NotImplementedException();

            return newListOfPeople;
        }
    }
}
