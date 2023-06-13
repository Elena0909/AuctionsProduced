// <copyright file="AuctionTest.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Tests.Validation
{
    using System;
    using AuctionProject.Enum;
    using AuctionProject.Models;
    using AuctionProject.Models.Validator;
    using NUnit.Framework;

    /// <summary>
    /// Class with test for Auction.
    /// </summary>
    public class AuctionTest
    {
        /// <summary>
        /// Initializes AuctionValidator.
        /// </summary>
        private static readonly AuctionValidator AuctionValidator = new AuctionValidator();

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
                Bidder = this.bidder,
                Coins = Coins.Ron,
                Product = this.product,
                Date = new DateTime(2023, 1, 20),
                Price = 20,
            };
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestValidAuction()
        {
            Assert.IsTrue(AuctionValidator.Validate(this.auction));
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestAuctionInactiveProduct()
        {
            this.auction.Product.Active = false;
            Assert.IsFalse(AuctionValidator.Validate(this.auction));
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestAuctionFinishedBid()
        {
            this.auction.Product.EndDateAction = DateTime.Now;
            Assert.IsFalse(AuctionValidator.Validate(this.auction));
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestInvalidAuctionWithoutBidder()
        {
            this.auction.Bidder = null;
            Assert.IsFalse(AuctionValidator.Validate(this.auction));
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestInvalidAuctionWithoutProduct()
        {
            this.auction.Product = null;
            Assert.IsFalse(AuctionValidator.Validate(this.auction));
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestInvalidAuctionWithProductInvalid()
        {
            this.auction.Product.Name = "!";
            Assert.IsFalse(AuctionValidator.Validate(this.auction));
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestInvalidAuctionWithBidderInvalid()
        {
            this.auction.Bidder.Name = "!";
            Assert.IsFalse(AuctionValidator.Validate(this.auction));
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestInvalidAuctionWithOwner()
        {
            this.auction.Bidder.Role = Role.Offerer;
            Assert.IsFalse(AuctionValidator.Validate(this.auction));
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestInvalidAuctionDateBeforStart()
        {
            this.auction.Date = new DateTime(2023, 1, 1);
            Assert.IsFalse(AuctionValidator.Validate(this.auction));
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestInvalidAuctionDateAfterEnd()
        {
            this.auction.Date = new DateTime(2023, 2, 25);
            Assert.IsFalse(AuctionValidator.Validate(this.auction));
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestInvalidAuctionWithoutDate()
        {
            this.auction.Date = default;
            Assert.IsFalse(AuctionValidator.Validate(this.auction));
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestInvalidAuctionDiffrentCoins()
        {
            this.auction.Coins = Coins.Dollar;
            Assert.IsFalse(AuctionValidator.Validate(this.auction));
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestInvalidAuctionPriceNegative()
        {
            this.auction.Price = -1;
            Assert.IsFalse(AuctionValidator.Validate(this.auction));
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestInvalidAuctionWitoutPrice()
        {
            this.auction.Price = default;
            Assert.IsFalse(AuctionValidator.Validate(this.auction));
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestInvalidAuctionLowPrice()
        {
            this.auction.Price = 9;
            Assert.IsFalse(AuctionValidator.Validate(this.auction));
        }

        /// <summary>
        /// Check if a auction is valid.
        /// </summary>
        [Test]
        public void TestInvalidAuctionBigPrice()
        {
            this.auction.Price = 31;
            Assert.IsFalse(AuctionValidator.Validate(this.auction));
        }
    }
}
