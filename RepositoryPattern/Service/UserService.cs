// <copyright file="UserService.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Service
{
    using AuctionProject.Models;
    using AuctionProject.Models.Validator;
    using AuctionProject.Repository;
    using log4net;

    /// <summary>
    /// User Service.
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// Defines the Log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(UserService));

        /// <summary>
        /// Initialization UserRepository.
        /// </summary>
        private readonly UserRespository userRepository = new UserRespository();

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        public UserService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRespository">The user repository.</param>
        public UserService(UserRespository userRespository)
        {
            this.userRepository = userRespository;
        }

        /// <summary>
        /// Add a User in database.
        /// </summary>
        /// <param name="user">User to add.</param>
        /// <returns>true or false.</returns>
        public bool AddUser(User user)
        {
            UserValidator userValidator = new UserValidator();
            bool isValid = userValidator.Validate(user);

            if (isValid)
            {
                Log.Info("The user is valid!");
                this.userRepository.Insert(user);
                Log.Info("The user was added to the database!");
                return true;
            }

            Log.Error("The auction wasn't added to the database!");
            return false;
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">id user.</param>
        /// <returns>user or null.</returns>
        public User GetUser(int id)
        {
            User user = this.userRepository.GetByID(id);

            if (user == null)
            {
                Log.Info("The user wasn't found!");
            }
            else
            {
                Log.Info("The user was found!");
            }

            return user;
        }

        /// <summary>
        /// Update a User.
        /// </summary>
        /// <param name="user">User to insert.</param>
        /// <returns>true or false.</returns>
        public bool UpdateUser(User user)
        {
            UserValidator userValidator = new UserValidator();
            bool isValid = userValidator.Validate(user);

            if (!isValid)
            {
                return false;
            }

            User userDB = this.userRepository.GetByID(user.Id);
            if (userDB == null)
            {
                Log.Info("The user wan't found!");
                return false;
            }

            this.userRepository.Update(user);
            Log.Info("The user was updated!");
            return true;
        }

        /// <summary>
        /// Check if user already exist.
        /// </summary>
        /// <param name="user">the user.</param>
        /// <returns>true or false.</returns>
        public bool Existing(User user)
        {
            return this.userRepository.Existing(user);
        }
    }
}
