using System;
using System.Collections.Generic;
using DxHumanModel.Topology;
using DxHumanModel.Core;

namespace DxHumanModel.Sumps
{
    public class InterstitialMassTransportState : IAutomataState
    {
        public Volume volume {get; set; }
        public Dictionary<string, double> Concentrations {get; set; }

    }
    public class InterstitialPackage : IPackage<InterstitialMassTransportState>
    {
        public int messageIcrement { get;  }

        public InterstitialMassTransportState state { get; set; }
    }
    public delegate decimal TransportEventHandler(decimal inputConcentration);
    public class Interstitium : Automata<InterstitialMassTransportState>
    {

        public Volume volume { get; set; }

        private Dictionary<string, double> _TransferRates;
        //  these are total concentration
        private Dictionary<string, double> _currentState;
        private Dictionary<string, TransportEventHandler> _sendHandlers;
        public Interstitium()
        {
            this.Id = Guid.NewGuid();
        }
        public override void Receive(IPackage<InterstitialMassTransportState> input, int step)
        {
            this.volume.Expand(input.state.volume.GetVoxelSize());
            foreach (KeyValuePair<string, double> inputStates in input.state.Concentrations)
            {
                if (this._currentState.ContainsKey(inputStates.Key))
                {
                    this._currentState[inputStates.Key] += inputStates.Value;
                }
                else
                {
                    this._currentState.Add(inputStates.Key, inputStates.Value);
                }
            }
            // Should Register Delegates
        }
        //eventType
        public override IPackage<InterstitialMassTransportState> Send(int step, decimal rate)
        {
            InterstitialPackage output = new InterstitialPackage()
            {
                state = new InterstitialMassTransportState()
                {
                    Concentrations = new Dictionary<string, double>()
                }
                
            };

            foreach (KeyValuePair<string, double> inputStates in this._currentState)
            {
                // Compute!

                output.state.Concentrations.Add(inputStates.Key, inputStates.Value);

            }
            return output;
        }

        protected override InterstitialMassTransportState InternalStateUpdate(int timeStep)
        {
            throw new NotImplementedException();
        }
    }
}
