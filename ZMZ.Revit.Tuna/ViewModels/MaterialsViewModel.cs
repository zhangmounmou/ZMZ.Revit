using Autodesk.Revit.DB;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ZMZ.Revit.Entity.Materials;
using ZMZ.Revit.Toolkit.Extension;
using ZMZ.Revit.Tuna.IServices;

namespace ZMZ.Revit.Tuna.ViewModels
{
    public class MaterialsViewModel : ViewModelBase
    {
        private readonly IMaterialService _service;


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
                Set(ref _materials, value);
            }
        }
        #endregion

        private string _keyWorld;

        public string KeyWorld
        {
            get => _keyWorld;
            set
            {
                _keyWorld = value;
                _queryElementsCommand.RaiseCanExecuteChanged();
            }
        }

        public MaterialsViewModel(IMaterialService service)
        {
            _service = service;
            GetElements();
        }

        private void GetElements()
        {
            Materials = new ObservableCollection<MaterialData>(_service.GetElements(e => string.IsNullOrEmpty(KeyWorld) || e.Name.Contains(KeyWorld)));
        }

        private void DeleteMaterials(IList selectedItems)
        {
            for (int i = selectedItems.Count - 1; i >= 0; i--)
            {
                var data = selectedItems[i] as MaterialData;
                _service.DeleteElement(data);
                Materials.Remove(data);
            }
        }
        #region 命令

        private RelayCommand<IList> _deleteMaterialsCommand;
        private RelayCommand _queryElementsCommand;
        /// <summary>
        /// SelectionChangedEventArgs 就是界面事件对应的参数类型
        /// </summary>
        private RelayCommand<SelectionChangedEventArgs> _testCommand;

        private RelayCommand _submit;

        private RelayCommand<MaterialData> _editMaterialCommand;
        private RelayCommand _createMaterialCommand;

        public RelayCommand<IList> DeleteMaterialsCommand
        {
            get => _deleteMaterialsCommand ??= new RelayCommand<IList>(DeleteMaterials);
        }


        public RelayCommand QueryElementsCommand
        {
            get => new RelayCommand(GetElements);
        }

        /// <summary>
        /// 界面绑定的事件的参数类型，例如此测试界面绑定的是Listbox 的 SelectionChanged事件
        /// 这个事件的参数就是SelectionChangedEventArgs类型
        /// </summary>
        public RelayCommand<SelectionChangedEventArgs> TestCommand
        {
            get => _testCommand ??= new RelayCommand<SelectionChangedEventArgs>(e =>
            {
                //System.Windows.MessageBox.Show(e.AddedItems.Count.ToString());
            });
        }

        public RelayCommand SubmitCommand
        {
            get => _submit ??= new RelayCommand(Submit);
        }

        public void Submit()
        {
            //因为继承了ViewModelBase
            //Messenger.Default.Send(true, Contacts.Tokens.MaterialsDialog);

            MessengerInstance.Send(true, Contacts.Tokens.MaterialsDialog);
        }

        public RelayCommand<MaterialData> EditMaterialCommand
        {
            get => new RelayCommand<MaterialData>(obj =>
           {
               //MessengerInstance.Send(new NotificationMessageAction<MaterialData>(obj, Contacts.Tokens.EditMaterial, (e) => { }), Contacts.Tokens.ShowMaterialInfoDialog);
               MessengerInstance.Send(new NotificationMessageAction<MaterialData>(obj,
                    new MaterialInfoViewModel(_service),
                    Contacts.Tokens.CreateMaterial,
                    (e) =>
                    {
                        //if (e != null)
                        //{
                        //    Materials.Insert(0, e);
                        //}
                    }),
                     Contacts.Tokens.ShowMaterialInfoDialog);
           });
        }

        public RelayCommand CreateMaterialCommand
        {
            get => new RelayCommand(() =>
            {
                MessengerInstance.Send(new NotificationMessageAction<MaterialData>(null,
                    new MaterialInfoViewModel(_service),
                    Contacts.Tokens.CreateMaterial,
                    (e) =>
                     {
                         if (e != null)
                         {
                             Materials.Insert(0, e);
                         }
                     }),
                     Contacts.Tokens.ShowMaterialInfoDialog);
            });
        }
        private void CreateMaterial(MaterialData obj)
        {
            Materials.Insert(0, obj);
        }
        private bool CanQueryElements()
        {
            return !string.IsNullOrEmpty(KeyWorld);
        }

        #endregion
    }
}
