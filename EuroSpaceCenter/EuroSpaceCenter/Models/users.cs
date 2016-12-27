using System;
using System.Linq;
using System.Reflection;

namespace EuroSpaceCenter.Models {
    public partial class users {
        internal static bool Create(user u) {
            using (var db = new DataClassesDataContext()) {
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

        internal static void Update(user old, user newUser) {
            using (var db = new DataClassesDataContext()) {
                user usr = db.users.SingleOrDefault(us => us.email == old.email);
                usr.email = newUser.email;
                usr.name = newUser.name;
                db.SubmitChanges();
            }
        }

        internal static void SetPassword(string email, string password) {
            using (var db = new DataClassesDataContext()) {
                user u = db.users.SingleOrDefault(user => user.email == email);
                u.password = util.Encryption.Hash.CreateHash(password);
                db.SubmitChanges();
            }
        }

        internal static user Get(string email) {
            using (var db = new DataClassesDataContext()) {
                return db.users.SingleOrDefault(u => u.email == email);
            }
        }

        internal static user Get(string email, string password) {
            using (var db = new DataClassesDataContext()) {
                user user = db.users.SingleOrDefault(u => u.email == email);
                if (user != null && util.Encryption.Hash.ValidatePassword(password, user.password)) {
                    return user;
                } else {
                    return null;
                }
            }
        }
    }
}