using MyFinance.Core;
using MyFinance.Extention;
using MyFinance.Utils;
using System.Collections.Generic;
using Tikasa.Business;
using Tikasa.Model;

namespace Tikasa.Service
{
    public interface IWebsiteService
    {
        Response<int> WebsiteCreate(WebsiteDTO model);
        Response<WebsiteDTO> GetWebsite(int Id);
        Response<List<CategoryDTO>> GetTypeOfWebsite();
        Response<List<CategoryDTO>> GetCategories();
    }
    public partial class TikasaService
    {
        public Response<List<CategoryDTO>> GetCategories()
        {
            List<CategoryDTO> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWebsiteBusiness>().GetCategories();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<CategoryDTO>> GetTypeOfWebsite()
        {
            List<CategoryDTO> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWebsiteBusiness>().GetTypeOfWebsite();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<WebsiteDTO> GetWebsite(int Id)
        {
            WebsiteDTO result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWebsiteBusiness>().GetWebsite(Id);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<int> WebsiteCreate(WebsiteDTO model)
        {
            int result =0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWebsiteBusiness>().Create(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
       
    }
}
