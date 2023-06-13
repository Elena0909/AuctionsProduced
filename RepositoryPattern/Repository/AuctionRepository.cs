// <copyright file="AuctionRepository.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Repository
{
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using AuctionProject.DataMapper;
    using AuctionProject.Interfaces.DataAccess;
    using AuctionProject.Models;

    /// <summary>
    /// Repository for Auction.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AuctionRepository : BaseRepository<Models.Auction>, IAuctionRepository
    {
        /// <summary>
        /// The auction database.
        /// </summary>
        private readonly MyContext context = new MyContext();

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionRepository"/> class.
        /// </summary>
        public AuctionRepository()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionRepository"/> class.
        ///  Initializes a new instance.
        /// </summary>
        /// <param name="myContext">My context.</param>
        public AuctionRepository(MyContext myContext)
        {
            this.context = myContext;
        }

        /// <summary>
        /// Insert Auction in database, check name unique.
        /// </summary>
        /// <param name="entity">Auction to insert.</param>
        public override void Insert(Auction entity)
        {
            DbSet<Auction> dbSet = this.context.Set<Auction>();
            dbSet.Add(entity);

            this.context.SaveChanges();
        }

        /// <summary>
        /// Get item with item.id=id from database.
        /// </summary>
        /// <param name="id">object id.</param>
        /// <returns>Auction from database.</returns>
        public override Auction GetByID(object id)
        {
            return this.context.Set<Auction>().Find(id);
        }

        /// <summary>
        /// Update item in database.
        /// </summary>
        /// <param name="product">Auction to update.</param>
        public override void Update(Auction product)
        {
            using (this.context)
            {
                DbSet<Auction> dbSet = this.context.Set<Auction>();
                dbSet.Attach(product);

                this.context.SaveChanges();
            }
        }
    }
}
