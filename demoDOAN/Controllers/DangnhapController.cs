using demoDOAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demoDOAN.Controllers
{
    public class DangnhapController : Controller
    {
        DSSPhamEntities1 dbs = new DSSPhamEntities1();
        // GET: Dangnhap
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAccount(AdminUser _user)
        {
            var check = dbs.AdminUsers.Where(s => s.ID == _user.ID && s.PasswordUser == _user.PasswordUser).FirstOrDefault();
            if (check != null)
            {
                ViewBag.ErrorInfo = "SaiInfor";
                return View("Index");
            }
            else
            {
                dbs.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"] = _user.ID;
                Session["PasswordUser"] = _user.PasswordUser;
                return RedirectToAction("Index", "Product");
            }
        }
        public ActionResult RegisterUser()
        {
            return View();
        }
        public ActionResult RegisterUser(AdminUser _user)
        {
            if (ModelState.IsValid)
            {
                var check_ID = dbs.AdminUsers.Where(s => s.ID == _user.ID).FirstOrDefault();
                if (check_ID == null)
                {
                    dbs.Configuration.ValidateOnSaveEnabled = false;
                    dbs.AdminUsers.Add(_user);
                    dbs.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister = "This ID is exixstt";
                    return View();
                }
            }
            return View();
        }
    }
}