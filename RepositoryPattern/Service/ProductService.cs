// <copyright file="ProductService.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Service
{
    using System.Collections.Generic;
    using AuctionProject.Helper;
    using AuctionProject.Models;
    using AuctionProject.Models.Validator;
    using AuctionProject.Repository;
    using log4net;

    /// <summary>
    /// Class service for Product.
    /// </summary>
    public class ProductService
    {
        /// <summary>
        /// Defines the Log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ProductService));

        /// <summary>
        /// Initialization ProductValidator.
        /// </summary>
        private static readonly ProductValidator ProductValidator = new ProductValidator();

        /// <summary>
        /// Initialization productRepo.
        /// </summary>
        private readonly ProductRepository productRepository = new ProductRepository();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        public ProductService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        public ProductService(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Add a Product in database.
        /// </summary>
        /// <param name="product">Product to add.</param>
        /// <returns>true or false.</returns>
        public bool AddProduct(Product product)
        {
            bool isValid = ProductValidator.ValidateInsert(product);

            bool isSimilar = this.SimilarProduct(product);

            if (isValid && isSimilar)
            {
                Log.Info("The product is valid!");
                this.productRepository.Insert(product);
                Log.Info("The product was added to the database!");
                return true;
            }

            Log.Error("The product wasn't added to the database!");
            return false;
        }

        /// <summary>
        /// Get product by id.
        /// </summary>
        /// <param name="id">id product.</param>
        /// <returns>product or null.</returns>
        public Product GetProduct(int id)
        {
            Product product = this.productRepository.GetByID(id);

            if (product == null)
            {
                Log.Info("The product wasn't found!");
            }
            else
            {
                Log.Info("The product was found!");
            }

            return product;
        }

        /// <summary>
        /// Update a product.
        /// </summary>
        /// <param name="product">Product to insert.</param>
        /// <returns>true or false.</returns>
        public bool UpdateProduct(Product product)
        {
            bool isValid = ProductValidator.Validate(product);

            if (!isValid)
            {
                return false;
            }

            Product productDB = this.productRepository.GetByID(product.Id);
            if (productDB == null)
            {
                Log.Info("The product wan't found!");
                return false;
            }

            this.productRepository.Update(product);
            Log.Info("The product was updated!");
            return true;
        }

        /// <summary>
        /// how many active products the user has for auction.
        /// </summary>
        /// <param name="user">my user.</param>
        /// <returns>number of products.</returns>
        public int OfferedProducts(User user)
        {
            List<Product> products = (List<Product>)this.productRepository.Get(filter: product => user.Id == product.Owner.Id);
            List<Product> activeProducts = products.FindAll(product => product.IsActive() == true);
            return activeProducts.Count;
        }

        /// <summary>
        /// check the similarity between products.
        /// </summary>
        /// <param name="product">my product.</param>
        /// <returns>true or false.</returns>
        private bool SimilarProduct(Product product)
        {
            List<Product> products = (List<Product>)this.productRepository.Get(filter: p => p.Owner.Id == product.Owner.Id);

            Helper helper = new Helper();

            foreach (Product productDB in products)
            {
                int distance = Algorithms.LevenshteinDistance(productDB.Description, product.Description);
                if (distance <= helper.LevendhteinDistance)
                {
                    Log.Error("The product is similar to another product!");
                    return false;
                }
            }

            return true;
        }
    }
}