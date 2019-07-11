using System;
using System.Collections.Generic;
using DxHumanModel.Topology;
using DxHumanModel.Inputs;
using Xunit;

namespace TestDxHuman.UnitTests.Inputs
{
    public class TestMeal
    {
        public TestMeal()
        {
        }
        [Fact]
        public void TestConstruction()
        {
            Meal meal = new Meal();
            Volume vol = meal.GetVolume();
            Assert.InRange(vol.GetVoxelSize(), 125, 1000);
            Assert.Equal(4, meal.getConcentrations().Count);
            Assert.True(meal.getConcentrations().ContainsKey("H2O"));
            Assert.True(meal.getConcentrations().ContainsKey("K"));
            Assert.True(meal.getConcentrations().ContainsKey("Na"));
            Assert.True(meal.getConcentrations().ContainsKey("Other"));
        }
        [Fact]
        public void TestFill()
        {
            Meal meal = new Meal();
            Dictionary<string, decimal> dict = meal.Fill();
            Volume vol = meal.GetVolume();
            Assert.InRange<decimal>(dict.GetValueOrDefault("H2O"), (decimal)0,  vol.GetVoxelSize() );
            Assert.InRange<decimal>(dict.GetValueOrDefault("K"), (decimal)0, vol.GetVoxelSize());
            Assert.InRange<decimal>(dict.GetValueOrDefault("Na"), (decimal)0, vol.GetVoxelSize());
            Assert.InRange<decimal>(dict.GetValueOrDefault("Other"), (decimal)0, vol.GetVoxelSize());
            Console.WriteLine("Na contains: {0}", dict.GetValueOrDefault("Na"));
            Console.WriteLine("H20 contains: {0}", dict.GetValueOrDefault("H20"));
        }

    }
}
