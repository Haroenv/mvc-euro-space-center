using EuroSpaceCenter.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System;
using System.Web.Http.Description;

namespace EuroSpaceCenter.Controllers {
    public class ItemsController : ApiController {

        /// <summary>
        /// Get all of the items in Euro Space Center
        /// </summary>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Item>))]
        public IHttpActionResult Items() {
            var items = item.GetAllDisposed().Select(i => new Item() {
                id = i.id,
                title = i.title,
                image = i.image,
                alt = i.alt,
                payment_type = i.restaurant != null ? i.restaurant.payment_type : null,
                datetime = i.show != null ? i.show.datetime : null,
                min_height = i.attraction != null ? i.attraction.max_height : null,
                max_height = i.attraction != null ? i.attraction.min_height : null,
                rating = i.ratings.Any() ? i.ratings.Average(r => r.rating1) : double.NaN,
                ratings = i.ratings.Select(r => new RatingEntity() {
                    users_id = r.users_id,
                    datetime = r.datetime,
                    rating = r.rating1,
                    message = r.message
                }),
                url = Url.Content($"~/Detail?id={i.id}")
            });

            return Json(items);
        }

        /// <summary>
        /// Get details about a single item
        /// </summary>
        /// <param name="id">the id of the item</param>
        [HttpGet]
        [ResponseType(typeof(Item))]
        public IHttpActionResult Single(int id) {
            var i = item.Get(id);
            if (i == null) {
                return NotFound();
            }

            var result = new Item() {
                id = i.id,
                title = i.title,
                image = i.image,
                alt = i.alt,
                payment_type = i.restaurant != null ? i.restaurant.payment_type : null,
                datetime = i.show != null ? i.show.datetime : null,
                min_height = i.attraction != null ? i.attraction.max_height : null,
                max_height = i.attraction != null ? i.attraction.min_height : null,
                rating = i.ratings.Any() ? (double?)i.ratings.Average(r => r.rating1) : null,
                ratings = i.ratings.Select(r => new RatingEntity() {
                    users_id = r.users_id,
                    datetime = r.datetime,
                    rating = r.rating1,
                    message = r.message
                }),
                url = Url.Content($"~/Detail?id={i.id}"),
                description = i.description
            };

            return Json(result);
        }
    }

    internal class Item {

        public Item() {
        }

        public string alt { get; set; }
        public DateTime? datetime { get; set; }
        public int id { get; set; }
        public string image { get; set; }
        public int? max_height { get; set; }
        public int? min_height { get; set; }
        public string payment_type { get; set; }
        public double? rating { get; set; }
        public IEnumerable<RatingEntity> ratings { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string description { get; set; }
    }
}
