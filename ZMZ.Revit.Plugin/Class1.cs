using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZMZ.Revit.Tuna
{
    internal class Class1
    {
        public Class1()
        {
            var images = Properties.Resources.github;
            string name = Properties.Settings.Default.Name;

            //用户数据是可以修改的，但是必须保存
            Properties.Settings.Default.Name = "ZMZ1";
            MessageBox.Show(Properties.Settings.Default.Name);
            Properties.Settings.Default.Save();
            MessageBox.Show(Properties.Settings.Default.Name);
        }
    }
}
