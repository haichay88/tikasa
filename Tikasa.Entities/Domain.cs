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
    
    public partial class Domain
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Age { get; set; }
        public string Title { get; set; }
        public int StatusId { get; set; }
        public string Note { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ExpiredDate { get; set; }
    
        public virtual User User { get; set; }
    }
}
