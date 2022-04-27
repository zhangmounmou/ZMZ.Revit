using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMZ.Revit.Toolkit.Extension
{
    public static class UIDocumentExt
    {

        public static RibbonPanel CreateButton<T>(this RibbonPanel panel, Action<PushButtonData> action)
        {
            Type type = typeof(T);
            string name = $"btn_{type.Name}";
            PushButtonData pushButtonData = new PushButtonData(name, name, type.Assembly.Location, type.FullName);
            action.Invoke(pushButtonData);
            panel.AddItem(pushButtonData);
            return panel;
        }

        
    }
}
