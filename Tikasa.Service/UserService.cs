using MyFinance.Core;
using MyFinance.Extention;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tikasa.Business;
using Tikasa.Model;

namespace Tikasa.Service
{
    public interface IUserService
    {
        Response<bool> Register(UserRegisterDTO model);
    }
    public partial class TikasaService
    {
        public Response<bool> Register(UserRegisterDTO model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().Register(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
    }
}
