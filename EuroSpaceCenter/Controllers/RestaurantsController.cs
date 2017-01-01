using EuroSpaceCenter.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace EuroSpaceCenter.Controllers {
    public class RestaurantsController : ApiController {

        /// <summary>
        /// Get all of the restaurants in Euro Space Center
        /// </summary>
        [HttpGet]
        [ResponseType(typeof(List<Restaurant>))]
        public IHttpActionResult Restaurants() {
            var items = item.GetAllDisposed().Where(i => i.restaurant != null).Select(i => new Restaurant() {
                id = i.id,
                title = i.title,
                image = i.image,
                alt = i.alt,
                payment_type = i.restaurant.payment_type,
                rating = i.ratings.Any() ? i.ratings.Average(r => r.rating1) : double.NaN,
                ratings = i.ratings.Select(r => new {
                    users_id = r.users_id,
                    datetime = r.datetime,
                    rating = r.rating1,
                    message = r.message
                })
            });

            return Json(items);
        }
    }

    internal class Restaurant {
        public Restaurant() {
        }

        public string alt { get; set; }
        public int id { get; set; }
        public string image { get; set; }
        public string payment_type { get; set; }
        public double rating { get; set; }
        public System.Collections.Generic.IEnumerable<object> ratings { get; set; }
        public string title { get; set; }
    }
}
