using CSVTestProject.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVTestProject.BLL.Models
{
    public class Person : IImportable
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string StreetName
        {
            get
            {

                #region Split the Address field and extract the street Name

                string streetName = "";
                try
                {
                    string[] words;
                    words = Address.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                    streetName = words[1];
                }
                //The rule for addresses in thi ssolution is that the second word of the address would be the street name
                //The exception handler is not handles below, as an error would indicate that the above rule was broken
                catch { }
                return streetName;

                #endregion
            }
        }
    }
}
