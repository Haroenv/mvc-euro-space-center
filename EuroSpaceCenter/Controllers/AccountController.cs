using EuroSpaceCenter.Models;
using EuroSpaceCenter.util;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace EuroSpaceCenter.Controllers {
    public class AccountController : Controller {
        // GET: Account
        [Authorize]
        public ActionResult Index() {
            var u = user.GetDeferred(User.Identity.Name);
            u.password = "";
            ViewBag.Ratings = u.ratings.ToList();
            return View(u);
        }

        // POST: Account
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Index(user u) {
            var active = user.GetDeferred(User.Identity.Name);
            try {
                if (ModelState.IsValid) {
                    user.Update(active, u);
                    FormsAuthentication.SetAuthCookie(u.email, false);
                    Flash.Set(TempData, "Information edited.");
                    ViewBag.Ratings = u.ratings.ToList();
                    return View(u);
                }
                return View(u);
            } catch {
                ViewBag.Ratings = u.ratings.ToList();
                Flash.Set(TempData, "Something went wrong");
                return View(u);
            }
        }

        public RedirectToRouteResult DeleteRating(int id) {
            if (user.HasRating(User.Identity.Name, id)) {
                rating.Delete(id);
                Flash.Set(TempData, "That's gone! 💨");
                return RedirectToAction("Index");
            } else {
                Flash.Set(TempData, "That's not your rating! 😏");
                return RedirectToAction("Index");
            }
        }

        // POST: Account/Password
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Password(user u) {
            var active = user.Get(User.Identity.Name);
            try {
                if (Request.Form.Get("password1") != u.password) {
                    ModelState.AddModelError(String.Empty, "Passwords don't match");
                }
                if (ModelState.IsValid) {
                    user.SetPassword(User.Identity.Name, u.password);
                    Flash.Set(TempData, "Password edited.");
                }
                return View("Index", active);
            } catch (Exception e) {
                Flash.Set(TempData, "Something went wrong " + e);
                return View("Index", active);
            }
        }

        // GET: Account/Login
        public ActionResult Login() {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(user u) {
            if (ModelState.IsValid) {
                user active = user.Get(u.email, u.password);
                if (active != null) {
                    if (active.verified) {
                        FormsAuthentication.SetAuthCookie(active.email, false);
                        return Redirect("/");
                    } else {
                        ModelState.AddModelError(String.Empty, "You didn't verify yet, can you check your emails?");
                    }
                } else {
                    ModelState.AddModelError(String.Empty, "Oops! Email or password was wrong");
                }
            }
            return View(u);
        }

        // GET Account/Logout
        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            Flash.Set(TempData, "Logged out 👋");
            return Redirect("/");
        }

        // GET: Account/Register
        public ActionResult Register() {
            return Redirect("~/Register/Index.aspx");
        }

        // Get Account/Activate?code=qsdfqsdf
        public ActionResult Activate(string code) {
            try {
                if (activation.activate(code)) {
                    Flash.Set(TempData, "You've been registered! 🎉");
                    return Redirect(url: "/");
                } else {
                    Flash.Set(TempData, "Something went wrong, try again? ❤️");
                    return Redirect(url: "/Register/Index.aspx");
                }
            } catch (Exception e) {
                Flash.Set(TempData, "Something went wrong, try again? ❤️ <details>" + e + "</details>");
                return Redirect(url: "/Register/Index.aspx");
            }
        }
    }
}