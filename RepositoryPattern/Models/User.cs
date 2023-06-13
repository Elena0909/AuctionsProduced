// <copyright file="User.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using AuctionProject.Enum;
    using AuctionProject.Helper;

    /// <summary>
    /// Class User.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            Helper helper = new Helper();
            this.Score = helper.Score;
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
        /// Gets or sets the Name.
        /// </summary>
        [Required(ErrorMessage = "The name cannot be null")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The length must be between 3 and 100")]
        [Index(IsUnique = true)]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Role.
        /// </summary>
        [Required(ErrorMessage = "The role cannot be null")]
        public Role Role
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Score.
        /// </summary>
        [Required(ErrorMessage = "The score cannot be null")]
        public double Score
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Products.
        /// </summary>
        public ICollection<Product> Products
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Auctions.
        /// </summary>
        public ICollection<Auction> Auctions
        {
            get;
            set;
        }
    }
}
