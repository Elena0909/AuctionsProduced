// <copyright file="AuctionMockTest.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Tests.Mocking
{
    using System;
    using System.Linq;
    using AuctionProject.DataMapper;
    using AuctionProject.Enum;
    using AuctionProject.Models;
    using AuctionProject.Repository;
    using AuctionProject.Service;
    using NUnit.Framework;
    using Telerik.JustMock.EntityFramework;

    /// <summary>
    /// Test user using mock.
    /// </summary>
    public class AuctionMockTest
    {
        /// <summary>
        /// The library database mock <see cref="MyContext"/>.
        /// </summary>
        private MyContext myContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="productService"/>.
        /// </summary>
        private AuctionService auctionService;

        /// <summary>
        /// The bidder.
        /// </summary>
        private User bidder;

        /// <summary>
        /// The owner.
        /// </summary>
        private User owner;

        /// <summary>
        /// The category.
        /// </summary>
        private Category category;

        /// <summary>
        /// The product.
        /// </summary>
        private Product product;

        /// <summary>
        /// This auction.
        /// </summary>
        private Auction auction;

        /// <summary>
        /// Sets up for tests.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.myContext = EntityFrameworkMock.Create<MyContext>();
            this.auctionService = new AuctionService(new AuctionRepository(this.myContext));

            this.bidder = new User()
            {
                Name = "Andrei",
                Role = Role.Bidder,
            };

            this.category = new Category()
            {
                Name = "Haine",
            };

            this.owner = new User()
            {
                Name = "Valentina",
                Role = Role.Offerer,
            };

            this.product = new Product()
            {
                Name = "Bluza",
                Owner = this.owner,
                Category = this.category,
                Price = 10,
                Coins = Coins.Ron,
                Description = "Bluza marca Zara, Marimea M",
                EndDateAction = new DateTime(2024, 2, 24),
                StartDateAction = new DateTime(2023, 1, 2),
                Active = true,
            };

            this.auction = new Auction()
            {
                Id = 1,
                Bidder = this.bidder,
                Coins = Coins.Ron,
                Product = this.product,
                Date = new DateTime(2023, 1, 20),
                Price = 20,
            };
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestAddAuction()
        {
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsTrue(result);
            Assert.True(this.myContext.Auctions.Count() == 1);
        }

        /// <summary>
        /// Test add null auction.
        /// </summary>
        [Test]
        public void TestAddNullAuction()
        {
            bool result = this.auctionService.AddAuction(null);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Auctions.Count() == 0);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestAddInvalidAuctionWithoutBidder()
        {
            this.auction.Bidder = null;
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Auctions.Count() == 0);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestAddInvalidAuctionWithoutProduct()
        {
            this.auction.Product = null;
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Auctions.Count() == 0);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestAddInvalidAuctionWithProductInvalid()
        {
            this.auction.Product.Name = "!";
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Auctions.Count() == 0);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestAddInvalidAuctionWithBidderInvalid()
        {
            this.auction.Bidder.Name = "!";
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Auctions.Count() == 0);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestAddInvalidAuctionWithOwner()
        {
            this.auction.Bidder.Role = Role.Offerer;
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Auctions.Count() == 0);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestAddInvalidAuctionDateBeforStart()
        {
            this.auction.Date = new DateTime(2023, 1, 1);
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Auctions.Count() == 0);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestAddInvalidAuctionDateAfterEnd()
        {
            this.auction.Date = product.EndDateAction;
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Auctions.Count() == 0);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestAddInvalidAuctionWithoutDate()
        {
            this.auction.Date = default;
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Auctions.Count() == 0);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestAddInvalidAuctionDiffrentCoins()
        {
            this.auction.Coins = Coins.Dollar;
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Auctions.Count() == 0);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestAddAuctionPriceNegative()
        {
            this.auction.Price = -1;
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Auctions.Count() == 0);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestAddAuctionWithoutPrice()
        {
            this.auction.Price = default;
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Auctions.Count() == 0);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestAddAuctionLowPrice()
        {
            this.auction.Price = 9;
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Auctions.Count() == 0);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestAddAuctionBigPrice()
        {
            this.auction.Price = 31;
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsFalse(result);
            Assert.True(this.myContext.Auctions.Count() == 0);
        }

        /// <summary>
        /// Test get Auction.
        /// </summary>
        [Test]
        public void TestGetAuction()
        {
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsTrue(result);

            Auction auctionDB = this.auctionService.GetAuction(this.auction.Id);
            Assert.NotNull(auctionDB);
            Assert.IsTrue(this.auction == auctionDB);
        }

        /// <summary>
        /// Test get Auction with non-existent id.
        /// </summary>
        [Test]
        public void TestGetAuctionNonExistentId()
        {
            Auction auctionDB = this.auctionService.GetAuction(1);
            Assert.Null(auctionDB);
        }

        /// <summary>
        /// Test get Auction with negative id.
        /// </summary>
        [Test]
        public void TestGetAuctionNegativeId()
        {
            Auction auctionDB = this.auctionService.GetAuction(-1);
            Assert.Null(auctionDB);
        }

        /// <summary>
        /// Test update auction.
        /// </summary>
        [Test]
        public void TestUpdateAuction()
        {
            bool result = this.auctionService.AddAuction(this.auction);
            Assert.IsTrue(result);

            this.auction.Bidder.Name = "Ionut";

            result = this.auctionService.UpdateAuction(this.auction);

            Assert.That(result);

            Auction auctionDB = this.auctionService.GetAuction(this.auction.Id);

            Assert.AreEqual(auctionDB.Bidder.Name, this.auction.Bidder.Name);
        }

        /// <summary>
        /// Test update auction.
        /// </summary>
        [Test]
        public void TestUpdateAuctionDontExist()
        {
            this.auction.Bidder.Name = "Ionut";

            bool result = this.auctionService.UpdateAuction(this.auction);

            Assert.False(result);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestUpdateInvalidAuctionWithoutBidder()
        {
            this.auctionService.AddAuction(this.auction);

            this.auction.Bidder = null;
            bool result = this.auctionService.UpdateAuction(this.auction);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestUpdateInvalidAuctionWithoutProduct()
        {
            this.auctionService.AddAuction(this.auction);

            this.auction.Product = null;
            bool result = this.auctionService.UpdateAuction(this.auction);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestUpdateInvalidAuctionWithProductInvalid()
        {
            this.auctionService.AddAuction(this.auction);

            this.auction.Product.Description = string.Empty;
            bool result = this.auctionService.UpdateAuction(this.auction);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestUpdateInvalidAuctionWithBidderInvalid()
        {
            this.auctionService.AddAuction(this.auction);

            this.auction.Bidder.Score = -5;
            bool result = this.auctionService.UpdateAuction(this.auction);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestUpdateInvalidAuctionPriceNegativ()
        {
            this.auctionService.AddAuction(this.auction);

            this.auction.Price = -5;
            bool result = this.auctionService.UpdateAuction(this.auction);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestUpdateInvalidAuctionLowPrice()
        {
            this.auctionService.AddAuction(this.auction);

            this.auction.Price = 5;
            bool result = this.auctionService.UpdateAuction(this.auction);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestUpdateInvalidAuctionBigPrice()
        {
            this.auctionService.AddAuction(this.auction);

            this.auction.Price = 55;
            bool result = this.auctionService.UpdateAuction(this.auction);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test add auction.
        /// </summary>
        [Test]
        public void TestUpdatedAuctionPrice()
        {
            this.auctionService.AddAuction(this.auction);

            this.auction.Price = 15;
            bool result = this.auctionService.UpdateAuction(this.auction);
            Assert.True(result);
        }
    }
}