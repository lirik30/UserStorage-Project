using System;
using UserStorageServices;

namespace UserStorageApp
{
    /// <summary>
    /// Represents a client that uses an instance of the <see cref="UserStorageServiceBase"/>.
    /// </summary>
    public class Client
    {
        private readonly UserStorageServiceMaster _userStorageService;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserStorageService userStorageService = null, IUserRepository userRepository = null)
        {
            //var slave1 = new UserStorageServiceSlave();
            //var slave2 = new UserStorageServiceSlave();

            _userRepository = userRepository ?? new UserMemoryCacheWithState();
            _userStorageService = (UserStorageServiceMaster)userStorageService ?? new UserStorageServiceMaster(userRepository: _userRepository);
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the <see cref="UserStorageServiceBase"/> class.
        /// </summary>
        public void Run()
        {
            var slave1 = new UserStorageServiceSlave(userRepository: _userRepository);
            var slave2 = new UserStorageServiceSlave(userRepository: _userRepository);

            _userStorageService.AddSubscriber(slave1);
            _userStorageService.AddSubscriber(slave2);

            //_userRepository.Start();
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

            _userRepository.Stop();
        }
    }
}
