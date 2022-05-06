using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMZ.Revit.Entity.Materials;
using ZMZ.Revit.Toolkit.Extension;
using ZMZ.Revit.Tuna.Interfaces;
using ZMZ.Revit.Tuna.IServices;

namespace ZMZ.Revit.Tuna.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IDataContext _context;

        public MaterialService(IDataContext dataContext)
        {
            _context = dataContext;
        }
        public MaterialData CreateElement(string name)
        {
            Element element = null;
            _context.Document.NewTrans("创建材质", () =>
            {

                ElementId id = Material.Create(_context.Document, name);
                element = _context.Document.GetElement(id);
            });
            return new MaterialData(element as Material);
        }

        public void DeleteElement(MaterialData element)
        {
            if (element == null || element.Material == null)
                return;

            _context.Document.NewTrans("删除材质", () =>
            {
                _context.Document.Delete(element.Material.Id);
            });
        }

        public void DeleteElements(IEnumerable<MaterialData> elements)
        {
            _context.Document.NewTrans("删除材质", () =>
            {
                foreach (MaterialData element in elements)
                    _context.Document.Delete(element.Material.Id);
            });
        }


        public IEnumerable<MaterialData> GetElements(Func<MaterialData, bool> predicate = null)
        {
            FilteredElementCollector elements = new FilteredElementCollector(_context.Document).OfClass(typeof(Material));
            IEnumerable<MaterialData> result = elements.ToList()
                .ConvertAll(x => new MaterialData(x as Material));
            if (predicate != null)
            {
                result = result.Where(predicate);
            }
            return result;
        }

        public void Export(IEnumerable<MaterialData> elements)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MaterialData> Import()
        {
            throw new NotImplementedException();
        }
    }
}
