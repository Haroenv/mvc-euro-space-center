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
            ViewBag.Items = plan.parkplans_has_items.Select(t => t.item);
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
                        // create this user without password
                        u.email = email;
                        u.verified = false;
                        u.password = Rand.String(10); // this is how WebPanel does it and I now understand why 😠😠😠
                        Email.Send(u.email, null, $"Your friend just signed you up for ESC, but you didn't have an account yet. You can temporarily log in with this password: <code>{u.password}</code>", "Log In", "http://eurospacecenter.haroenviaene.ikdoeict.be/Account/Login", "New Account");
                        user.Create(u);
                        // UX here is really hard
                        parkplan.Invite(users_id: u.id, parkplan_id: id);
                        Flash.Set(TempData, "This user didn't have an account yet, they are invited now!");
                        return RedirectToAction("Detail", new { id = id });
                    } else {
                        parkplan.Invite(users_id: u.id, parkplan_id: id);
                        Flash.Set(TempData, "Invited");
                        return RedirectToAction("Detail", new { id = id });
                    }
                } catch (Exception e) {
                    Flash.Set(TempData, "That didn't work 😲");
                    return RedirectToAction("Detail", id);
                }
            }
            Flash.Set(TempData, "That's not your plan 💁");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Add(int id, int item_id) {
            try {
                if (parkplan.HasUser(parkplan_id: id, user_id: user.Get(User.Identity.Name).id)) {
                    parkplan.AddItem(id, item_id);
                    return new HttpStatusCodeResult(204);
                }
                return new HttpStatusCodeResult(403);
            } catch {
                return new HttpStatusCodeResult(500);
            }
        }

        [HttpPost]
        public HttpStatusCodeResult Remove(int id, int item_id) {
            if (parkplan.HasUser(parkplan_id: id, user_id: user.Get(User.Identity.Name).id)) {
                parkplan.RemoveItem(id, item_id);
                return new HttpStatusCodeResult(204);
            }
            return new HttpStatusCodeResult(403);
        }
    }
}