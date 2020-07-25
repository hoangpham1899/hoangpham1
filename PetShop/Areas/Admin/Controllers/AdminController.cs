using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult MenuAD()
        {
            return PartialView();
        }
        public PartialViewResult MenuChon()
        {
            return PartialView();
        }
    }
}