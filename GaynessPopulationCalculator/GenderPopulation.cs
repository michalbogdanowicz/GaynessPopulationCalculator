﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaynessPopulationCalculator
{
    class GenderPopulation
    {
        public decimal Gay { get; set; }
        public decimal Straight { get; set; }

        public GenderPopulation(decimal pop)
        {
            Gay = pop / 10;
            Straight = pop - Gay;
        }
        /// <summary>
        /// total pupulation Gay + straight
        /// </summary>
        /// <returns></returns>
        public decimal GetPop() {
            return Gay + Straight;
        }
        /// <summary>
        /// For the emperor!
        /// </summary>
        public void Purge() {
            Gay = 0;
            Straight = 0;
        }
    }
}
