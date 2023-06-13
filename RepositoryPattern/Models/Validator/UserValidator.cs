// <copyright file="UserValidator.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Models.Validator
{
    using System;
    using System.Linq;
    using AuctionProject.Enum;
    using log4net;

    /// <summary>
    /// Class with validation for User.
    /// </summary>
    internal class UserValidator
    {
        /// <summary>
        /// Defines the Log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(UserValidator));

        /// <summary>
        /// Check if user is valid.
        /// </summary>
        /// <param name="user">User to validate.</param>
        /// <returns>true or false.</returns>
        public bool Validate(User user)
        {
            if (user == null)
            {
                Log.Error("User can't be null");
                return false;
            }

            return this.CheckName(user.Name) && this.CheckScore(user.Score) && this.CheckRole(user.Role);
        }

        /// <summary>
        /// Check name.
        /// </summary>
        /// <param name="name">object's name.</param>
        /// <returns>true or false.</returns>
        private bool CheckName(string name)
        {
            if (name == null)
            {
                Log.Error("Name is required");
                return false;
            }

            if (name.Length < 3 || name.Length > 100)
            {
                Log.Error("The name's length must be between 3 and 100");
                return false;
            }

            if (!name.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                Log.Info("the name cannot contain symbols or numbers!");
                return false;
            }

            foreach (string nameSplit in name.Split(
                   new char[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (char.IsLower(nameSplit.First()))
                {
                    Log.Error("The name can not start with a lower case letter!");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check role.
        /// </summary>
        /// <param name="role">object's role.</param>
        /// <returns>true or false.</returns>
        private bool CheckRole(Role role)
        {
            return role == Role.Bidder || role == Role.Offerer;
        }

        /// <summary>
        /// Check score.
        /// </summary>
        /// <param name="score">object's score.</param>
        /// <returns>true or false.</returns>
        private bool CheckScore(double score)
        {
            if (score < 0)
            {
                Log.Error("Score can't be negative");
                return false;
            }

            return true;
        }
    }
}
