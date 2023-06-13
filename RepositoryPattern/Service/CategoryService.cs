// <copyright file="CategoryService.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using AuctionProject.Models;
    using AuctionProject.Models.Validator;
    using AuctionProject.Repository;
    using log4net;

    /// <summary>
    /// Category Service.
    /// </summary>
    public class CategoryService
    {
        /// <summary>
        /// Defines the Log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(CategoryService));

        /// <summary>
        /// Initialization CategoryValidator.
        /// </summary>
        private static readonly CategoryValidator CategoryValidator = new CategoryValidator();

        /// <summary>
        /// Initialization UserRepository.
        /// </summary>
        private readonly CategoryRepository categoryRepository = new CategoryRepository();

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        public CategoryService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        /// <param name="categoryRepository">The category repository.</param>
        public CategoryService(CategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Check if the category is valid, if so add it.
        /// </summary>
        /// <param name="category">Category to add.</param>
        /// <returns>true or false.</returns>
        public bool AddCategory(Category category)
        {
            bool isValid = CategoryValidator.Validate(category);

            if (isValid)
            {
                Log.Info("The category is valid!");
                this.categoryRepository.Insert(category);
                Log.Info("The category was added to the database!");
                return true;
            }

            Log.Error("The category wasn't added to the database!");
            return false;
        }

        /// <summary>
        /// Update item in database.
        /// </summary>
        /// <param name="category">Category to update.</param>
        /// <returns>true or false.</returns>
        public bool Update(Category category)
        {
            bool isValid = CategoryValidator.Validate(category);

            if (isValid)
            {
                Log.Info("The category is valid!");
                this.categoryRepository.Update(category);
                Log.Info("The category was update");
                return true;
            }

            Log.Error("The category wasn't update");
            return false;
        }

        /// <summary>
        /// Get item with item.id=id from database.
        /// </summary>
        /// <param name="id">object id.</param>
        /// <returns>Category or null.</returns>
        public Category GetCategory(int id)
        {
            Category category = this.categoryRepository.GetByID(id);

            if (category == null)
            {
                Log.Info("The user wasn't found!");
            }
            else
            {
                Log.Info("The user was found!");
            }

            return category;
        }

        /// <summary>
        /// Check if user already exist.
        /// </summary>
        /// <param name="category">the user.</param>
        /// <returns>true or false.</returns>
        public bool Existing(Category category)
        {
            return this.categoryRepository.Existing(category);
        }

        /// <summary>
        /// Get Children.
        /// </summary>
        /// <param name="name">category name.</param>
        /// <returns>list with category.</returns>
        public List<Category> GetChildren(string name)
        {
            List<Category> categories = (List<Category>)this.categoryRepository.Get(filter: category => category.Name == name, includeProperties: "CategoryChildren");
            Category categoryDB = categories.FirstOrDefault();
            return (List<Category>)categoryDB.CategoryChildren;
        }

        /// <summary>
        /// Get products.
        /// </summary>
        /// <param name="name">category name.</param>
        /// <returns>list with product.</returns>
        public List<Product> GetProducts(string name)
        {
            List<Category> categories = (List<Category>)this.categoryRepository.Get(filter: category => category.Name == name, includeProperties: "Products");
            Category categoryDB = categories.FirstOrDefault();
            return (List<Product>)categoryDB.Products;
        }
    }
}
