using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZMZ.Revit.Toolkit.Extension.DotNet
{
    public static class AssemblyExt
    {
        /// <summary>
        /// 加载当前dll同级目录的dll
        /// </summary>
        /// <param name="dllName"></param>
        /// <returns></returns>
        public static bool LoadAssembly(this string dllName)
        {
            try
            {
                string path = Assembly.GetExecutingAssembly().Location;
                string commanuuiPath = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(dllName) + ".dll");
                if (File.Exists(commanuuiPath))
                {
                    Assembly.LoadFrom(commanuuiPath);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool LoadAssemblyByPath(this string dllPath)
        {
            try
            {
                Assembly.LoadFrom(dllPath);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
