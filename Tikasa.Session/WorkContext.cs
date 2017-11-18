using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tikasa.Model;

namespace Tikasa.Session
{
    public static class WorkContext
    {
        public const string SessionTikasaKey = "browser";
        public const string CookieBizkasaKey = "BizkasaKey";
        public const string SessionBizkasaInsideKey = "Insidekasa";
        public static void SetInSession(object value)
        {
            HttpContext.Current.Session[SessionTikasaKey] = value;
        }
        public static T GetSession<T>()
        {
            return (T)HttpContext.Current.Session[SessionTikasaKey];
        }

        public static UserContext BizKasaContext
        {
            get
            {
                if (HttpContext.Current.Session != null)
                    return HttpContext.Current.Session[SessionTikasaKey] as UserContext;
                else
                    return null;
            }
            set
            {
                if (HttpContext.Current.Session != null)
                    HttpContext.Current.Session[SessionTikasaKey] = value;
            }
        }

       
    }
   

}
