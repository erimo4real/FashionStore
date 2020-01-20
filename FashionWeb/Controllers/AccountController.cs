using FashionDb;
using FashionHelpers.CommonHelpers;
using FashionHelpers.HelperServices;
using FashionWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FashionWeb.Controllers
{
      //[Authorize]
    public class AccountController : Controller
    {
        private readonly UserServices services;
        private readonly PublicHelper helers;

        public AccountController()
        {
            services = new UserServices();
            helers = new PublicHelper();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string ReturnUrl = "")
        {
            string message = "";

            var v = services.context.tbl_Users.Where(a => a.Email == model.Email).FirstOrDefault();
            if (v != null)
            {
                if (!v.isEmailVerifield)
                {
                    ViewBag.Message = "Please verify your email from your email address first";
                    return View();
                }
               
                if (helers.IsStringSameAsHash(model.Password , v.Password))
                {
                    int timeout = model.RememberMe ? 500 : 20; // 525600 min = 1 year
                    var ticket = new FormsAuthenticationTicket(model.Email, model.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);
                    
                    if (v.isAdmin == true)
                    {
                        //TempData["CurrentUser"] = model.Email;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            else
            {
                message = "Invalid credential provided";
            }

            ViewBag.Message = message;
            return View();
        }

        // GET: Account
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationViewModel model, HttpPostedFileBase ImageFile)
        {
            bool Status = false;
            string message = "";

            // Model Validation 
            if (ModelState.IsValid)
            {

                #region //Email is already Exist 
                var isExist = services.UsernameExists(model.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    Status = false;
                    message = "Email already exist";
                    ViewBag.Message = message;
                    ViewBag.Status = Status;
                    return View(model);
                }
                #endregion

                #region Generate Activation Code 
                model.ActivationCode = Guid.NewGuid();
                #endregion

                #region  Password Hashing 
                model.Password = helers.HashPassword(model.Password);
              
                #endregion
                model.isEmailVerifield = false;

                if (ImageFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(ImageFile.FileName);
                    string physicalPath = Server.MapPath("~/Passports/" + ImageName);

                    // save image in folder
                    ImageFile.SaveAs(physicalPath);
                    tbl_Users user = new tbl_Users()
                    {
                         UserName = model.UserName, FirstName = model.FirstName, LastName = model.LastName,
                          Email = model.Email, DOB = model.DOB , Password = model.Password ,  ActivationCode = model.ActivationCode, ImageName = ImageName,
                           AddedON = DateTime.Now, isActive = true
                    };
                    #region Save to Database

                    services.insert(user);

                    //Send Email to User
                    SendVerificationLinkEmail(model.Email, model.ActivationCode.ToString());
                    message = "Registration successfully done. Account activation link " +
                        " has been sent to your email id:" + model.Email;
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


        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            //Verify Email ID
            //Generate Reset password link 
            //Send Email 
            string message = "";
            bool status = false;


            var account = services.context.tbl_Users.Where(a => a.Email == EmailID).FirstOrDefault();
            if (account != null)
            {
                //Send email for reset password
                string resetCode = Guid.NewGuid().ToString();
                SendVerificationLinkEmail(account.Email, resetCode, "ResetPassword");
                account.ResetPasswordCode = resetCode;
                //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                //in our model class in part 1
                services.context.Configuration.ValidateOnSaveEnabled = false;
                services.context.SaveChanges();
                message = "Reset password link has been sent to your email id.";
            }
            else
            {
                message = "Account not found";
            }

            ViewBag.Message = message;
            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            var user = services.context.tbl_Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
            if (user != null)
            {
                ResetPasswordViewModel model = new ResetPasswordViewModel();
                model.ResetCode = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                var user = services.context.tbl_Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                if (user != null)
                {
                    user.Password = helers.HashPassword(model.NewPassword);
                    user.ResetPasswordCode = "";
                    services.context.Configuration.ValidateOnSaveEnabled = false;
                    services.context.SaveChanges();
                    message = "New password updated successfully";
                }

            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }


        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Account/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("eromoxlx@gmail.com", "AppStore Awesome");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings.Get("password")); // Replace with actual password

            string subject = "";
            string body = "";

            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your AppStore Awesome account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;

            services.context.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
                                                                  // Confirm password does not match issue on save changes
            var v = services.context.tbl_Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
            if (v != null)
            {
                v.isEmailVerifield = true;
                services.context.SaveChanges();
                Status = true;
            }
            else
            {
                ViewBag.Message = "Invalid Request";
            }

            ViewBag.Status = Status;
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            helers.Logout();
            return RedirectToAction("Login", "Account");
        }

    }
}