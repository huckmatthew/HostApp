﻿using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HostApp.Core.Common;
using HostApp.Core.Extensions;
using HostApp.Core.Utility;
using Autofac;
using HostApp.Core;
using HostApp.Core.Constants;
using HostApp.Core.DTO;
using HostApp.Core.Interfaces;
using HostApp.Logic;
using IApplicationConfigLogic = HostApp.Interfaces.IApplicationConfigLogic;

namespace HostApp.BootStrap
{
    public class LoadConfiguration
    {
        private readonly IConfigureHost _host;

        /// <summary>
        /// Finds the module/dll that will load the host configuration.  
        /// Will use reflection to find a class that implements IConfigureHost
        /// </summary>
        public LoadConfiguration()
        {
            var hostImps = AssemblyHelper.GetImplementations(typeof(IConfigureHost)).ToArray();

            if (hostImps.Count() > 1)
                throw new Exception("To many Application Configuration Classes Implemented");

            //if (!hostImps.Any())
            //    throw new Exception("No Application Configuration Classes Implemented");
            _host = null;
            if (hostImps.Count() == 1)
            {
                _host = (IConfigureHost)Activator.CreateInstance(hostImps.First());
            }

        }

        /// <summary>
        /// Gets the default Host application name.  Will get the from the Application Config .
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            var defaultApp = GetDefaultApplication();
            var temp =  Get(defaultApp);
            return temp;
            //return Get(defaultApp);
        }

        /// <summary>
        /// Get the Application Name for the running application.
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public object Get(string applicationName)
        {
            var defaultApp = applicationName;

            string hostName = GetHostName();
            if (_host != null)
            {
                return _host.LoadConfiguration(hostName, defaultApp);
            }

            return  LoadServiceConfiguration(hostName, defaultApp);

        }

        /// <summary>
        /// Configure the Cache for the application.  Will set up Autofac for this caching for the configuring application.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="config"></param>
        //public void ConfigureCachingIOC(ContainerBuilder builder, IApplicationConfiguration config)
        //{
        //    if (_host != null)
        //    {
        //        _host.ConfigureCaching(builder, config);
        //    }
        //    else
        //    {
        //        ConfigureCaching(builder, config);
        //    }
        //}

        /// <summary>
        /// Get the Default host name.
        /// </summary>
        /// <returns></returns>
        private string GetHostName()
        {
            try
            {
                var hostName = ConfigurationManager.AppSettings["DefaultApplication"];
                if (hostName.IsNullOrWhiteSpace())
                    hostName = System.Net.Dns.GetHostName();
                return hostName;
            }
            catch (Exception ex)
            {
                LogToEventLog(ex);
                return null;
            }
        }

        /// <summary>
        /// Get the Default Application Name.
        /// </summary>
        /// <returns></returns>
        private string GetDefaultApplication()
        {
            try
            {
                var defaultApp2 = ConfigurationManager.AppSettings["DefaultApplication"];
                IConfigWrapper configWrapper = new ConfigWrapper();
                var defaultApp = configWrapper.GetValue(ConfigKeys.DefaultApplication);
                if (defaultApp.IsNullOrWhiteSpace())
                    defaultApp = Assembly.GetCallingAssembly().GetName().Name;
                return defaultApp;
            }
            catch (Exception ex)
            {
                LogToEventLog(ex);
                return null;
            }
        }

        /// <summary>
        /// Log Configuration Error to the EventLog.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private void LogToEventLog(Exception ex)
        {
            var appLog = new EventLog { Source = Assembly.GetCallingAssembly().GetName().Name };
            var message = string.Format("ERROR connect to or getting configuration data from the MongoDB Database. {0}\n{1}", ex.Message,
                ex.InnerException != null ? ex.InnerException.Message : string.Empty);
            appLog.WriteEntry(message, EventLogEntryType.Error);
            Console.WriteLine(message);
        }

        //public void ConfigureCaching(ContainerBuilder builder, IApplicationConfiguration config)
        //{
        //    var localCacheSettings = new CacheSettings();
        //    var localSettings =
        //        config.cacheConfiguration.FirstOrDefault(
        //            d => d.cacheLocation.Equals(CacheLocation.Local.ToString(), StringComparison.CurrentCultureIgnoreCase));

        //    if (localSettings != null)
        //    {
        //        localCacheSettings.Configure(localSettings);
        //        if (localSettings.cacheType.Equals(CacheConstants.Memory, StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            builder.RegisterType<CacheMemory>()
        //                .SingleInstance()
        //                .Keyed<ICache>(CacheLocation.Local)
        //                .WithParameter(new TypedParameter(typeof(CacheSettings), localCacheSettings));
        //        }
        //        else
        //        {
        //            builder.RegisterType<CacheRedis>()
        //                .Keyed<ICache>(CacheLocation.Local)
        //                .WithParameter(new TypedParameter(typeof(CacheSettings), localCacheSettings));
        //        }
        //    }
        //    else
        //    {
        //        builder.RegisterType<CacheMemory>()
        //            .SingleInstance()
        //            .Keyed<ICache>(CacheLocation.Local)
        //            .WithParameter(new TypedParameter(typeof(CacheSettings), localCacheSettings));
        //    }

