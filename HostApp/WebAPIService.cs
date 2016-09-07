using System;
using System.Diagnostics;
using System.Reflection;
using Autofac;
using HostApp.BootStrap;
using log4net;
using HostApp.Core.Extensions;
using HostApp.Core.Interfaces;
using Microsoft.Owin.Hosting;
using Topshelf;

namespace HostApp
{
    public class WebAPIService : IService
    {
        private IDisposable _webAppHolder;
        private IApplicationConfiguration _appConfiguration;
        private ILog _log;
        private IContainer _container;

        public WebAPIService()
        {
            _log = LogManager.GetLogger("WebPIService");
            _log.Debug("WebAPIService Created!");
        }

        public bool Start(HostControl hostControl, IContainer container)
        {
            _container = container;
            _log = LogManager.GetLogger("WebPIService");
            _log.Debug("Starting WebAPIService Starting!");

            _appConfiguration = container.Resolve<IApplicationConfiguration>();
            if (_appConfiguration == null)
                throw new Exception("Application Not configured correctly");
            var startUp = container.Resolve<IConfigureWebAPIService>();
            if (startUp == null)
                throw new Exception("StartUp Not configured correctly");

            var baseAddress = $"{_appConfiguration.url}:{_appConfiguration.servicePort}";
            if (_webAppHolder == null)
            {
                _webAppHolder = WebApp.Start(baseAddress,
                        appBuilder => startUp.Configuration(appBuilder, container));
            }

            _log.Info("Host:{0} App:{1} Service Started on {2}!".FormatWith(_appConfiguration.HostName,
                _appConfiguration.ApplicationName, baseAddress));

            return true;
        }


        public bool Stop()
        {
            if (_webAppHolder != null)
            {
                _log.Info("{0} {1} Service Stopped!".FormatWith ( _appConfiguration.HostName, _appConfiguration.ApplicationName));
                _webAppHolder.Dispose();
                _webAppHolder = null;
            }
            return true;
        }

        public bool Reload(string applicationName)
        {
            _log.Debug("{0} Reload Config Received".FormatWith( applicationName));

            var loadConfig = new LoadConfiguration();
            try
            {
                var newconfig = loadConfig.Get(applicationName);

                var tempconfig = (IApplicationConfiguration)newconfig;

                IService myservice = _container.Resolve<IService>();
                var loadIOC = new LoadIOC();
                loadIOC.Get(newconfig, myservice, _container);

            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }


            return true;
        }

        private static bool LogToEventLog(Exception ex)
        {
            var appLog = new EventLog { Source = Assembly.GetCallingAssembly().GetName().Name };
            var message =
                $"ERROR connect to or getting configuration data from the MongoDB Database. {ex.Message}\n{(ex.InnerException != null ? ex.InnerException.Message : string.Empty)}";
            appLog.WriteEntry(message, EventLogEntryType.Error);
            Console.WriteLine(message);
            return false;
        }



    }
}
