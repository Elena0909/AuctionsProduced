// <copyright file="TestMainService.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Tests.Mocking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AuctionProject.DataMapper;
    using AuctionProject.Enum;
    using AuctionProject.Models;
    using AuctionProject.Repository;
    using AuctionProject.Service;
    using NUnit.Framework;
    using Telerik.JustMock.EntityFramework;

    /// <summary>
    /// Test Main Service.
    /// </summary>
    public class TestMainService
    {
        /// <summary>
        /// The library database mock <see cref="MyContext"/>.
        /// </summary>
        private MyContext myContext;

        /// <summary>
        /// The main service.
        /// </summary>
        private MainService mainService;

        /// <summary>
        /// The user repository.
        /// </summary>
        private UserRespository userRespository;

        /// <summary>
        /// The product repository.
        /// </summary>
        private ProductRepository productRepository;

        /// <summary>
        /// The category repository.
        /// </summary>
        private CategoryRepository categoryRepository;

        /// <summary>
        /// The auction repository.
        /// </summary>
        private AuctionRepository auctionRepository;

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
        /// List with products.
        /// </summary>
        private List<Product> products;

        /// <summary>
        /// Sets up for tests.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.myContext = EntityFrameworkMock.Create<MyContext>();

            Category categoryA, categoryB;

            categoryA = new Category()
            {
                Id = 1,
                Name = "category A",
            };

            categoryB = new Category()
            {
                Id = 2,
                Name = "category B",
            };

            this.owner = new User()
            {
                Id = 2,
                Name = "Valentina",
                Role = Role.Offerer,
            };

            this.products = new List<Product>()
            {
                new Product()
                {
                    Id = 2,
                    Name = "Pantaloni",
                    Price = 16,
                    Category = this.category,
                    Owner = this.owner,
                    Coins = Coins.Euro,
                    Description = "Pantaloni negrii marca Zara, Marimea M",
                    EndDateAction = new DateTime(2023, 7, 24),
                    StartDateAction = new DateTime(2023, 1, 1),
                    Active = false,
                },
                new Product()
                {
                    Id = 3,
                    Name = "Vesta",
                    Price = 12,
                    Category = this.category,
                    Owner = this.owner,
                    Coins = Coins.Euro,
                    Description = "Vesta bej marca C&A, Marimea M",
                    EndDateAction = new DateTime(2023, 7, 24),
                    StartDateAction = new DateTime(2023, 1, 1),
                    Active = true,
                },
                new Product()
                {
                    Id = 4,
                    Name = "helanca",
                    Price = 10,
                    Category = this.category,
                    Owner = this.owner,
                    Coins = Coins.Euro,
                    Description = "Helanca synsay, Marimea M",
                    EndDateAction = new DateTime(2023, 7, 24),
                    StartDateAction = new DateTime(2023, 1, 1),
                    Active = true,
                },
                new Product()
                {
                Id = 5,
                Name = "Geaca",
                Price = 10,
                Category = this.category,
                Owner = this.owner,
                Coins = Coins.Euro,
                Description = "Geaca calduroasa pt iarna, Marimea M",
                EndDateAction = new DateTime(2023, 7, 24),
                StartDateAction = new DateTime(2023, 1, 1),
                Active = true,
                },
            };

            this.userRespository = new UserRespository(this.myContext);
            this.productRepository = new ProductRepository(this.myContext);
            this.categoryRepository = new CategoryRepository(this.myContext);
            this.auctionRepository = new AuctionRepository(this.myContext);

            UserService userService = new UserService(this.userRespository);
            ProductService productService = new ProductService(this.productRepository);
            CategoryService categoryService = new CategoryService(this.categoryRepository);
            AuctionService auctionService = new AuctionService(this.auctionRepository);

            this.mainService = new MainService(userService, productService, categoryService, auctionService);

            this.bidder = new User()
            {
                Id = 1,
                Name = "Andrei",
                Role = Role.Bidder,
            };

            this.category = new Category()
            {
                Id = 3,
                Name = "Haine",
                CategoryParents = new List<Category>()
                {
                    categoryA,
                    categoryB,
                },
            };

            this.product = new Product()
            {
                Id = 1,
                Name = "Bluza",
                Price = 10,
                Coins = Coins.Euro,
                Description = "Bluza marca Zara, Marimea M",
                EndDateAction = new DateTime(2023, 7, 24),
                StartDateAction = new DateTime(2023, 7, 1),
                Active = true,
            };

            this.auction = new Auction()
            {
                Id = 1,
                Bidder = this.bidder,
                Coins = Coins.Ron,
                Product = this.product,
                Date = new DateTime(2023, 1, 5),
                Price = 20,
            };

            this.userRespository.Insert(this.owner);
            this.userRespository.Insert(this.bidder);
            this.categoryRepository.Insert(categoryA);
            this.categoryRepository.Insert(categoryB);
        }

        /// <summary>
        /// Test user with role owner offer product for bid.
        /// </summary>
        [Test]
        public void TestUserOfferProdutForBid()
        {
            this.mainService.UserOffersProdutForBid(this.owner, this.product, this.category);

            Assert.IsTrue(this.myContext.Products.Count() == 1);
            Assert.IsTrue(this.myContext.Users.Count() == 2);
            Assert.IsTrue(this.myContext.Categories.Count() == 3);
        }

        /// <summary>
        /// Test user with role owner offer product for bid.
        /// </summary>
        [Test]
        public void TestNewUserOfferProdutForBid()
        {
            User owner = new User()
            {
                Id = 3,
                Name = "Carol",
                Role = Role.Offerer,
            };

            this.mainService.UserOffersProdutForBid(owner, this.product, this.category);

            Assert.IsTrue(this.myContext.Products.Count() == 1);
            Assert.IsTrue(this.myContext.Users.Count() == 3);
            Assert.IsTrue(this.myContext.Categories.Count() == 3);
        }

        /// <summary>
        /// Test user with role bidder offer product for bid.
        /// </summary>
        [Test]
        public void TestBidderOfferProdutForBid()
        {
            this.mainService.UserOffersProdutForBid(this.bidder, this.product, this.category);

            Assert.IsTrue(this.myContext.Products.Count() == 0);
            Assert.IsTrue(this.myContext.Users.Count() == 2);
            Assert.IsTrue(this.myContext.Categories.Count() == 2);
        }

        /// <summary>
        /// Test user offer product for bid.
        /// In my context exist 4 product for this user, 3 active so user can do that.
        /// </summary>
        [Test]
        public void TestValidNumberMaxOfProducts()
        {
            foreach (Product product in this.products)
            {
                this.productRepository.Insert(product);
            }

            this.mainService.UserOffersProdutForBid(this.owner, this.product, this.category);
            Assert.IsTrue(this.myContext.Products.Count() == 5);
            Assert.IsTrue(this.myContext.Users.Count() == 2);
            Assert.IsTrue(this.myContext.Categories.Count() == 3);
        }

        /// <summary>
        /// Test user offer product for bid.
        /// In my context exist 4 product for this user, 4 active this is max so adding fails.
        /// </summary>
        [Test]
        public void TestInalidNumberMaxOfProducts()
        {
            foreach (Product product in this.products)
            {
                this.productRepository.Insert(product);
            }

            this.myContext.Products.Find(2).Active = true;

            this.mainService.UserOffersProdutForBid(this.owner, this.product, this.category);
            Assert.IsTrue(this.myContext.Products.Count() == 4);
            Assert.IsTrue(this.myContext.Users.Count() == 2);
            Assert.IsTrue(this.myContext.Categories.Count() == 2);
        }

        /// <summary>
        /// Test user choose a category with products.
        /// </summary>
        [Test]
        public void TestUserChooseACategoryWithProduct()
        {
            this.category.Products = this.products;

            foreach (Product product in this.products)
            {
                this.productRepository.Insert(product);
            }

            this.categoryRepository.Insert(this.category);

            Tuple<List<Category>, List<Product>> myTuple = this.mainService.UserChooseACategory(this.category.Name);
            Assert.NotNull(myTuple);
            Assert.Null(myTuple.Item1);
            Assert.IsTrue(myTuple.Item2.Count == 4);
        }

        /// <summary>
        /// Test user choose a category with children.
        /// </summary>
        [Test]
        public void TestUserChooseACategoryWithChildren()
        {
            Category category = this.myContext.Categories.Find(1);
            this.myContext.Categories.Find(1).CategoryChildren = new List<Category>() { this.category };

            Tuple<List<Category>, List<Product>> myTuple = this.mainService.UserChooseACategory(category.Name);
            Assert.NotNull(myTuple);
            Assert.IsNull(myTuple.Item2);
            Assert.IsTrue(myTuple.Item1.Count == 1);
        }

        /// <summary>
        /// Test user choose a category with empty.
        /// </summary>
        [Test]
        public void TestUserChooseACategoryEmpty()
        {
            Category category = this.categoryRepository.GetByID(1);

            Tuple<List<Category>, List<Product>> myTuple = this.mainService.UserChooseACategory(category.Name);
            Assert.Null(myTuple.Item1);
            Assert.Null(myTuple.Item2);
        }

        /// <summary>
        /// the user closes the auction.
        /// </summary>
        [Test]
        public void TestUserCloseAuction()
        {
            this.product.Owner = this.owner;
            this.productRepository.Insert(this.product);
            this.mainService.UserCloseAuction(this.owner, this.product);

            Assert.IsFalse(this.product.Active);
        }

        /// <summary>
        /// the user closes the auction.
        /// </summary>
        [Test]
        public void TestBidderCloseAuction()
        {
            this.product.Owner = this.owner;
            this.productRepository.Insert(this.product);

            this.mainService.UserCloseAuction(this.bidder, this.product);

            Assert.IsTrue(this.product.Active);
        }

        /// <summary>
        /// the user try closes the auction for not him product.
        /// </summary>
        [Test]
        public void TestUserCloseAuction2()
        {
            this.product.Owner = new User() { Id = 3, Name = "Madalina", Role = Role.Offerer };
            this.productRepository.Insert(this.product);

            this.mainService.UserCloseAuction(this.owner, this.product);

            Assert.IsTrue(this.product.Active);
        }

        /// <summary>
        /// the user update product.
        /// </summary>
        [Test]
        public void TestUserUpdateProductInvalid()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.productRepository.Insert(this.product);
            Product product = new Product()
            {
                Name = "Bluza",
                Price = 16,
                Coins = Coins.Euro,
                Description = "Bl",
                EndDateAction = new DateTime(2023, 7, 24),
                StartDateAction = new DateTime(2023, 2, 1),
                Active = true,
            };

            bool isvalid = this.mainService.UserUpdateProduct(this.owner, this.product, product);

            Assert.False(isvalid);
        }

        /// <summary>
        /// the user update product.
        /// </summary>
        [Test]
        public void TestUserUpdateProductValid()
        {
            this.product.Owner = this.owner;
            this.product.Category = this.category;
            this.productRepository.Insert(this.product);
            Product product = new Product()
            {
                Name = "Bluza",
                Price = 16,
                Coins = Coins.Euro,
                Description = "Bluza este de stofa, culoarea roz, marime M",
                EndDateAction = new DateTime(2023, 7, 24),
                StartDateAction = new DateTime(2023, 2, 1),
                Active = true,
            };

            bool isvalid = this.mainService.UserUpdateProduct(this.owner, this.product, product);

            Assert.True(isvalid);
        }

        /// <summary>
        /// the bidder update product.
        /// </summary>
        [Test]
        public void TestBidderUpdateProduct()
        {
            this.product.Owner = this.bidder;
            this.product.Category = this.category;
            this.productRepository.Insert(this.product);
            Product product = new Product()
            {
                Name = "Bluza",
                Price = 16,
                Coins = Coins.Euro,
                Description = "Bl",
                EndDateAction = new DateTime(2023, 7, 24),
                StartDateAction = new DateTime(2023, 2, 1),
                Active = true,
            };

            bool isvalid = this.mainService.UserUpdateProduct(this.bidder, this.product, product);

            Assert.False(isvalid);
        }

        /// <summary>
        /// the user try update not him product.
        /// </summary>
        [Test]
        public void TestUserUpdateProduct()
        {
            this.product.Owner = new User() { Id = 3, Name = "Madalina", Role = Role.Offerer };
            this.product.Category = this.category;
            this.productRepository.Insert(this.product);
            Product product = new Product()
            {
                Name = "Bluza",
                Price = 16,
                Coins = Coins.Euro,
                Description = "Bl",
                EndDateAction = new DateTime(2023, 7, 24),
                StartDateAction = new DateTime(2023, 2, 1),
                Active = true,
            };

            bool isvalid = this.mainService.UserUpdateProduct(this.owner, this.product, product);

            Assert.False(isvalid);
        }

        /// <summary>
        /// the user bids for a product.
        /// </summary>
        [Test]
        public void TestBidderAuctionForAProductWrongCoins()
        {
            foreach (Product product in this.products)
            {
                this.productRepository.Insert(product);
            }

            bool result = this.mainService.UserAuctionForAProduct(this.bidder, this.myContext.Products.Find(3), this.auction);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// the user bids for a product.
        /// </summary>
        [Test]
        public void TestBidderAuctionForAProduct()
        {
            foreach (Product product in this.products)
            {
                this.productRepository.Insert(product);
            }

            this.auction.Coins = Coins.Euro;
            bool result = this.mainService.UserAuctionForAProduct(this.bidder, this.myContext.Products.Find(3), this.auction);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// the user bids for a product.
        /// </summary>
        [Test]
        public void TestOffererAuctionForAProduct()
        {
            foreach (Product product in this.products)
            {
                this.productRepository.Insert(product);
            }

            this.auction.Coins = Coins.Euro;
            bool result = this.mainService.UserAuctionForAProduct(this.owner, this.myContext.Products.Find(3), this.auction);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// the user bids for a product.
        /// </summary>
        [Test]
        public void TestUserAuctionForHimProduct()
        {
            foreach (Product product in this.products)
            {
                this.productRepository.Insert(product);
            }

            this.auction.Coins = Coins.Euro;
            Product product1 = this.myContext.Products.Find(3);
            product1.Owner = this.bidder;
            bool result = this.mainService.UserAuctionForAProduct(this.bidder, product1, this.auction);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// the user bids for a product.
        /// </summary>
        [Test]
        public void TestUserAuctionForProductLowPrice()
        {
            foreach (Product product in this.products)
            {
                this.productRepository.Insert(product);
            }

            this.auction.Coins = Coins.Euro;
            this.auction.Price = 9;
            bool result = this.mainService.UserAuctionForAProduct(this.bidder, this.myContext.Products.Find(3), this.auction);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// the user bids for a product.
        /// </summary>
        [Test]
        public void TestUserAuctionForProductBigPrice()
        {
            foreach (Product product in this.products)
            {
                this.productRepository.Insert(product);
            }

            this.auction.Coins = Coins.Euro;
            this.auction.Price = 50;
            bool result = this.mainService.UserAuctionForAProduct(this.bidder, this.myContext.Products.Find(3), this.auction);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// the user bids for a product.
        /// </summary>
        [Test]
        public void TestUserAuctionForProduct()
        {
            foreach (Product product in this.products)
            {
                this.productRepository.Insert(product);
            }

            this.auction.Coins = Coins.Euro;
            this.auction.Price = 20;
            bool result = this.mainService.UserAuctionForAProduct(this.bidder, this.myContext.Products.Find(3), this.auction);
            Product productDB = this.productRepository.GetByID(3);

            Assert.IsTrue(result);
            Assert.IsTrue(productDB.Price == this.auction.Price);
        }

        /// <summary>
        /// the user bids for a product.
        /// </summary>
        [Test]
        public void Test2UsersAuctionForProduct()
        {
            foreach (Product product in this.products)
            {
                this.productRepository.Insert(product);
            }

            User bidder2 = new User()
            {
                Id = 3,
                Name = "Sergiu",
                Role = Role.Bidder,
            };

            this.userRespository.Insert(bidder2);

            this.auction.Coins = Coins.Euro;
            this.auction.Price = 20;
            bool result = this.mainService.UserAuctionForAProduct(this.bidder, this.myContext.Products.Find(3), this.auction);
            Product productDB = this.productRepository.GetByID(3);

            Assert.IsTrue(result);
            Assert.IsTrue(productDB.Price == this.auction.Price);

            this.auction.Price = 19;
            result = this.mainService.UserAuctionForAProduct(bidder2, this.myContext.Products.Find(3), this.auction);
            productDB = this.productRepository.GetByID(3);

            Assert.IsFalse(result);
            Assert.IsFalse(productDB.Price == this.auction.Price);
        }

        /// <summary>
        /// the user bids for a product.
        /// </summary>
        [Test]
        public void TestUserAuctionForInactiveProduct()
        {
            foreach (Product product in this.products)
            {
                this.productRepository.Insert(product);
            }

            this.auction.Coins = Coins.Euro;
            this.auction.Price = 20;
            Product productDB = this.productRepository.GetByID(3);
            productDB.Active = false;

            bool result = this.mainService.UserAuctionForAProduct(this.bidder, productDB, this.auction);

            Assert.IsFalse(result);
            Assert.IsFalse(productDB.Price == this.auction.Price);
        }

        /// <summary>
        /// the user bids for a product.
        /// </summary>
        [Test]
        public void TestUserAuctionForInactiveProduct2()
        {
            foreach (Product product in this.products)
            {
                this.productRepository.Insert(product);
            }

            this.auction.Coins = Coins.Euro;
            this.auction.Price = 20;
            Product productDB = this.productRepository.GetByID(3);
            productDB.EndDateAction = DateTime.Now;

            bool result = this.mainService.UserAuctionForAProduct(this.bidder, productDB, this.auction);
            productDB = this.productRepository.GetByID(3);

            Assert.IsFalse(result);
            Assert.IsFalse(productDB.Price == this.auction.Price);
            Assert.IsFalse(productDB.Active);
        }
    }
}
