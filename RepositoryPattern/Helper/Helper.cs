// <copyright file="Helper.cs" company="Transilvania University of Brasov">
// Ghinea Alexandra Elena
// </copyright>

namespace AuctionProject.Helper
{
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    /// <summary>
    /// Class with constants read from file.
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Helper"/> class.
        /// </summary>
        public Helper()
        {
            var text = File.ReadAllText(".\\constants.json");
            var dict = JsonConvert.DeserializeObject<Dictionary<string, int>>(text);

            this.Score = dict["SCORE"];

            this.LevendhteinDistance = dict["LEVENSHTEIN_DISTANCE"];

            this.NumberMaxForActiveProducts = dict["NUMBER_MAX_OF_ACTIVE_PRODUCTS"];
        }

        /// <summary>
        /// Gets default score user.
        /// </summary>
        public int Score
        {
            get;
        }

        /// <summary>
        /// Gets Levendhtein distance.
        /// </summary>
        public int LevendhteinDistance
        {
            get;
        }

        /// <summary>
        /// Gets umber max for active products.
        /// </summary>
        public int NumberMaxForActiveProducts
        {
            get;
        }
    }
}
