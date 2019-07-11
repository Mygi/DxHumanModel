using System;
namespace DxHumanModel.Core
{
    public interface HasElements<T> where T : IAutomataState
    {
        void AddRelationship(AutomataConnection<T> connection);
        void Update(int stepIndex);
        bool KillRelationship(Guid Id);
    }
    // This can just be a static measure or a function
    // public delegate decimal GetRate(int step);

    public interface AutomataConnection<T> where T : IAutomataState
    {
        bool canSend();
        IAutomaton<T> connectedNode { get; set; }
        // Total Flow Rate - should the send rate change?
        // Blood flow changes. Perhaps we can change this to a functiom instead of 
        decimal GetRate(int step);
    }
}
