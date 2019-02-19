using System;
using System.Collections.Generic;
using DxHumanModel.Classifiers;
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
            Volume<int> vol = meal.GetVolume();
            Assert.InRange<int>(vol.GetVoxelSize(), 125, 1000);
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
            Dictionary<string, double> dict = meal.Fill();
            Volume<int> vol = meal.GetVolume();
            Assert.InRange<double>(dict.GetValueOrDefault("H2O"), (double)0, (double)vol.GetVoxelSize());
            Assert.InRange<double>(dict.GetValueOrDefault("K"), (double)0, (double)vol.GetVoxelSize());
            Assert.InRange<double>(dict.GetValueOrDefault("Na"), (double)0, (double)vol.GetVoxelSize());
            Assert.InRange<double>(dict.GetValueOrDefault("Other"), (double)0, (double)vol.GetVoxelSize());
            Console.WriteLine("Na contains: {0}", dict.GetValueOrDefault("Na"));
            Console.WriteLine("H20 contains: {0}", dict.GetValueOrDefault("H20"));
        }

    }
}
