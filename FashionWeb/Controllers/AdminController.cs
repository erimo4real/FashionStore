using FashionHelpers.HelperServices;
using System;
using FashionDb;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionWeb.Models;
using System.Data.Entity;

namespace FashionWeb.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserServices UserS;
        public UserViewModel UVM = new UserViewModel();
        private static FashionAppDBEntities _context = new FashionAppDBEntities();
        public AdminController()
        {
            this.UserS = new UserServices();
        }
        // GET: Admin
        public ActionResult Index()
        {
            UVM.users = UserS.GetAllUser();
            return View(UVM);
        }

        [HttpGet]
        public ActionResult GetProfile()
        {

            return View();
        }

        [HttpGet]
        public ActionResult EditMyprofile1(int id)
        {
            var result = _context.Tbl_Profile.Where(u => u.UserId == id).SingleOrDefault();
            return Json(result, JsonRequestBehavior.AllowGet);
            
        }

        [HttpGet]
        public ActionResult EditMyprofile2(int id)
        {
            var result = _context.tbl_Users.Where(u => u.ID == id).SingleOrDefault();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Myprofile(int id)
        {
           
            try
            {
                
                return Json(new { success = true, message = "Order updated successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Error");
            }

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var result = UserS.GetById(id);
            UVM.user = result;
            return View(UVM);
        }


        [HttpPost]
        public ActionResult profilework(FormCollection  formCollection , HttpPostedFileBase Image)
        {
            int id = Convert.ToInt32(formCollection["userid"]);
            tbl_Users us = _context.tbl_Users.Where(u => u.ID == id).SingleOrDefault();
            us.FirstName = formCollection["fname"];
            us.LastName = formCollection["lname"];
            us.Email = formCollection["email"];
            if (Image != null)
            {
                string ImageName = System.IO.Path.GetFileName(Image.FileName);
                string physicalPath = Server.MapPath("~/Passports/" + ImageName);
                us.ImageName = ImageName;
                // save image in folder
                Image.SaveAs(physicalPath);
                _context.SaveChanges();
            }
                _context.SaveChanges();
            return View("GetProfile");
        }

        [HttpPost]
        public ActionResult profilechnge(FormCollection formCollection)
        {
            int id = Convert.ToInt32(formCollection["userid"]);
            Tbl_Profile pr = _context.Tbl_Profile.Where(u => u.UserId == id).SingleOrDefault();

            if (pr == null)
            {
                Tbl_Profile prof = new Tbl_Profile();
                prof.Age =  Convert.ToInt32(formCollection["age"]);
                prof.Position = formCollection["position"];
                prof.PhoneNumber = formCollection["phone"];
                prof.City = formCollection["city"];
                prof.Address = formCollection["address"];
                prof.Description = formCollection["description"];
                prof.UserId = Convert.ToInt32(formCollection["userid"]);
                _context.Tbl_Profile.Add(prof);
                _context.SaveChanges();
            }
            else
            {
                pr.Age = Convert.ToInt32(formCollection["age"]);
                pr.Position = formCollection["position"];
                pr.PhoneNumber = formCollection["phone"];
                pr.City = formCollection["city"];
                pr.Address = formCollection["address"];
                pr.Description = formCollection["description"];
                pr.UserId = Convert.ToInt32(formCollection["userid"]);
                _context.SaveChanges();
            }
            return View("GetProfile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel model , HttpPostedFileBase ImageFile)
        {
            bool Status = false;
            string message = "";
            var u = UserS.context.tbl_Users.Where(m => m.ID == model.user.ID).SingleOrDefault();
            // Model Validation 
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(ImageFile.FileName);
                    string physicalPath = Server.MapPath("~/Passports/" + ImageName);

                    // save image in folder
                    ImageFile.SaveAs(physicalPath);
                    u.ImageName = ImageName;

                }
               
                u.UserName = model.user.UserName;
                u.FirstName = model.user.FirstName;
                u.LastName = model.user.LastName;
                u.Email = model.user.Email;
                u.DOB = model.user.DOB;
                u.UpdatedOn = DateTime.Now;
                UserS.context.SaveChanges();

                #region Save to Database
                message = "User Update successfully done.";
                Status = true;
                #endregion
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(model);
        }

       

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {

                if (UserS.Delete(id))
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