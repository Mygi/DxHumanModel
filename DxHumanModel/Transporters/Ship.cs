using System;
using System.Collections.Generic;
using DxHumanModel.Topology;

namespace DxHumanModel.Transporters
{
    public class ProbabilityMap
    {
        public ProbabilityMap(decimal sigma, decimal mean)
        {
            this.sigma = sigma;
            this.meanLikelihoodPercentage = mean;
        }
        public decimal meanLikelihoodPercentage { get; set; }
        public decimal sigma { get; set; }
    }
    public class Ship
    {
        protected Volume _shipSize;
        private decimal _range;


        public Ship()
        {
        }
        public void SetSize(Volume size)
        {
            this._shipSize = size;
            this._range = size.GetVoxelSize();
        }
        protected Dictionary<string, decimal> StandardFill(Dictionary<string, ProbabilityMap> moleculeLikelihood)
        {
            Dictionary<string, decimal> concentration = new Dictionary<string, decimal>();
            dynamic remainingSpace = this._range;
            foreach(KeyValuePair<string, ProbabilityMap> item in moleculeLikelihood)
            {
                // We need to reduce the total pixels claimed by concentration
                // So a bit hacky but it gets the job done
                if( remainingSpace <= 0)
                {
                    break;
                }
                var container = this.BoxMullerTransform(item.Value.sigma, item.Value.meanLikelihoodPercentage) * remainingSpace;
                concentration.Add(item.Key, container );
                remainingSpace = remainingSpace - container; 
            }

            return concentration;
        }
        //Move elsewhere
        public decimal BoxMullerTransform(decimal sigma, decimal mean)
        {
            Random rand = new Random(); //reuse this if you are generating many
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            decimal randNormal =
                         mean + sigma * Convert.ToDecimal(randStdNormal); //random normal(mean,stdDev^2)
            return randNormal;
        }

    }
}
