//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FashionDb
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public string Image1 { get; set; }
        public string Image3 { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> Discount { get; set; }
        public bool IsSlide { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public Nullable<int> BrandID { get; set; }
        public string Type { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> AddedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
