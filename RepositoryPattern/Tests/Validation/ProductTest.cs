// <copyright file="ProductTest.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Tests.Validation
{
    using System;
    using AuctionProject.Enum;
    using AuctionProject.Helper;
    using AuctionProject.Models;
    using AuctionProject.Models.Validator;
    using NUnit.Framework;

    /// <summary>
    ///  Class with test for Product.
    /// </summary>
    internal class ProductTest
    {
        /// <summary>
        /// Initializes ProductValidator.
        /// </summary>
        private static readonly ProductValidator ProductValidator = new ProductValidator();

        /// <summary>
        /// The category.
        /// </summary>
        private Category category;

        /// <summary>
        /// The owner.
        /// </summary>
        private User owner;

        /// <summary>
        /// The product.
        /// </summary>
        private Product product;

        /// <summary>
        /// Sets up for tests.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.category = new Category
            {
                Name = "Haine",
            };

            this.owner = new User
            {
                Name = "Valentina",
                Role = Role.Offerer,
            };

            this.product = new Product
            {
                Name = "Bluza",
                Owner = this.owner,
                Category = this.category,
                Price = 10,
                Coins = Coins.Euro,
                Description = "Bluza marca Zara, Marimea M",
                EndDateAction = new DateTime(2023, 7, 24),
                StartDateAction = new DateTime(2023, 7, 1),
            };
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestProductValid()
        {
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Check a null product.
        /// </summary>
        [Test]
        public void TestProductNull()
        {
            bool isValid = ProductValidator.ValidateInsert(null);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestInvalidProductShortName()
        {
            this.product.Name = "B";

            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestInvalidProductLongName()
        {
            this.product.Name = new string('a', 101);
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestInvalidProductWithoutName()
        {
            this.product.Name = null;
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestInvalidProductWithoutOwner()
        {
            this.product.Owner = null;
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestInvalidProductWithoutCategory()
        {
            this.product.Category = null;
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestInvalidProductWithoutPrice()
        {
            this.product.Price = 0;
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestInvalidProductWithoutDescription()
        {
            this.product.Description = null;
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestInvalidProductWithoutDateEnd()
        {
            Product product = new Product
            {
                Name = "Bluza",
                Owner = this.owner,
                Category = this.category,
                Price = 10,
                Coins = Coins.Euro,
                Description = "Bluza marca Zara, Marimea M",
                StartDateAction = new DateTime(2023, 7, 1),
            };

            bool isValid = ProductValidator.ValidateInsert(product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestInvalidProductWithoutDateStart()
        {
            this.product.StartDateAction = default;
            bool isValid = ProductValidator.Validate(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestInvalidProductShortDescription()
        {
            this.product.Description = "Bluza";
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestInvalidProductLongDescription()
        {
            this.product.Description = new string('a', 201);
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestProductDateStartInPast()
        {
            this.product.StartDateAction = new DateTime(2022, 7, 1);
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestProductDateEndInPast()
        {
            this.product.EndDateAction = new DateTime(2022, 7, 1);
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestProductDateEndBeforeDateStart()
        {
            this.product.EndDateAction = new DateTime(2023, 7, 1);
            this.product.StartDateAction = new DateTime(2023, 7, 10);
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestProductDateEndSameWitheDateStart()
        {
            this.product.EndDateAction = new DateTime(2023, 7, 24);
            this.product.StartDateAction = new DateTime(2023, 7, 24);
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestProductInvalidNegativePrice()
        {
            this.product.Price = -1;
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestProductWithInvalidCategory()
        {
            Category category = new Category
            {
                Name = "H",
            };

            this.product.Category = category;
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestProductWithInvalidOwner()
        {
            User owner = new User
            {
                Name = new string('a', 201),
            };

            this.product.Owner = owner;
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Check if a product is valid.
        /// </summary>
        [Test]
        public void TestProductWithBidder()
        {
            this.owner.Role = Role.Bidder;
            this.product.Owner = this.owner;
            bool isValid = ProductValidator.ValidateInsert(this.product);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Test active bid for product.
        /// </summary>
        [Test]
        public void TestActiveProduct()
        {
            this.product.StartDateAction = new DateTime(2023, 1, 10);
            this.product.Active = true;
            Assert.IsTrue(this.product.IsActive());
        }

        /// <summary>
        /// Test inactive bid for product.
        /// </summary>
        [Test]
        public void TestInactiveProduct()
        {
            this.product.Active = false;
            Assert.IsFalse(this.product.IsActive());
        }

        /// <summary>
        /// Test finish bid for product.
        /// </summary>
        [Test]
        public void TestProductFinishBid()
        {
            this.product.Active = true;
            this.product.EndDateAction = DateTime.Now;
            Assert.IsFalse(this.product.IsActive());
        }

        /// <summary>
        /// Test L distance.
        /// </summary>
        [Test]
        public void TestLDistance1()
        {
            int distance = Algorithms.LevenshteinDistance("Mere", "pere");
            Assert.IsTrue(distance == 1);
        }

        /// <summary>
        /// Test L distance.
        /// </summary>
        [Test]
        public void TestLDistance2()
        {
            int distance = Algorithms.LevenshteinDistance("Pere", "pere");
            Assert.IsTrue(distance == 0);
        }

        /// <summary>
        /// Test L distance.
        /// </summary>
        [Test]
        public void TestLDistance3()
        {
            int distance = Algorithms.LevenshteinDistance(string.Empty, "pere");
            Assert.IsTrue(distance == 4);
        }

        /// <summary>
        /// Test L distance.
        /// </summary>
        [Test]
        public void TestLDistance4()
        {
            int distance = Algorithms.LevenshteinDistance("mere", string.Empty);
            Assert.IsTrue(distance == 4);
        }
    }
}
