// <copyright file="IUserRepository.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Interfaces.DataAccess
{
    using AuctionProject.Models;

    /// <summary>
    /// Type <see cref="IUserRepository"/>.
    /// </summary>
    internal interface IUserRepository : IRepository<User>
    {
    }
}
