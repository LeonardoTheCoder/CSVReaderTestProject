using CSVTestProject.BLL.Interfaces;
using CSVTestProject.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CSVTestProject.BLL.Constants.Constants;

namespace CSVTestProject.BLL
{
    interface IFileReader : IDisposable
    {
        string GetImportFilePath(FileReaderFormat format);
        List<IImportable> GetRecordsFromFile(EntityTypes entityType, FileReaderFormat format);
    }
}
