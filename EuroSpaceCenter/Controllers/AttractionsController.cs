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

        /// <summary>
        /// Alt attribute for the image
        /// </summary>
        public string alt { get; set; }

        /// <summary>
        /// ID of this item
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// URL to an image for this item
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// Maximum allowed height (attraction)
        /// </summary>
        public int? max_height { get; set; }

        /// <summary>
        /// Minimum allowed height (attraction)
        /// </summary>
        public int? min_height { get; set; }

        /// <summary>
        /// Average rating (out of five)
        /// </summary>
        public double? rating { get; set; }

        /// <summary>
        /// All the ratings given
        /// </summary>
        public IEnumerable<RatingEntity> ratings { get; set; }

        /// <summary>
        /// The identifiable title
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// URL to a detail page for this item
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// Plain text description for this item
        /// </summary>
        public string description { get; set; }

    }
}
