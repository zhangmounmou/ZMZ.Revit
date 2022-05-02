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
        private ViewModels.MaterialsViewModel _viewModel;
        public Materials(Document doc)
        {
            InitializeComponent();
            _viewModel = new ViewModels.MaterialsViewModel(doc);
            this.DataContext = _viewModel;
            //FilteredElementCollector elements = new FilteredElementCollector(doc).OfClass(typeof(Material));
            //foreach (var item in elements)
            //{
            //    ListMaterial.Items.Add(item);
            //}
            //ListMaterial.ItemsSource = elements.ToList();
        }


    }
}
