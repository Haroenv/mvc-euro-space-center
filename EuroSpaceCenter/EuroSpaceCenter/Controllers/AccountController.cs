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
        public ActionResult Index() {
            return View();
        }

        // GET: Account/Login
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(user u) {
            if (ModelState.IsValid) {
                user active = users.Get(u.email, u.password);
                if (active != null) {
                    FormsAuthentication.SetAuthCookie(active.email, false);
                    Flash.Set(TempData, User.Identity.IsAuthenticated + User.Identity.Name);
                    return Redirect("/");
                } else {
                    ModelState.AddModelError(String.Empty, "Oops! A mistake happened");
                }
            }
            return View(u);
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