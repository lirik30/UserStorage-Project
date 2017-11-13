using System;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UserStorageServices;
using UserStorageServices.Notifications;
using UserStorageServices.Repositories;
using UserStorageServices.Services;
using UserStorageServices.Validators;
using ServiceConfiguration = ServiceConfigurationSection.ServiceConfigurationSection;

namespace UserStorageApp
{
    /// <summary>
    /// Represents a client that uses an instance of the <see cref="UserStorageServiceBase"/>.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Master node
        /// </summary>
        private readonly UserStorageServiceMaster _userStorageService;

        /// <summary>
        /// Repository manager
        /// </summary>
        private readonly IUserRepositoryManager _userRepositoryManager;

        private readonly NotificationReceiver _receiver = new NotificationReceiver();

        private UserStorageServiceSlave[] slaves;

        private void CreateSlaveServices()
        {
            var serviceConfiguration = (ServiceConfiguration)System.Configuration.ConfigurationManager.GetSection("serviceConfiguration");
            var masterService = serviceConfiguration.ServiceInstances.SingleOrDefault(x => x.Type == "UserStorageMaster");
            var slaveCount = masterService.Master.Count;
            slaves = new UserStorageServiceSlave[slaveCount];
            int i = 0;
            foreach (var slave in masterService.Master)
            {
                slaves[i++] = CreateSlave(slave.Name, _receiver);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserStorageService userStorageService = null, IUserRepositoryManager userRepositoryManager = null)
        {
            _userRepositoryManager = userRepositoryManager ?? new UserMemoryCacheWithState();

            var sender = new NotificationSender(_receiver);

            //slaves[0] = CreateSlave(1, _receiver);
            //slaves[1] = CreateSlave(2, _receiver);
            CreateSlaveServices();

            _userStorageService = userStorageService as UserStorageServiceMaster ?? 
                CreateMaster( userRepository: _userRepositoryManager as IUserRepository, sender: sender);
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the <see cref="UserStorageServiceBase"/> class.
        /// </summary>
        public void Run()
        {   
            _userStorageService.Add(new User
            {
                FirstName = "Alex",
                LastName = "BLack",
                Age = 25
            });
            _userStorageService.Add(new User
            {
                FirstName = "Ivan",
                LastName = "Ivanov", 
                Age = 16
            });
            _userStorageService.Add(new User
            {
                FirstName = "Troll",
                LastName = "Trollson",
                Age = 19
            });

            Console.WriteLine(_userStorageService.Count);
            Console.WriteLine(slaves[0].Count);
            Console.WriteLine(slaves[1].Count);

            foreach (var user in _userStorageService.Search(x => x.FirstName != null))
            {
                Console.WriteLine($"Id: {user.Id}");
            }    
        }

        private UserStorageServiceMaster CreateMaster(
            INotificationSender sender = null,
            IUserRepository userRepository = null,
            IUserValidator validator = null)
        {
            var serviceConfiguration = (ServiceConfiguration)System.Configuration.ConfigurationManager.GetSection("serviceConfiguration");
            var masterService = serviceConfiguration.ServiceInstances.SingleOrDefault(x => x.Type == "UserStorageMaster");
            var domainName = masterService.Name;
            var domain = AppDomain.CreateDomain(domainName);
            var master = domain.CreateInstanceAndUnwrap(
                typeof(UserStorageServiceMaster).Assembly.FullName,
                typeof(UserStorageServiceMaster).FullName,
                true,
                BindingFlags.CreateInstance, 
                null,
                new object[] { sender, userRepository, validator },
                null,
                null);

            if (master == null)
            {
                throw new ArgumentException("Fail in master node creation");
            }

            return (UserStorageServiceMaster)master;
        }

        private UserStorageServiceSlave CreateSlave(
            string domainName,
            INotificationReceiver receiver = null,
            IUserValidator validator = null,
            IUserRepository userRepository = null)
        {
            var domain = AppDomain.CreateDomain(domainName);
            var slave = domain.CreateInstanceAndUnwrap(
                typeof(UserStorageServiceSlave).Assembly.FullName,
                typeof(UserStorageServiceSlave).FullName,
                true,
                BindingFlags.CreateInstance, 
                null,
                new object[] { receiver, validator, userRepository },
                null,
                null);

            if (slave == null)
            {
                throw new ArgumentException("Fail in slave node creation");
            }

            return (UserStorageServiceSlave)slave;
        }
    }
}
