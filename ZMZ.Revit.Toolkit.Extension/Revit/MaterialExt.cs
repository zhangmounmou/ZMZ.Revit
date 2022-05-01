using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMZ.Revit.Toolkit.Extension.Revit
{
    public static class MaterialExt
    {

        public static Color GetAppearanceColor(this Material material)
        {

#if RVT_18 || RVT_19 || DEBUG
            return material.SurfacePatternColor;

#else
            var mId = material.AppearanceAssetId;
            if (mId != null && mId.IntegerValue != -1)
            {
                AppearanceAssetElement appearanceAssetElement = material.Document.GetElement(mId) as AppearanceAssetElement;
                Autodesk.Revit.DB.Visual.Asset asset = appearanceAssetElement.GetRenderingAsset();
                if (asset != null)
                {
                    Autodesk.Revit.DB.Visual.AssetPropertyDoubleArray4d property = (Autodesk.Revit.DB.Visual.AssetPropertyDoubleArray4d)asset?.FindByName("generic_diffuse");
                    return property.GetValueAsColor();
                }
            }
            return null;
#endif


        }
    }
}
