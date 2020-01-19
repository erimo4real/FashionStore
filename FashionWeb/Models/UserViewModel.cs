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
}