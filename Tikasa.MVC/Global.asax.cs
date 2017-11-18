using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Tikasa.MVC.App_Start;
using Tikasa.MVC.Infractstructure;
using Tikasa.Session;

namespace Tikasa.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityWebActivator.Start();
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            try
            {
                HttpCookie authCookie = Request.Cookies[WorkContext.CookieBizkasaKey];
                if (authCookie != null)
                {
                    string authTicket = EncryptDecryptUtility.Decrypt(authCookie.Value, true);
                    //FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    Model.UserContext serializeModel = XmlUtility.DeSerialize<Model.UserContext>(authTicket);
                    KasaPrincipal newUser = new KasaPrincipal(serializeModel.UserName);
                    if (serializeModel != null)
                    {
                        newUser.UserId = serializeModel.UserId;
                        //newUser.FirstName = serializeModel.;
                        newUser.LastName = serializeModel.Email;
                        //newUser.roles = serializeModel.;

                        HttpContext.Current.User = newUser;
                    }


                }
            }
            catch (Exception)
            {


            }


        }
    }
}
