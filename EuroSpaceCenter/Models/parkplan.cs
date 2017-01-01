using EuroSpaceCenter.util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace EuroSpaceCenter.Models {
    [MetadataType(typeof(ParkplanValidation))]
    public partial class parkplan {
        internal parkplan Create(int users_id) {
            using (var db = new DataClassesDataContext()) {
                db.parkplans.InsertOnSubmit(this);
                db.SubmitChanges();
                db.users_has_parkplans.InsertOnSubmit(new users_has_parkplan() { users_id = users_id, parkplans_id = this.id, accepted = true });
                db.SubmitChanges();
                return this;
            }
        }

        internal static parkplan Get(int parkplan_id) {
            using (var db = new DataClassesDataContext()) {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<parkplan>(t => t.parkplans_has_items);
                options.LoadWith<parkplan>(t => t.users_has_parkplans);
                options.LoadWith<users_has_parkplan>(t => t.user);
                options.LoadWith<parkplans_has_item>(t => t.item);
                db.LoadOptions = options;

                return db.parkplans.SingleOrDefault(p => p.id == parkplan_id);
            }
        }

        internal static List<users_has_parkplan> GetAll(int user_id) {
            using (var db = new DataClassesDataContext()) {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<users_has_parkplan>(t => t.parkplan);
                db.LoadOptions = options;

                return db.users_has_parkplans.Where(uhp => uhp.users_id == user_id).ToList();
            }
        }

        internal static void Delete(int id) {
            using (var db = new DataClassesDataContext()) {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<parkplan>(t => t.parkplans_has_items);
                options.LoadWith<parkplan>(t => t.users_has_parkplans);
                db.LoadOptions = options;

                var plan = db.parkplans.SingleOrDefault(p => p.id == id);
                db.users_has_parkplans.DeleteAllOnSubmit(plan.users_has_parkplans);
                db.parkplans_has_items.DeleteAllOnSubmit(plan.parkplans_has_items);
                db.parkplans.DeleteOnSubmit(plan);
                db.SubmitChanges();
            }
        }

        internal static void Invite(int users_id, int parkplan_id) {
            using (var db = new DataClassesDataContext()) {
                db.users_has_parkplans.InsertOnSubmit(new users_has_parkplan() { users_id = users_id, parkplans_id = parkplan_id, accepted = false });
                db.SubmitChanges();
                var u = user.Get(users_id);
                Email.Send(u.email, u.name, $"Nice, you've been invited to join a plan to go to Euro Space Center", "Join", $"http://eurospacecenter.haroenviaene.ikdoeict.be/Plan/Accept/{parkplan_id}", "Join a plan");
            }
        }

        internal static void RemoveItem(int id) {
            using (var db = new DataClassesDataContext()) {
                var phi = db.parkplans_has_items.SingleOrDefault(has => has.id == id);
                db.parkplans_has_items.DeleteOnSubmit(phi);
                db.SubmitChanges();
            }
        }

        internal static int AddItem(int parkplan_id, int item_id) {
            using (var db = new DataClassesDataContext()) {
                var ret = new parkplans_has_item() { items_id = item_id, parkplans_id = parkplan_id };
                db.parkplans_has_items.InsertOnSubmit(ret);
                db.SubmitChanges();
                return ret.id;
            }
        }

        internal static bool Accept(int user_id, int parkplan_id) {
            using (var db = new DataClassesDataContext()) {
                var has = db.users_has_parkplans.SingleOrDefault(uhp => uhp.parkplans_id == parkplan_id && uhp.users_id == user_id);
                if (has != null) {
                    has.accepted = true;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        internal static bool Reject(int user_id, int parkplan_id) {
            using (var db = new DataClassesDataContext()) {
                var has = db.users_has_parkplans.SingleOrDefault(uhp => uhp.parkplans_id == parkplan_id && uhp.users_id == user_id);
                if (has != null) {
                    has.accepted = false;
                    db.users_has_parkplans.DeleteOnSubmit(has);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        internal static bool HasUser(int user_id, int parkplan_id) {
            using (var db = new DataClassesDataContext()) {
                return db.users_has_parkplans.Any(uhp => uhp.parkplans_id == parkplan_id && uhp.users_id == user_id);
            }
        }
    }

    public class ParkplanValidation {
        [Required(ErrorMessage = "The name you can recognise your visit as")]
        public string name;

        [Required(ErrorMessage = "When you will come")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime date;

        [Required(ErrorMessage = "Explain why other people should come 😊")]
        public string description;
    }
}