using FashionHelpers.HelperServices;
using System;
using FashionDb;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionWeb.Models;

namespace FashionWeb.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly UserServices UserS;
        public UserViewModel UVM = new UserViewModel();
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
        public ActionResult Edit(int id)
        {
            var result = UserS.GetById(id);
            UVM.user = result;
            return View(UVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel model , HttpPostedFileBase ImageFile)
        {
            bool Status = false;
            string message = "";

            // Model Validation 
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(ImageFile.FileName);
                    string physicalPath = Server.MapPath("~/Passports/" + ImageName);

                    // save image in folder
                    ImageFile.SaveAs(physicalPath);

                 var u =   UserS.context.tbl_Users.Where(m => m.ID == model.user.ID).SingleOrDefault();

                    u.UserName = model.user.UserName;
                    u.FirstName = model.user.FirstName;
                    u.LastName = model.user.LastName;
                    u.Email = model.user.Email;
                    u.DOB = model.user.DOB;
                    u.ImageName = ImageName;
                    u.UpdatedOn = DateTime.Now;
                    UserS.context.SaveChanges();

                    #region Save to Database

                    //UserS.Update(user);

                    message = "User Update successfully done.";
                    Status = true;
                }
                else
                {
                    message = "Error while file uploading.";
                }
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