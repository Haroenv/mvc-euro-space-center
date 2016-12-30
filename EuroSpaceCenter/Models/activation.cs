using System;
using System.Linq;

namespace EuroSpaceCenter.Models {
    public partial class activation {
        public static activation create(user u) {
            using (var db = new DataClassesDataContext()) {
                try {
                    var a = new activation();
                    a.code = util.Rand.String(15);
                    a.users_id = u.id;
                    db.activations.InsertOnSubmit(a);
                    db.SubmitChanges();
                    return a;
                } catch (Exception e) {
                    throw e;
                }
            }
        }

        public static bool activate(string code) {
            using (var db = new DataClassesDataContext()) {
                try {
                    var act = db.activations.SingleOrDefault(ac => ac.code == code);
                    if (act != null) {
                        act.user.verified = true;
                        db.activations.DeleteOnSubmit(act);
                        db.SubmitChanges();
                        return true;
                    } else {
                        return false;
                    }
                } catch (Exception e) {
                    throw e;
                }
            }
        }
    }
}