using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMZ.Revit.Toolkit.Extension;
using ZMZ.Revit.Toolkit.Extension.Revit;

namespace ZMZ.Revit.Entity.Materials
{
    public class MaterialData
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        private Color _color;
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
            }
        }

        public Color AppearanceColor { get; set; }
        public Material Material { get; set; }

        public Document Doc { get => Material.Document; }

        public MaterialData(Material material)
        {
            Name = material.Name;
            Color = material.Color;
            Material = material;
            AppearanceColor = material.GetAppearanceColor();
        }

        private void Save()
        {
            Doc.NewTrans("修改材质", () => 
            {
                Material.Name = Name;
                Material.Color = Color;
            });
        }
    }
}
