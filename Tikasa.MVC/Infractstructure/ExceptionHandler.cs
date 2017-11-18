using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace Tikasa.MVC.Infractstructure
{
    public class ExceptionHandler : System.Web.Mvc.IExceptionFilter
    {
        //private static readonly ILog logger =
        //   LogManager.GetLogger(typeof(ExceptionHandler));
        public static bool IsAjaxRequest()
        {
            var request = HttpContext.Current.Request;
            return ((request["X-Requested-With"] == "XMLHttpRequest") || ((request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest")));
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {

              //  logger.Error(filterContext.Exception);
                //if (filterContext.HttpContext.Request.IsAuthenticated)
                //{
                //    if (WorkContext.BizKasaContext == null)
                //    {

                //        HttpContext.Current.Response.Redirect("~/CPanelAdmin/Home");
                //        return;

                //    }
                //    else
                //    {
                //        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
                //        return;
                //    }

                //}

                //if (IsAjaxRequest())
                //{
                //    HttpContext.Current.Response.Redirect("~/Home/Error?code=999");
                //    return;
                //}

                //if (!HttpContext.Current.IsDebuggingEnabled)
                //{
                //    if (filterContext.Exception is System.Web.HttpRequestValidationException)
                //    {
                //        HttpContext.Current.Response.Redirect("~/Home/Error?code=9000");
                //        return;
                //    }
                //}

                //HttpContext.Current.Response.Redirect("~/CPanelAdmin/Home/Logon");

            }
        }
    }
    public class KasaPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public KasaPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] roles { get; set; }
    }
}