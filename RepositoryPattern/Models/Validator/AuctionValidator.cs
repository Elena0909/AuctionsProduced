// <copyright file="AuctionValidator.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Models.Validator
{
    using AuctionProject.Enum;
    using log4net;

    /// <summary>
    /// Class with validation for Auction.
    /// </summary>
    internal class AuctionValidator
    {
        /// <summary>
        /// Defines the Log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(AuctionValidator));

        /// <summary>
        /// Initialization UserValidator.
        /// </summary>
        private static readonly UserValidator UserValidator = new UserValidator();

        /// <summary>
        /// Initialization ProductValidator.
        /// </summary>
        private static readonly ProductValidator ProductValidator = new ProductValidator();

        /// <summary>
        /// Check auction.
        /// </summary>
        /// <param name="auction">The auction.</param>
        /// <returns>true or false.</returns>
        public bool Validate(Auction auction)
        {
            if (auction == null)
            {
                Log.Error("Auction can't be null");
                return false;
            }

            return this.CheckBidder(auction.Bidder) && this.CheckProduct(auction.Product) &&
                this.CheckCoins(auction) && this.CheckDate(auction) && this.CheckPrice(auction) && this.CheckAvtiveProduct(auction.Product);
        }

        /// <summary>
        /// Check User valid.
        /// </summary>
        /// <param name="user">The bidder.</param>
        /// <returns>true or false.</returns>
        private bool CheckBidder(User user)
        {
            return UserValidator.Validate(user) && user.Role == Role.Bidder;
        }

        /// <summary>
        /// Check product.
        /// </summary>
        /// <param name="product">the product.</param>
        /// <returns>true or false.</returns>
        private bool CheckProduct(Product product)
        {
            return ProductValidator.Validate(product);
        }

        /// <summary>
        /// Check if the auction is due.
        /// </summary>
        /// <param name="auction">tThe auction.</param>
        /// <returns>true or false.</returns>
        private bool CheckDate(Auction auction)
        {
            if (auction.Date == default)
            {
                Log.Error("The date is required");
                return false;
            }

            if (auction.Date.CompareTo(auction.Product.EndDateAction) >= 0)
            {
                Log.Error("The auction is over, you late");
                return false;
            }

            if (auction.Date.CompareTo(auction.Product.StartDateAction) < 0)
            {
                Log.Error("The auction has not started yet");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check auction coins.
        /// </summary>
        /// <param name="auction">The auction.</param>
        /// <returns>true or false.</returns>
        private bool CheckCoins(Auction auction)
        {
            if (auction.Product.Coins != auction.Coins)
            {
                Log.Error("The auction coins is invalid!");
                return false;
            }

            Log.Error("The auction coins is valid!");
            return true;
        }

        /// <summary>
        /// Check price.
        /// </summary>
        /// <param name="auction">the action.</param>
        /// <returns>true or false.</returns>
        private bool CheckPrice(Auction auction)
        {
            if (auction.Price <= 0)
            {
                Log.Error("Price is invalid");
                return false;
            }

            if (auction.Price <= auction.Product.Price)
            {
                Log.Error("You have to bid more");
                return false;
            }

            if (auction.Price >= 3 * auction.Product.Price)
            {
                Log.Error("You bid too high");
                return false;
            }

            Log.Info("Price is valide");
            return true;
        }

        /// <summary>
        /// Check if the auction is due.
        /// </summary>
        /// <param name="product">my product.</param>
        /// <returns>true or false.</returns>
        private bool CheckAvtiveProduct(Product product)
        {
            if (product.IsActive())
            {
                Log.Info("Bid for product active.");
                return true;
            }

            Log.Error("Bid for product finised!");
            return false;
        }
    }
}
