using demoDOAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demoDOAN.Controllers
{
    public class SanphamController : Controller
    {
        DSSPhamEntities1 database=new DSSPhamEntities1();
        // GET: Sanpham
        
        public ActionResult Sanpham()
        {
            var listProduct = database.Products.ToList();
            return View(listProduct);
        }
        
       

    }
}