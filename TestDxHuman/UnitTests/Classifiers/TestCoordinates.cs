using System;
using DxHumanModel.Classifiers;
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
            Coordinate<int> corrd = new Coordinate<int>(5, 6, 7);
            Assert.Equal(5, corrd.X);
            Assert.Equal(6, corrd.Y);
            Assert.Equal(7, corrd.Z);

            corrd.Z = 15;
            Assert.Equal(15, corrd.Z);

        }
    }
}
