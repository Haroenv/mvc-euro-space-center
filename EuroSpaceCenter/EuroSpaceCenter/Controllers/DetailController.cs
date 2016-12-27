using EuroSpaceCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroSpaceCenter.Controllers {
    public class DetailController : Controller {
        // GET: Detail/{id}
        public ActionResult Index(int? id) {
            if (id == null) {
                return Redirect("/Search");
            }
            item i = items.Get((int)id);
            if (i == null) {
                return Redirect("/Search");
            }
            return View(i);
        }
    }
}