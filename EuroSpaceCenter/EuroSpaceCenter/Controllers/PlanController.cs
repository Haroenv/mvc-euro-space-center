using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroSpaceCenter.Controllers {
    [Authorize]
    public class PlanController : Controller {
        // GET: Plan
        public ActionResult Index() {
            return View();
        }

        public ActionResult Join() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int id) {
            return View();
        }
    }
}