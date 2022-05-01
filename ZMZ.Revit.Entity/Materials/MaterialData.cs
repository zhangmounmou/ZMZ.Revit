using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMZ.Revit.Toolkit.Extension.Revit;

namespace ZMZ.Revit.Entity.Materials
{
    public class MaterialData
    {
        public string Name { get; set; }

        public Color Color { get; set; }

        public Color AppearanceColor { get; set; }
        public Material Material { get; set; }

        public Document Doc { get => Material.Document; }

        public MaterialData(Material material)
        {
            Name = material.Name;
            Color = material.Color;
            Material = material;
            AppearanceColor = material.GetAppearanceColor() ;
        }

    }
}
