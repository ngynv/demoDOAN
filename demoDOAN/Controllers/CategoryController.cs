using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demoDOAN.Models;

namespace demoDOAN.Controllers
{
    public class CategoryController : Controller
    {
        ProductsEntities database = new ProductsEntities();
        // GET: Categories
        

        [ChildActionOnly]
        public PartialViewResult CategoryPartial()

        {

            var cateList = database.Categories.ToList();

            return PartialView(cateList);

        }



    }
}