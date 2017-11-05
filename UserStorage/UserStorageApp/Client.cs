using System;
using UserStorageServices;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(UserStorageServiceMaster userStorageService = null, IUserRepositoryManager userRepositoryManager = null)
        {
            _userRepositoryManager = userRepositoryManager ?? new UserMemoryCacheWithState();
            _userStorageService = userStorageService ?? new UserStorageServiceMaster(userRepository: _userRepositoryManager as IUserRepository);
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the <see cref="UserStorageServiceBase"/> class.
        /// </summary>
        public void Run()
        {
            var slave1 = new UserStorageServiceSlave(userRepository: _userRepositoryManager as IUserRepository);
            var slave2 = new UserStorageServiceSlave(userRepository: _userRepositoryManager as IUserRepository);

            _userStorageService.AddSubscriber(slave1);
            _userStorageService.AddSubscriber(slave2);

            //_userRepositoryManager.Start();
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
            Console.WriteLine(slave1.Count);
            Console.WriteLine(slave2.Count);

            foreach (var user in _userStorageService.Search(x => x.FirstName != null))
            {
                Console.WriteLine($"Id: {user.Id}");
            }    

            _userRepositoryManager.Stop();
        }
    }
}
