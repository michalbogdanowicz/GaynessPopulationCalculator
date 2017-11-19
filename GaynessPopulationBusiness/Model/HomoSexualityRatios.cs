using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopuolationSimulation.Business.Model
{
  public  class HomosexualityRatios
    {
        public decimal ManHomosexualityRatio { get; set; }
        public decimal FemaleHomoSexualityRatio { get; set; }
        public decimal LesboToLesboRatio { get; set; }

            public HomosexualityRatios(decimal manHomosexualityRatio, decimal femaleHomoSexualityRatio, decimal lesboToLesboRatio) {
            this.ManHomosexualityRatio = manHomosexualityRatio;
            this.FemaleHomoSexualityRatio = femaleHomoSexualityRatio;
            this.LesboToLesboRatio = lesboToLesboRatio;
        }
             
    }
}
