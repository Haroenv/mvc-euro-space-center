using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroSpaceCenter.Models {
    public partial class items {
        internal static item Get(int id) {
            using (var db = new DataClassesDataContext()) {
                return db.items.SingleOrDefault(i => i.id == id);
            }
        }

        internal static List<item> Get(string cat) {
            using (var db = new DataClassesDataContext()) {
                //return db.items.Where(i => i[cat] != null).ToList();
                switch(cat) {
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

        internal static item Create(item i) {
            using (var db = new DataClassesDataContext()) {
                if (i.attraction != null) {
                    db.attractions.InsertOnSubmit(i.attraction);
                } else if (i.show != null) {
                    db.shows.InsertOnSubmit(i.show);
                } else if (i.restaurant != null) {
                    db.restaurants.InsertOnSubmit(i.restaurant);
                }
                db.items.InsertOnSubmit(i);
                db.SubmitChanges();
                return i;
            }
        }

        internal static List<item> GetAll() {
            using (var db = new DataClassesDataContext()) {
                return db.items.ToList();
            }
        }
    }
}