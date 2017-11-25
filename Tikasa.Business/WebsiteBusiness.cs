using MyFinance.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tikasa.Data;
using Tikasa.Entities;
using Tikasa.Model;
using Tikasa.Session;

namespace Tikasa.Business
{
    public interface IWebsiteBusiness
    {
        int Create(WebsiteDTO model);
        WebsiteDTO GetWebsite(int Id);
        List<CategoryDTO> GetCategories();
        List<CategoryDTO> GetTypeOfWebsite();
    }
    public class WebsiteBusiness: BusinessBase, IWebsiteBusiness
    {
        #region Contructor
        public WebsiteBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        #endregion

        #region Properties
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Methods
        public int Create(WebsiteDTO model)
        {
            try
            {
                var websiteRepository = unitOfWork.Repository<Website>();

                var row = new Website()
                {
                    Name = model.Name,
                    CreatedDate = DateTime.Now,
                    UserId = WorkContext.BizKasaContext.UserId,
                    StatusId=(int) WebsiteStatus.Draft,
                    ExpiredDate=DateTime.Now.AddDays(30),
                    PlatformId= (int)Platform.NA,
                    SiteAge=0,
                };

                websiteRepository.Add(row);
                unitOfWork.Commit();

                return row.Id;
            }
            catch (Exception)
            {
                base.AddError("Có lỗi trong quá trình tạo dữ liệu");
                return 0;
            }

        }

        public WebsiteDTO GetWebsite(int Id)
        {
            var websiteRepository = unitOfWork.Repository<Website>();
            var website = websiteRepository.GetQueryable().Where(a => a.Id == Id && a.UserId==WorkContext.BizKasaContext.UserId)
                .Select(a=> new WebsiteDTO() {
                    Id=a.Id,
                    Name=a.Name,
                    SiteAge=a.SiteAge,
                   PlatformId=a.PlatformId,
                   CreatedDate=a.CreatedDate,
                   SiteTypeId=a.TypeOfWebsiteId.HasValue?a.TypeOfWebsiteId.Value:0,
                   StatusId=a.StatusId,
                   IsCertificated=a.IsCertificated
                })
                .FirstOrDefault();
            return website;
        }

        public List<CategoryDTO> GetCategories()
        {
            var cateRepository = unitOfWork.Repository<Category>();
            var cates = cateRepository.GetAll();
            var parents = cates.Where(a => a.ParentId == 0)
                .Select(a=> new CategoryDTO(){
                Id=a.Id,
                Name=a.Name,
            }).ToList();

            foreach (var item in parents)
            {
                item.Childs = cates.Where(a => a.ParentId == item.Id)
                    .Select(a => new CategoryDTO()
                    {
                        Id=a.Id,
                        Name=a.Name,
                        ParentId=a.ParentId
                    })
                    .ToList();
            }
            return parents;
        }

        public List<CategoryDTO> GetTypeOfWebsite()
        {
            var cateRepository = unitOfWork.Repository<TypeOfWebsite>();
            var cates = cateRepository.GetAll();
            var parents = cates.Where(a => a.ParentId == 0)
                .Select(a => new CategoryDTO()
                {
                    Id = a.Id,
                    Name = a.Name,
                }).ToList();

            foreach (var item in parents)
            {
                item.Childs = cates.Where(a => a.ParentId == item.Id)
                    .Select(a => new CategoryDTO()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        ParentId = a.ParentId
                    })
                    .ToList();
            }
            return parents;
        }

        #endregion
    }
}
