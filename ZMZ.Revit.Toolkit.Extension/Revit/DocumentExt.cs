using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMZ.Revit.Toolkit.Extension
{
    public static class DocumentExt
    {
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="name">事务名称</param>
        /// <param name="action">执行的操作</param>
        public static void NewTrans(this Document doc, string name, Action action)
        {
            using (Transaction ts = new Transaction(doc, name))
            {
                try
                {
                    if (!doc.IsModifiable)
                        ts.Start();
                    action?.Invoke();
                    if (ts.HasStarted())
                        ts.Commit();
                }
                catch (Exception ex)
                {
                    if (ts.HasStarted())
                        ts.RollBack();
                }

            }
        }
    }
}
