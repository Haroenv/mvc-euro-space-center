using EuroSpaceCenter.Models;
using EuroSpaceCenter.util;
using System;
using System.Web.Mvc;

namespace EuroSpaceCenter.Controllers {
    public class DetailController : Controller {
        // GET: Detail?id={id}
        public ActionResult Index(int? id) {
            if (id == null) {
                return Redirect("/Search");
            }
            item i = item.Get((int)id);
            if (i == null) {
                return Redirect("/Search");
            }
            ViewBag.Ratings = rating.Get((int)id);
            ViewBag.Rating = new rating();
            return View(i);
        }

        // POST Detail/Rate
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Rate(rating r) {
            try {
                if (ModelState.IsValid) {
                    r.users_id = user.Get(User.Identity.Name).id;
                    r.datetime = DateTime.Now;
                    rating.Rate(r);
                    Flash.Set(TempData, "Rated! 🍾");
                    return Redirect("/Detail?id=" + r.items_id);
                }
                item i = item.Get(r.items_id);
                ViewBag.Ratings = rating.Get(r.items_id);
                ViewBag.Rating = r;
                Flash.Set(TempData, "You forgot to fill in something 😧");
                return View("Index", i);
            } catch {
                Flash.Set(TempData, "Something went wrong 😕");
                return Redirect("/Detail?id=" + r.items_id);
            }
        }

        [CustomAuthorize(Roles = "admin")]
        public ActionResult Edit(int? id) {
            if (id == null) {
                Flash.Set(TempData, "You didn't supply an id 🆔");
                return Redirect("/Search");
            }
            item i = item.Get((int)id);
            if (i == null) {
                Flash.Set(TempData, "This item doesn't exist 😞");
                return Redirect("/Search");
            }
            return View(i);
        }
    }
}