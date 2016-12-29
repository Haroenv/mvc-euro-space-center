using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
    }

    public class ShowValidation {
        [Required(ErrorMessage = "A show always has a particular moment it's on. Repeating shows don't exist yet.")]
        public DateTime datetime;
    }
}