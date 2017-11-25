//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tikasa.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Website
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int PlatformId { get; set; }
        public int SiteAge { get; set; }
        public string Note { get; set; }
        public Nullable<int> TypeOfWebsiteId { get; set; }
        public Nullable<int> TypeOfCategoryId { get; set; }
        public Nullable<decimal> NetProfit { get; set; }
        public int StatusId { get; set; }
        public bool IsCertificated { get; set; }
        public string Summary { get; set; }
        public Nullable<int> GoliveMonth { get; set; }
        public Nullable<int> GoliveYear { get; set; }
        public Nullable<decimal> MinimumPrice { get; set; }
        public Nullable<decimal> BuyItNowPrice { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ExpiredDate { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual TypeOfWebsite TypeOfWebsite { get; set; }
        public virtual User User { get; set; }
    }
}
