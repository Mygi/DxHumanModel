using System;
using System.Collections.Generic;
using DxHumanModel.Topology;
using DxHumanModel.Transporters;

namespace DxHumanModel.Inputs
{
    public class Meal : Ship
    {
        private readonly ProbabilityMap H2O = new ProbabilityMap((decimal)0.2, (decimal)0.5);
        private readonly ProbabilityMap Na = new ProbabilityMap((decimal)0.005, (decimal)0.005);
        private readonly ProbabilityMap K = new ProbabilityMap((decimal)0.002, (decimal)0.001);
        private readonly ProbabilityMap Other = new ProbabilityMap((decimal)0.2, (decimal)0.4);

        private Dictionary<string, ProbabilityMap> moleculeLikelihood;

        private const int maxDimension = 10;
        private const int minDimension = 5;
        public Meal() : base()
        {
            Console.Write("Meal started");
            this.SetSize(this.SetVolume());
            Console.Write("Meal size set");
            this.moleculeLikelihood = new Dictionary<string, ProbabilityMap>();
            this.moleculeLikelihood.Add("H2O", this.H2O);
            this.moleculeLikelihood.Add("Na", this.Na);
            this.moleculeLikelihood.Add("K", this.K);
            this.moleculeLikelihood.Add("Other", this.Other);
            Console.Write("Molecule likelihood added");
        }
        public Dictionary<string, ProbabilityMap> getConcentrations()
        {
            return this.moleculeLikelihood;
        }
        private Volume SetVolume()
        {
            Coordinate resolution = new Coordinate(1m, 1m, 1m);
            Random rand = new Random();
            Coordinate size = new Coordinate(Convert.ToDecimal(rand.Next(5, 10)), Convert.ToDecimal(rand.Next(5, 10)), Convert.ToDecimal(rand.Next(5, 10)));

            Volume containerVolume = new Volume(resolution, size);
            Console.Write("Size x: {0}", containerVolume.Size.X);

            return containerVolume;
        }
        public Dictionary<string, decimal> Fill()
        {
            return this.StandardFill(this.moleculeLikelihood);
        }
        public Volume GetVolume()
        {
            return this._shipSize;
        }
    }
}
