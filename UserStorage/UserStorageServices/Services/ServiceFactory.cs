using System;
using System.Linq;
using System.Reflection;
using UserStorageServices.Attributes.ServicesAttributes;
using UserStorageServices.Notifications;
using UserStorageServices.Repositories;
using UserStorageServices.Validators;
using ServiceConfiguration = ServiceConfigurationSection.ServiceConfigurationSection;

namespace UserStorageServices.Services
{
    public static class ServiceFactory
    {
        private static INotificationReceiver _receiver = new NotificationReceiver();
        private static UserStorageServiceSlave[] _slaves;

        public static IUserStorageService CreateService(IUserRepository repository)
        {
            var sender = new NotificationSender(_receiver);

            CreateSlaveServices();

            return CreateMaster(userRepository: repository, sender: sender);
        }

        private static UserStorageServiceMaster CreateMaster(
            INotificationSender sender = null,
            IUserRepository userRepository = null,
            IUserValidator validator = null)
        {
            var applicationAttr = typeof(UserStorageServiceMaster).GetCustomAttributes<ApplicationServiceAttribute>().FirstOrDefault();
            var serviceType = applicationAttr?.ServiceType;

            var serviceConfiguration = (ServiceConfiguration)System.Configuration.ConfigurationManager.GetSection("serviceConfiguration");
            var masterService = serviceConfiguration.ServiceInstances.SingleOrDefault(x => x.Type == serviceType);
            var domainName = masterService.Name;
            var domain = AppDomain.CreateDomain(domainName);
            var master = Activator.CreateInstance(
                domain,
                typeof(UserStorageServiceMaster).Assembly.FullName,
                typeof(UserStorageServiceMaster).FullName,
                true,
                BindingFlags.CreateInstance,
                null,
                new object[] { sender, userRepository, validator },
                null,
                null).Unwrap() as UserStorageServiceMaster;
            if (master == null)
            {
                throw new ArgumentException("Fail in master node creation");
            }

            return master;
        }

        private static void CreateSlaveServices()
        {
            var applicationAttr = typeof(UserStorageServiceMaster).GetCustomAttributes<ApplicationServiceAttribute>().FirstOrDefault();
            var serviceType = applicationAttr?.ServiceType;
            var serviceConfiguration = (ServiceConfiguration)System.Configuration.ConfigurationManager.GetSection("serviceConfiguration");
            var masterService = serviceConfiguration.ServiceInstances.SingleOrDefault(x => x.Type == serviceType);
            var slaveCount = masterService.Master.Count;
            _slaves = new UserStorageServiceSlave[slaveCount];
            int i = 0;
            foreach (var slave in masterService.Master)
            {
                _slaves[i++] = CreateSlave(slave.Name, _receiver);
            }
        }

        private static UserStorageServiceSlave CreateSlave(
            string domainName,
            INotificationReceiver receiver = null,
            IUserValidator validator = null,
            IUserRepository userRepository = null)
        {
            var domain = AppDomain.CreateDomain(domainName);

            var slave = Activator.CreateInstance(
                domain,
                typeof(UserStorageServiceSlave).Assembly.FullName,
                typeof(UserStorageServiceSlave).FullName,
                true,
                BindingFlags.CreateInstance,
                null,
                new object[] { receiver, validator, userRepository },
                null,
                null).Unwrap() as UserStorageServiceSlave;
            if (slave == null)
            {
                throw new ArgumentException("Fail in slave node creation");
            }

            return slave;
        }
    }
}
