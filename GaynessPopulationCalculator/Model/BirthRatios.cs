using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaynessPopulationCalculator.Model
{
    public class BirthRatios
    {
        public decimal Straight { get; set; }
        public decimal Lesbian { get; set; }
        public BirthRatios(decimal straight, decimal lesbian) {
            this.Straight = straight;
            this.Lesbian = lesbian;
        }
    }
}
