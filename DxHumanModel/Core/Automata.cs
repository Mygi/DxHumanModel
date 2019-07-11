using System;
using System.Collections.Generic;


namespace DxHumanModel.Core
{
    public abstract class Automata<state>: IAutomaton<state>, HasElements<state>, IConfigurable<state> where state : IAutomataState
    {
        protected List<AutomataConnection<state>> neighbours;

        public Guid Id { get; set;  }

        public string Name { get; }

        public IConfiguration<state> configuration { get; set; }

        protected state currentState { get; set; }

        protected state previousState { get; set; }

        
        // Can Update Transfer Rates and Or FilledVolume
        protected abstract state InternalStateUpdate(int timeStep);

        public void Configure(IConfiguration<state> config)
        {
            this.configuration = config;
            this.neighbours = config.Neighbours;
            this.currentState = config.InitialState;
        }

        public List<AutomataConnection<state>> GetAllNeighbours()
        {
            return this.neighbours;
        }
        public void Update(int step)
        {
           this.neighbours.ForEach(neighbour =>
           {
               if ( neighbour.canSend() )
               this.Receive(neighbour.connectedNode.Send(step, neighbour.GetRate(step) ), step);
           });
            this.previousState = this.currentState;
            this.currentState = this.InternalStateUpdate(step);
        }

        public void AddRelationship(AutomataConnection<state> connection)
        {
            if( neighbours == null)
            {
                neighbours = new List<AutomataConnection<state>>();
            }
            this.neighbours.Add(connection);
        }
        // For when a process switches off
        public bool KillRelationship(Guid Id)
        {
            if (neighbours == null)
            {
                return false;
            }
            return this.neighbours.Remove(this.neighbours.Find(x => x.connectedNode.Id == Id));
            
        }

        public abstract void Receive(IPackage<state> input, int step);

        public abstract IPackage<state> Send(int step, decimal rate);
    }
}


