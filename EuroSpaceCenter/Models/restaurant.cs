using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroSpaceCenter.Models {
    public partial class restaurant {
        internal static restaurant Create(restaurant i) {
            using (var db = new DataClassesDataContext()) {
                db.restaurants.InsertOnSubmit(i);
                db.SubmitChanges();
                return i;
            }
        }
    }
}