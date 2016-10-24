using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVTestProject.BLL.Interfaces
{
    public interface IImportable
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string Address { get; set; }

        string PhoneNumber { get; set; }

        string StreetName { get; }
    }
}
