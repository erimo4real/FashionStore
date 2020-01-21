using FashionDb;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionWeb.Models
{
    public class UserViewModel
    {
        public IEnumerable<tbl_Users> users { get; set; }
        public tbl_Users user { get; set; }
        public string Image { get; set; }

    }

    public class ProfileViewModel
    {
        public Tbl_Profile profile { get; set; }
        public tbl_Users user { get; set; }
        public int ProfileId { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> UserId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
    }
}