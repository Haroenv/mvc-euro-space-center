using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
    }
    public class RestaurantValidation {
        [Required(ErrorMessage = "Customers should know if they need to bring extra cash or a visa card.")]
        public string payment_type;
    }
}