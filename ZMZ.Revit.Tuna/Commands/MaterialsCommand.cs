
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZMZ.Revit.Toolkit.Extension.DotNet;
using ZMZ.Revit.Tuna.IServices;
using ZMZ.Revit.Tuna.Services;
using ZMZ.Revit.Tuna.ViewModels;

namespace ZMZ.Revit.Tuna.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class MaterialsCommand : IExternalCommand, IExternalCommandAvailability
    {
        public Result Execute(ExternalCommandData commandData, ref string messages, ElementSet elements)
        {
            UIDocument uIDocument = commandData.Application.ActiveUIDocument;
            Document document = uIDocument.Document;

            ///构建ioc容器
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            //注册服务
            //接口和服务的注入，实例注入
            SimpleIoc.Default.Register<Document>(() => document);
            SimpleIoc.Default.Register<IMaterialService, MaterialService>();
            SimpleIoc.Default.Register<Interfaces.IDataContext, Services.DataContext>();
            SimpleIoc.Default.Register<ViewModels.MaterialsViewModel>();
            MessageBox.Show("IOC注册成功");
            //使用服务
            ///1。使用IOC、Provider去获取服务
            //ServiceLocator.Current.GetInstance<MaterialsViewModel>();
            /// 
            ///2.依赖注入：构造函数注入，方法注入，属性注入
            /*DataContext就是一个构造函数注入，依赖Document
             * 解释：DataContext的初始化需要一个Document的参数，IOC容器会检索已经注册的数据中Document类型的数据，然后自动初始化DataContext
             * MaterialsViewModel 的初始化需要IMaterialService 类型，IOC会检索已经注册的IMaterialService类型数据，如果存在，就自动帮我们构造MaterialsViewModel
             */
            Views.Materials materials = new Views.Materials(document);
            //通过locator拿到服务，这样就可以简化 Materials 的Datacontext的绑定
            materials.DataContext = ServiceLocator.Current.GetInstance<MaterialsViewModel>();
            //"ZMZ.Revit.Entity.dll".LoadAssembly();
            TransactionStatus starus;
            using (TransactionGroup group = new TransactionGroup(document, "材质管理"))
            {
                try
                {
                    group.Start();
                    if (materials.ShowDialog().Value)
                        starus = group.Assimilate();
                    else
                        starus = group.RollBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    group.RollBack();
                    starus = TransactionStatus.Error;
                }

            }
            SimpleIoc.Default.Unregister<Document>();
            if (starus == TransactionStatus.Committed)
                return Result.Succeeded;
            else
                return Result.Cancelled;
        }

        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            try
            {
                //Todo
                return applicationData.ActiveUIDocument != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
