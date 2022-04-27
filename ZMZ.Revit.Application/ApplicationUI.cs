using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMZ.Revit.Toolkit.Extension;
using ZMZ.Revit.Tuna.Commands;

namespace ZMZ.Revit.Application
{

    public class ApplicationUI : IExternalApplication
    {
        private const string _tab = "Ming";
        public Result OnShutdown(UIControlledApplication application)
        {

            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            application.CreateRibbonTab(_tab);
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(_tab,"资源");
            ribbonPanel.CreateButton<MaterialsCommand>(a => 
            {
                a.Text = "材质管理";
                a.LargeImage = Properties.Resources.github.ConvertToBitmapSource();
                a.ToolTip = "这是一个材质管理的作业";
            });



            return Result.Succeeded;
        }
    }
}
