using System;
using System.Collections.Generic;
using DxHumanModel.Core;

namespace DxHumanModel.Topology
{
   
    public class Volume : ISpatialData
    {
        public Coordinate Resolution { get; set; }
        public Coordinate Size { get; set; }
        public List<IDimension<decimal>> Bounds { get; set; }

        public Volume() {
            this.Bounds = new List<IDimension<decimal>>();
        }
        public Volume(Coordinate resolution, Coordinate size) {
            this.Resolution = resolution;
            this.Size = size;
            this.Bounds = new List<IDimension<decimal>>();
        }
        public decimal GetVoxelSize()
        {
            dynamic totalSize = this.Resolution.DotPreProduct(this.Size);
            return totalSize.Range();

        }
        // Uniform Expansion
        public void Expand(decimal additionalSize)
        {
            var divisor = (decimal)Math.Pow((double)additionalSize, 1.0 / 3);
            this.Size.X *= divisor;
            this.Size.Y *= divisor;
            this.Size.Z *= divisor;           

        }
        //Unifrom contraction
        public void Contract(decimal subtractedSize)
        {
            var divisor = (decimal)Math.Pow((double)subtractedSize, 1.0 / 3);
            this.Size.X /= divisor;
            this.Size.Y /= divisor;
            this.Size.Z /= divisor;
        }

        public IDimension<decimal> GetResolution()
        {
            return this.Resolution;
        }

        public IDimension<decimal> GetSize()
        {
            return this.Size;
        }

        public List<IDimension<decimal>> GetBounds()
        {
            return this.Bounds;
        }
    }
}
