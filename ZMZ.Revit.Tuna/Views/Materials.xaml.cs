using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZMZ.Revit.Tuna.Views
{
    /// <summary>
    /// Materials.xaml 的交互逻辑
    /// </summary>
    public partial class Materials : Window
    {
        private Document _doc;
        public Materials(Document doc)
        {
            InitializeComponent();
            _doc = doc;
            FilteredElementCollector elements = new FilteredElementCollector(doc).OfClass(typeof(Material));
            ListMaterial.ItemsSource = elements.ToList();
        }
    }
}
