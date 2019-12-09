using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BioSys.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Welcome to BioSys, the Born in Bradford Biobanking Web Tool.";
            return View();
        }

        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Address and contact information relevant to the BiB Biosys webtool and Biobanking";
            return View();
        }

        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        public ActionResult Biobanking()
        {
            return View();
        }
    }
}