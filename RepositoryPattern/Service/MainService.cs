// <copyright file="MainService.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Service
{
    using System;
    using System.Collections.Generic;
    using AuctionProject.Enum;
    using AuctionProject.Helper;
    using AuctionProject.Models;
    using log4net;

    /// <summary>
    /// Main Service.
    /// </summary>
    public class MainService
    {
        /// <summary>
        /// Defines the Log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(MainService));

        /// <summary>
        /// Initialization UserService.
        /// </summary>
        private readonly UserService userService = new UserService();

        /// <summary>
        /// Initialization ProductService.
        /// </summary>
        private readonly ProductService productService = new ProductService();

        /// <summary>
        /// Initialization CategoryService.
        /// </summary>
        private readonly CategoryService categoryService = new CategoryService();

        /// <summary>
        /// Initialization AuctionService.
        /// </summary>
        private readonly AuctionService auctionService = new AuctionService();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainService"/> class.
        /// </summary>
        /// <param name="userService">Initialization user service.</param>
        /// <param name="productService">Initialization product service.</param>
        /// <param name="categoryService">Initialization category service.</param>
        /// <param name="auctionService">Initialization auction service.</param>
        public MainService(UserService userService, ProductService productService, CategoryService categoryService, AuctionService auctionService)
        {
            this.userService = userService;
            this.productService = productService;
            this.categoryService = categoryService;
            this.auctionService = auctionService;
        }

        /// <summary>
        /// User add a new product.
        /// </summary>
        /// <param name="user">my owner.</param>
        /// <param name="product">new product.</param>
        /// <param name="category">product's category.</param>
        public void UserOffersProdutForBid(User user, Product product, Category category)
        {
            if (user.Role != Role.Offerer)
            {
                Log.Error("Change role for offer product for bid.");
                return;
            }

            product.Owner = user;
            product.Category = category;

            int activeProducts = this.productService.OfferedProducts(user);
            Helper helper = new Helper();

            if (activeProducts > helper.NumberMaxForActiveProducts)
            {
                Log.Error("You have bid many products.");
                return;
            }

            if (!this.userService.Existing(user))
            {
                this.userService.AddUser(user);
            }

            if (!this.categoryService.Existing(category))
            {
                this.categoryService.AddCategory(category);
            }

            this.productService.AddProduct(product);
        }

        /// <summary>
        /// Search the category and return children or products.
        /// </summary>
        /// <param name="nameCategory">category's name.</param>
        /// <returns>tuple with 2 list.</returns>
        public Tuple<List<Category>, List<Product>> UserChooseACategory(string nameCategory)
        {
            return new Tuple<List<Category>, List<Product>>(this.categoryService.GetChildren(nameCategory), this.categoryService.GetProducts(nameCategory));
        }

        /// <summary>
        /// the user closes the auction.
        /// </summary>
        /// <param name="user">the user.</param>
        /// <param name="product">the product.</param>
        public void UserCloseAuction(User user, Product product)
        {
            if (user.Role != Role.Offerer)
            {
                Log.Error("Change role for edit product!.");
                return;
            }

            if (user != product.Owner)
            {
                Log.Error("This is not your product!.");
                return;
            }

            product.Active = false;

            this.productService.UpdateProduct(product);
        }

        /// <summary>
        /// the user update the product.
        /// </summary>
        /// <param name="user">the user.</param>
        /// <param name="product">the product.</param>
        /// <param name="updateProduct">update product.</param>
        /// <returns>true or false.</returns>
        public bool UserUpdateProduct(User user, Product product, Product updateProduct)
        {
            if (user.Role != Role.Offerer)
            {
                Log.Error("Change role for edit product!.");
                return false;
            }

            if (user != product.Owner)
            {
                Log.Error("This is not your product!.");
                return false;
            }

            product.Description = updateProduct.Description;
            product.StartDateAction = updateProduct.StartDateAction;
            product.EndDateAction = updateProduct.EndDateAction;
            product.Coins = updateProduct.Coins;
            product.Price = updateProduct.Price;
            product.Name = updateProduct.Name;
            return this.productService.UpdateProduct(product);
        }

        /// <summary>
        /// User bit for a product.
        /// </summary>
        /// <param name="user">the user.</param>
        /// <param name="product">the product.</param>
        /// <param name="auction">the action.</param>
        /// <returns>true or false.</returns>
        public bool UserAuctionForAProduct(User user, Product product, Auction auction)
        {
            if (user.Role != Role.Bidder)
            {
                Log.Error("Change role for bit!.");
                return false;
            }

            if (user.Name == product.Owner.Name)
            {
                Log.Error("You can't bit for your product.");
                return false;
            }

            auction.Bidder = user;
            auction.Product = product;

            if (!product.IsActive())
            {
                product.Active = false;
                this.productService.UpdateProduct(product);

                Log.Error("The auction is over, you late");
                return false;
            }

            if (this.auctionService.AddAuction(auction))
            {
                product.Price = auction.Price;
                this.productService.UpdateProduct(product);
                return true;
            }

            return false;
        }
    }
}