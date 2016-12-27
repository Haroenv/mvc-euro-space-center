using System;
using System.Linq;

namespace EuroSpaceCenter.Models {
    public partial class users {
        public static bool create(user u) {
            using(var db = new DataClassesDataContext()) {
                try {
                    u.password = util.Encryption.Hash.CreateHash(u.password);
                    db.users.InsertOnSubmit(u);
                    db.SubmitChanges();
                    var a = activations.create(u);
                    if (a == null) {
                        return false;
                    }
                    return util.Email.sendInvite(u, a);
                } catch (Exception e) {
                    throw e;
                }
            }
        }

        internal static user Get(string email, string password) {
            using (var db = new DataClassesDataContext()) {
                user user = db.users.SingleOrDefault(u => u.email == email);
                if (util.Encryption.Hash.ValidatePassword(password, user.password)) {
                    return user;
                } else {
                    return null;
                }
            }
        }
    }
}