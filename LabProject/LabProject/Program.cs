using LabProject.Shell;
using OfficeOpenXml;
using System;
using System.Windows.Forms;

namespace LabProject
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Установка лицензии для EPPlus 8.x+
            ExcelPackage.License.SetNonCommercialPersonal("Andrey Manyakov");

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}