using System;
using System.Collections.Generic;
using DxHumanModel.Classifiers;
using DxHumanModel.Transporters;

namespace DxHumanModel.Inputs
{
    public class Meal : Ship<int>
    {
        private readonly ProbabilityMap H2O = new ProbabilityMap((double)0.2, (double)0.5);
        private readonly ProbabilityMap Na = new ProbabilityMap((double)0.005, (double)0.005);
        private readonly ProbabilityMap K = new ProbabilityMap((double)0.002, (double)0.001);
        private readonly ProbabilityMap Other = new ProbabilityMap((double)0.2, (double)0.4);

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
        private Volume<int> SetVolume()
        {
            Coordinate<int> resolution = new Coordinate<int>(1, 1, 1);
            Random rand = new Random();
            Coordinate<int> size = new Coordinate<int>(rand.Next(5, 10), rand.Next(5, 10), rand.Next(5, 10));

            Volume<int> containerVolume = new Volume<int>(resolution, size);
            Console.Write("Size x: {0}", containerVolume.Size.X);

            return containerVolume;
        }
        public Dictionary<string, double> Fill()
        {
            return this.StandardFill(this.moleculeLikelihood);
        }
        public Volume<int> GetVolume()
        {
            return this._shipSize;
        }
    }
}
