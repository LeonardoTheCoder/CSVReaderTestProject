using CSVTestProject.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVTestProject.BLL.Models
{
    public class PersonNameFrequency: IExportable
    {
        public string Name { get; set; }

        public Int32 Frequency { get; set; }
    }
}
