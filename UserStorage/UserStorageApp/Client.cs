using System;
using UserStorageServices;
using UserStorageServices.Notifications;
using UserStorageServices.Repositories;
using UserStorageServices.Services;

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

        private readonly UserStorageServiceSlave[] slaves = new UserStorageServiceSlave[2];

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(UserStorageServiceMaster userStorageService = null, IUserRepositoryManager userRepositoryManager = null)
        {
            _userRepositoryManager = userRepositoryManager ?? new UserMemoryCacheWithState();

            var sender = new NotificationSender(_receiver);

            slaves[0] = new UserStorageServiceSlave(receiver: _receiver);
            slaves[1] = new UserStorageServiceSlave(receiver: _receiver);
            _userStorageService = userStorageService ?? new UserStorageServiceMaster(userRepository: _userRepositoryManager as IUserRepository, sender: sender);
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
    }
}
