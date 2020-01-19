using FashionDb;
using FashionHelpers.HelperServices;
using FashionWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionWeb.Controllers
{
    //[Authorize]
    public class ProductController : Controller
    {
        public ProductService _ps = new ProductService();
        public FashionAppDBEntities _context = new FashionAppDBEntities();
        public CategorySerivce cs = new CategorySerivce();

      
        // GET: Product
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
                                 select new RecordProductViewModel
                                 {
                                    
                                     product = e,
                                     category = d,
                                     brand = i
                                 };
            
            ViewBag.Message = TempData["OperStatus"];
            ViewBag.Status = TempData["Status"];
            return View(productRecord);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ProductViewModel Pvm = new ProductViewModel();
            Pvm.BrandList = new SelectList(_context.tbl_Brands.ToList(), "ID", "BrandName");
            Pvm.CategoryList = new SelectList(cs.GetAll(), "CategoryId", "CategoryName");
            ViewBag.Message = TempData["OperStatus"];
            ViewBag.Status = TempData["Status"];
            return View(Pvm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductViewModel model , HttpPostedFileBase image, HttpPostedFileBase image1 , FormCollection collection)
        {  
            //var currentUser = HttpContext.User.Identity.Name;
            //tbl_Users user = _context.tbl_Users.Where(m => m.Email == currentUser).SingleOrDefault();

            tbl_Product prod = new tbl_Product();
            if (!ModelState.IsValid)
            {
                ProductViewModel vm = new ProductViewModel();
                vm.BrandList = new SelectList(_context.tbl_Brands.ToList(), "ID", "BrandName");
                vm.CategoryList = new SelectList(cs.GetAll(), "CategoryId", "CategoryName");
                ViewBag.Message = "Pls fill Missing Fileds !!!";
                ViewBag.Status = false;
                return View(vm);
            }
            else
            {
                if (image != null)
                {
                    string ImageName = System.IO.Path.GetFileName(image.FileName);
                    string physicalPath1 = Server.MapPath("~/ProductImage/" + ImageName);
                    // save image in folder
                    image.SaveAs(physicalPath1);
                    prod.Image = ImageName;
                }

                if (image1 != null)
                {
                    string ImageName = System.IO.Path.GetFileName(image1.FileName);
                    string physicalPath1 = Server.MapPath("~/ProductImage/" + ImageName);
                    // save image in folder
                    image1.SaveAs(physicalPath1);
                    prod.Image1 = ImageName;
                }

                prod.Name = model.Name;
                prod.Price = model.Price;
                prod.Description = model.Description;
                prod.BrandID = Convert.ToInt32(model.SelectedBrand);
                prod.CategoryID = Convert.ToInt32(model.SeletedCategory);
                prod.Discount = model.Discount;
                prod.IsNew = model.IsNew;
                prod.Gender = collection["Gender"];
                prod.AddedOn = DateTime.Now;
                _ps.Insert(prod);
            }


            ProductViewModel Pvm = new ProductViewModel();
            Pvm.BrandList = new SelectList(_context.tbl_Brands.ToList(), "ID", "BrandName");
            Pvm.CategoryList = new SelectList(cs.GetAll(), "CategoryId", "CategoryName");
            ViewBag.Message = "Product Added Successfully !!!";
            ViewBag.Status = true;
            
            return View(Pvm);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductViewModel Pvm = new ProductViewModel();
            Pvm.BrandList = new SelectList(_context.tbl_Brands.ToList(), "ID", "BrandName");
            Pvm.CategoryList = new SelectList(cs.GetAll(), "CategoryId", "CategoryName");
            var prod =  _ps.GetById(id);
            Pvm.product = prod;
            Pvm.SelectedBrand = prod.BrandID.ToString();
            Pvm.SeletedCategory = prod.CategoryID.ToString();
            Pvm.Name = prod.Name;
            return View(Pvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel model, HttpPostedFileBase image, HttpPostedFileBase image1, FormCollection collection)
        {

            //var currentUser = HttpContext.User.Identity.Name;
            //tbl_Users user = _context.tbl_Users.Where(m => m.Email == currentUser).SingleOrDefault();

            tbl_Product prod = new tbl_Product();
            if (!ModelState.IsValid)
            {
                ProductViewModel vm = new ProductViewModel();
                vm.BrandList = new SelectList(_context.tbl_Brands.ToList(), "ID", "BrandName");
                vm.CategoryList = new SelectList(cs.GetAll(), "CategoryId", "CategoryName");
                vm.product.Gender = collection["Gender"];
                ViewBag.Message = "Pls fill Missing Fileds !!!";
                ViewBag.Status = false;
                return View(vm);
            }
            else
            {
                if (image != null)
                {
                    string ImageName = System.IO.Path.GetFileName(image.FileName);
                    string physicalPath1 = Server.MapPath("~/ProductImage/" + ImageName);
                    // save image in folder
                    image.SaveAs(physicalPath1);
                    prod.Image = ImageName;
                }
                else
                {
                    prod.Image = model.product.Image;
                }

                if (image1 != null)
                {
                    string ImageName = System.IO.Path.GetFileName(image1.FileName);
                    string physicalPath1 = Server.MapPath("~/ProductImage/" + ImageName);
                    // save image in folder
                    image1.SaveAs(physicalPath1);
                    prod.Image1 = ImageName;
                }
                else
                {
                    prod.Image1 = model.product.Image1;
                }

                prod.ProductId = model.product.ProductId;
                prod.Name = model.Name;
                prod.Price = model.product.Price;
                prod.Description = model.product.Description;
                prod.BrandID = Convert.ToInt32(model.SelectedBrand);
                prod.CategoryID = Convert.ToInt32(model.SeletedCategory);
                prod.Discount = model.product.Discount;
                prod.IsNew = model.product.IsNew;
                prod.Gender = collection["Gender"];
                prod.AddedOn = model.product.AddedOn;
                prod.UpdatedOn = DateTime.Now;
                _ps.Update(prod);
            }
            ProductViewModel Pvm = new ProductViewModel();
            Pvm.BrandList = new SelectList(_context.tbl_Brands.ToList(), "ID", "BrandName");
            Pvm.CategoryList = new SelectList(cs.GetAll(), "CategoryId", "CategoryName");
            ViewBag.Message = "Product Updated Successfully !!!";
            ViewBag.Status = true;

            return View(Pvm);
        }

        [HttpPost]
        
        public JsonResult Delete(int id)
        {
            try
            {

                if (_ps.Delete(id))
                {
                    TempData["Message"] = "Product Details Deleted Successfully";
                }

                return Json("Deleted");
            }
            catch
            {
                return Json("Error");
            }
        }
    }
}