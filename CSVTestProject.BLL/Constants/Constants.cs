using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVTestProject.BLL.Constants
{
    public static class Constants
    {
        public enum FileReaderFormat
        {
            CSV
        }

        public enum EntityTypes
        {
            Person,
            Address,
            PersonNameFrequency
        }
    }
}
