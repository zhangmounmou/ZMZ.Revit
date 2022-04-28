using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMZ.Revit.Toolkit.Extension.Revit
{
    public static class UnitExt
    {
        /// <summary>
        /// mm转英尺
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ConvertToFeet(this double value)
        {
#if RVT_19 || RVT_18 || RVT_20
            return UnitUtils.Convert(value, DisplayUnitType.DUT_MILLIMETERS, DisplayUnitType.DUT_DECIMAL_FEET);
#else
            return UnitUtils.Convert(value, UnitTypeId.Millimeters, UnitTypeId.Feet);
#endif
        }

    }
}
