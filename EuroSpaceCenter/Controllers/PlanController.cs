using EuroSpaceCenter.Models;
using EuroSpaceCenter.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EuroSpaceCenter.Controllers {
    [Authorize]
    public class PlanController : Controller {

        public ActionResult Index() {
            List<users_has_parkplan> all = parkplan.GetAll(user.Get(User.Identity.Name).id);
            var accepted = all.Where(uhp => uhp.accepted).Select(uhp => uhp.parkplan);
            ViewBag.UnAccepted = all.Where(uhp => !uhp.accepted).Select(uhp => uhp.parkplan);
            return View(accepted);
        }

        public ActionResult Detail(int id) {
            return View(parkplan.Get(parkplan_id: id));
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id) {
            //try {
                if (parkplan.HasUser(user_id: user.Get(User.Identity.Name).id, parkplan_id: id)) {
                    parkplan.Delete(id);
                    return new HttpStatusCodeResult(204);
                } else {
                    return new HttpStatusCodeResult(403);
                }
            //} catch (Exception ex){
            //    return Content(ex.ToString());
            //}
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(parkplan plan) {
            if (TryValidateModel(plan)) {
                var p = plan.Create(user.Get(User.Identity.Name).id);
                return RedirectToAction("Detail", new { id = p.id });
            }
            return View(plan);
        }

        public ActionResult Invite(int id, int user_id) {
            if (parkplan.HasUser(user_id: user_id, parkplan_id: id)) {
                parkplan.Invite(users_id: user_id, parkplan_id: id);
                Flash.Set(TempData, "Invited");
                return RedirectToAction("Detail", new { id = id });
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