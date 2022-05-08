using Autodesk.Revit.DB;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using ZMZ.Revit.Entity.Materials;
using ZMZ.Revit.Tuna.Services;
using ZMZ.Revit.Tuna.ViewModels;

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
            _viewModel = new ViewModels.MaterialsViewModel(new MaterialService(new DataContext(doc)));
            this.DataContext = _viewModel;
            Messenger.Default.Register<bool>(this, Contacts.Tokens.MaterialsDialog, CloseWindow);
            Messenger.Default.Register<NotificationMessageAction<MaterialData>>(this, Contacts.Tokens.ShowMaterialInfoDialog, ShowMaterialInfo);
            Unloaded += Materials_Unloaded;
        }

        private void ShowMaterialInfo(NotificationMessageAction<MaterialData> messageAction)
        {
            MaterialInfoView materialInfoView = new MaterialInfoView();
            MaterialInfoViewModel materialInfoViewModel = messageAction.Target as MaterialInfoViewModel;
            materialInfoView.DataContext = materialInfoViewModel;
            materialInfoViewModel.Initial(messageAction.Sender);
            if (materialInfoView.ShowDialog().Value)
            {
                messageAction.Execute(materialInfoViewModel.MaterialData);
            } 
        }

        private void Materials_Unloaded(object sender, RoutedEventArgs e)
        {
            //这里不要使用带泛型的注销消息，有可能会导致非给定泛型的消息无法注销的问题
            //Messenger.Default.Unregister<bool>(this);
            Messenger.Default.Unregister(this);

            //将绑定的DataContext的消息中心也注销掉
            Messenger.Default.Unregister(this.DataContext);
        }

        private void CloseWindow(bool result)
        {
            DialogResult = result;
            Close();
        }
    }
}
