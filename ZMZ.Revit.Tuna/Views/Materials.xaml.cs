using Autodesk.Revit.DB;
using GalaSoft.MvvmLight.Messaging;
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
using ZMZ.Revit.Entity.Materials;

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
            Messenger.Default.Register<bool>(this, Contacts.Tokens.MaterialsDialog, CloseWindow);
            Messenger.Default.Register<MaterialData>(this, Contacts.Tokens.ShowMaterialInfoDialog, ShowMaterialInfo);
            Unloaded += Materials_Unloaded;
        }

        private void ShowMaterialInfo(MaterialData obj)
        {
            MaterialInfoView materialInfoView = new MaterialInfoView(obj);
            materialInfoView.ShowDialog();
        }

        private void Materials_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<bool>(this);
        }

        private void CloseWindow(bool result)
        {
            DialogResult = result;
            Close();
        }
    }
}
