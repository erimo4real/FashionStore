using FashionDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionWeb.Models
{
    public class HomeViewModel
    {
        public tbl_Product product { get; set; }
        public tbl_Category category { get; set; }
        public tbl_Brands brand { get; set; }
    }
}