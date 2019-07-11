using System;
using Xunit;
using DxHumanModel.Core;

namespace TestDxHuman.UnitTests.Core
{
    public class MockState : IAutomataState {
        public int receives { get; set; }
        public decimal output { get; set; }

    }
    public class MockPackage : IPackage<MockState>
    {
        public MockPackage(int increment)
        {
            this.messageIcrement = increment;
        }
        public int messageIcrement { get; }

        public MockState state { get; set; }
    }
    public class MockConnection : AutomataConnection<MockState>
    {
        private bool canSendData;
        private decimal sendRate;
        public IAutomaton<MockState> connectedNode { get; set; }
        public MockConnection(bool canSend, MockAutomata automata, decimal rate)
        {
            this.canSendData = canSend;
            this.sendRate = rate;
            this.connectedNode = automata;
        }


        public bool canSend()
        {
            return this.canSendData;
        }

        
        public decimal GetRate(int step)
        {
            throw new NotImplementedException();
        }
    }
    public class MockAutomata : Automata<MockState>
    {
        
        public override void Receive(IPackage<MockState> input, int step)
        {
            this.currentState = input.state;
            this.currentState.receives += step;
        }

        public override IPackage<MockState> Send(int step, decimal rate)
        {
            var outputState = this.currentState;
            outputState.output *= rate;
            return new MockPackage(step)
            {
                state = outputState
            };
        }

        protected override MockState InternalStateUpdate(int timeStep)
        {
            this.currentState.receives++;
            return this.currentState;
        }
    }
    public class TestBaseAutomata
    {
        public TestBaseAutomata()
        {
        }
        [Fact]
        public void TestAddNeighbours()
        {
            MockAutomata mockAutomataA = new MockAutomata();
            MockAutomata mockAutomataB = new MockAutomata();
            MockAutomata mockAutomataC = new MockAutomata();

            MockConnection ab = new MockConnection(true, mockAutomataB, 1.2m);
            mockAutomataA.AddRelationship(ab);

            MockConnection ac = new MockConnection(true, mockAutomataC, 1.4m);
            mockAutomataA.AddRelationship(ac);

            MockConnection bc = new MockConnection(true, mockAutomataC, 0.4m);
            mockAutomataB.AddRelationship(bc);

            MockConnection ca = new MockConnection(true, mockAutomataA, 0.7m);
            mockAutomataC.AddRelationship(ca);

            Assert.Equal(mockAutomataA.GetAllNeighbours().Count, 2);
            Assert.Single(mockAutomataB.GetAllNeighbours());
            Assert.Single(mockAutomataC.GetAllNeighbours());
        }
    }
}
