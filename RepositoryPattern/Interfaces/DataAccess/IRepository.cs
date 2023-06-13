// <copyright file="IRepository.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Interfaces.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using AuctionProject.DataMapper;

    /// <summary>
    /// Interface for Repo.
    /// </summary>
    /// <typeparam name="T"><see cref="Category"/>,<see cref="User"/>,<see cref="Product"/>,<see cref="Auction"/>.</typeparam>
    internal interface IRepository<T>
    {
        /// <summary>
        /// Insert entity in database.
        /// </summary>
        /// <param name="entity">entity from models.</param>
        void Insert(T entity);

        /// <summary>
        /// Update item in database.
        /// </summary>
        /// <param name="item">entity from models.</param>
        void Update(T item);

        /// <summary>
        /// Get item with item.id=id from database.
        /// </summary>
        /// <param name="id">object id.</param>
        /// <returns>entity from models.</returns>
        T GetByID(object id);

        /// <summary>
        /// Get from database a list of objects that meet the specified conditions.
        /// </summary>
        /// <param name="filter">specified conditions.</param>
        /// <param name="orderBy">order to sort.</param>
        /// <param name="includeProperties">other proprieties.</param>
        /// <param name="myContext">my context.</param>
        /// <returns>the list of objects that comply with the conditions.</returns>
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            MyContext myContext = null);
    }
}
