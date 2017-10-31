using UserStorageServices;

namespace UserStorageApp
{
    /// <summary>
    /// Represents a client that uses an instance of the <see cref="UserStorageServiceBase"/>.
    /// </summary>
    public class Client
    {
        private readonly IUserStorageService _userStorageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserStorageService userStorageService = null)
        {
            var slave1 = new UserStorageServiceSlave();
            var slave2 = new UserStorageServiceSlave();

            _userStorageService = userStorageService ?? new UserStorageServiceMaster(new[] { slave1, slave2 });
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the <see cref="UserStorageServiceBase"/> class.
        /// </summary>
        public void Run()
        {
            _userStorageService.Add(new User
            {
                FirstName = "Alex",
                LastName = "Black",
                Age = 25
            });

            int a = _userStorageService.Count;

            _userStorageService.Remove(new User
            {
                FirstName = "Alex",
                LastName = "Black",
                Age = 25
            });

            _userStorageService.Search(u => u.FirstName == "Alex");
        }
    }
}
