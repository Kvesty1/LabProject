using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Core
{
    internal interface ILabModule
    {
        int LabNumber { get; }
        string LabTitle { get; }

        void OnShow();   // вызывается при переключении
        void OnHide();   // при уходе с лабы
    }
}
