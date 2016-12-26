using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroSpaceCenter.Models {
    public partial class users {
        public bool create(user u) {
            using(var db = new DataClassesDataContext()) {
                try {
                    db.users.InsertOnSubmit(u);
                    db.SubmitChanges();
                    var a = activations.create(u);
                    if (a == null) {
                        return false;
                    }
                    return util.Email.sendInvite(u, a);
                } catch {
                    return false;
                }
            }
        }
    }
}