using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostApp.Core.DTO;
using HostApp.Core.Interfaces;
using HostApp.Core.Repository;
using HostApp.Interfaces;
using HostApp.Repository;
using HostApp.Repository.File;
using HostApp.Repository.Mongo;

namespace HostApp.Logic
{
    public class ApplicationConfigLogic : IApplicationConfigLogic
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMetricsRepository _metricsRepository;
        private readonly IBusConfigurationRepository _rabbitMqRepository;

        public ApplicationConfigLogic(IApplicationRepository applicationRepository,
            IMetricsRepository metricsRepository, 
            IBusConfigurationRepository rabbitMqRepository)
        {
            _applicationRepository = applicationRepository;
            _metricsRepository = metricsRepository;
            _rabbitMqRepository = rabbitMqRepository;
       
        }

        public ApplicationConfigLogic(string mongodbConnectionString, string dbName)
        {
            _applicationRepository = new ApplicationMongoDBRepository(mongodbConnectionString, dbName);
            _metricsRepository = new MetricsMongDBRepository(new ContextMongoDB<MetricsSettingDTO>(mongodbConnectionString, dbName));
            _rabbitMqRepository = new BusSettingsMongoDBRepository(new ContextMongoDB<BusSettingDTO>(mongodbConnectionString, dbName));
        }
        public ApplicationConfigLogic()
        {
            _applicationRepository = new ApplicationFileRepository();
            _metricsRepository = new MetricsFileRepository();
            _rabbitMqRepository = new BusSettingsFileRepository();

        }

        public IRestServiceConfiguration GetRestServiceAppConfig(string connectionString, string hostName, string applicationName)
        {
            var config = _applicationRepository.GetRestServiceAppConfig(hostName, applicationName);

            UpdateApplicationConfiguration(config, connectionString, hostName, applicationName);

            return config;
        }

        public IServiceHostConfiguration GetServiceHostConfig(string connectionString, string hostName, string applicationName)
        {
            var config = _applicationRepository.GetServiceHostConfig(hostName, applicationName);

            UpdateApplicationConfiguration(config, connectionString, hostName, applicationName);

            return config;
        }

        public IBusinessServiceConfiguration GetBusinessServiceAppConfig(string connectionString, string hostName, string applicationName)
        {
            var config = _applicationRepository.GetBusinessServiceAppConfig(hostName, applicationName);

            UpdateApplicationConfiguration(config, connectionString, hostName, applicationName);

            if (config.busConfiguration == null)
            {
                config.busConfiguration = GetBusConfiguration(hostName, applicationName);
            }

            return config;
        }

        private void UpdateApplicationConfiguration(IApplicationConfiguration config, string connectionString, string hostName, string applicationName)
        {
            config.MetricConfiguration = GetMetricConfiguration(hostName, applicationName);

            config.MongoDBConnection = connectionString;
            config.ApplicationName = applicationName;
            config.HostName = hostName;
        }

        private IEnumerable<MetricsSettingDTO> GetMetricConfiguration(string hostName, string applicationName)
        {
            return _metricsRepository.GetAll(hostName, applicationName);
        }

        private BusSettingDTO GetBusConfiguration(string hostName, string applicationName)
        {
            return _rabbitMqRepository.Get(hostName, applicationName);
        }

    }
}
