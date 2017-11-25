using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationSimulation.Business.Model
{
    class World
    {
        /// <summary>
        /// As in UTC
        /// </summary>
        public DateTime Time { get; set; }   
        public DateTime WorldCreationTime { get; set; }
        public DateTime InitializationTime { get; set; }

        public List<Person> People { get; set; }

        public World(List<Person> startingPopulation, DateTime startingWorldTime) {

            InitializationTime = DateTime.Now;
        }

    }
}
