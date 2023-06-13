// <copyright file="202301160033221_AuctionPrice.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Class automatically.
    /// </summary>
    public partial class AuctionPrice : DbMigration
    {
        /// <summary>
        /// Add column.
        /// </summary>
        public override void Up()
        {
            this.AddColumn("dbo.Auctions", "Price", c => c.Double(nullable: false));
        }

        /// <summary>
        /// Drop column.
        /// </summary>
        public override void Down()
        {
            this.DropColumn("dbo.Auctions", "Price");
        }
    }
}
