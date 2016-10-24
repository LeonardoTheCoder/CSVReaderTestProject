using CSVTestProject.BLL.Interfaces;
using CSVTestProject.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CSVTestProject.BLL.Constants.Constants;

namespace CSVTestProject.BLL
{
    interface IFileWriter : IDisposable
    {
        string GetExportFilePath(FileReaderFormat format, EntityTypes type);
        bool ExportRecordsToFile(EntityTypes entityType, FileReaderFormat format, List<IExportable> records);
    }
}
