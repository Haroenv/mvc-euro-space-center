using EuroSpaceCenter.Models;
using EuroSpaceCenter.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroSpaceCenter.Controllers {
    [CustomAuthorizeAttribute(Roles = "admin")]
    public class AdminController : Controller {
        // GET: Admin
        public ActionResult Index() {
            ViewBag.Restaurants = item.Get("restaurants");
            ViewBag.Attractions = item.Get("attractions");
            ViewBag.Shows = item.Get("shows");
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create(string cat) {
            ViewBag.category = cat;
            return View();
        }

        // POST: Admin/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(item i, restaurant r = null, attraction a = null, show s = null) {
            if (ModelState.IsValid) {
                item result = item.Create(i);
                if (r != null) {
                    restaurant.Create((restaurant)r);
                } else if (a != null) {
                    attraction.Create((attraction)a);
                } else if (s != null) {
                    show.Create((show)s);
                }
                Flash.Set(TempData, "Created 🍻");
                return RedirectToAction("Index", "Detail", i.id);
            }
            Flash.Set(TempData, "oops! 😬");
            return View(i);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(attraction a) {
            try {
                attraction result = attraction.Create(a);
                return Content("ok");
            } catch {
                return new HttpStatusCodeResult(500);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(show s) {
            try { 
                show result = show.Create(s);
                return Content("ok");
            } catch {
                return new HttpStatusCodeResult(500);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(restaurant r) {
            try {
                restaurant result = restaurant.Create(r);
                return Content("ok");
            } catch {
                return new HttpStatusCodeResult(500);
            }
        }

        public ActionResult Extra(string category) {
            ViewBag.category = category;
            return PartialView();
        }
    }
}