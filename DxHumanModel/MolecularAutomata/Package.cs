using System;
using System.Collections.Generic;
using DxHumanModel.Core;
using DxHumanModel.Topology;

namespace DxHumanModel.MolecularAutomata
{
    public class Package : IPackage<decimal>
    {
        public Package(int step)
        {
            this.messageIcrement = step;
        }

        public FilledVolume volume { get; set; }
        public int messageIcrement { get; }
        ISpatialData<decimal> IPackage<decimal>.volume { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
