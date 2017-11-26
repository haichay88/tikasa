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
        Response<int> UpdateBaseInfo(WebsiteDTO model);
        Response<int> UpdateMoreInfo(WebsiteDTO model);
        Response<ListResult<WebsiteDTO>> GetWebsites(SearchModel model);
        Response<int> PublicWebsite(WebsiteDTO model);
    }
    public partial class TikasaService
    {
        public Response<int> PublicWebsite(WebsiteDTO model)
        {
            int result = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWebsiteBusiness>().PublicWebsite(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<ListResult<WebsiteDTO>> GetWebsites(SearchModel model)
        {
            ListResult<WebsiteDTO> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWebsiteBusiness>().GetWebsites(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<int> UpdateMoreInfo(WebsiteDTO model)
        {
            int result = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWebsiteBusiness>().UpdateMoreInfo(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<int> UpdateBaseInfo(WebsiteDTO model)
        {
            int result = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWebsiteBusiness>().UpdateBaseInfo(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
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
