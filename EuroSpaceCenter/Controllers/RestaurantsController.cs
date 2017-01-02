using EuroSpaceCenter.Models;
using System;
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
        [ResponseType(typeof(IEnumerable<Restaurant>))]
        public IHttpActionResult Restaurants() {
            var items = item.GetAllDisposed().Where(i => i.restaurant != null).Select(i => new Restaurant() {
                id = i.id,
                title = i.title,
                image = i.image,
                alt = i.alt,
                payment_types = i.restaurant.payment_type.Replace(", ", ";").Split(';'),
                rating = i.ratings.Any() ? (double?)i.ratings.Average(r => r.rating1) : null,
                ratings = i.ratings.Select(r => new RatingEntity() {
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

    internal class Restaurant {

        public Restaurant() {
        }

        public string alt { get; set; }
        public int id { get; set; }
        public string image { get; set; }
        public string[] payment_types { get; set; }
        public double? rating { get; set; }
        public IEnumerable<RatingEntity> ratings { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string description { get; set; }

    }
}
