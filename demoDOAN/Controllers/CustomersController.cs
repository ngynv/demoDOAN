












using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using demoDOAN.Models;

namespace demoDOAN.Controllers
{
    public class CustomersController : Controller
    {
        private ProductsEntities db = new ProductsEntities();

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer cust)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cust.NameCus))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(cust.PassCus))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (string.IsNullOrEmpty(cust.EmailCus))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(cust.PhoneCus))
                    ModelState.AddModelError(string.Empty, "Điện thoại không được để trống");

                var khachhang = db.Customers.FirstOrDefault(k => k.NameCus == cust.NameCus);
                if (khachhang != null)
                    ModelState.AddModelError(string.Empty, "Đã có người đăng kí tên này");
                if (ModelState.IsValid)
                {
                    db.Customers.Add(cust);
                    db.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Customer cust)
        {
            if (string.IsNullOrEmpty(cust.NameCus))
                ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
            if (string.IsNullOrEmpty(cust.PassCus))
                ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
            if (ModelState.IsValid)
            {
                var adminAccount =  db.AdminUsers.FirstOrDefault(k => k.NameUser == cust.NameCus && k.PasswordUser == cust.PassCus);

                if (adminAccount != null)
                {
                    Session["Account"] = adminAccount;
                    return RedirectToAction("Index", "Admin/AdminProducts");
                }
                var account = db.Customers.FirstOrDefault(k => k.NameCus == cust.NameCus && k.PassCus == cust.PassCus);
                if (account != null)
                {
                    Session["Account"] = account;
                    return RedirectToAction("Trangchu", "Trangchu");
                }
                else
                    ViewBag.ThongBao = "*Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();

        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        
        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCus,NameCus,PhoneCus,EmailCus,PassCus")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Customers", new {id=customer.IDCus});
            }
            return View(customer);
        }

        
        public ActionResult Logout()
        {
            Session["Account"] = null;
            return RedirectToAction("Login", "Customers");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
