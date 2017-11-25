using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tikasa.Session;

namespace Tikasa.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {


            return View();
        }
        public ActionResult Create()
        {

            ViewBag.userId = Tikasa.Session.WorkContext.BizKasaContext!=null ? Tikasa.Session.WorkContext.BizKasaContext.UserId:0;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Logout()
        {

            HttpContext.Session.Clear();

            HttpCookie myCookie = new HttpCookie(WorkContext.SessionTikasaKey);
            myCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(myCookie);

            //SendoSession.Current.Dispose();

            WorkContext.BizKasaContext = null;
            
            return RedirectToAction("Index");
          
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}