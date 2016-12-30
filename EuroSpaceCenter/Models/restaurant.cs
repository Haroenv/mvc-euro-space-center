using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace EuroSpaceCenter.Models {
    [MetadataType(typeof(RestaurantValidation))]
    public partial class restaurant {
        internal static restaurant Create(restaurant i) {
            using (var db = new DataClassesDataContext()) {
                db.restaurants.InsertOnSubmit(i);
                db.SubmitChanges();
                return i;
            }
        }

        internal static restaurant Update(restaurant i) {
            using (var db = new DataClassesDataContext()) {
                var old = db.restaurants.SingleOrDefault(it => it.items_id == i.items_id);
                if (old != null) {
                    foreach (PropertyInfo property in typeof(restaurant).GetProperties()) {
                        if (property.Name == "item") {
                            break;
                        }
                        property.SetValue(old, property.GetValue(i, null), null);
                    }
                } else {
                    db.restaurants.InsertOnSubmit(i);
                }
                db.SubmitChanges();
                return i;
            }
        }
    }
    public class RestaurantValidation {
        [Required(ErrorMessage = "Customers should know if they need to bring extra cash or a visa card.")]
        [Display(Name = "Payment type")]
        public string payment_type;
    }
}