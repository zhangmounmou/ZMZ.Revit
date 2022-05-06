using Autodesk.Revit.DB;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMZ.Revit.Entity.Materials;
using ZMZ.Revit.Toolkit.Extension;
using ZMZ.Revit.Toolkit.Extension.Revit;
using ZMZ.Revit.Tuna.IServices;

namespace ZMZ.Revit.Tuna.ViewModels
{
    public class MaterialInfoViewModel : ViewModelBase
    {

        #region Name
        private string _name;
        public string Name
        {
            get => _name;
            set { Set(ref _name, value); }
        }
        #endregion

        #region Color
        private Color _color;

        public Color Color
        {
            get => _color;
            set { Set(ref _color, value); }
        }
        #endregion

        #region AppranceColor
        private Color _appranceColor;

        /// <summary>
        /// 外观颜色
        /// </summary>
        public Color AppearnceColor
        {
            get => _appranceColor;
            set { Set(ref _appranceColor, value); }
        }
        #endregion

        private MaterialData _materialData;
        private readonly IMaterialService _service;
        private NotificationMessageAction<MaterialData> _message;
        public MaterialInfoViewModel(IMaterialService service)
        {
            _service = service;
        }

        public void Initial(object sender)
        {
            if (sender != null && sender is MaterialData materialData)
            {
                _materialData = materialData;
                Name = _materialData.Name;
                Color = _materialData.Color;
                AppearnceColor = _materialData.AppearanceColor;
            }
        }


        public RelayCommand SetColorCommand
        {
            get => new RelayCommand(() =>
            {
                System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Color = colorDialog.Color.ToRevitColor();
                }
            });
        }

        public RelayCommand SetAppearanceColorCommand
        {
            get => new RelayCommand(() =>
            {
                System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    AppearnceColor = colorDialog.Color.ToRevitColor();
                }
            });
        }

        public RelayCommand SubmitCommand
        {
            get => new RelayCommand(() =>
            {
                if (_materialData == null)
                {
                    _materialData = _service.CreateElement(Name);
                }
                if (_materialData.Name != Name)
                {
                    _materialData.Doc.NewTrans("修改材质名称", () => _materialData.Material.Name = Name);
                    _materialData.Name = Name;
                }
                if (_materialData.Color != Color)
                {
                    _materialData.Doc.NewTrans("修改材质颜色", () => _materialData.Material.Color = Color);
                    _materialData.Color = Color;
                }
                _materialData.AppearanceColor = AppearnceColor;

                //更新材质列表德消息
                //方式1：使用消息通知进行列表德更新
                //_message.Execute(_materialData);
                //方式2 ：使用消息中心进行材质列表的更新
                MessengerInstance.Send(_materialData, Contacts.Tokens.CreateMaterial);
                //窗体关闭的消息
                MessengerInstance.Send(true, Contacts.Tokens.CloseMaterialInfoDialog);
            });
        }
    }
}
