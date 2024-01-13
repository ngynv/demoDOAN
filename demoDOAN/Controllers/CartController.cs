using demoDOAN.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace demoDOAN.Controllers
{
    public class CartController : Controller
    {
        ProductsEntities dbs = new ProductsEntities();
        // GET: Cart
        public List<CartItem> GetCart()
        {
            List<CartItem> myCart = Session["GioHang"] as List<CartItem>;
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
        // Phương thức cập nhật số lượng khi khách hàng chọn SP mua thêm
        public ActionResult UpdateCart(int id, int soluongmoi)
        {
            List<CartItem> myCart = Session["GioHang"] as List<CartItem>;
            CartItem currentproduct = myCart.FirstOrDefault(m => m.ProductID == id);
            if (currentproduct != null)
            {
                currentproduct.Number = soluongmoi;
            }
            return RedirectToAction("GetCartInfo");
        }

        public ActionResult Remove(int id)
        {
            //Lấy giỏ hàng từ Session   
            List<CartItem> myCart = GetCart();

            //Kiểm tra giỏ hàng đã có trong Session ["Giohang"]
            CartItem currentProduct = myCart.FirstOrDefault(p => p.ProductID == id);

            //Nếu tồn tại thì cho sửa số lượng
            if (currentProduct != null)
            {
                myCart.Remove(currentProduct);
                return RedirectToAction("GetCartInfo");
            }
            if (myCart.Count == 0)
            {
                return RedirectToAction("ProductList", "Sanpham");
            }
            return RedirectToAction("GetCartInfo");
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
                return View("GetCartInfo");
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
    
        public ActionResult CheckOut(FormCollection form)

        {

            try

            {
                Customer cus = (Customer)Session["Account"];
                
                List<CartItem> myCart = GetCart();

                OrderPro _order = new OrderPro();

                _order.DateOrder = DateTime.Now;

                _order.AddressDeliverry = form["AddressDeliverry"];
                _order.Totalprice = GetTotalPrice();
                _order.Quantity = GetTotalNumber();
                _order.IDCus = cus.IDCus;
                dbs.OrderProes.Add(_order);
                dbs.SaveChanges();

                foreach (var item in myCart )

                {

                    // lưu dòng sản phẩm vào chi tiết hóa đơn

                    OrderDetail _order_detail = new OrderDetail();

                    _order_detail.IDOrder = _order.ID;

                    _order_detail.IDProduct = item.ProductID;

                    _order_detail.UnitPrice = (double)item.Price;

                    _order_detail.Quantity = item.Number;
                   

                    dbs.OrderDetails.Add(_order_detail);

                }

                dbs.SaveChanges();

                Session["myCart"] = null;
                myCart.Clear();
                return RedirectToAction("CheckOut_Success", "Cart", new {id=_order.ID});

            }

            catch

            {

               return Content("Có sai sót! Xin kiểm tra lại thông tin"); ;

            }
        }
        // Thông báo thanh toán thành công

        public ActionResult CheckOut_Success(int? id)

        {
            List<CartItem> myCart = GetCart();
            CartItem currentProduct = myCart.FirstOrDefault(p => p.ProductID == id);
            if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OrderPro orderPro = dbs.OrderProes.Find(id);
                if (orderPro == null)
                {
                    return HttpNotFound();
                }
            return View(orderPro);

        }

    }
}