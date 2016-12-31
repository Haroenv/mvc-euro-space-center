using EuroSpaceCenter.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EuroSpaceCenter.Controllers {
    public class ShowsController : ApiController {
        [HttpGet]
        public IHttpActionResult Shows() {
            var items = item.GetAllDisposed().Where(i => i.show != null).Select(i => new {
                id = i.id,
                title = i.title,
                image = i.image,
                alt = i.alt,
                datetime = i.show.datetime,
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
