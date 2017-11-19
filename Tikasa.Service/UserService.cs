using MyFinance.Core;
using MyFinance.Extention;
using MyFinance.Utils;
using Tikasa.Business;
using Tikasa.Model;

namespace Tikasa.Service
{
    public interface IUserService
    {
        Response<string> Register(UserRegisterDTO model);
        Response<string> Login(UserRegisterDTO model);
    }
    public partial class TikasaService
    {
        public Response<string> Register(UserRegisterDTO model)
        {
            string result = string.Empty;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().Register(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<string> Login(UserRegisterDTO model)
        {
            string result = string.Empty;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().Login(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
    }
}
