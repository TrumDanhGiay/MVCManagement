using DuongTrang.Core.IServices;
using Management.APICustomAuthorize;
using Management.Property;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMenuRepository _menuRepository;

        public HomeController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public ActionResult Layout()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Navbar()
        {
            return View();
        }

        public ActionResult SettingPanel()
        {
            return View();
        }

        [AuthorizeRole(ListRoles.Administrator)]
        public ActionResult Dashboard()
        {
            return View();
        }

        [AuthorizeRole(ListRoles.Administrator)]
        public ActionResult UserProfile()
        {
            return View();
        }

        [AuthorizeRole(ListRoles.Administrator)]
        public ActionResult LayoutAdmin()
        {
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roles = userManager.GetRoles(User.Identity.GetUserId());
            var menu  = _menuRepository.GetListMenuByRoles(roles.First());
            return View(menu);
        }

        public ActionResult BookManagement()
        {
            return View();
        }
    }
}
