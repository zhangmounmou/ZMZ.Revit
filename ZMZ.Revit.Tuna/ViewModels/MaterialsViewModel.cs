using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMZ.Revit.Entity.Materials;

namespace ZMZ.Revit.Tuna.ViewModels
{
    public class MaterialsViewModel
    {
        private Document _doc;


        #region Materials 

        private ObservableCollection<MaterialData> _materials = new ObservableCollection<MaterialData>();

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<MaterialData> Materials
        {
            get { return _materials; }
            set
            {
                _materials = value;
                //NotifyPropertyChanged("Materials");
            }
        }
        #endregion


        public MaterialsViewModel(Document doc)
        {
            _doc = doc;
            FilteredElementCollector elements = new FilteredElementCollector(doc).OfClass(typeof(Material));
            Materials = new ObservableCollection<MaterialData>(elements.ToList().ConvertAll(x => new MaterialData(x as Material)));
        }
    }
}
