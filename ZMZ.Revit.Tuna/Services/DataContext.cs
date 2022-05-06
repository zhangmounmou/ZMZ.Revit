
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMZ.Revit.Tuna.Interfaces;

namespace ZMZ.Revit.Tuna.Services
{
    public class DataContext : IDataContext
    {

        public DataContext(Document doc)
        {
            Document = doc;
        }
        public Document Document { get; set; }

        public UIApplication GetUIApplication()
        {
            return GetUIDocument().Application;
        }

        public UIDocument GetUIDocument()
        {
            return new UIDocument(Document);
        }
    }
}
