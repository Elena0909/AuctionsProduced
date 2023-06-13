// <copyright file="Program.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using log4net;

    /// <summary>
    /// Main Class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        /// <summary>
        /// Defines the Log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// Main method.
        /// </summary>
        private static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();
            Log.Debug("merge");
            Console.WriteLine("Heey");
            Console.ReadKey(true);
        }
    }
}
