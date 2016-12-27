using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroSpaceCenter.Models {
    public partial class items {
        internal static item Get(int id) {
            using (var db = new DataClassesDataContext()) {
                return db.items.SingleOrDefault(i => i.id == id);
            }
        }

        internal static dynamic Get(string cat) {
            using (var db = new DataClassesDataContext()) {
                //return db
                throw new NotImplementedException();
            }
        }

        internal static List<item> GetAll() {
            using (var db = new DataClassesDataContext()) {
                return db.items.Where(i => true).ToList();
            }
        }
    }
}