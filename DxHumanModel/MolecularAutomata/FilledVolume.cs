using System;
using System.Collections.Generic;
using DxHumanModel.Core;
using DxHumanModel.Topology;

namespace DxHumanModel.MolecularAutomata
{
    
    public class FilledVolume : Volume
    {
        // Mls
        public decimal ScalarVolume { get; set; }
        // cm cubed volume
        
        public Dictionary<string, MolecularConcentration> Constituents { get; set; }
        
        public FilledVolume(decimal scalarVolume)
        {
            this.ScalarVolume = scalarVolume;
            this.setVolume(scalarVolume);
            this.Constituents = new Dictionary<string, MolecularConcentration>();
        }
        private void setVolume(decimal scalarValue)
        {
            this.Resolution = new Coordinate(1m, 1m, 1m);
            this.Size = new Coordinate(1m, 1m, 1m);
            this.Expand(scalarValue);
        }
        public void Fill(decimal scalarVolume, Dictionary<string, MolecularConcentration> Constituents)
        {
            // should create a generic volume
            this.ScalarVolume = scalarVolume;
            this.Constituents = Constituents;
            this.setVolume(scalarVolume);
        }
        public void AddConstituent(MolecularConcentration input)
        {
            // Should Update Total Density, average charge?
            this.Constituents.Add(input.Molecule.Id, input);
        }
        public void Mix(FilledVolume substance)
        {
            // Upon Mixing further changes could occur.
            // Probably should leave this to some other process.
            // We'll just deal with concentrations.
            this.ScalarVolume += substance.ScalarVolume;
            this.Expand(substance.ScalarVolume);
            foreach (KeyValuePair<string, MolecularConcentration> concentration in substance.Constituents)
            {
                if (this.Constituents.ContainsKey(concentration.Key))
                {
                    this.Constituents[concentration.Key].Extend(concentration.Value.Mmols);
                } else
                {
                    this.AddConstituent(concentration.Value);
                }
            }
        }
        // Reduce should really be sieving co-efficents + OutFlow Rate
        // If outFLow > Current Volume
        public void Reduce(FilledVolume substance)
        {
            this.ScalarVolume = this.ScalarVolume > substance.ScalarVolume ? this.ScalarVolume - substance.ScalarVolume : 0m;
            if (this.ScalarVolume > 0m)
            {
                this.Contract(substance.ScalarVolume);
                
                foreach (KeyValuePair<string, MolecularConcentration> concentration in substance.Constituents)
                {
                    if (this.Constituents.ContainsKey(concentration.Key))
                    {
                        this.Constituents[concentration.Key].Extend(-concentration.Value.Mmols);
                    }
                    else
                    {
                        throw new Exception("Cannot remove a molecule that does not exist in a volume. Should be checked!");
                    }
                }
            } else
            {
                Constituents.Clear();
            }
        }
       public FilledVolume Diffuse(decimal FlowRateMlsPerStep, Dictionary<string, MolecularDiffusion> SievingCoefficients)
       {
            FilledVolume output = new FilledVolume(FlowRateMlsPerStep);
            if( FlowRateMlsPerStep >= this.ScalarVolume )
            {
                output.ScalarVolume = FlowRateMlsPerStep;
                output.Constituents = this.Constituents;
                this.ScalarVolume = 0m;
                this.Constituents.Clear();
                return output;
            }
            foreach(KeyValuePair<string, MolecularDiffusion> sc in SievingCoefficients)
            {
                //flowRate / totalVolume * SievingCoefficient * mmol
                if ( this.Constituents.ContainsKey( sc.Key ))
                {
                    output.AddConstituent(sc.Value.DiffuseBySieving(FlowRateMlsPerStep, this.ScalarVolume));
                }
            }
            this.Reduce(output);
            return output;
       }
    }
}
