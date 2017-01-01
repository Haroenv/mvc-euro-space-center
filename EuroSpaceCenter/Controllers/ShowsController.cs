using EuroSpaceCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace EuroSpaceCenter.Controllers {
    public class ShowsController : ApiController {

        /// <summary>
        /// Get all of the shows in Euro Space Center
        /// </summary>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Show>))]
        public IHttpActionResult Shows() {
            var items = item.GetAllDisposed().Where(i => i.show != null).Select(i => new Show() {
                id = i.id,
                title = i.title,
                image = i.image,
                alt = i.alt,
                datetime = i.show.datetime,
                rating = i.ratings.Any() ? i.ratings.Average(r => r.rating1) : double.NaN,
                ratings = i.ratings.Select(r => new RatingEntity(){
                    users_id = r.users_id,
                    datetime = r.datetime,
                    rating = r.rating1,
                    message = r.message
                }),
                url = Url.Content($"~/Detail?id={i.id}")
            });

            return Json(items);
        }
    }

    internal class Show {
        public Show() {
        }

        public string alt { get; set; }
        public DateTime? datetime { get; set; }
        public int id { get; set; }
        public string image { get; set; }
        public double rating { get; set; }
        public IEnumerable<RatingEntity> ratings { get; set; }
        public string title { get; set; }
        public Uri url { get; set; }

    }
}
