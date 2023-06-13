// <copyright file="ProductValidator.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Models.Validator
{
    using System;
    using AuctionProject.Enum;
    using log4net;

    /// <summary>
    /// Class with validation for Product.
    /// </summary>
    internal class ProductValidator
    {
        /// <summary>
        /// Defines the Log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ProductValidator));

        /// <summary>
        /// Initialization CategoryValidator.
        /// </summary>
        private static readonly CategoryValidator CategoryValidator = new CategoryValidator();

        /// <summary>
        /// Initialization UserValidator.
        /// </summary>
        private static readonly UserValidator UserValidator = new UserValidator();

        // private static readonly UserService userService = new UserService();

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        /// <param name="product">Product to validate.</param>
        /// <returns>true or false.</returns>
        public bool ValidateInsert(Product product)
        {
            log4net.Config.XmlConfigurator.Configure();

            if (product == null)
            {
                Log.Error("Produuct can't be null");
                return false;
            }

            DateTime now = DateTime.Now;

            if (product.StartDateAction.CompareTo(now) < 0)
            {
                Log.Error("Can't start the auction for the product in past");
                return false;
            }

            return this.CheckName(product.Name) && this.CheckDescription(product.Description) &&
                this.CheckDate(product.StartDateAction, product.EndDateAction) && this.CheckCoins(product.Coins)
                && this.CheckPrice(product.Price) && this.CheckOwner(product.Owner) && this.CheckCategory(product.Category);
        }

        /// <summary>
        ///  Check if a product is valid.
        /// </summary>
        /// <param name="product">my product.</param>
        /// <returns>true or false.</returns>
        public bool Validate(Product product)
        {
            log4net.Config.XmlConfigurator.Configure();

            if (product == null)
            {
                Log.Error("Produuct can't be null");
                return false;
            }

            return this.CheckName(product.Name) && this.CheckDescription(product.Description) &&
                this.CheckDate(product.StartDateAction, product.EndDateAction) && this.CheckCoins(product.Coins)
                && this.CheckPrice(product.Price);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        /// <param name="product">Product to validate.</param>
        /// <returns>true or false.</returns>
        public bool ValidateWithoutCategory(Product product)
        {
            log4net.Config.XmlConfigurator.Configure();

            return this.CheckName(product.Name) && this.CheckDescription(product.Description) &&
                this.CheckDate(product.StartDateAction, product.EndDateAction) && this.CheckCoins(product.Coins)
                && this.CheckPrice(product.Price) && this.CheckOwner(product.Owner);
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

            if (name.Length < 2 || name.Length > 100)
            {
                Log.Error("The name's length must be between 2 and 100");
                return false;
            }

            Log.Info("Name is valid");
            return true;
        }

        /// <summary>
        /// Check description.
        /// </summary>
        /// <param name="description">object's description.</param>
        /// <returns>true or false.</returns>
        private bool CheckDescription(string description)
        {
            if (description == null)
            {
                Log.Error("Description is required");
                return false;
            }

            if (description.Length < 10 || description.Length > 200)
            {
                Log.Error("The sescription's length must be between 10 and 200");
                return false;
            }

            Log.Info("Description is valid");
            return true;
        }

        /// <summary>
        /// Check date.
        /// </summary>
        /// <param name="start">date start auction.</param>
        /// <param name="end">date end auction.</param>
        /// <returns>true or false.</returns>
        private bool CheckDate(DateTime start, DateTime end)
        {
            if (start == default)
            {
                Log.Error("The start date is required");
                return false;
            }

            if (end == default)
            {
                Log.Error("The end date is required");
                return false;
            }

            if (start.CompareTo(end) >= 0)
            {
                Log.Error("Start date shoud be befor end date");
                return false;
            }

            Log.Info("product's dates are valid");
            return true;
        }

        /// <summary>
        /// Check coins.
        /// </summary>
        /// <param name="coins">object's coins.</param>
        /// <returns>true or false.</returns>
        private bool CheckCoins(Coins coins)
        {
            return coins == Coins.Dollar || coins == Coins.Euro || coins == Coins.Ron || coins == Coins.Pound;
        }

        /// <summary>
        /// Check price.
        /// </summary>
        /// <param name="price">object's price.</param>
        /// <returns>true or false.</returns>
        private bool CheckPrice(double price)
        {
            if (price > 0)
            {
                Log.Info("Price is valid");
                return true;
            }

            Log.Error("Price is invalid");
            return false;
        }

        /// <summary>
        /// Check owner.
        /// </summary>
        /// <param name="user">object's owner.</param>
        /// <returns>true or false.</returns>
        private bool CheckOwner(User user)
        {
            return UserValidator.Validate(user) && user.Role == Role.Offerer;
        }

        /// <summary>
        /// Check category.
        /// </summary>
        /// <param name="category">object's category.</param>
        /// <returns>true or false.</returns>
        private bool CheckCategory(Category category)
        {
            return CategoryValidator.Validate(category);
        }
    }
}
