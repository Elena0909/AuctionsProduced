// <copyright file="ProductMockTest.cs" company="Transilvania University of Brasov">
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
    ///  Test product with Mock.
    /// </summary>
    public class ProductMockTest
    {
        /// <summary>
        /// The library database mock <see cref="MyContext"/>.
        /// </summary>
        private MyContext myContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="productService"/>.
        /// </summary>
        private ProductService productService;

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
            this.myContext = EntityFrameworkMock.Create<MyContext>();
            this.productService = new ProductService(new ProductRepository(this.myContext));

            this.category = new Category
            {
                Name = "Haine",
            };

            this.owner = new User
            {
                Id = 1,
                Name = "Valentina",
                Role = Role.Offerer,
            };

            this.product = new Product
            {
                Id = 1,
                Name = "Bluza",
                Price = 10,
                Coins = Coins.Euro,
                Description = "Bluza marca Zara, Marimea M",
                EndDateAction = new DateTime(2023, 7, 24),
                StartDateAction = new DateTime(2023, 7, 1),
            };
        }

        /// <summary>
        /// Test add product valid.
        /// </summary>
        [Test]
        public void TestAddValidProduct()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;

            bool productAdd = this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 1);
        }

        /// <summary>
        /// Test add null.
        /// </summary>
        [Test]
        public void TestAddProductNull()
        {
            this.productService.AddProduct(null);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithoutOwner()
        {
            this.product.Category = this.category;

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithoutCategory()
        {
            this.product.Owner = this.owner;

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithNameNull()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.product.Name = null;

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithNameEmpty()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.product.Name = string.Empty;

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithLongName()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.product.Name = new string('a', 101);

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithShortName()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.product.Name = "A";

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithoutPrice()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.product.Price = 0;

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithNegativePrice()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.product.Price = -5;

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithoutDescription()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.product.Description = null;

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithoutDateEnd()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.product.EndDateAction = default;

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithoutDateStart()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.product.StartDateAction = default;

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithShortDescription()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.product.Description = "Bluzica";

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithLongDescription()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.product.Description = new string('a', 201);

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithDateStartInPast()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.product.StartDateAction = new DateTime(2022, 7, 1);

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithDateEndInPast()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.product.EndDateAction = new DateTime(2022, 7, 1);

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithDateEndSameWitheDateStart()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.product.StartDateAction = new DateTime(2022, 7, 1);
            this.product.EndDateAction = new DateTime(2022, 7, 1);

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithInvalidCategory()
        {
            this.product.Owner = this.owner;
            this.category.Name = null;
            this.product.Category = this.category;

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test add product invalid.
        /// </summary>
        [Test]
        public void TestAddProductWithInvalidOwner()
        {
            this.owner.Name = null;
            this.product.Owner = this.owner;
            this.product.Category = this.category;

            this.productService.AddProduct(this.product);

            Assert.True(this.myContext.Products.Count() == 0);
        }

        /// <summary>
        /// Test get product valid.
        /// </summary>
        [Test]
        public void TestGetValidProduct()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;

            this.productService.AddProduct(this.product);

            Product productDB = this.productService.GetProduct(1);

            Assert.True(productDB == this.product);
        }

        /// <summary>
        /// Get product with non-existent id.
        /// </summary>
        [Test]
        public void TestGetProductNonExistentId()
        {
            Product productDB = this.productService.GetProduct(1);
            Assert.Null(productDB);
        }

        /// <summary>
        /// Get product with negative id.
        /// </summary>
        [Test]
        public void TestGetProductNegativeId()
        {
            Product categoryDB = this.productService.GetProduct(-1);
            Assert.Null(categoryDB);
        }

        /// <summary>
        /// Test update product valid.
        /// </summary>
        [Test]
        public void TestUpdateValidProduct()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;

            this.productService.AddProduct(this.product);

            this.product.Name = "UpdateName";

            this.productService.UpdateProduct(this.product);

            Product productDB = this.productService.GetProduct(this.product.Id);

            Assert.True(productDB == this.product);
        }

        /// <summary>
        /// Test update product don't exist.
        /// </summary>
        [Test]
        public void TestUpdateProductDontExist()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.product.Name = "UpdateName";

            bool result = this.productService.UpdateProduct(this.product);

            Assert.False(result);
        }

        /// <summary>
        /// Test update product invalid.
        /// </summary>
        [Test]
        public void TestUpdateInvalidProduct()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;

            this.productService.AddProduct(this.product);

            this.product.Name = "U";

            bool result = this.productService.UpdateProduct(this.product);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test update product invalid.
        /// </summary>
        [Test]
        public void TestUpdateInvalidProductWrongDescription()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;

            this.productService.AddProduct(this.product);

            this.product.Description = new string('a', 201);

            bool result = this.productService.UpdateProduct(this.product);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test update product invalid.
        /// </summary>
        [Test]
        public void TestUpdateInvalidProductWrongDateStart()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;

            this.productService.AddProduct(this.product);

            this.product.StartDateAction = default;

            bool result = this.productService.UpdateProduct(this.product);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test update product invalid.
        /// </summary>
        [Test]
        public void TestUpdateInvalidProductNegativePreice()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;

            this.productService.AddProduct(this.product);

            this.product.Price = -6;

            bool result = this.productService.UpdateProduct(this.product);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test add product similar.
        /// </summary>
        [Test]
        public void TestAddProductSimilar()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;

            this.productService.AddProduct(this.product);

            Product product = new Product
            {
                Id = 2,
                Name = "Bluza2",
                Price = 10,
                Coins = Coins.Euro,
                Owner = this.owner,
                Category = this.category,
                Description = "Bluza marca C&A, Marimea L",
                EndDateAction = new DateTime(2023, 7, 24),
                StartDateAction = new DateTime(2023, 7, 1),
            };

            this.productService.AddProduct(product);

            Assert.True(this.myContext.Products.Count() == 2);
        }

        /// <summary>
        /// Test add product duplicate.
        /// </summary>
        [Test]
        public void TestAddProductDuplicate()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;

            this.productService.AddProduct(this.product);

            Product product = new Product
            {
                Id = 2,
                Name = "Bluza",
                Price = 10,
                Coins = Coins.Euro,
                Owner = this.owner,
                Category = this.category,
                Description = "Bluza marca ZARA, Marimea L",
                EndDateAction = new DateTime(2023, 7, 24),
                StartDateAction = new DateTime(2023, 7, 1),
            };

            this.productService.AddProduct(product);

            Assert.True(this.myContext.Products.Count() == 1);
        }
    }
}
