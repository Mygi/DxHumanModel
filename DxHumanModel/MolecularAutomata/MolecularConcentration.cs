using System;
namespace DxHumanModel.MolecularAutomata
{
    public class MolecularConcentration
    {
        public IMolecule Molecule { get; set; }
        public decimal Mmols { get; set; }
        public decimal Mass { get; set; }


        public void Mix(Concentration inputConcentration)
        {
            decimal newMmol = inputConcentration.inputMmolPerMl * inputConcentration.inputScalarVolumeInMls;
            this.Mmols += newMmol;
            this.UpdateMass();
        }
        public void Extend(decimal inputMmol)
        {
            this.Mmols += inputMmol;
            this.UpdateMass();
        }
        public void Reduce(decimal inputMmol)
        {
            this.Mmols -= inputMmol;
            this.UpdateMass();
        }
        // Sieving Rate is percentage of total per Ml
        protected void UpdateMass()
        {
            this.Mass = this.Mmols / this.Molecule.AtomicMass;
        }
    }
}
