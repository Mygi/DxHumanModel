using System;
using System.Collections.Generic;
using DxHumanModel.Classifiers;

namespace DxHumanModel.Transporters
{
    public class ProbabilityMap
    {
        public ProbabilityMap(double sigma, double mean)
        {
            this.sigma = sigma;
            this.meanLikelihoodPercentage = mean;
        }
        public double meanLikelihoodPercentage { get; set; }
        public double sigma { get; set; }
    }
    public class Ship<T>
    {
        protected Volume<T> _shipSize;
        private T _range;


        public Ship()
        {
        }
        public void SetSize(Volume<T> size)
        {
            this._shipSize = size;
            this._range = size.GetVoxelSize();
        }
        protected Dictionary<string, double> StandardFill(Dictionary<string, ProbabilityMap> moleculeLikelihood)
        {
            Dictionary<string, double> concentration = new Dictionary<string, double>();
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
        public double BoxMullerTransform(double sigma, double mean)
        {
            Random rand = new Random(); //reuse this if you are generating many
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + sigma * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }

    }
}
