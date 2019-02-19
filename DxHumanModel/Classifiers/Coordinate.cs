using System;
using System.Collections.Generic;

namespace DxHumanModel.Classifiers
{
    public enum Dimension
    {
        x = 0,
        y = 1,
        z = 2,
        t = 3
    }
    public class Coordinate<T>
    {
        private T[] coordinate;
        public T X { 
                    get { return this.coordinate[(int)Dimension.x]; }
                    set { this.coordinate[(int)Dimension.x] = value; } 
                    }
        public T Y
        {
            get { return this.coordinate[(int)Dimension.y]; }
            set { this.coordinate[(int)Dimension.y] = value; }
        }
        public T Z
        {
            get { return this.coordinate[(int)Dimension.z]; }
            set { this.coordinate[(int)Dimension.z] = value; }
        }
        public Coordinate(T x, T y, T z)
        {
            this.coordinate = new T[3];
            this.coordinate[(int)Dimension.x] = x;
            this.coordinate[(int)Dimension.y] = y;
            this.coordinate[(int)Dimension.z] = z;
        }
        public Coordinate<T> DotPreProduct(Coordinate<T> other)
        {
            Coordinate<T> sum = new Coordinate<T>(default(T), default(T), default(T));
          
            dynamic dx = this.X;
            dynamic dy = this.Y;
            dynamic dz = this.Z;

            dynamic dx2 = other.X;
            dynamic dy2 = other.Y;
            dynamic dz2 = other.Z;

            sum.X = dx * dx2;
            sum.Y = dy * dy2;
            sum.Z = dz * dz2;

            return sum;
        }
        public T Range()
        {
            dynamic dx = X;
            dynamic dy = Y;
            dynamic dz = Z;
            return dx * dy * dz;
        }
    }
}
