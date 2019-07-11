using System;
namespace DxHumanModel.MolecularAutomata
{
    public class MolecularDiffusion
    {
        public IMolecule Molecule { get; set; }
        public decimal SievingCoefficient { get; set; }
        public decimal Temperature { get; set; }

        public MolecularConcentration DiffuseBySieving(decimal flowRateOutInMls, decimal totalVolumeInMls)
        {
            var output = new MolecularConcentration()
            {
                Molecule = this.Molecule
            };
            output.Extend((flowRateOutInMls / totalVolumeInMls) * this.SievingCoefficient);

            return output;
        }
    }
}
