using System;


namespace DxHumanModel.Core
{
    public interface IAutomaton<state> where state : IAutomataState
    {
        Guid Id { get; set; }
        string Name { get; }
        
        void Receive(IPackage<state> input, int step);
        IPackage<state> Send(int step, decimal rate);
        void Update(int step);
        // these are sieving co-efficients; but arguably they should be
        // different for each neighbour.
        // Theory 1: The output rates for the membrane are the same since the output membrane is consistent
        //           The input rates are governed by the output rate of a neighbour
        // Theory 2: Both inout and output rates are unique for any realtionship or neighbours since we are
        //           describing transport between a shared membrane
        // Theory 3: Not all medium have a membrane. Perhaps the interstitum is not a membrane - we only have
        //          Cells and luminal walls (with paracellular and transcellular transport).                    
        
    }
    public interface IConfigurable<state> where state : IAutomataState
    {
        void Configure(IConfiguration<state> config);
    }
}
