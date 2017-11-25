using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PopulationSimulation.Business.Model
{


    internal class Disease
    {
        /// <summary>
        ///  this Id might make no sense.. without a database
        /// </summary>
        public int Id { get; set; }

        public String ScientificDecommination { get; set;}
        public String CommonName { get; set; }

       
        public DeathRatio DeathRatio { get; set; }

    }
}