// <copyright file="Configuration.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Class automatically.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal sealed class Configuration : DbMigrationsConfiguration<AuctionProject.DataMapper.MyContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// This method will be called after migrating to the latest version.
        /// </summary>
        /// <param name="context"> my context.</param>
        protected override void Seed(AuctionProject.DataMapper.MyContext context)
        {
            // This method will be called after migrating to the latest version.
            // You can use the DbSet<T>.AddOrUpdate() helper extension method
            // to avoid creating duplicate seed data.
        }
    }
}
