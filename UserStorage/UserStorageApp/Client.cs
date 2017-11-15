using System;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UserStorageServices;
using UserStorageServices.Attributes.ServicesAttributes;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserStorageService userStorageService = null, IUserRepositoryManager userRepositoryManager = null)
        {
            _userRepositoryManager = userRepositoryManager ?? new UserMemoryCacheWithState();
            
            _userStorageService = userStorageService as UserStorageServiceMaster ??
                                  ServiceFactory.CreateService(_userRepositoryManager as IUserRepository) as UserStorageServiceMaster;
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

            foreach (var user in _userStorageService.Search(x => x.FirstName != null))
            {
                Console.WriteLine($"Id: {user.Id}");
            }    
        }
    }
}
