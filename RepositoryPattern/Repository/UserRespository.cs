// <copyright file="UserRespository.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Repository
{
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using AuctionProject.DataMapper;
    using AuctionProject.Interfaces.DataAccess;
    using AuctionProject.Models;

    /// <summary>
    /// Repository for User.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UserRespository : BaseRepository<User>, IUserRepository
    {
        /// <summary>
        /// The auction database.
        /// </summary>
        private readonly MyContext context = new MyContext();

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRespository"/> class.
        /// </summary>
        public UserRespository()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRespository"/> class.
        ///  Initializes a new instance.
        /// </summary>
        /// <param name="myContext">My context.</param>
        public UserRespository(MyContext myContext)
        {
            this.context = myContext;
        }

        /// <summary>
        /// Insert user in database, check name unique.
        /// </summary>
        /// <param name="entity">User to insert.</param>
        public override void Insert(User entity)
        {
            User existing = this.context.Users.FirstOrDefault(e => e.Name == entity.Name);
            if (existing == null)
            {
                DbSet<User> dbSet = this.context.Set<User>();
                dbSet.Add(entity);

                this.context.SaveChanges();
            }
        }

        /// <summary>
        /// Get item with item.id=id from database.
        /// </summary>
        /// <param name="id">object id.</param>
        /// <returns>User from database.</returns>
        public override User GetByID(object id)
        {
            return this.context.Set<User>().Find(id);
        }

        /// <summary>
        /// Update item in database.
        /// </summary>
        /// <param name="user">user to update.</param>
        public override void Update(User user)
        {
            using (this.context)
            {
                DbSet<User> dbSet = this.context.Set<User>();
                dbSet.Attach(user);

                this.context.SaveChanges();
            }
        }

        /// <summary>
        /// Check if user already exist.
        /// </summary>
        /// <param name="user">the user.</param>
        /// <returns>true or false.</returns>
        public bool Existing(User user)
        {
            User existing = this.context.Users.FirstOrDefault(e => e.Name == user.Name);
            if (existing == null)
            {
                return false;
            }

            return true;
        }
    }
}
