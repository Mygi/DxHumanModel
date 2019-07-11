using System;
using DxHumanModel.Topology;
using Xunit;


namespace TestDxHuman.UnitTests.Classifiers
{
    public class TestCoordinates
    {

        public TestCoordinates()
        {

        }
        [Fact]
        public void TestCoordinateCreation()
        {
            Coordinate corrd = new Coordinate(5, 6, 7);
            Assert.Equal(5, corrd.X);
            Assert.Equal(6, corrd.Y);
            Assert.Equal(7, corrd.Z);

            corrd.Z = 15;
            Assert.Equal(15, corrd.Z);

        }
    }
}
