using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tikasa.Session;

namespace Tikasa.MVC.Models
{
    public class AuthorizeModel
    {
        public bool IsLogined
        {
            get
            {
                if (WorkContext.BizKasaContext == null) return false;

                return true;
            }
        }
    }
}