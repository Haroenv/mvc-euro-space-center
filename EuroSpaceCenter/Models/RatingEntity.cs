using System;

namespace EuroSpaceCenter.Controllers {
    internal class Rating {
        public Rating() {
        }

        public DateTime datetime { get; set; }
        public string message { get; set; }
        public short rating { get; set; }
        public int users_id { get; set; }
    }
}