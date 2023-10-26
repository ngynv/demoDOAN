using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demoDOAN.Models;

namespace demoDOAN.Controllers
{
    public class CategoriesController : Controller
    {
        DSSPhamEntities1 database = new DSSPhamEntities1();
        // GET: Categories
        
        public ActionResult DanhMuc()
        {
            var categories = database.Categories.ToList();
            return View(categories);
        }
        
       
        
    }
}