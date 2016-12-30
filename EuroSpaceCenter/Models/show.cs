using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace EuroSpaceCenter.Models {
    [MetadataType(typeof(ShowValidation))]
    public partial class show {
        internal static show Create(show i) {
            using (var db = new DataClassesDataContext()) {
                db.shows.InsertOnSubmit(i);
                db.SubmitChanges();
                return i;
            }
        }

        internal static show Update(show i) {
            using (var db = new DataClassesDataContext()) {
                var old = db.shows.SingleOrDefault(it => it.items_id == i.items_id);
                if (old != null) {
                    foreach (PropertyInfo property in typeof(show).GetProperties()) {
                        property.SetValue(old, property.GetValue(i, null), null);
                    }
                } else {
                    db.shows.InsertOnSubmit(i);
                }
                db.SubmitChanges();
                return i;
            }
        }
    }

    public class ShowValidation {
        [Required(ErrorMessage = "A show always has a particular moment it's on. Repeating shows don't exist yet.")]
        public DateTime datetime;
    }
}