using EuroSpaceCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace EuroSpaceCenter.Controllers {
    public class AttractionsController : ApiController {

        /// <summary>
        /// Get all of the attractions in Euro Space Center
        /// </summary>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Attraction>))]
        public IHttpActionResult Attractions() {
            var items = item.GetAllDisposed().Where(i => i.attraction != null).Select(i => new Attraction() {
                id = i.id,
                title = i.title,
                image = i.image,
                alt = i.alt,
                min_height = i.attraction.min_height,
                max_height = i.attraction.max_height,
                rating = i.ratings.Any() ? (double?)i.ratings.Average(r => r.rating1) : null,
                ratings = i.ratings.Select(r => new RatingEntity(){
                    users_id = r.users_id,
                    datetime = r.datetime,
                    rating = r.rating1,
                    message = r.message
                }),
                url = Url.Content($"~/Detail?id={i.id}"),
                description = i.description
            });

            return Json(items);
        }
    }

    internal class Attraction {
        public Attraction() {
        }

        public string alt { get; set; }
        public string description { get; internal set; }
        public int id { get; set; }
        public string image { get; set; }
        public int? max_height { get; set; }
        public int? min_height { get; set; }
        public double? rating { get; set; }
        public IEnumerable<RatingEntity> ratings { get; set; }
        public string title { get; set; }
        public string url { get; set; }

    }
}
