using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EuroSpaceCenter.Models {
    [MetadataType(typeof(AttractionValidation))]
    public partial class attraction {
        internal static attraction Create(attraction i) {
            using (var db = new DataClassesDataContext()) {
                db.attractions.InsertOnSubmit(i);
                db.SubmitChanges();
                return i;
            }
        }
    }
    public class AttractionValidation {
        [Required(ErrorMessage = "Imagine a poor kid reading about a cool thing on your site and then only once it's there it figures out it can't go on yet!")]
        public int min_height;

        [Required(ErrorMessage = "Don't make them bump their heads!")]
        public int max_height;
    }

}