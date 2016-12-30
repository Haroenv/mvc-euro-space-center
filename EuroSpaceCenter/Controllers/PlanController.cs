using EuroSpaceCenter.Models;
using EuroSpaceCenter.util;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EuroSpaceCenter.Controllers {
    [Authorize]
    public class PlanController : Controller {

        public ActionResult Index() {
            IEnumerable<users_has_parkplan> all = parkplan.GetAll(user.Get(User.Identity.Name).id);
            var accepted = all.Where(uhp => uhp.accepted).Select(uhp => uhp.parkplan);
            ViewBag.UnAccepted = all.Where(uhp => !uhp.accepted).Select(uhp => uhp.parkplan);
            return View(accepted);
        }

        public ActionResult Detail(int id) {
            return View(parkplan.Get(parkplan_id: id));
        }

        [HttpPost]
        public ActionResult Create(parkplan plan) {
            if (TryValidateModel(plan)) {
                var p = plan.Create(user.Get(User.Identity.Name).id);
                return RedirectToAction("Detail", p.id);
            }
            return View(plan);
        }

        public ActionResult Invite(int id, int user_id) {
            if (parkplan.HasUser(user_id: id, parkplan_id: id)) {
                parkplan.Invite(users_id: user_id, parkplan_id: id);
                Flash.Set(TempData, "Invited");
                return RedirectToAction("Detail", id);
            } else {
                Flash.Set(TempData, "That's not your plan 💁");
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult Add(int id) {
            return View();
        }
    }
}