// <copyright file="202301142257115_CreateDB.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Class automatically.
    /// </summary>
    public partial class CreateDB : DbMigration
    {
        /// <summary>
        /// Create DB.
        /// </summary>
        public override void Up()
        {
            this.CreateTable(
                "dbo.Auctions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Coins = c.Int(nullable: false),
                        Bidder_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Bidder_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Bidder_Id)
                .Index(t => t.Product_Id);
            this.CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Role = c.Int(nullable: false),
                        Score = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            this.CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 200),
                        StartDateAction = c.DateTime(nullable: false),
                        EndDateAction = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        Coins = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                        Owner_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Owner_Id, cascadeDelete: false)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Owner_Id);
            this.CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            this.CreateTable(
                "dbo.CategoryCategories",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Category_Id1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Category_Id1 })
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id1)
                .Index(t => t.Category_Id)
                .Index(t => t.Category_Id1);
        }

        /// <summary>
        /// Drop DB.
        /// </summary>
        public override void Down()
        {
            this.DropForeignKey("dbo.Auctions", "Product_Id", "dbo.Products");
            this.DropForeignKey("dbo.Auctions", "Bidder_Id", "dbo.Users");
            this.DropForeignKey("dbo.Products", "Owner_Id", "dbo.Users");
            this.DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            this.DropForeignKey("dbo.CategoryCategories", "Category_Id1", "dbo.Categories");
            this.DropForeignKey("dbo.CategoryCategories", "Category_Id", "dbo.Categories");
            this.DropIndex("dbo.CategoryCategories", new[] { "Category_Id1" });
            this.DropIndex("dbo.CategoryCategories", new[] { "Category_Id" });
            this.DropIndex("dbo.Categories", new[] { "Name" });
            this.DropIndex("dbo.Products", new[] { "Owner_Id" });
            this.DropIndex("dbo.Products", new[] { "Category_Id" });
            this.DropIndex("dbo.Products", new[] { "Name" });
            this.DropIndex("dbo.Users", new[] { "Name" });
            this.DropIndex("dbo.Auctions", new[] { "Product_Id" });
            this.DropIndex("dbo.Auctions", new[] { "Bidder_Id" });
            this.DropTable("dbo.CategoryCategories");
            this.DropTable("dbo.Categories");
            this.DropTable("dbo.Products");
            this.DropTable("dbo.Users");
            this.DropTable("dbo.Auctions");
        }
    }
}
