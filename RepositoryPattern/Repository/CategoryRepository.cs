// <copyright file="CategoryRepository.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using AuctionProject.DataMapper;
    using AuctionProject.Interfaces.DataAccess;
    using AuctionProject.Models;
    using log4net;

    /// <summary>
    /// Repository for Category.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        /// <summary>
        /// Defines the Log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(CategoryRepository));

        /// <summary>
        /// My context.
        /// </summary>
        private readonly MyContext context;

        /// <summary>
        /// My database.
        /// </summary>
        private readonly DbSet<Category> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepository"/> class.
        /// </summary>
        public CategoryRepository()
        {
            this.context = new MyContext();
            this.dbSet = this.context.Set<Category>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepository"/> class.
        /// </summary>
        /// <param name="context">my context.</param>
        public CategoryRepository(MyContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<Category>();
        }

        /// <summary>
        /// Insert category first parents, children and last one the category.
        /// </summary>
        /// <param name="category">category to insert.</param>
        public override void Insert(Category category)
        {
            if (category.CategoryParents != null)
            {
                foreach (Category parent in category.CategoryParents)
                {
                    if (parent.Id == 0)
                    {
                        this.Insert(parent);
                    }
                }
            }

            if (category.CategoryChildren != null)
            {
                foreach (Category child in category.CategoryChildren)
                {
                    if (child.Id == 0)
                    {
                        this.Insert(child);
                    }
                }
            }

            Category existing = this.context.Categories.FirstOrDefault(e => e.Name == category.Name);
            if (existing == null)
            {
                this.dbSet.Add(category);
            }
            else
            {
                category.Id = existing.Id;
            }

            if (category.Products != null)
            {
                ProductRepository productRepository = new ProductRepository(this.context);
                foreach (Product product in category.Products)
                {
                    product.Category = category;
                    productRepository.Insert(product);
                }
            }

            this.context.SaveChanges();
        }

        /// <summary>
        /// Use Get with filter and includeProperties for get the category c where c.id=id.
        /// </summary>
        /// <param name="id">id of category.</param>
        /// <returns>the category or null.</returns>
        public Category GetByID(int id)
        {
            List<Category> categories = (List<Category>)this.Get(filter: c => c.Id == id, includeProperties: "CategoryChildren,CategoryParents,Products", myContext: this.context);
            return categories.FirstOrDefault();
        }

        /// <summary>
        /// Update item in database.
        /// </summary>
        /// <param name="item">Category to update.</param>
        public override void Update(Category item)
        {
            DbSet<Category> dbSet = this.context.Set<Category>();
            dbSet.Attach(item);

            this.context.SaveChanges();
        }

        /// <summary>
        /// Check if user already exist.
        /// </summary>
        /// <param name="category">the user.</param>
        /// <returns>true or false.</returns>
        public bool Existing(Category category)
        {
            Category existing = this.context.Categories.FirstOrDefault(e => e.Name == category.Name);
            if (existing == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get from database a list of objects that meet the specified conditions.
        /// </summary>
        /// <param name="filter"> my filter.</param>
        /// <param name="orderBy"> my order.</param>
        /// <param name="includeProperties"> other proprieties.</param>
        /// <param name="myContext">my context.</param>
        /// <returns> the list of objects that comply with the conditions.</returns>
        public override IEnumerable<Category> Get(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "", MyContext myContext = null)
        {
            return base.Get(filter, orderBy, includeProperties, this.context);
        }
    }
}
