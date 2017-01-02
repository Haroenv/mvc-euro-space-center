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

    internal class Show {

        public Show() {
        }

        /// <summary>
        /// Alt attribute for the image
        /// </summary>
        public string alt { get; set; }

        /// <summary>
        /// Moment of this item (for shows)
        /// </summary>
        public DateTime? datetime { get; set; }

        /// <summary>
        /// ID of this item
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// URL to an image for this item
        /// </summary>
        public string image { get; set; }

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
