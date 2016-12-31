using EuroSpaceCenter.Models;
using EuroSpaceCenter.util;
using System;
using System.Linq;
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
            ViewBag.id = id;
            ViewBag.Ratings = rating.Get((int)id);
            ViewBag.Rating = new rating();
            if (i.attraction != null) {
                ViewBag.Properties = i.attraction.GetType().GetProperties().Where(itm => itm.Name != "item" && itm.Name != "items_id");
                ViewBag.PropModel = i.attraction;
            } else if (i.show != null) {
                ViewBag.Properties = i.show.GetType().GetProperties().Where(itm => itm.Name != "item" && itm.Name != "items_id");
                ViewBag.PropModel = i.show;
            } else if (i.restaurant != null) {
                ViewBag.Properties = i.restaurant.GetType().GetProperties().Where(itm => itm.Name != "item" && itm.Name != "items_id");
                ViewBag.PropModel = i.restaurant;
            }
            if (User.Identity.IsAuthenticated) {
                ViewBag.Parkplans = parkplan.GetAll(user.Get(User.Identity.Name).id).Where(uhp => uhp.accepted).Select(uhp => uhp.parkplan);
            }
            return View(i);
        }

        // POST Detail/Rate/
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
                ViewBag.id = r.items_id;
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

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Edit(item i) {
            if (TryValidateModel(i)) {
                item result = item.Update(i);
                Flash.Set(TempData, "Updated 🍻");
                return RedirectToAction("Index", "Detail", i.id);
            }
            Flash.Set(TempData, "oops! 😬");
            return View(i);
        }
    }
}