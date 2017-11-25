using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tikasa.Model;
using Tikasa.Service;
using Tikasa.MVC.Infractstructure;

namespace Tikasa.MVC.Controllers
{
    public class WebsiteController : Controller
    {
        #region contructor
        private readonly ITikasaService _Service;


        public WebsiteController(ITikasaService userService)
        {
            this._Service = userService;

        }
        #endregion
        // GET: Website
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Detail(int Id)
        {
            ViewBag.WebsiteId = Id;
            return View();
        }
        [HttpPost]
        public JsonResult Create(WebsiteDTO model)
        {
            var result = _Service.WebsiteCreate(model);
            return result.ToJsonResult(result.Data);
        }

        [HttpGet]
        public JsonResult GetWebsite(int Id)
        {
            var result = _Service.GetWebsite(Id);
            return result.ToJsonResult(result.Data);
        }
        [HttpGet]
        public JsonResult GetCategories()
        {
            var result = _Service.GetCategories();
            return result.ToJsonResult(result.Data);
        }
        [HttpGet]
        public JsonResult GetTypeOfWebsite()
        {
            var result = _Service.GetTypeOfWebsite();
            return result.ToJsonResult(result.Data);
        }
    }
}