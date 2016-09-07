using Autofac;
using HostApp.Core.Constants;
using HostApp.Core.Interfaces;
using HostApp.Core.Repository;
using log4net;
using HostApp.Core.Utility;
using PersonService.DTO;
using PersonService.Interfaces;
using PersonService.Repository;

namespace PersonService.BootStrap
{
    public class AutoFac : IConfigureIOC
    {
        private ILog _log;

        public AutoFac()
        {
            _log = LogManager.GetLogger("PersonService");
            _log.Debug("AutoFac Created!");

        }

        public void Configure(ContainerBuilder builder, object appConfig)
        {

            _log.Debug("Starting Configure IOC Ping");
            var config = (IRestServiceConfiguration)appConfig;

            builder.Register(c => LogManager.GetLogger("PersonService")).As<ILog>();

            builder.Register(
                    c => new ContextMongoDB<PersonDTO>(config.MongoDBConnection, MongoConstants.PersonDatabase))
                    .As<IContextMongoDB<PersonDTO>>();

            builder.RegisterType<PersonSQLRepository>().As<IPersonRepository>();
            //builder.RegisterType<PersonMongoRepository>().As<IPersonRepository>();

            AutofacHelper.RegisterDLL(builder, "PersonService.dll", "Logic");

            _log.Debug("Finished Configure IOC PersonService");
        }

    }
}
