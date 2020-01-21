﻿using FashionDb;
using FashionWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public FashionAppDBEntities _context = new FashionAppDBEntities();
        public ActionResult Index()
        {
            List<tbl_Product> products = _context.tbl_Product.ToList();
            List<tbl_Category> categories = _context.tbl_Category.ToList();
            List<tbl_Brands> brands = _context.tbl_Brands.ToList();

            var productRecord = from e in products
                                join d in categories on e.CategoryID equals d.CategoryId into table1
                                from d in table1.ToList()
                                join i in brands on e.BrandID equals i.ID into table2
                                from i in table2.ToList()
                                select new HomeViewModel
                                {

                                    product = e,
                                    category = d,
                                    brand = i
                                };
            return View(productRecord);
        }
        
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {

            return View();
        }

        public ActionResult Shop()
        {
            return View();
        }

        public ActionResult checkout()
        {
            return View();
        }

        public ActionResult SingleProductDetails()
        {
            return View();
        }
    }
}