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
                return db.parkplans.FirstOrDefault(p => p.id == parkplan_id);
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

        internal static void Invite(int users_id, int parkplan_id) {
            using (var db = new DataClassesDataContext()) {
                db.users_has_parkplans.InsertOnSubmit(new users_has_parkplan() { users_id = users_id, parkplans_id = parkplan_id, accepted = false });
                db.SubmitChanges();
            }
        }

        internal static bool Accept(int user_id, int parkplan_id) {
            using (var db = new DataClassesDataContext()) {
                var has = db.users_has_parkplans.FirstOrDefault(uhp => uhp.parkplans_id == parkplan_id && uhp.users_id == user_id);
                if (has != null) {
                    has.accepted = true;
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