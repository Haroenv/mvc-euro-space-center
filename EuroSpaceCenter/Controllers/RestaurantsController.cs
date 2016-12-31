using EuroSpaceCenter.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EuroSpaceCenter.Controllers {
    public class RestaurantsController : ApiController {

        /// <summary>
        /// Get all of the restaurants in Euro Space Center
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Restaurants() {
            var items = item.GetAllDisposed().Where(i => i.restaurant != null).Select(i => new {
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
}
