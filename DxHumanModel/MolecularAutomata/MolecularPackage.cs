using DxHumanModel.Core;

namespace DxHumanModel.MolecularAutomata
{
    public class MolecularState : IAutomataState
    {
        public FilledVolume volume { get; set; }
    }
    public class MolecularPackage : IPackage<MolecularState>
    {
        public int messageIcrement { get; }
        public MolecularState state { get; set; }

        public MolecularPackage(int step)
        {
            this.messageIcrement = step;
        }

    }
}
