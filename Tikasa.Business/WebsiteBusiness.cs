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
        int UpdateBaseInfo(WebsiteDTO model);
        int UpdateMoreInfo(WebsiteDTO model);
        int PublicWebsite(WebsiteDTO model);
        WebsiteDTO GetWebsite(SearchModel model);
        List<CategoryDTO> GetCategories();
        List<CategoryDTO> GetTypeOfWebsite();
        ListResult<WebsiteDTO> GetWebsites(SearchModel model);
    }
    public class WebsiteBusiness : BusinessBase, IWebsiteBusiness
    {
        #region Contructor
        public WebsiteBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        #endregion

        #region Properties
        private readonly IUnitOfWork unitOfWork;
        public const string NOTE = "NOTE";
        #endregion

        #region Methods
        public int Create(WebsiteDTO model)
        {
            try
            {
                var websiteRepository = unitOfWork.Repository<Website>();
                var note = unitOfWork.Repository<DefaultValue>().Get(a => a.Code == NOTE);
                var row = new Website()
                {
                    Name = model.Name,
                    CreatedDate = DateTime.Now,
                    UserId = WorkContext.BizKasaContext.UserId,
                    StatusId = (int)WebsiteStatus.Draft,
                    ExpiredDate = DateTime.Now.AddDays(30),
                    PlatformId = (int)Platform.NA,
                    SiteAge = 0,
                    Note = note.Value
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

        public int UpdateBaseInfo(WebsiteDTO model)
        {
            try
            {
                var websiteRepository = unitOfWork.Repository<Website>();
                var website = websiteRepository.Get(a => a.Id == model.Id);
                website.HasRevenue = model.HasRevenue;
                website.GAAccountId = model.GAAccountId;
                website.GAPropertyId = model.GAPropertyId;
                website.HasTraffic = model.HasTraffic;
                website.AvgExpense = model.AvgExpense;
                website.AvgRevenue = model.AvgRevenue;
                website.GoliveMonth = model.GoliveMonth;
                website.GoliveYear = model.GoliveYear;
                website.TypeOfCategoryId = model.TypeOfCategoryId;
                website.TypeOfWebsiteId = model.TypeOfWebsiteId;
                website.Step = model.Step;
                websiteRepository.Update(website);
                unitOfWork.Commit();

                return website.Id;
            }
            catch (Exception)
            {
                base.AddError("Có lỗi trong quá trình tạo dữ liệu");
                return 0;
            }

        }

        public int UpdateMoreInfo(WebsiteDTO model)
        {
            try
            {
                var websiteRepository = unitOfWork.Repository<Website>();
                var website = websiteRepository.Get(a => a.Id == model.Id);
                website.Title = model.Title;
                website.Summary = model.Summary;
                website.Note = model.Note;
                website.MinimumPrice = model.MinimumPrice;
                website.BuyItNowPrice = model.BuyItNowPrice;
                website.IsAuction = model.IsAuction;
                website.Reserve = model.Reserve;
                website.Step = model.Step;
                website.StartingPrice = model.StartingPrice;
                websiteRepository.Update(website);
                unitOfWork.Commit();

                return website.Id;
            }
            catch (Exception)
            {
                base.AddError("Có lỗi trong quá trình tạo dữ liệu");
                return 0;
            }

        }

        public int PublicWebsite(WebsiteDTO model)
        {
            try
            {
                var websiteRepository = unitOfWork.Repository<Website>();
                var website = websiteRepository.Get(a => a.Id == model.Id);
                website.StatusId = (int)WebsiteStatus.Active;
                website.Step = model.Step;
                websiteRepository.Update(website);
                unitOfWork.Commit();

                return website.Id;
            }
            catch (Exception)
            {
                base.AddError("Có lỗi trong quá trình tạo dữ liệu");
                return 0;
            }

        }

        public ListResult<WebsiteDTO> GetWebsites(SearchModel model)
        {
            model.Page.currentPage--;
            ListResult<WebsiteDTO> result = new ListResult<WebsiteDTO>();
            var websiteRepository = unitOfWork.Repository<Website>().GetQueryable().Where(a => a.StatusId == (int)WebsiteStatus.Active);
            if (!string.IsNullOrEmpty(model.Keyword))
                websiteRepository = websiteRepository.Where(a => a.Name.Contains(model.Keyword));

            result.TotalRow = websiteRepository.Count();

            var website = websiteRepository
                .OrderBy(a => a.Id).Take(model.Page.pageSize).Skip(model.Page.currentPage * model.Page.pageSize)
                .Select(a => new WebsiteDTO()
                {
                    Id = a.Id,
                    Step = a.Step,
                    Name = a.Name,
                    SiteAge = a.SiteAge,
                    PlatformId = a.PlatformId,
                    CreatedDate = a.CreatedDate,
                    TypeOfWebsiteId = a.TypeOfWebsiteId.HasValue ? a.TypeOfWebsiteId.Value : 0,
                    StatusId = a.StatusId,
                    IsCertificated = a.IsCertificated,
                    HasRevenue = a.HasRevenue,
                    HasTraffic = a.HasTraffic,
                    IsAuction = a.IsAuction,
                    AvgExpense = a.AvgExpense,
                    AvgRevenue = a.AvgRevenue,
                    BuyItNowPrice = a.BuyItNowPrice,
                    GoliveMonth = a.GoliveMonth,
                    GoliveYear = a.GoliveYear,
                    MinimumPrice = a.MinimumPrice,
                    Note = a.Note,
                    Reserve = a.Reserve,
                    StartingPrice = a.StartingPrice,
                    Summary = a.Summary,
                    Title = a.Title,
                    TypeOfCategoryId = a.TypeOfCategoryId
                })
                .ToList();
            result.Data = website;
            return result;
        }

        public WebsiteDTO GetWebsite(SearchModel model)
        {
            var websiteRepository = unitOfWork.Repository<Website>().GetQueryable().Where(a => a.Id == model.Id);
            if (model.UserId > 0)
                websiteRepository = websiteRepository.Where(a => a.UserId == model.UserId);
            var website = websiteRepository.Select(a => new WebsiteDTO()
                {
                    Id = a.Id,
                    Step = a.Step,
                    Name = a.Name,
                    SiteAge = a.SiteAge,
                    PlatformId = a.PlatformId,
                    CreatedDate = a.CreatedDate,
                    GAPropertyId=a.GAPropertyId,
                    GAAccountId=a.GAAccountId,
                    TypeOfWebsiteId = a.TypeOfWebsiteId.HasValue ? a.TypeOfWebsiteId.Value : 0,
                    StatusId = a.StatusId,
                    IsCertificated = a.IsCertificated,
                    HasRevenue = a.HasRevenue,
                    HasTraffic = a.HasTraffic,
                    IsAuction = a.IsAuction,
                    AvgExpense = a.AvgExpense,
                    AvgRevenue = a.AvgRevenue,
                    BuyItNowPrice = a.BuyItNowPrice,
                    GoliveMonth = a.GoliveMonth,
                    GoliveYear = a.GoliveYear,
                    MinimumPrice = a.MinimumPrice,
                    Note = a.Note,
                    Reserve = a.Reserve,
                    StartingPrice = a.StartingPrice,
                    Summary = a.Summary,
                    Title = a.Title,
                    TypeOfCategoryId = a.TypeOfCategoryId
                })
                .FirstOrDefault();
            return website;
        }

        public List<CategoryDTO> GetCategories()
        {
            var cateRepository = unitOfWork.Repository<Category>();
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
