using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroSpaceCenter.Models {
    public partial class rating {
        internal static void Rate(rating r) {
            using (var db = new DataClassesDataContext()) {
                db.ratings.InsertOnSubmit(r);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// Get ratings of a certain item
        /// </summary>
        /// <param name="id">item_id</param>
        /// <returns>list of all ratings</returns>
        internal static List<rating> Get(int id) {
            using (var db = new DataClassesDataContext()) {
                return db.ratings.Where(r => r.items_id == id).ToList();
            }
        }
    }
}