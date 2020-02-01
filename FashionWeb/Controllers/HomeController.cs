using FashionDb;
using FashionWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using X.PagedList;
using System.Web;
using System.Web.Mvc;
using FashionHelpers.HelperServices;

namespace FashionWeb.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public FashionAppDBEntities _context = new FashionAppDBEntities();
        private const int _pageSize = 2;
        private CartService cart = new CartService();

        public ActionResult search(FormCollection collection)
        {
            var search = collection["search"];
            return View();
        }


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
            ViewBag.Message = TempData["ProductAddedToCart"];
            ViewBag.Status = TempData["Status"];
            return View(productRecord);
        }
        
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Shop(int page = 1)
        {
            ViewBag.brands = _context.tbl_Brands.ToList();
            ViewBag.Category = _context.tbl_Category.ToList();
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
            ViewBag.ResultsPage = productRecord.ToPagedList(page, _pageSize);
            return View(productRecord);
        }

        public ActionResult checkout(int id)
        {
            return View();
        }

        public ActionResult SingleProductDetails(int id)
        {
            var a = (from prod in _context.tbl_Product
                     join cat in _context.tbl_Category on prod.CategoryID equals cat.CategoryId
                     join b in _context.tbl_Brands on prod.BrandID equals b.ID
                     where prod.ProductId == id
                     select new HomeViewModel
                     {
                          product = prod,
                          category = cat,
                          brand = b
                     });
            return View(a);
        }

        public JsonResult Addart()
        {
            return Json("ADD", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Remove()
        {
            return Json("remove", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {

            var Deleted = cart.Delete(id);
            try
            {
                if (Deleted)
                {
                    TempData["Message"] = "Cart Item Deleted Successfully";
                }

                return Json("Remove");
            }
            catch
            {
                return Json("Error");
            }
        }

        public JsonResult Shopadds()
        {
            return Json("Shopadds", JsonRequestBehavior.AllowGet);
        }

    }
}