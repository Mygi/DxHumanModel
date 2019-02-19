using System;
namespace DxHumanModel.Classifiers
{
    public class Volume<T>
    {
        public Coordinate<T> Resolution { get; set; }
        public Coordinate<T> Size { get; set; }

        public Volume(Coordinate<T> resolution, Coordinate<T> size) {
            this.Resolution = resolution;
            this.Size = size;
        }
        public T GetVoxelSize()
        {
            dynamic totalSize = this.Resolution.DotPreProduct(this.Size);
            return totalSize.Range();

        }
    }
}
