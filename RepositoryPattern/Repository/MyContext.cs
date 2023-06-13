// <copyright file="MyContext.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.DataMapper
{
    using System.Data.Entity;
    using AuctionProject.Models;

    /// <summary>
    /// Class MyContext, my connection with database.
    /// </summary>
    public class MyContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyContext"/> class.
        /// </summary>
        public MyContext()
            : base("auctiondb")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MyContext>());
        }

        /// <summary>
        /// Gets or sets table Users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets Categories.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets Products.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets Auctions.
        /// </summary>
        public DbSet<Auction> Auctions { get; set; }
    }
}
