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
        public bool IsCertificated { get; set; }
        public string Note { get; set; }
        public int SiteTypeId { get; set; }
        public Nullable<decimal> NetProfit { get; set; }
        public int StatusId { get; set; }
        public string Summary { get; set; }
        public Nullable<int> GoliveMonth { get; set; }
        public Nullable<int> GoliveYear { get; set; }
        public Nullable<decimal> MinimumPrice { get; set; }
        public Nullable<decimal> BuyItNowPrice { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ExpiredDate { get; set; }
    }
}
