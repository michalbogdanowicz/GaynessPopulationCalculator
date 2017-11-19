using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationSimulation.Business.Model
{
    /// <summary>
    /// Contains the ratios of male/femalre birth for Straight and Lesbian.
    /// </summary>
    public class BirthRatios
    {
        public decimal Straight { get; set; }
        public decimal Lesbian { get; set; }

     
            
        public BirthRatios(decimal straight, decimal lesbian)
        {
            this.Straight = straight;
            this.Lesbian = lesbian;
   
        }
    }
}
