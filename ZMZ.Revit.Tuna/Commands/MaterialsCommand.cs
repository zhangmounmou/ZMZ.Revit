﻿
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZMZ.Revit.Toolkit.Extension.DotNet;

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
            Views.Materials materials = new Views.Materials(document);
            //"ZMZ.Revit.Entity.dll".LoadAssembly();
            if (materials.ShowDialog().Value)
                return Result.Succeeded;
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
