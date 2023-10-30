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
        DSSPhamEntities2 dbs=new DSSPhamEntities2();
        // GET: Sanpham
        
        public ActionResult Sanpham()
        {
            var products = dbs.Products.Include(p => p.Category1);
            return View(products.ToList());
        }
        // GET: Products

        public ActionResult ProductList(string SearchString, double min = double.MinValue, double max = double.MaxValue)

        {

            var products = dbs.Products.Include(p => p.Category1);

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