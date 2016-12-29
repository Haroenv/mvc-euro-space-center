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
            ViewBag.Restaurants = items.Get("restaurants");
            ViewBag.Attractions = items.Get("attractions");
            ViewBag.Shows = items.Get("shows");
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create(string type) {
            
            return View();
        }

        // POST: Admin/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(item i) {
            if (ModelState.IsValid) {
                item result = items.Create(i);
                Flash.Set(TempData, "Created 🍻");
                return RedirectToAction("Index", "Detail", i.id);
            }
            Flash.Set(TempData, "oops! 😬");
            return View(i);
        }
    }
}