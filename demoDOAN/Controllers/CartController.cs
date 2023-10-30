using demoDOAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demoDOAN.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public List<CartItem> GetCart()
        {
            List<CartItem> myCart = Session["GioHang"] as
           List<CartItem>;
            if (myCart == null)
            {
                myCart = new List<CartItem>();
                Session["GioHang"] = myCart;
            }
            return myCart;
        }
        // Thêm một sản phẩm
        public ActionResult AddToCart(int id)
        {
            List<CartItem> myCart = GetCart();
            CartItem currentProduct = myCart.FirstOrDefault(p => p.ProductID == id);
            if (currentProduct == null)
            {
                currentProduct = new CartItem(id);
                myCart.Add(currentProduct);
            }
            else
            {
                currentProduct.Number++;
            }
            return RedirectToAction("ProductList", "Sanpham");
        }
        // tính tổng SỐ LƯỢNG mặt hàng được mua
        private int GetTotalNumber()
        {
            int totalNumber = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
                totalNumber = myCart.Sum(sp => sp.Number);
            return totalNumber;
        }
        //tính tổng TIỀN các mặt hàng được mua
        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
            {
                totalPrice = myCart.Sum(sp => sp.FinalPrice());
            }
            return totalPrice;
        }
        //hiển thị thông tin bên trong giỏ hàng
        public ActionResult GetCartInfo()   
        {
            List<CartItem> myCart = GetCart();
            if (myCart == null || myCart.Count == 0)
            {
                return RedirectToAction("ProductList", "Sanpham");
            }
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return View(myCart);
        }
        public ActionResult CartPartial()
        {
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return PartialView();
        }
    }
}