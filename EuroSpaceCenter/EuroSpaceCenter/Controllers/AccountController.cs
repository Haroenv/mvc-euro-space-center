﻿using System;
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
    }
}