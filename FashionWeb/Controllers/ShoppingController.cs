using FashionDb;
using FashionHelpers.CommonHelpers;
using FashionWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionWeb.Controllers
{
    //[Authorize]
    public class ShoppingController : Controller
    {
        public FashionAppDBEntities _context;

        public ShoppingController()
        {
          this._context  = new FashionAppDBEntities();
        }
        // GET: Shopping
        /// <summary>
        /// Add Product To Cart
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ActionResult AddProductToCart(HomeViewModel Model, FormCollection collection)
        {
            var productId = Convert.ToInt32(collection["userid"]);  
            Tbl_Cart c = new Tbl_Cart();
            c.AddedOn = DateTime.Now;
            c.CartStatusId = 1;
            c.UserId = Convert.ToInt32(PublicHelper.UserId);
            c.ProductId = productId;
            c.Size = collection["selectsize"];
            c.Color = collection["selectcolor"];
            c.UpdatedOn = DateTime.Now;
          //  _context.Tbl_Cart.Add(c);
          //  _context.SaveChanges();
            TempData["ProductAddedToCart"] = "Product added to cart successfully";
            TempData["Status"] = true;
            return RedirectToAction("Index", "Home" , new { prodId = productId });
        }

        /// <summary>
        /// MyCart
        /// </summary>
        /// <returns>List of cart items</returns>
        public ActionResult MyCart()
        {
            List<Tbl_Cart> cart = _context.Tbl_Cart.ToList();
            List<tbl_Product> product = _context.tbl_Product.ToList();
            List<tbl_Category> categories = _context.tbl_Category.ToList();
            List<tbl_Users> user = _context.tbl_Users.ToList();
            

            var cartRecord = from c in cart
                               join d in product on c.ProductId equals d.ProductId into table1
                                from d in table1.ToList()
                                join u in user on c.UserId equals u.ID into table2
                                from u in table2.ToList()
                                select new CartViewModel
                                {

                                    cart = c,
                                    product = d,
                                    user = u
                                };
            return View(cartRecord);
        }

        /// <summary>
        /// Remove Cart Item
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        //public ActionResult RemoveCartItem(int productId)
        //{
        //    var username = HttpContext.Current.User.Identity.Name;
        //    Tbl_Cart c = _context.Tbl_Cart.Where(i => i.ProductId == productId && i.UserId == username && i.CartStatusId == 1);
        //    c.CartStatusId = 2;
        //    c.UpdatedOn = DateTime.Now;
        //    //_unitOfWork.GetRepositoryInstance<Tbl_Cart>().Update(c);
        //    //_unitOfWork.SaveChanges();
        //    return RedirectToAction("MyCart");
        //}

        /// <summary>
        /// CheckOut the Cart items
        /// </summary>
        /// <returns></returns>
        //public ActionResult CheckOut()
        //{
        //    List<USP_MemberShoppingCartDetails_Result> cd = _unitOfWork.GetRepositoryInstance<USP_MemberShoppingCartDetails_Result>().GetResultBySqlProcedure("USP_MemberShoppingCartDetails @memberId",
        //       new SqlParameter("memberId", System.Data.SqlDbType.Int) { Value = memberId }).ToList();
        //    ViewBag.TotalPrice = cd.Sum(i => i.Price);
        //    ViewBag.CartIds = string.Join(",", cd.Select(i => i.CartId).ToList());
        //    return View(cd);
        //}

        /// <summary>
        /// Payment Success
        /// </summary>
        /// <param name="shippingDetails"></param>
        /// <returns></returns>
        //public ActionResult PaymentSuccess(ShippingDetails shippingDetails)
        //{
        //    Tbl_ShippingDetails sd = new Tbl_ShippingDetails();
        //    sd.MemberId = memberId;
        //    sd.AddressLine = shippingDetails.Address;
        //    sd.City = shippingDetails.City;
        //    sd.State = shippingDetails.State;
        //    sd.Country = shippingDetails.Country;
        //    sd.ZipCode = shippingDetails.ZipCode;
        //    sd.OrderId = Guid.NewGuid().ToString();
        //    sd.AmountPaid = shippingDetails.TotalPrice;
        //    sd.PaymentType = shippingDetails.PaymentType;
        //    _unitOfWork.GetRepositoryInstance<Tbl_ShippingDetails>().Add(sd);
        //    _unitOfWork.GetRepositoryInstance<Tbl_Cart>().UpdateByWhereClause(i => i.MemberId == memberId && i.CartStatusId == 1, (j => j.CartStatusId = 3));
        //    _unitOfWork.SaveChanges();
        //    if (!string.IsNullOrEmpty(Request["CartIds"]))
        //    {
        //        int[] cartIdsToUpdate = Request["CartIds"].Split(',').Select(Int32.Parse).ToArray();
        //        _unitOfWork.GetRepositoryInstance<Tbl_Cart>().UpdateByWhereClause(i => cartIdsToUpdate.Contains(i.CartId), (j => j.ShippingDetailId = sd.ShippingDetailId));
        //        _unitOfWork.SaveChanges();

        //    }
        //    return View(sd);
        //}
    }
}