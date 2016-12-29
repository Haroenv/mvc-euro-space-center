using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroSpaceCenter.Models {
    public partial class attraction {
        internal static attraction Create(attraction i) {
            using (var db = new DataClassesDataContext()) {
                db.attractions.InsertOnSubmit(i);
                db.SubmitChanges();
                return i;
            }
        }
    }
}