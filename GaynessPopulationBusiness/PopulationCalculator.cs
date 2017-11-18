
using GaynessPopulationBusiness.Helpers;
using GaynessPopulationBusiness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaynessPopulationBusiness
{

    public class PopulationCalculator
    {
        public bool WritePopDetails;
        public GenderPopulation Male { get; set; }
        public GenderPopulation Female { get; set; }
        Random Random;
        BirthRatios BirthRatios;
        PercentageProvider MaleHomoercentageProvider;
        PercentageProvider LesbianPercentageProvider;
        /// <summary>
        /// Provider for women born from straight couples.
        /// </summary>
        PercentageProvider FemalePercentageProvider;
        decimal manToWomanRatio = 0.519m;
        HomosexualityRatios HomosexualityRatios;


        public PopulationCalculator(decimal totalPopulation, bool writePopDetails, BirthRatios ratios, HomosexualityRatios homosexualityRatios)
        {
            if (totalPopulation <= 0) { throw new ArgumentException("total population cannot be less than 0"); }
            HomosexualityRatios = homosexualityRatios;
            Male = new GenderPopulation(Math.Round(totalPopulation * manToWomanRatio), homosexualityRatios.ManHomosexualityRatio);
            Female = new GenderPopulation(totalPopulation - Male.GetPop(), homosexualityRatios.FemaleHomoSexualityRatio);
            WritePopDetails = writePopDetails;
            Random = new Random();
            BirthRatios = ratios;
            MaleHomoercentageProvider = new PercentageProvider(homosexualityRatios.ManHomosexualityRatio);
            LesbianPercentageProvider = new PercentageProvider(homosexualityRatios.LesboToLesboRatio);
            FemalePercentageProvider = new PercentageProvider(homosexualityRatios.FemaleHomoSexualityRatio);
        }

        public void CalculateNextGeneration()
        {
            decimal straightBirths = CalculateBirthByStraight(BirthRatios.Straight);
            decimal lesboBirths = CalculateBirthByLesbo(BirthRatios.Lesbian);
            // PURGE CURRENT POP
            Male.Purge();
            Female.Purge();
            // Calculate new population
            // 51.9 is male. https://en.wikipedia.org/wiki/Human_sex_ratio
            decimal newMale = Math.Round(straightBirths * manToWomanRatio);
            decimal newFemale = straightBirths - newMale;

            DistributeMale(newMale);
            DistributeFemale(newFemale);


            if (WritePopDetails) { Console.WriteLine("From straight Gay Male  : {0} , Straight Male : {1}", Male.Gay, Male.Straight); }
            if (WritePopDetails) { Console.WriteLine("From straight Gay Female  : {0} , Straight Female : {1}", Female.Gay, Female.Straight); }
            decimal savedStateOfFemaleGay = Female.Gay;
            decimal savedStateOfFemaleStraight = Female.Straight;
            // lesbo 50% lesbo rate, lesbo has 100% of female offspring.
            DistributeLesbiansBirths(lesboBirths);

            if (WritePopDetails) { Console.WriteLine("From lesbo Gay Female  : {0} , Straight Female : {1}", Female.Gay - savedStateOfFemaleGay, Female.Straight - savedStateOfFemaleStraight); }
        }

        private void DistributeLesbiansBirths(decimal lesboBirths)
        {
            if (lesboBirths > 100)
            {
                decimal newGay = Math.Round(lesboBirths / 2);
                decimal newStraight = lesboBirths - newGay;
                Female.Gay += newGay;
                Female.Straight += newStraight;
            }
            else
            {
                // give them a chance...
                for (int i = 0; i < lesboBirths; i++)
                {
                    if (LesbianPercentageProvider.DoesHit())
                    {
                        Female.Gay++;
                    }
                    else
                    {
                        Female.Straight++;
                    }
                }
            }
        }

        private void DistributeFemale(decimal newFemale)
        {
            if (newFemale > 100)
            {
                decimal newGay = Math.Round(newFemale * 0.1m);
                decimal newStraight = newFemale - newGay;
                Female.Gay += newGay;
                Female.Straight += newStraight;
            }
            else
            {
                for (int i = 0; i < newFemale; i++)
                {
                    if (FemalePercentageProvider.DoesHit())
                    {
                        Female.Gay++;
                    }
                    else
                    {
                        Female.Straight++;
                    }
                }
            }
        }

        private void DistributeMale(decimal newMale)
        {
            if (newMale > 100)
            {
                decimal newGay = Math.Round(newMale * 0.1m);
                decimal newStraight = newMale - newGay;
                Male.Gay += newGay;
                Male.Straight += newStraight;
            }
            else
            {
                for (int i = 0; i < newMale; i++)
                {
                    if (MaleHomoercentageProvider.DoesHit())
                    {
                        Male.Gay++;
                    }
                    else
                    {
                        Male.Straight++;
                    }
                }
            }
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
                decimal families = Math.Round(Female.Gay / 2);
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
