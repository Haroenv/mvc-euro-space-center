using System;

namespace EuroSpaceCenter.Controllers {
    internal class RatingEntity {
        public RatingEntity() {
        }

        /// <summary>
        /// The moment this rating was written
        /// </summary>
        public DateTime datetime { get; set; }

        /// <summary>
        /// A comment given with this rating
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// The rating (out of five) given
        /// </summary>
        public short rating { get; set; }

        /// <summary>
        /// The id of the user that has commmented
        /// </summary>
        public int users_id { get; set; }
    }
}