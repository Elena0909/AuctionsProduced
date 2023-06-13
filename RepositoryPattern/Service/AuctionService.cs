// <copyright file="AuctionService.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Service
{
    using AuctionProject.Models;
    using AuctionProject.Models.Validator;
    using AuctionProject.Repository;
    using log4net;

    /// <summary>
    /// Class service for Auction.
    /// </summary>
    public class AuctionService
    {
        /// <summary>
        /// Defines the Log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(AuctionService));

        /// <summary>
        /// Initialization AuctionValidator.
        /// </summary>
        private static readonly AuctionValidator AuctionValidator = new AuctionValidator();

        /// <summary>
        /// Initialization auctionRepository.
        /// </summary>
        private readonly AuctionRepository auctionRepository = new AuctionRepository();

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionService"/> class.
        /// </summary>
        public AuctionService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionService"/> class.
        /// </summary>
        /// <param name="auctionRepository">The auction repository.</param>
        public AuctionService(AuctionRepository auctionRepository)
        {
            this.auctionRepository = auctionRepository;
        }

        /// <summary>
        /// Add a auction in database.
        /// </summary>
        /// <param name="auction">auction to add.</param>
        /// <returns>true or false.</returns>
        public bool AddAuction(Auction auction)
        {
            bool isValid = AuctionValidator.Validate(auction);

            if (isValid)
            {
                Log.Info("The auction is valid!");
                this.auctionRepository.Insert(auction);
                Log.Info("The auction was added to the database!");
                return true;
            }

            Log.Error("The auction wasn't added to the database!");
            return false;
        }

        /// <summary>
        /// Get auction by id.
        /// </summary>
        /// <param name="id">id auction.</param>
        /// <returns>Auction or null.</returns>
        public Auction GetAuction(int id)
        {
            Auction auction = this.auctionRepository.GetByID(id);

            if (auction == null)
            {
                Log.Info("The auction wasn't found!");
            }
            else
            {
                Log.Info("The auction was found!");
            }

            return auction;
        }

        /// <summary>
        /// Update a auction.
        /// </summary>
        /// <param name="auction">Auction to insert.</param>
        /// <returns>true or false.</returns>
        public bool UpdateAuction(Auction auction)
        {
            bool isValid = AuctionValidator.Validate(auction);

            if (!isValid)
            {
                return false;
            }

            Auction auctionDB = this.auctionRepository.GetByID(auction.Id);
            if (auctionDB == null)
            {
                Log.Info("The auction wan't found!");
                return false;
            }

            this.auctionRepository.Update(auction);
            Log.Info("The auction was updated!");
            return true;
        }
    }
}