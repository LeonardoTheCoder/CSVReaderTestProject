using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSVTestProject.BLL.Constants;
using CSVTestProject.BLL.Models;
using CSVTestProject.BLL.Interfaces;
using System.IO;
using System.Text;

namespace CSVTestProject.BLL.Providers
{
    public class PersonProvider : IFileReader, IFileWriter
    {

        public void Dispose()
        {
            //nothing to close or dispose at this stage
        }

        public string GetImportFilePath(Constants.Constants.FileReaderFormat format)
        {
            string fileName = "Uploads\\data.csv"; //LCB Todo: Use an OpenFileDialog and allow user to select a CSV file
            return fileName;
        }

        public string GetExportFilePath(Constants.Constants.FileReaderFormat format, Constants.Constants.EntityTypes type)
        {
            string fileName = "Exports\\"; //LCB Todo: Use an OpenFileDialog and allow user to select a CSV file
            switch (type)
            {
                case Constants.Constants.EntityTypes.PersonNameFrequency:
                    fileName += "PersonNameFrequency";
                    break;
                case Constants.Constants.EntityTypes.Address:
                    fileName += "Address";
                    break;
                default:
                    fileName += "General";
                    break;
            }
            switch (format)
            {
                case Constants.Constants.FileReaderFormat.CSV:
                    fileName += ".csv";
                    break;
                default:
                    fileName += ".txt";
                    break;
            }
            return fileName;
        }

        public List<IImportable> GetRecordsFromFile(Constants.Constants.EntityTypes entityType, Constants.Constants.FileReaderFormat format)
        {
            List<IImportable> listItems = new List<IImportable>();
            switch (format)
            {
                case Constants.Constants.FileReaderFormat.CSV:

                    #region Read the CSV file and populate list

                    string fileName = GetImportFilePath(format);
                    string[] allLines = File.ReadAllLines(fileName);
                    var query = from line in allLines
                                let data = line.Split(',')
                                select new
                                {
                                    FirstName = data[0],
                                    LastName = data[1],
                                    Address = data[2],
                                    PhoneNumber = data[3]
                                };
                    //Ignore the first record as it holds the column headers
                    foreach (var item in query.Where(x=>x.FirstName != "FirstName"))
                    {
                        listItems.Add(new Person()
                        {
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Address = item.Address,
                            PhoneNumber = item.PhoneNumber
                        });
                    }

                    #endregion

                    break;
            }
            return listItems;
        }

        public List<IExportable> GetPersonNameFrequency(List<IImportable> people)
        {
            List<IExportable> result = new List<IExportable>();

            #region Iterate through list and populate results with Name and frequency

            foreach (Person person in people)
            {
                if (result.Any(x => x.Name == person.FirstName || x.Name == person.LastName))
                {
                    result.First(x => x.Name == person.FirstName || x.Name == person.LastName).Frequency++;
                }
                if (!result.Any(x => x.Name == person.FirstName))
                {
                    result.Add(new PersonNameFrequency()
                    {
                        Name = person.FirstName,
                        Frequency = 1
                    });
                }
                if (!result.Any(x => x.Name == person.LastName))
                {
                    result.Add(new PersonNameFrequency()
                    {
                        Name = person.LastName,
                        Frequency = 1
                    });
                }
            }

            #endregion

            return result.OrderByDescending(x => x.Frequency).ThenBy(x => x.Name).ToList();
        }

        public List<IExportable> GetAddressListFromPeopleByStreetName(List<IImportable> people)
        {
            List<IExportable> result = new List<IExportable>();

            #region Sort the people collection by street name and populate list of addresses

            foreach (string address in people.OrderBy(x => x.StreetName).Select(x => x.Address))
            {
                result.Add(new Address() { Name = address });
            }

            #endregion

            return result;
        }

        public bool ExportRecordsToFile(Constants.Constants.EntityTypes entityType, Constants.Constants.FileReaderFormat format, List<IExportable> records)
        {
            bool result = false;
            try
            {
                string fileName = GetExportFilePath(format, entityType);
                var csv = new StringBuilder();
                foreach (IExportable record in records)
                {
                    var newLine = "";
                    switch (entityType)
                    {
                        case Constants.Constants.EntityTypes.PersonNameFrequency:
                            newLine = string.Format("{0},{1}", record.Name, record.Frequency);
                            break;
                        case Constants.Constants.EntityTypes.Address:
                            newLine = string.Format("{0}", record.Name);
                            break;
                    }
                    csv.AppendLine(newLine);
                }
                File.WriteAllText(fileName, csv.ToString());
                result = true;
            }
            catch { } //Proper exception handling is outside of the scope of the test

            return result;
        }
    }
}
