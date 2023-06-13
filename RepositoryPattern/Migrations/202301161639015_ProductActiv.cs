// <copyright file="202301161639015_ProductActiv.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Class automatically.
    /// </summary>
    public partial class ProductActiv : DbMigration
    {
        /// <summary>
        /// Add column.
        /// </summary>
        public override void Up()
        {
            this.AddColumn("dbo.Products", "Active", c => c.Boolean(nullable: false));
        }

        /// <summary>
        /// Drop column.
        /// </summary>
        public override void Down()
        {
            this.DropColumn("dbo.Products", "Active");
        }
    }
}
