using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace EuroSpaceCenter.Models {
    [MetadataType(typeof(ItemValidation))]

    public partial class item {
        internal static item Get(int id) {
            using (var db = new DataClassesDataContext()) {
                return db.items.SingleOrDefault(i => i.id == id);
            }
        }

        internal static List<item> Get(string cat) {
            using (var db = new DataClassesDataContext()) {
                //return db.items.Where(i => i[cat] != null).ToList();
                switch (cat) {
                    case "attractions":
                        return db.items.Where(i => i.attraction != null).ToList();
                    case "shows":
                        return db.items.Where(i => i.show != null).ToList();
                    case "restaurants":
                        return db.items.Where(i => i.restaurant != null).ToList();
                    default:
                        throw new Exception("no such category");
                }
            }
        }

        internal static List<item> GetAll() {
            using (var db = new DataClassesDataContext()) {
                return db.items.ToList();
            }
        }

        internal static item Create(item i) {
            using (var db = new DataClassesDataContext()) {
                db.items.InsertOnSubmit(i);
                db.SubmitChanges();
                return i;
            }
        }

        internal static item Update(item i) {
            using (var db = new DataClassesDataContext()) {
                var old = db.items.SingleOrDefault(it => it.id == i.id);
                if (old != null) {
                    foreach (PropertyInfo property in typeof(item).GetProperties()) {
                        // don't break the linkages :)
                        if (property.Name == "attraction" || property.Name == "restaurant" || property.Name == "show") {
                            break;
                        }
                        property.SetValue(old, property.GetValue(i, null), null);
                    }
                } else {
                    db.items.InsertOnSubmit(i);
                }
                db.SubmitChanges();
                return i;
            }
        }

        internal static void Delete(int id) {
            using(var db = new DataClassesDataContext()) {
                var it = db.items.SingleOrDefault(i => i.id == id);
                db.items.DeleteOnSubmit(it);
                db.SubmitChanges();
            }
        }
    }

    public class ItemValidation {
        [Required(ErrorMessage = "Items have a title, like 'Moonshot Fountain'")]
        public int title;

        [Required(ErrorMessage = "When items don't have a description, potential clients will be less likely to go to our park 😨")]
        public DateTime description;

        [Required(ErrorMessage = "Nice images bring nice people 😉")]
        [Url]
        public string image;

        [Required(ErrorMessage = "This describes the image for blind users 😎")]
        public int alt;
    }
}