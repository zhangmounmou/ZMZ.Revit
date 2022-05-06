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
using ZMZ.Revit.Tuna.IServices;
using ZMZ.Revit.Tuna.ViewModels;

namespace ZMZ.Revit.Tuna.Views
{
    /// <summary>
    /// MaterialInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialInfoView : Window
    {
        public MaterialInfoView()
        {
            InitializeComponent();
            //DataContext = new MaterialInfoViewModel(service);
            Messenger.Default.Register<bool>(this, Contacts.Tokens.CloseMaterialInfoDialog, CloseWindow);
            Unloaded += (o, e) => 
            { 
                Messenger.Default.Unregister(this);
            };
        }


        private void CloseWindow(bool obj)
        {
            DialogResult = obj;
            Close();
        }
    }
}
