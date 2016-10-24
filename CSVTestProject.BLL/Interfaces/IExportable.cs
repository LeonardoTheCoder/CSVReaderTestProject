using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVTestProject.BLL.Interfaces
{
    public interface IExportable
    {
        string Name { get; set; }

        Int32 Frequency { get; set; }
    }
}
