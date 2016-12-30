using EuroSpaceCenter.Models;
using System.Web.Mvc;

namespace EuroSpaceCenter.Controllers {
    [Authorize]
    public class PlanController : Controller {
        // GET: Plan
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(parkplan plan) {
            return View();
        }

        public ActionResult Join() {
            return View();
        }

        [HttpPost]
        public ActionResult Add(int id) {
            return View();
        }
    }
}