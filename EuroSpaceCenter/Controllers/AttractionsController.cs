using EuroSpaceCenter.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EuroSpaceCenter.Controllers {
    public class AttractionsController : ApiController {

        /// <summary>
        /// Get all of the attractions in Euro Space Center
        /// </summary>
        [HttpGet]
        public IHttpActionResult Attractions() {
            var items = item.GetAllDisposed().Where(i => i.attraction != null).Select(i => new {
                id = i.id,
                title = i.title,
                image = i.image,
                alt = i.alt,
                min_height = i.attraction.min_height,
                max_height = i.attraction.max_height,
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
