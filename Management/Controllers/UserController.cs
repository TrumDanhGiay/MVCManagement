using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Management.Controllers
{
    public class UserController : Controller
    {
        public ActionResult LayoutUser()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }

    }
}