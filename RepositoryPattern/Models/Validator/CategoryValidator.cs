// <copyright file="CategoryValidator.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Models.Validator
{
    using System.Linq;
    using log4net;

    /// <summary>
    /// Class with validation for Category.
    /// </summary>
    internal class CategoryValidator
    {
        /// <summary>
        /// Defines the Log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(CategoryValidator));

        /// <summary>
        /// Check if category is valid.
        /// </summary>
        /// <param name="category">Category to validate.</param>
        /// <returns>true or false.</returns>
        public bool Validate(Category category)
        {
            if (category == null)
            {
                Log.Error("Category can't be null");
                return false;
            }

            if (category.Name == null)
            {
                Log.Error("Name is required");
                return false;
            }

            if (category.Name.Length < 2 || category.Name.Length > 100)
            {
                Log.Error("The name's length must be between 2 and 100");
                return false;
            }

            if (!category.Name.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                Log.Info("the name cannot contain symbols or numbers!");
                return false;
            }

            if (category.CategoryParents != null)
            {
                foreach (Category parentCategory in category.CategoryParents)
                {
                    if (!this.Validate(parentCategory))
                    {
                        Log.Error("The category's parent is invalid");
                        return false;
                    }
                }
            }

            if (category.CategoryChildren != null)
            {
                foreach (Category child in category.CategoryChildren)
                {
                    if (!this.Validate(child))
                    {
                        Log.Error("The category's children is invalid");
                        return false;
                    }
                }
            }

            if (category.Products != null)
            {
                foreach (Product product in category.Products)
                {
                    ProductValidator productValidator = new ProductValidator();
                    if (!productValidator.ValidateWithoutCategory(product))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
