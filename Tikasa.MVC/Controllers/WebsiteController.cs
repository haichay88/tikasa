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

        #region Action
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
        public ActionResult Edit(int Id)
        {
            ViewBag.WebsiteId = Id;
            return View();
        }
        public ActionResult List()
        {
            return View();
        }

        #endregion
        // GET: Website


        #region Ajax


        [HttpPost]
        public JsonResult Create(WebsiteDTO model)
        {
            var result = _Service.WebsiteCreate(model);
            return result.ToJsonResult(result.Data);
        }

        [HttpPost]
        public JsonResult UpdateBaseInfo(WebsiteDTO model)
        {
            var result = _Service.UpdateBaseInfo(model);
            return result.ToJsonResult(result.Data);
        }

        [HttpPost]
        public JsonResult UpdateMoreInfo(WebsiteDTO model)
        {
            var result = _Service.UpdateMoreInfo(model);
            return result.ToJsonResult(result.Data);
        }


        [HttpPost]
        public JsonResult GetWebsites(SearchModel model)
        {
            var result = _Service.GetWebsites(model);
            return result.ToJsonResult(result.Data);
        }

        [HttpPost]
        public JsonResult PublicWebsite(WebsiteDTO model)
        {
            var result = _Service.PublicWebsite(model);
            return result.ToJsonResult(result.Data);
        }

        [HttpPost]
        public JsonResult GetWebsite(SearchModel model)
        {
            var result = _Service.GetWebsite(model);
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
        #endregion
    }
}