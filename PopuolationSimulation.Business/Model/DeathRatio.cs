


using System;
using System.Collections.Generic;

namespace PopulationSimulation.Business.Model
{

    public class DeathRatio
    {
        bool IsFixedPerYear;
        decimal AbsoluteRatio;
        Dictionary<int, decimal> DeathRatioPerYear { get; set; }
        int YearWhenDeathRatioStabilizesForEver;
        public decimal GetDearthRatio()
        {
            if (IsFixedPerYear)
            {
                return GetDearthRatio(0);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        public decimal GetDearthRatio(int passedYearSinceContraction)
        {
            if (IsFixedPerYear)
            {
                return AbsoluteRatio;
            }
            else
            {
                int yearToUse;
                if (passedYearSinceContraction > YearWhenDeathRatioStabilizesForEver)
                {
                    yearToUse = passedYearSinceContraction;
                }
                else
                {
                    yearToUse = YearWhenDeathRatioStabilizesForEver;
                }

                if (DeathRatioPerYear.ContainsKey(passedYearSinceContraction))
                {
                    return DeathRatioPerYear[passedYearSinceContraction];
                }
                else
                {
                    throw new InvalidOperationException("Year Of Death not set.");
                }
            }
        }

        public DeathRatio(decimal absoluteratio)
        {
            AbsoluteRatio = absoluteratio;
            IsFixedPerYear = true;
        }

        public DeathRatio(Dictionary<int, decimal> DeathPerYear, int YearWhenDeathRatioStabilizesForEver)
        {

            IsFixedPerYear = false;
            DeathRatioPerYear = DeathPerYear;
            this.YearWhenDeathRatioStabilizesForEver = YearWhenDeathRatioStabilizesForEver;
        }
    }


}