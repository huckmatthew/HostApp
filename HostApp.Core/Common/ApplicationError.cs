using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostApp.Core.Common
{
    public class ApplicationError
    {
        public const string ApplicationReadingConfig = "ERROR connect to or getting configuration data from the MongoDB Database. {0}\n{1}\n{2}";
        public const string AppicationNotDefined = "No Application Settings Configured";
        public const string DefaultAppNotDefined = "Default Application not defined";
        public const string InvalidParameter = "Invalid Parameter {0}";
        public const string LogSettingsNotDefined = "No Log Settings Configured";
        public const string LogSettingsInvalid = "Invalid Log Type";
        public const string MetricsSettingsNotDefined = "No Metrics Settings Configured";
        public const string CacheSettingsNotDefined = "No Cache Settings Configured";
        public const string NoMongoConnectionString = "No MongoDB connection string defined";
        public const string NoSqlCommandsDefined = "No SQL Commands Defined";

        public const string ActivityNotFound = "Activity Not Found";
        public const string TimeOut = "Time Out";
        public const string WorkingStateNotFound = "Working State Not Found";
        public const string WorkingStateInvalid = "Invalid Working State";
        public const string BusExchangeInvalid = "Exchange Configuration is Invalid: {0}";
        public const string UndefinedError = "Internal Server Error";
        public const string NoRecoredsFound = "No Records Found";
        public const string NoError = "Success";





    }
}
