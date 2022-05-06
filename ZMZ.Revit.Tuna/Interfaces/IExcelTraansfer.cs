using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMZ.Revit.Tuna.Interfaces
{
    public interface  IExcelTraansfer<TElement>
    {

        IEnumerable<TElement> Import();

        void Export(IEnumerable<TElement> elements);
    }
}
