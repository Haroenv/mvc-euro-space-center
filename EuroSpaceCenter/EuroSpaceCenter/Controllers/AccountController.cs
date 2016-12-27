using EuroSpaceCenter.Models;
using EuroSpaceCenter.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroSpaceCenter.Controllers {
    public class AccountController : Controller {
        // GET: Account
        public ActionResult Index() {
            return View();
        }

        // GET: Account/Register
        public ActionResult Register() {
            return Redirect("~/Register/Index.aspx");
        }

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