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
        //private readonly ISqlConnectionRepository _sqlConnectionRepository;
        private readonly IMetricsRepository _metricsRepository;
        //private readonly ILogSettingsRepository _logSettingsRepository;
        private readonly IBusConfigurationRepository _rabbitMqRepository;
        //private readonly ICacheSettingRepository _cacheSettingRepository;

        public ApplicationConfigLogic(IApplicationRepository applicationRepository,//, ISqlConnectionRepository sqlConnectionRepository,
            IMetricsRepository metricsRepository, //ILogSettingsRepository logSettingsRepository, 
            IBusConfigurationRepository rabbitMqRepository)//,
            //ICacheSettingRepository cacheSettingRepository)
        {
            _applicationRepository = applicationRepository;
            //_sqlConnectionRepository = sqlConnectionRepository;
            _metricsRepository = metricsRepository;
            //_logSettingsRepository = logSettingsRepository;
            _rabbitMqRepository = rabbitMqRepository;
            //_cacheSettingRepository = cacheSettingRepository;
        }

        public ApplicationConfigLogic(string mongodbConnectionString, string dbName)
        {
            _applicationRepository = new ApplicationMongoDBRepository(mongodbConnectionString, dbName);
            //_sqlConnectionRepository = new SqlConnectionRepository(new ContextMongoDB<SQLConnectionDTO>(mongodbConnectionString, dbName));
            _metricsRepository = new MetricsMongDBRepository(new ContextMongoDB<MetricsSettingDTO>(mongodbConnectionString, dbName));
            //_logSettingsRepository = new LogSettingsRepository(new ContextMongoDB<LogSettingDTO>(mongodbConnectionString, dbName));
            _rabbitMqRepository = new BusSettingsMongoDBRepository(new ContextMongoDB<BusSettingDTO>(mongodbConnectionString, dbName));
            //_cacheSettingRepository = new CacheSettingRepository(new ContextMongoDB<CacheConfigurationDTO>(mongodbConnectionString, dbName));
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

        //public async Task<IIdentityApiConfiguration> GetIdentityApiAppConfig(string connectionString, string hostName, string applicationName)
        //{
        //    var config = await _applicationRepository.GetIdentityApiAppConfig(hostName, applicationName);

        //    await UpdateApplicationConfiguration(config, connectionString, hostName, applicationName);

        //    return config;
        //}

        //public async Task<IIdentityWebConfiguration> GetIdentityWebAppConfig(string connectionString, string hostName, string applicationName)
        //{
        //    var config = await _applicationRepository.GetIdentityWebAppConfig(hostName, applicationName);

        //    await UpdateApplicationConfiguration(config, connectionString, hostName, applicationName);

        //    return config;
        //}

        //public async Task<IOrchestrationConfiguration> GetOrchestrationAppConfig(string connectionString, string hostName, string applicationName)
        //{
        //    var config = await _applicationRepository.GetOrchestrationAppConfig(hostName, applicationName);

        //    await UpdateApplicationConfiguration(config, connectionString, hostName, applicationName);

        //    if (config.busConfiguration == null)
        //    {
        //        config.busConfiguration = await GetBusConfiguration(hostName, applicationName);
        //    }

        //    return config;
        //}

        private void UpdateApplicationConfiguration(IApplicationConfiguration config, string connectionString, string hostName, string applicationName)
        {
            //config.LogConfiguration = GetLogConfiguration(hostName, applicationName).Result;
            config.MetricConfiguration = GetMetricConfiguration(hostName, applicationName);

            //if (config.cacheConfiguration == null || !config.cacheConfiguration.Any())
            //{
            //    config.cacheConfiguration = await GetCacheConfiguration(hostName, applicationName);

            //}
            config.MongoDBConnection = connectionString;
            config.ApplicationName = applicationName;
            config.HostName = hostName;
            //config.SQLConnection = await GetDatabaseConnections();
        }

        //private async Task<IEnumerable<SQLConnectionDTO>> GetDatabaseConnections()
        //{
        //    return (await _sqlConnectionRepository.GetAll()).ToArray();
        //}

        private IEnumerable<MetricsSettingDTO> GetMetricConfiguration(string hostName, string applicationName)
        {
            return _metricsRepository.GetAll(hostName, applicationName);
        }
        //private async Task<IEnumerable<CacheConfigurationDTO>> GetCacheConfiguration(string hostName, string applicationName)
        //{
        //    return (await _cacheSettingRepository.GetAll(hostName, applicationName)).ToArray();
        //}


        private BusSettingDTO GetBusConfiguration(string hostName, string applicationName)
        {
            return _rabbitMqRepository.Get(hostName, applicationName);
        }

        //private async Task<LogSettingDTO> GetLogConfiguration(string hostName, string applicationName)
        //{
        //    return await _logSettingsRepository.Get(hostName, applicationName);
        //}
    }
}
