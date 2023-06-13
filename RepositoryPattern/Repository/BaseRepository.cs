// <copyright file="BaseRepository.cs" company="Transilvania University of Brasov">
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

    /// <summary>
    /// Class BaseRepository, main implementation for methods:
    /// <see cref="Get"/>, <see cref="Inset(T)"/>, <see cref="Update(T)"/>, <see cref="Delete(object)"/>, <see cref="Delete(T)"/>,
    /// <see cref="Get(System.Linq.Expressions.Expression{System.Func{T, bool}}, System.Func{System.Linq.IQueryable{T}, System.Linq.IOrderedQueryable{T}}, string)"/>.
    /// </summary>
    /// <typeparam name="T"><see cref="Category"/>,<see cref="User"/>,<see cref="Product"/>,<see cref="Auction"/>.</typeparam>
    [ExcludeFromCodeCoverage]
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        /// <summary>
        /// Get from database a list of objects that meet the specified conditions.
        /// </summary>
        /// <param name="filter"> my filter.</param>
        /// <param name="orderBy"> my order.</param>
        /// <param name="includeProperties"> other proprieties.</param>
        /// <param name="myContext">my context.</param>
        /// <returns> the list of objects that comply with the conditions.</returns>
        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            MyContext myContext = null)
        {
            if (myContext == null)
            {
                myContext = new MyContext();
            }

            using (myContext)
            {
                DbSet<T> dbSet = myContext.Set<T>();

                IQueryable<T> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (string includeProperty in includeProperties.Split(
                    new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
        }

        /// <summary>
        /// Insert entity in database.
        /// </summary>
        /// <param name="entity">entity from models.</param>
        public virtual void Insert(T entity)
        {
            using (MyContext myContext = new MyContext())
            {
                DbSet<T> dbSet = myContext.Set<T>();
                dbSet.Add(entity);

                myContext.SaveChanges();
            }
        }

        /// <summary>
        /// Update item in database.
        /// </summary>
        /// <param name="item">entity from models.</param>
        public virtual void Update(T item)
        {
            using (MyContext myContext = new MyContext())
            {
                DbSet<T> dbSet = myContext.Set<T>();
                dbSet.Attach(item);
                myContext.Entry(item).State = EntityState.Modified;

                myContext.SaveChanges();
            }
        }

        /// <summary>
        /// Delete from database.
        /// </summary>
        /// <param name="id">object id.</param>
        public virtual void Delete(object id)
        {
            this.Delete(this.GetByID(id));
        }

        /// <summary>
        /// Get item with item.id=id from database.
        /// </summary>
        /// <param name="id">type object.</param>
        /// <returns>entity from models.</returns>
        public virtual T GetByID(object id)
        {
            using (MyContext myContext = new MyContext())
            {
                return myContext.Set<T>().Find(id);
            }
        }
    }
}
