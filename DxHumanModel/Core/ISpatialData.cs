using System;
using System.Collections.Generic;

namespace DxHumanModel.Core
{
    public interface ISpatialData
    {
        IDimension<decimal> GetResolution();
        IDimension<decimal> GetSize();
        List<IDimension<decimal>> GetBounds();
    }
    public interface IDimension<T>
    {
        T X { get; set; }
        T Y { get; set; }
        T Z { get; set; }
    }
}
