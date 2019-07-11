using System;
using System.Collections.Generic;
using DxHumanModel.Core;
// We need a molecularVolume - which is just a set of sub-volumes
// volume.output(rates, timestep) => subVolume
// volume.input(subVolume) -> void
// Are we sending out mmol ? mls or gms?
namespace DxHumanModel.MolecularAutomata
{
    public class MolecularAutomata : Automata<MolecularState>
    {
        protected FilledVolume volume;
        protected Dictionary<string, MolecularDiffusion> MassTransferRates { get; set; }
        public MolecularAutomata()
        {

        }
        
        protected override MolecularState InternalStateUpdate(int timeStep)
        {
            return this.currentState;
        }

        public override void Receive(IPackage<MolecularState> input, int step)
        {
            this.volume.Mix(input.state.volume);
        }

        public override IPackage<MolecularState> Send(int step, decimal rate)
        {
            return new MolecularPackage(step)
            {
                 state = new MolecularState()
                 {
                     volume = this.volume.Diffuse(rate, this.MassTransferRates)
                 }
            };
        }
    }
}
