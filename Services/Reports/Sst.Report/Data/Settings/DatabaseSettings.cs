using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sst.Report.Data.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ContactCollectionName { get; set; }
        public string ContactInformationCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
