using System;
using System.Collections.Generic;

namespace DxHumanModel.Core
{
    public interface IConfiguration<state> where state : IAutomataState 
    {
        ISpatialData InitialSpatialData { get; set; }
        List<AutomataConnection<state>> Neighbours { get; set; }
        state InitialState { get; set; }
        // Initial State

    }
}
