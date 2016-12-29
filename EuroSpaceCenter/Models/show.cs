using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroSpaceCenter.Models {
    public partial class show {
        internal static show Create(show i) {
            using (var db = new DataClassesDataContext()) {
                db.shows.InsertOnSubmit(i);
                db.SubmitChanges();
                return i;
            }
        }
    }
}