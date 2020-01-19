using FashionDb;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionWeb.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [Display(Name = "CategoryName")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "category Name name required")]
        public string CategoryName { get; set; }
        public Nullable<System.DateTime> AddedON { get; set; }
        public Nullable<int> AddedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public IEnumerable<tbl_Category> categories { get; set; }
    }
}