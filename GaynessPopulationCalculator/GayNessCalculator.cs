using GaynessPopulationCalculator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaynessPopulationCalculator
{

    class GayNessCalculator
    {
        public bool WritePopDetails;
        public GenderPopulation Male { get; set; }
        public GenderPopulation Female { get; set; }
        Random Random;
        BirthRatios Ratios;


        public GayNessCalculator(decimal totalPopulation, bool writePopDetails, BirthRatios ratios)
        {
            if (totalPopulation <= 0) { throw new ArgumentException("total population cannot be less than 0"); }
            Male = new GenderPopulation(totalPopulation / 2);
            Female = new GenderPopulation(totalPopulation / 2);
            WritePopDetails = writePopDetails;
            Random = new Random();
            Ratios = ratios;
        }

        public void CalculateNextGeneration()
        {
            //  Every famiily has a mean birht radio of 2.36 https://en.wikipedia.org/wiki/Total_fertility_rate

            decimal straightBirths = CalculateBirthByStraight(Ratios.Straight);
            decimal lesboBirths = CalculateBirthByLesbo(Ratios.Lesbian);
            // PURGE CURRENT POP
            Male.Purge();
            Female.Purge();
            // Calculate new population
            // 51.9 is male. https://en.wikipedia.org/wiki/Human_sex_ratio
            decimal newMale = (decimal)Math.Round(straightBirths * 0.519m);
            decimal newFemale = straightBirths - newMale;

            DistributeMale(newMale);
            DistributeFemale(newFemale);


            if (WritePopDetails) { Console.WriteLine("From straight Gay Male  : {0} , Straight Male : {1}", Male.Gay, Male.Straight); }
            if (WritePopDetails) { Console.WriteLine("From straight Gay Female  : {0} , Straight Female : {1}", Female.Gay, Female.Straight); }
            decimal saveStateOfFemaleGay = Female.Gay;
            decimal saveStateOfFemaleStraight = Female.Straight;
            // lesbo 50% lesbo rate, lesbo has 100% of female offspring.
            DistributeLesbiansBirths(lesboBirths);

            if (WritePopDetails) { Console.WriteLine("From lesbo Gay Female  : {0} , Straight Female : {1}", Female.Gay - saveStateOfFemaleGay, Female.Straight - saveStateOfFemaleStraight); }
        }

        private void DistributeLesbiansBirths(decimal lesboBirths)
        {
            for (int i = 0; i < lesboBirths; i++)
            {
                if (TrueOn50Percentage())
                {
                    Female.Gay++;
                }
                else
                {
                    Female.Straight++;
                }
            }
        }

        private void DistributeFemale(decimal newFemale)
        {
            for (int i = 0; i < newFemale; i++)
            {
                if (TrueOn10Percentage())
                {
                    Female.Gay++;
                }
                else
                {
                    Female.Straight++;
                }
            }
        }

        private void DistributeMale(decimal newMale)
        {
            for (int i = 0; i < newMale; i++)
            {
                if (TrueOn10Percentage())
                {
                    Male.Gay++;
                }
                else
                {
                    Male.Straight++;
                }
            }
        }

        private bool TrueOn10Percentage()
        {
            return Random.Next(0, 9) == 0;
        }

        private bool TrueOn50Percentage()
        {
            return Random.Next(0, 9) < 5;
        }
        /// <summary>
        /// using the same birht ration of straight https://en.wikipedia.org/wiki/Total_fertility_rate
        /// </summary>
        /// <returns></returns>
        private decimal CalculateBirthByLesbo(decimal birthRatio)
        {
            decimal birthnumber;
            if (Female.Gay == 0)
            {
                birthnumber = 0;
            }
            else
            {
                decimal families = Female.Gay / 2;
                birthnumber = (decimal)Math.Round((decimal)families * birthRatio);

            }
            if (WritePopDetails) { Console.WriteLine("The lesbo births are : {0}", birthnumber); }
            return birthnumber;
        }


        public decimal CalculateBirthByStraight(decimal birthRatio)
        {
            decimal birthnumber = 0;
            if (Male.Straight == 0 || Female.Straight == 0)
            {
                birthnumber = 0;
            }
            else
            {
                decimal families = Math.Min(Male.Straight, Female.Straight);
                birthnumber = (decimal)Math.Round((decimal)families * birthRatio);
            }
            if (WritePopDetails) { Console.WriteLine("The straight births are : {0}", birthnumber); }
            return birthnumber;
        }
    }
}
