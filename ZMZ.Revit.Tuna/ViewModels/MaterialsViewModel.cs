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

namespace ZMZ.Revit.Tuna.ViewModels
{
    public class MaterialsViewModel : ViewModelBase
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

        public MaterialsViewModel(Document doc)
        {
            _doc = doc;
            QueryElements();
            //DeleteMaterialsCommand = new RelayCommand(DeleteMaterials);
        }

        private void DeleteMaterials(IList selectedItems)
        {
            _doc.NewTrans("删除材质", () =>
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                {
                    var data = selectedItems[i] as MaterialData;
                    _doc.Delete(data.Material.Id);
                    Materials.Remove(data);
                }
            });

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

        public RelayCommand<IList> DeleteMaterialsCommand
        {
            get => _deleteMaterialsCommand ??= new RelayCommand<IList>(DeleteMaterials);
        }


        public RelayCommand QueryElementsCommand
        {
            get => _queryElementsCommand ??= new RelayCommand(QueryElements);
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
            get => _editMaterialCommand ??= new RelayCommand<MaterialData>(obj =>
            {
                MessengerInstance.Send(obj, Contacts.Tokens.ShowMaterialInfoDialog);
            });
        }


        private bool CanQueryElements()
        {
            return !string.IsNullOrEmpty(KeyWorld);
        }

        private void QueryElements()
        {
            FilteredElementCollector elements = new FilteredElementCollector(_doc).OfClass(typeof(Material));
            var materialDatas = elements.ToList().ConvertAll(x => new MaterialData(x as Material))
                .Where(e => string.IsNullOrEmpty(KeyWorld) || e.Name.Contains(KeyWorld));
            //foreach (var item in materialDatas)
            //{
            //    Materials.Add(item);
            //}
            Materials = new ObservableCollection<MaterialData>(materialDatas);
        }
        #endregion
    }
}
