using CSVTestProject.BLL.Interfaces;
using CSVTestProject.BLL.Models;
using CSVTestProject.BLL.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVTestProject
{
    class Program
    {

        static void Main(string[] args)
        {
            List<IImportable> people;
            List<IExportable> nameFrequency;
            List<IExportable> addresses;
            using (PersonProvider provider = new PersonProvider())
            {
                Console.WriteLine("Let's Start");

                #region Step 1: Reading file

                Console.Write("Step 1: Reading file...");
                people = provider.GetRecordsFromFile(BLL.Constants.Constants.EntityTypes.Person, BLL.Constants.Constants.FileReaderFormat.CSV);
                Console.WriteLine("Done (" + people.Count().ToString() + " people imported)");

                #endregion

                #region Step 2: Sorting people by name frequency

                Console.Write("Step 2: Sorting people by name frequency...");
                nameFrequency = provider.GetPersonNameFrequency(people);
                Console.WriteLine("Done (" + nameFrequency.Count().ToString() + " name frequency records created)");

                #endregion

                #region Step 3: Sorting addresses from people

                Console.Write("Step 3: Sorting addresses from people...");
                addresses = provider.GetAddressListFromPeopleByStreetName(people);
                Console.WriteLine("Done (" + addresses.Count().ToString() + " addresses created)");

                #endregion

                #region Step 4: Export files

                Console.Write("Step 4a: Export person name frequency...");
                bool exportResult = false;
                exportResult = provider.ExportRecordsToFile(BLL.Constants.Constants.EntityTypes.PersonNameFrequency, BLL.Constants.Constants.FileReaderFormat.CSV, nameFrequency);
                if (exportResult) Console.WriteLine("Done (" + nameFrequency.Count().ToString() + " name frequency records exported)");
                else Console.WriteLine("Exception Occured: No records were exported");
                Console.Write("Step 4a: Export person name frequency...");
                exportResult = provider.ExportRecordsToFile(BLL.Constants.Constants.EntityTypes.Address, BLL.Constants.Constants.FileReaderFormat.CSV, addresses);
                if (exportResult) Console.WriteLine("Done (" + addresses.Count().ToString() + " address records exported)");
                else Console.WriteLine("Exception Occured: No records were exported");

                #endregion


                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
