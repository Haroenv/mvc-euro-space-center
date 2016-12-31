using EuroSpaceCenter.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EuroSpaceCenter.Controllers {
    public class ItemsController : ApiController {

        /// <summary>
        /// Get all of the items in Euro Space Center
        /// </summary>
        [HttpGet]
        public IHttpActionResult Items() {
            var items = item.GetAllDisposed().Select(i => new {
                id = i.id,
                title = i.title,
                image = i.image,
                alt = i.alt,
                payment_type = i.restaurant != null ? i.restaurant.payment_type : null,
                datetime = i.show != null ? i.show.datetime : null,
                min_height = i.attraction != null ? i.attraction.max_height : null,
                max_height = i.attraction != null ? i.attraction.min_height : null,
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

        /// <summary>
        /// Get details about a single item
        /// </summary>
        /// <param name="id">the id of the item</param>
        [HttpGet]
        public IHttpActionResult Single(int id) {
            var i = item.Get(id);
            if (i == null) {
                return NotFound();
            }

            var result = new {
                id = i.id,
                title = i.title,
                image = i.image,
                alt = i.alt,
                payment_type = i.restaurant != null ? i.restaurant.payment_type : null,
                datetime = i.show != null ? i.show.datetime : null,
                min_height = i.attraction != null ? i.attraction.max_height : null,
                max_height = i.attraction != null ? i.attraction.min_height : null,
                rating = i.ratings.Any() ? i.ratings.Average(r => r.rating1) : double.NaN,
                ratings = i.ratings.Select(r => new {
                    users_id = r.users_id,
                    datetime = r.datetime,
                    rating = r.rating1,
                    message = r.message
                })
            };

            return Json(result);
        }
    }
}
