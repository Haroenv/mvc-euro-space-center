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

        // plans are public on purpose
        public ActionResult Detail(int id) {
            var plan = parkplan.Get(parkplan_id: id);
            ViewBag.Items = item.GetAll();
            ViewBag.PlanItems = plan.parkplans_has_items.ToList();
            ViewBag.Users = plan.users_has_parkplans.Select(t => t.user);
            return View(plan);
        }

        public HttpStatusCodeResult Set(int id) {
            if (parkplan.HasUser(user_id: user.Get(User.Identity.Name).id, parkplan_id: id)) {
                Session.Add("plan", new { id = id });
                return new HttpStatusCodeResult(200);
            } else {
                return new HttpStatusCodeResult(403);
            }
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public HttpStatusCodeResult Delete(int id) {
            try {
                if (parkplan.HasUser(user_id: user.Get(User.Identity.Name).id, parkplan_id: id)) {
                    parkplan.Delete(id);
                    return new HttpStatusCodeResult(204);
                } else {
                    return new HttpStatusCodeResult(403);
                }
            } catch (Exception) {
                return new HttpStatusCodeResult(500);
            }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Invite(int id, string email) {
            // parkplan needs to be yours
            if (parkplan.HasUser(parkplan_id: id, user_id: user.Get(User.Identity.Name).id)) {
               try {
                    var u = user.Get(email);
                    // does invitee exist?
                    if (u == null) {
                        Email.Send(email, null, $"Your friend just signed you up for ESC, but you didn't have an account yet. You can Register at this link", "Log In", "http://eurospacecenter.haroenviaene.ikdoeict.be/Register/Index?plan=" + id, "Invited");
                        Flash.Set(TempData, "This user didn't have an account yet, they are invited now!");
                        return RedirectToAction("Detail", new { id = id });
                    } else {
                        parkplan.Invite(users_id: u.id, parkplan_id: id);
                        Flash.Set(TempData, "Invited");
                        return RedirectToAction("Detail", new { id = id });
                    }
                } catch (Exception e) {
                    Flash.Set(TempData, "That didn't work 😲");
                    return RedirectToAction("Detail", new { id = id });
                }
            }
            Flash.Set(TempData, "That's not your plan 💁");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Add(int id, int item_id) {
            try {
                if (parkplan.HasUser(parkplan_id: id, user_id: user.Get(User.Identity.Name).id)) {
                    int has_id = parkplan.AddItem(id, item_id);
                    return Json(new { id = has_id });
                }
                return new HttpStatusCodeResult(403);
            } catch {
                return new HttpStatusCodeResult(500);
            }
        }

        [HttpPost]
        public HttpStatusCodeResult Remove(int id, int plan_has_item) {
            if (parkplan.HasUser(parkplan_id: id, user_id: user.Get(User.Identity.Name).id)) {
                parkplan.RemoveItem(plan_has_item);
                return new HttpStatusCodeResult(204);
            }
            return new HttpStatusCodeResult(403);
        }

        [HttpPost]
        public HttpStatusCodeResult Accept(int id) {
            if (parkplan.HasUser(parkplan_id: id, user_id: user.Get(User.Identity.Name).id)) {
                parkplan.Accept(user.Get(User.Identity.Name).id, id);
                return new HttpStatusCodeResult(200);
            }
            return new HttpStatusCodeResult(403);
        }

        [HttpPost]
        public HttpStatusCodeResult Reject(int id) {
            if (parkplan.HasUser(parkplan_id: id, user_id: user.Get(User.Identity.Name).id)) {
                parkplan.Reject(user.Get(User.Identity.Name).id, id);
                return new HttpStatusCodeResult(204);
            }
            return new HttpStatusCodeResult(403);
        }
    }
}