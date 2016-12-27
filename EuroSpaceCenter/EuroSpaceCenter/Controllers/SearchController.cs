using EuroSpaceCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroSpaceCenter.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string cat)
        {
            if (cat != null) {
                // zoek categorien
                ViewBag.Items = items.Get(cat);
            } else {
                ViewBag.Items = items.GetAll();
            }
            return View();
        }
    }
}