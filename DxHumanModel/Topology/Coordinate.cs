using DxHumanModel.Core;

namespace DxHumanModel.Topology
{
    public enum Dimension
    {
        x = 0,
        y = 1,
        z = 2,
        t = 3
    }
    public class Coordinate : IDimension<decimal>
    {
        private decimal[] coordinate;
        public decimal X { 
                    get { return this.coordinate[(int)Dimension.x]; }
                    set { this.coordinate[(int)Dimension.x] = value; } 
                    }
        public decimal Y
        {
            get { return this.coordinate[(int)Dimension.y]; }
            set { this.coordinate[(int)Dimension.y] = value; }
        }
        public decimal Z
        {
            get { return this.coordinate[(int)Dimension.z]; }
            set { this.coordinate[(int)Dimension.z] = value; }
        }

        public Coordinate(decimal x, decimal y, decimal z)
        {
            this.coordinate = new decimal[3];
            this.coordinate[(int)Dimension.x] = x;
            this.coordinate[(int)Dimension.y] = y;
            this.coordinate[(int)Dimension.z] = z;
        }
        public Coordinate DotPreProduct(Coordinate other)
        {
            Coordinate sum = new Coordinate(0m, 0m, 0m);
          
            sum.X = this.X * other.X;
            sum.Y = this.Y * other.Y;
            sum.Z = this.Z * other.Z;

            return sum;
        }
        public decimal Range()
        {
            dynamic dx = X;
            dynamic dy = Y;
            dynamic dz = Z;
            return this.X * this.Y * this.Z;
        }
    }
}
