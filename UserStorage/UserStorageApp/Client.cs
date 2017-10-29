using UserStorageServices;

namespace UserStorageApp
{
    /// <summary>
    /// Represents a client that uses an instance of the <see cref="UserStorageService"/>.
    /// </summary>
    public class Client
    {
        private readonly IUserStorageService _userStorageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserStorageService userStorageService = null)
        {
            _userStorageService = userStorageService ?? new UserStorageService();
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the <see cref="UserStorageService"/> class.
        /// </summary>
        public void Run()
        {
            _userStorageService.Add(new User
            {
                FirstName = "Alex",
                LastName = "Black",
                Age = 25
            });

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
