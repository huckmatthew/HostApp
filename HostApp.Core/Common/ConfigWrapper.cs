using System;
using System.Configuration;
using System.IO;
using HostApp.Core.Interfaces;

namespace HostApp.Core.Common
{
    public class ConfigWrapper : IConfigWrapper
    {
        public string GetPath(ConfigKeys key)
        {
            var path = GetValue(key);
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            var dirpath = AppDomain.CurrentDomain.BaseDirectory;
            var returnpath = Path.Combine(dirpath, path);
            return returnpath;
        }

        public string GetTemplate(ConfigKeys key)
        {
            var path = GetPath(key);
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            if (!File.Exists(path))
            {
                return string.Empty;
            }

            return File.ReadAllText(path);
        }

        public string GetValue(ConfigKeys key)
        {
            return ConfigurationManager.AppSettings[key.ToString()];
        }

        public string GetConnectionValue(ConfigKeys key)
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[key.ToString()].ConnectionString;

            }
            catch (Exception ex)
            {

                return null;
            }

        }
    }
}
