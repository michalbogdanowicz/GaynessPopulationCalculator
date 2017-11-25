using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationSimulation.Business.Model
{
    class SocietyUnit
    {
        /// <summary>
        /// For example in standard christian family they would be 2 people, In muslim culture 1 man, 4 women and so on.
        /// </summary>
        public List<Person> Founders;
        public List<Person> OffSprings;
        public bool HasAHome;
        /// <summary>
        /// Housing? Might make senes...
        /// </summary>
        public bool HouseBigEnough;
        /// <summary>
        /// Just in case money was to be considered;
        /// </summary>
        public decimal MoneyBalance;
    }
}
