using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tikasa.Model;
using Tikasa.Service;
using MyFinance.Utils;
using Tikasa.MVC.Infractstructure;
using Tikasa.Session;

namespace Tikasa.MVC.Controllers
{
    public class UserController : Controller
    {
        #region contructor
        private readonly ITikasaService _Service;


        public UserController(ITikasaService userService)
        {
            this._Service = userService;

        }
        #endregion
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Register(UserRegisterDTO model)
        {
            model.Password = CommonUtil.CreateMD5(model.Password);
            var result = _Service.Register(model);
            if (!result.HasError)
            {
                WorkContext.BizKasaContext  = XmlUtility.DeSerialize<Model.UserContext>(EncryptDecryptUtility.Decrypt( result.Data, true));
                HttpCookie ContextCookie = new HttpCookie(WorkContext.SessionTikasaKey, result.Data);
                ContextCookie.Expires = DateTime.Now.AddDays(30);
                ContextCookie.Path = "/";
                Response.Cookies.Add(ContextCookie);
            }
            return result.ToJsonResult(result.Data);

        }

        [HttpPost]
        public JsonResult Login(UserRegisterDTO model)
        {
            model.Password = CommonUtil.CreateMD5(model.Password);
            var result = _Service.Login(model);
            if (!result.HasError)
            {
                WorkContext.BizKasaContext = XmlUtility.DeSerialize<Model.UserContext>(EncryptDecryptUtility.Decrypt(result.Data, true));
                HttpCookie ContextCookie = new HttpCookie(WorkContext.SessionTikasaKey, result.Data);
                ContextCookie.Expires = DateTime.Now.AddDays(30);
                ContextCookie.Path = "/";
                Response.Cookies.Add(ContextCookie);
            }
            return result.ToJsonResult(result.Data);

        }
    }   
}