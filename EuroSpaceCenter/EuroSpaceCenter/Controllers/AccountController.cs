using EuroSpaceCenter.Models;
using EuroSpaceCenter.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EuroSpaceCenter.Controllers {
    public class AccountController : Controller {
        // GET: Account
        [Authorize]
        public ActionResult Index() {
            return View(users.Get(User.Identity.Name));
        }

        // POST: Account
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Index(user u) {
            var active = users.Get(User.Identity.Name);
            try {
                if (ModelState.IsValid) {
                    users.Update(active, u);
                    FormsAuthentication.SetAuthCookie(u.email, false);
                    Flash.Set(TempData, "Information edited.");
                    return View(u);
                }
                return View(u);
            } catch {
                Flash.Set(TempData, "Something went wrong");
                return View(u);
            }
            
        }

        // POST: Account/Password
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Password(user u) {
            var active = users.Get(User.Identity.Name);
            try {
                if (ModelState.IsValid) {
                    users.Update(active, u);
                    FormsAuthentication.SetAuthCookie(u.email, false);
                    Flash.Set(TempData, "Password edited.");
                    return View(u);
                }
                return View(u);
            } catch {
                Flash.Set(TempData, "Something went wrong");
                return View(u);
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
                user active = users.Get(u.email, u.password);
                if (active != null) {
                    FormsAuthentication.SetAuthCookie(active.email, false);
                    return Redirect("/");
                } else {
                    ModelState.AddModelError(String.Empty, "Oops! A mistake happened");
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
                if (activations.activate(code)) {
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