        //    var remoteCacheSettings = new CacheSettings();
        //    var remoteSettings =
        //        config.cacheConfiguration.FirstOrDefault(
        //            d => d.cacheLocation.Equals(CacheLocation.Remote.ToString(), StringComparison.CurrentCultureIgnoreCase));
        //    if (remoteSettings != null)
        //    {
        //        remoteCacheSettings.Configure(remoteSettings);
        //        if (remoteSettings.cacheType.Equals(CacheConstants.Memory, StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            builder.RegisterType<CacheMemory>()
        //                .SingleInstance()
        //                .Keyed<ICache>(CacheLocation.Remote)
        //                .WithParameter(new TypedParameter(typeof(CacheSettings), remoteCacheSettings));
        //        }
        //        else
        //        {
        //            builder.RegisterType<CacheRedis>()
        //                .Keyed<ICache>(CacheLocation.Remote)
        //                .WithParameter(new TypedParameter(typeof(CacheSettings), remoteCacheSettings));
        //        }
        //    }
        //    else
        //    {
        //        builder.RegisterType<CacheMemory>()
        //            .SingleInstance()
        //            .Keyed<ICache>(CacheLocation.Remote)
        //            .WithParameter(new TypedParameter(typeof(CacheSettings), remoteCacheSettings));
        //    }
        //}

        public IRestServiceConfiguration LoadServiceConfiguration(string hostName, string appToLoad)
        {

            var mongoDBConnection = "";
            bool loadfromMongo = true;
            try
            {

                //var mongoDBConnection = ConfigurationManager.ConnectionStrings[MongoConstants.MongoDB].ConnectionString;
                IConfigWrapper configWrapper = new ConfigWrapper();
                var appconfig = configWrapper.GetValue(ConfigKeys.ApplicationConfigurationRespository);
                mongoDBConnection = configWrapper.GetConnectionValue(ConfigKeys.MongoDB);

                loadfromMongo = string.Compare(appconfig, "FILE", StringComparison.CurrentCultureIgnoreCase) != 0;

                if (loadfromMongo)
                {
                    if (mongoDBConnection.IsNullOrWhiteSpace())
                    {
                        throw new ArgumentException("No MongoDB connection string defined");
                    }
                }
            }
            catch (Exception ex)
            {
                var appLog = new EventLog { Source = Assembly.GetCallingAssembly().GetName().Name };
                var message = string.Format("ERROR connect to or getting configuration data from the MongoDB Database. {0}\n{1}\n{2}", ex.Message,
                    ex.InnerException != null ? ex.InnerException.Message : string.Empty, "Application Configuration");
                appLog.WriteEntry(message, EventLogEntryType.Error);
                Console.WriteLine(message);
                throw;
            }

            IRestServiceConfiguration newAppSettings = GetConfig(mongoDBConnection, hostName, appToLoad, loadfromMongo);
            if (newAppSettings == null)
            {
                throw new ApplicationException("No Log Settings Configured");
            }

            newAppSettings.MongoDBConnection = mongoDBConnection;

            //if (newAppSettings.IsConfigured)
            //{
            //    //Configure the Log now.
            //    EPTLogManager.ResetConfiguraiton();
            //    var settings = new EPTLogConfigure();
            //    settings.Setup(newAppSettings.LogConfiguration);
            //}

            return newAppSettings;
        }

        private IRestServiceConfiguration GetConfig(string mongodbConnectionString, string hostName, string applicationName, bool loadFromMongo)
        {
            try
            {
                IApplicationConfigLogic applicaitonconfiglogic;
                if (loadFromMongo)
                {
                    applicaitonconfiglogic = new ApplicationConfigLogic(mongodbConnectionString, MongoConstants.ConfigurationDatabase);
                }
                else
                {
                    applicaitonconfiglogic = new ApplicationConfigLogic();
                }

                IRestServiceConfiguration data = applicaitonconfiglogic.GetRestServiceAppConfig(mongodbConnectionString, hostName, applicationName);
                data.IsConfigured = true;
                return data;

            }
            catch (Exception ex)
            {
                var appLog = new EventLog { Source = Assembly.GetCallingAssembly().GetName().Name };
                var message = string.Format(ApplicationError.ApplicationReadingConfig, ex.Message,
                    ex.InnerException != null ? ex.InnerException.Message : string.Empty, "Application Configuration");
                appLog.WriteEntry(message, EventLogEntryType.Error);
                Console.WriteLine(message);
                return GetDefaultConfig();
            }


        }

        private IRestServiceConfiguration GetDefaultConfig()
        {
            var defaultConfig = new RestServiceConfiguration
            {
                IsConfigured = false,
                servicePort = 9000,
                url = "http://+"
            };

            return defaultConfig;
        }


    }
}
