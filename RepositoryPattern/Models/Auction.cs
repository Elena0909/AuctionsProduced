// <copyright file="Auction.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AuctionProject.Enum;

    /// <summary>
    /// Class Auction.
    /// </summary>
    public class Auction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Auction"/> class.
        /// </summary>
        public Auction()
        {
            this.Date = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Date.
        /// </summary>
        [Required(ErrorMessage = "The date time cannot be null")]
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Bidder.
        /// </summary>
        [Required(ErrorMessage = "The bidder cannot be null")]
        public User Bidder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Product.
        /// </summary>
        [Required(ErrorMessage = "The product cannot be null")]
        public Product Product
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Coins.
        /// </summary>
        [Required]
        public Coins Coins
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Price.
        /// </summary>
        [Required]
        public double Price
        {
            get;
            set;
        }
    }
}
