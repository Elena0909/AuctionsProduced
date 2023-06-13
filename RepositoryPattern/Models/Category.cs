// <copyright file="Category.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class Category.
    /// </summary>
    public class Category
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
        /// Gets or sets the Products.
        /// </summary>
        public ICollection<Product> Products
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the CategoryParents.
        /// </summary>
        public ICollection<Category> CategoryParents
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the CategoryChildren.
        /// </summary>
        public ICollection<Category> CategoryChildren
        {
            get;
            set;
        }
    }
}
