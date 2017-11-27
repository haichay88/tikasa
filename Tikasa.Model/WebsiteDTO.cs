using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tikasa.Model
{
   public class WebsiteDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int PlatformId { get; set; }
        public int SiteAge { get; set; }
        public int Step { get; set; }
        public bool IsCertificated { get; set; }
        public bool IsAuction { get; set; }
        public bool HasTraffic { get; set; }
        public bool HasRevenue { get; set; }
        public string Note { get; set; }
        public Nullable<int> TypeOfWebsiteId { get; set; }
        public Nullable<int> TypeOfCategoryId { get; set; }
        public Nullable<decimal> NetProfit { get; set; }
        public int StatusId { get; set; }
        public string GAAccountId { get; set; }
        public string GAPropertyId { get; set; }
        public string Summary { get; set; }
        public Nullable<int> GoliveMonth { get; set; }
        public Nullable<int> GoliveYear { get; set; }
        public Nullable<decimal> StartingPrice { get; set; }
        public Nullable<decimal> Reserve { get; set; }
        public Nullable<decimal> MinimumPrice { get; set; }
        public Nullable<decimal> BuyItNowPrice { get; set; }
        public Nullable<decimal> AvgRevenue { get; set; }
        public Nullable<decimal> AvgExpense { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ExpiredDate { get; set; }
    }

    public class ListResult<T>
    {
        public int TotalRow { get; set; }
        public List<T> Data { get; set; }

    }


   
}
