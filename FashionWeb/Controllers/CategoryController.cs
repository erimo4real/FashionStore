using FashionDb;
using FashionHelpers.HelperServices;
using FashionWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionWeb.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        public tbl_Category cat;
        private readonly CategorySerivce category;
        CategoryViewModel cmodel = new CategoryViewModel();
        // GET: Category

        public CategoryController()
        {
            category = new CategorySerivce();
        }
        public ActionResult Index()
        {
            ViewBag.Message = TempData["OperStatus"];
            ViewBag.Status = TempData["Status"];
            cmodel.categories = category.GetAll();
            return View(cmodel);
        }

        [HttpPost]
        public ActionResult Add(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["OperStatus"] = "pls type a category name !";
                TempData["Status"] = false;
                ModelState.Clear();
                return RedirectToAction("index");
            }
            else
            {
                cat = new tbl_Category()
                {
                    CategoryName = model.CategoryName,
                    AddedON = DateTime.Now
                };
                category.Insert(cat);
            }

            TempData["OperStatus"] = "Category added Sucessfully !";
            TempData["Status"] = true;
            ModelState.Clear();
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
          tbl_Category cat =  category.GetById(id);
            cmodel.CategoryName = cat.CategoryName;
            cmodel.CategoryId = cat.CategoryId;
            cmodel.AddedON = cat.AddedON;
            return PartialView("_Edit", cmodel);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["OperStatus"] = "pls type a category name !";
                TempData["Status"] = false;
                ModelState.Clear();
                return RedirectToAction("index");
            }
            else
            {
                tbl_Category c = new tbl_Category();
                c.CategoryId = model.CategoryId;
                c.CategoryName = model.CategoryName;
                c.AddedON = model.AddedON;
                c.UpdatedOn = DateTime.Now;
                category.Update(c);
            }

            TempData["OperStatus"] = "Category updated Sucessfully !";
            TempData["Status"] = true;
            ModelState.Clear();
            return RedirectToAction("index");
            
        }

        public ActionResult Delete(int id)
        {
            category.Delete(id);
            TempData["OperStatus"] = "Category Deleted Sucessfully !";
            TempData["Status"] = true;
            ModelState.Clear();
            return RedirectToAction("index");
        }
    }
}