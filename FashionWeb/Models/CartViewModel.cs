using FashionDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionWeb.Models
{
    public class CartViewModel
    {
        public int CartId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> CartStatusId { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> ShippingDetailId { get; set; }
        public tbl_Product product { get; set; }
        public tbl_Category category { get; set; }
        public tbl_Brands brand { get; set; }
        public tbl_Users user { get; set; }
        public Tbl_Cart cart { get; set; }
    }
}