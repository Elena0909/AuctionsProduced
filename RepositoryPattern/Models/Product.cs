// <copyright file="Product.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using AuctionProject.Enum;

    /// <summary>
    /// Class Product.
    /// </summary>
    public class Product
    {
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
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 100")]
        [Index(IsUnique = true)]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        [Required(ErrorMessage = "The description cannot be null")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "The length must be between 10 and 200")]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Owner.
        /// </summary>
        [Required]
        public User Owner
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the StartDateAction.
        /// </summary>
        [Required(ErrorMessage = "The start date cannot be null")]
        public DateTime StartDateAction
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the EndDateAction.
        /// </summary>
        [Required(ErrorMessage = "The end date cannot be null")]
        public DateTime EndDateAction
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the EndDateAction.
        /// </summary>
        [Required]
        public double Price
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Category.
        /// </summary>
        [Required]
        public Category Category
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
        /// Gets or sets a value indicating whether auction for this product is active.
        /// </summary>
        [Required]
        public bool Active
        {
            get;
            set;
        }

        /// <summary>
        /// check if the auction is active.
        /// </summary>
        /// <returns>true or false.</returns>
        public bool IsActive()
        {
            if (this.Active)
            {
                DateTime now = DateTime.Now;
                if (now.CompareTo(this.StartDateAction) < 0 || now.CompareTo(this.EndDateAction) >= 0)
                {
                    this.Active = false;
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
