
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
    public class SanphamController : Controller
    {
        ProductsEntities dbs = new ProductsEntities();
        // GET: Sanpham
        
        public ActionResult Sanpham()
        {
            var products = dbs.Products.Include(p => p.Category);
            return View(products.ToList());
        }
        // GET: Products

        public ActionResult ProductList(int? category,string SearchString, double min = double.MinValue, double max = double.MaxValue)

        {
            ViewBag.Keyword = SearchString;
            var products = dbs.Products.Include(p => p.Category);
            // Tìm kiếm chuỗi truy vấn theo category

            if (category == null)

            {

                products = dbs.Products.OrderByDescending(x => x.NamePro);
            }

            else

            {

                products = dbs.Products.OrderByDescending(x => x.CateID).Where(x => x.CateID == category);

            }
            // Tìm kiếm chuỗi truy vấn theo NamePro (SearchString)

            if (!String.IsNullOrEmpty(SearchString))

            {

                products = dbs.Products.OrderByDescending(x => x.NamePro).Where(s => s.NamePro.Contains(SearchString.Trim()));
            }

            // Tìm kiếm chuỗi truy vấn theo đơn giá

            if (min >= 0 && max > 0)

            {

                products = dbs.Products.OrderByDescending(x => x.Price).Where(p => (double)p.Price >= min && (double)p.Price <= max);


            }
                return View(products.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = dbs.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbs.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    }
