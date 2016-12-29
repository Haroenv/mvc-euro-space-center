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
        //            a.item_id = i.id; // oops
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

        public ActionResult Extra(string category) {
            ViewBag.category = category;
            return PartialView();
        }
    }
}