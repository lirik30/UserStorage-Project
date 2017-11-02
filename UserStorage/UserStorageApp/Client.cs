using System;
using UserStorageServices;

namespace UserStorageApp
{
    /// <summary>
    /// Represents a client that uses an instance of the <see cref="UserStorageServiceBase"/>.
    /// </summary>
    public class Client
    {
        private readonly IUserStorageService _userStorageService;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserStorageService userStorageService = null, IUserRepository userRepository = null)
        {
            var slave1 = new UserStorageServiceSlave();
            var slave2 = new UserStorageServiceSlave();

            _userRepository = userRepository ?? new UserMemoryCacheWithState();
            _userStorageService = userStorageService ?? new UserStorageServiceMaster(new[] { slave1, slave2 }, _userRepository);
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the <see cref="UserStorageServiceBase"/> class.
        /// </summary>
        public void Run()
        {
            _userRepository.Start();
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

            foreach (var user in _userStorageService.Search(x => x.FirstName != null))
            {
                Console.WriteLine($"First name: {user.FirstName}");
                Console.WriteLine($"Last name: {user.LastName}");
                Console.WriteLine($"Age: {user.Age}");
            }

            _userRepository.Stop();
        }
    }
}
