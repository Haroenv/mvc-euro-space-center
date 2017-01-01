using EuroSpaceCenter.Models;
using EuroSpaceCenter.util;
using System;
using System.Web.Mvc;

namespace EuroSpaceCenter.Controllers {
    [CustomAuthorize(Roles = "admin")]
    public class AdminController : Controller {
        // GET: Admin
        public ViewResult Index() {
            ViewBag.Restaurants = item.Get("restaurants");
            ViewBag.Attractions = item.Get("attractions");
            ViewBag.Shows = item.Get("shows");
            return View();
        }

        // GET: Admin/Create
        public ViewResult Create(string cat) {
            ViewBag.category = cat;
            return View();
        }

        // POST: Admin/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(item i) {
            if (TryValidateModel(i)) {
                try {
                    item.Create(i);
                    switch (Request["category"]) {
                        case "restaurant":
                            var r = new restaurant() {
                                items_id = i.id,
                                payment_type = Request["payment_type"]
                            };
                            if (TryValidateModel(r)) {
                                restaurant.Create(r);

                            } else {
                                Flash.Set(TempData, "oops, wrong extras! 😬");
                                return View(i);
                            }
                            break;
                        case "show":
                            var s = new show() {
                                items_id = i.id,
                                datetime = DateTime.Parse(Request["datetime"])
                            };
                            if (TryValidateModel(s)) {
                                show.Create(s);
                            } else {
                                Flash.Set(TempData, "oops, wrong extras! 😬");
                                return View(i);
                            }
                            break;
                        case "attraction":
                            var a = new attraction() {
                                items_id = i.id,
                                max_height = int.Parse(Request["max_height"]),
                                min_height = int.Parse(Request["min_height"])
                            };
                            if (TryValidateModel(a)) {
                                attraction.Create(a);
                            } else {
                                Flash.Set(TempData, "oops, wrong extras! 😬");
                                return View(i);
                            }
                            break;
                        default:
                            break;
                    }
                } catch {
                    Flash.Set(TempData, "something went wrong! 😬");
                    return View(i);
                }
                Flash.Set(TempData, "Created 🍻");
                return RedirectToAction("Index", "Detail", i.id);
            }
            Flash.Set(TempData, "oops! 😬");
            return View(i);
        }

        public ActionResult Ratings() {
            return View(rating.GetAll());
        }

        public RedirectToRouteResult DeleteRating(int id) {
            rating.Delete(id);
            Flash.Set(TempData, "That's gone! 💨");
            return RedirectToAction("Ratings");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectResult Show(int? id, show i) {
            try {
                i.items_id = (int)id;
                show.Update(i);
                Flash.Set(TempData, "Yep! Updated");
            } catch (Exception e) {
                Flash.Set(TempData, "Ay caramba, I made a mistake 😦" + e);
            }
            return Redirect("/Detail/?id=" + item.Get((int)id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectResult Attraction(int? id, attraction i) {
            try {
                i.items_id = (int)id;
                attraction.Update(i);
                Flash.Set(TempData, "Boom! Updated");
            } catch (Exception) {
                Flash.Set(TempData, "Something went wrong 😦");
            }
            return Redirect("/Detail/?id=" + item.Get((int)id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectResult Restaurant(int? id, restaurant i) {
            try {
                i.items_id = (int)id;
                restaurant.Update(i);
                Flash.Set(TempData, "Bam! Updated");
            } catch (Exception) {
                Flash.Set(TempData, "Sorry 😦");
            }
            return Redirect("/Detail/?id=" + item.Get((int)id));
        }

        public RedirectResult Delete(int id) {
            item.Delete(id);
            Flash.Set(TempData, "That's gone! 💨");
            return Redirect("/Search");
        }

        public PartialViewResult Extra(string category) {
            ViewBag.category = category;
            return PartialView();
        }
    }
}