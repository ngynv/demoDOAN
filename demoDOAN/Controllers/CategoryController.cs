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
        in4productEntities database = new in4productEntities();
        // GET: Categories
        public ActionResult Sanpham()
        {
            var categories = database.Categories.ToList();
            return View(categories);
        }
    }
}