using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaynessPopulationCalculator.Helpers
{
  public class PercentageProvider
    {
        public Random random;
        public int trasformed;

        public PercentageProvider(decimal percentage) {
            random = new Random();
            if ( percentage < 0.1m) { throw new ArgumentException("percentage cannot be less than 0.1"); }
            if (percentage > 100) { throw new ArgumentException("Cannot have more than 100 Percentage"); }
            trasformed = (int)(percentage * 10);
        }

        public bool DoesHit() {

            if (random.Next(1000) < trasformed) {
                return true;
            }
            return false;
        }

    }
}
