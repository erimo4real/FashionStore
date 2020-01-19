using FashionDb;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionWeb.Models
{
    public class ProductViewModel
    {
        [Display(Name = " Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = " name required")]
        public string Name { get; set; }  
        public string Gender { get; set; }
        public string Image { get; set; }
        public string Image1 { get; set; }
        public string Image3 { get; set; }     
        public Nullable<int> Price { get; set; } 
        public Nullable<int> Discount { get; set; }
        public bool IsNew { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public Nullable<int> AddedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public SelectList BrandList { get; set; }
        public string SelectedBrand { get; set; }
        public SelectList CategoryList { get; set; }
        public string SeletedCategory { get; set; }
        public IEnumerable<tbl_Product> Products { get; set; }
        public tbl_Product product { get; set; }

    }

    public class RecordProductViewModel
    {
        public tbl_Product product { get; set; }
        public tbl_Category category { get; set; }
        public tbl_Brands brand { get; set; }
        public tbl_Users user { get; set; }
    }
}