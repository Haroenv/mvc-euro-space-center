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

        //// POST: Admin/create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(item i, restaurant r = null, attraction a = null, show s = null) {
        //    if (TryValidateModel(i)) {
        //        item result = item.Create(i);
        //        if (TryValidateModel(r)) {
        //            r.items_id = i.id;
        //            restaurant.Create(r);
        //        } else if (TryValidateModel(a)) {
        //            a.items_id = i.id; // oops
        //            attraction.Create(a);
        //        } else if (TryValidateModel(s)) {
        //            s.items_id = i.id;
        //            show.Create(s);
        //        }
        //        Flash.Set(TempData, "Created 🍻");
        //        return RedirectToAction("Index", "Detail", i.id);
        //    }
        //    Flash.Set(TempData, "oops! 😬");
        //    return View(i);
        //}

        // POST: Admin/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(item i) {
            if (TryValidateModel(i)) {
                item result = item.Create(i);
                Flash.Set(TempData, "Created 🍻");
                return RedirectToAction("Index", "Detail", i.id);
            }
            Flash.Set(TempData, "oops! 😬");
            return View(i);
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