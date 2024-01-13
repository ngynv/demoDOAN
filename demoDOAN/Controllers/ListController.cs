using demoDOAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Net;
using System.Data;


namespace demoDOAN.Controllers
{
    public class ListController : Controller
    {
        ProductsEntities dbs = new ProductsEntities();
        // GET: List
        public ActionResult Index()
        {
            var pro = dbs.Products.Include(p => p.Category);
            return View(pro.ToList());
        }
    }
}