using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMZ.Revit.Entity.Materials;
using ZMZ.Revit.Tuna.Interfaces;

namespace ZMZ.Revit.Tuna.IServices
{
    public interface IMaterialService:IDataService<MaterialData>,IExcelTraansfer<MaterialData>
    {

    }
}
