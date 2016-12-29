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
        public ActionResult Create(item i) {
            if (ModelState.IsValid) {
                item result = item.Create(i);
                Flash.Set(TempData, "Created 🍻");
                return RedirectToAction("Index", "Detail", i.id);
            }
            Flash.Set(TempData, "oops! 😬");
            return View(i);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(attraction i) {
        //    if (ModelState.IsValid) {
        //        attraction result = attraction.Create(i);
        //        Flash.Set(TempData, "Created 🍻");
        //        return RedirectToAction("Index", "Detail", i.id);
        //    }
        //    Flash.Set(TempData, "oops! 😬");
        //    return View(i);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(show i) {
        //    if (ModelState.IsValid) {
        //        show result = show.Create(i);
        //        Flash.Set(TempData, "Created 🍻");
        //        return RedirectToAction("Index", "Detail", i.id);
        //    }
        //    Flash.Set(TempData, "oops! 😬");
        //    return View(i);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(restaurant i) {
        //    if (ModelState.IsValid) {
        //        restaurant result = restaurant.Create(i);
        //        Flash.Set(TempData, "Created 🍻");
        //        return RedirectToAction("Index", "Detail", i.id);
        //    }
        //    Flash.Set(TempData, "oops! 😬");
        //    return View(i);
        //}

        public ActionResult Extra(string category) {
            ViewBag.category = category;
            return PartialView();
        }
    }
}