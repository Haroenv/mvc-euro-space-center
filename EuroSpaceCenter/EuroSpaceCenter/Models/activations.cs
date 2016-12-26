using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroSpaceCenter.Models {
    public partial class activations {
        public static activation create(user u) {
            using (var db = new DataClassesDataContext()) {
                try {
                    var a = new activation();
                    a.code = util.Random.Words(5);
                    a.users_id = u.id;
                    db.activations.InsertOnSubmit(a);
                    db.SubmitChanges();
                    return a;
                } catch {
                    return null;
                }
            }
        }
    }
}