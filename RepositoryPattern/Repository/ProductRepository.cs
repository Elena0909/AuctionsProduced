// <copyright file="ProductRepository.cs" company="Transilvania University of Brasov">
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
    /// Repository for Product.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        /// <summary>
        /// Defines the Log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ProductRepository));

        /// <summary>
        /// The auction database.
        /// </summary>
        private MyContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        public ProductRepository()
        {
            this.context = new MyContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        ///  Initializes a new instance.
        /// </summary>
        /// <param name="myContext">My context.</param>
        public ProductRepository(MyContext myContext)
        {
            this.context = myContext;
        }

        /// <summary>
        /// Insert product in database, check name unique.
        /// </summary>
        /// <param name="entity">product to insert.</param>
        public override void Insert(Product entity)
        {
            Product existing = this.context.Products.FirstOrDefault(e => e.Name == entity.Name);
            if (existing == null)
            {
                DbSet<Product> dbSet = this.context.Set<Product>();
                dbSet.Add(entity);

                this.context.SaveChanges();
            }
        }

        /// <summary>
        /// Get item with item.id=id from database.
        /// </summary>
        /// <param name="id">object id.</param>
        /// <returns>product from database.</returns>
        public override Product GetByID(object id)
        {
            return this.context.Set<Product>().Find(id);
        }

        /// <summary>
        /// Update item in database.
        /// </summary>
        /// <param name="product">product to update.</param>
        public override void Update(Product product)
        {
            using (this.context)
            {
                DbSet<Product> dbSet = this.context.Set<Product>();
                dbSet.Attach(product);

                this.context.SaveChanges();
            }
        }

        /// <summary>
        /// Get from database a list of objects that meet the specified conditions.
        /// </summary>
        /// <param name="filter"> my filter.</param>
        /// <param name="orderBy"> my order.</param>
        /// <param name="includeProperties"> other proprieties.</param>
        /// <param name="myContext">my context.</param>
        /// <returns> the list of objects that comply with the conditions.</returns>
        public override IEnumerable<Product> Get(Expression<Func<Product, bool>> filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, string includeProperties = "", MyContext myContext = null)
        {
            return base.Get(filter, orderBy, includeProperties, this.context);
        }
    }
}
