using Autodesk.Revit.DB;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMZ.Revit.Toolkit.Extension;
using ZMZ.Revit.Toolkit.Extension.Revit;

namespace ZMZ.Revit.Entity.Materials
{
    public class MaterialData : ObservableObject
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Doc.NewTrans("修改材质名称", () => Material.Name = _name);
                RaisePropertyChanged();
            }
        }

        private Color _color;
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                Doc.NewTrans("修改颜色", () => Material.Color = _color);
                RaisePropertyChanged();
            }
        }

        private Color _appearanceColor;
        public Color AppearanceColor
        {
            get => _appearanceColor;
            set
            {
                #region 写法1
                _appearanceColor = value;
                RaisePropertyChanged();
                #endregion
                #region 写法2

                #endregion


            }
        }
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
