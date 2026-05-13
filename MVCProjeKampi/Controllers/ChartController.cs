using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using MVCProjeKampi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampi.Controllers
{
    public class ChartController : Controller
    {

        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult Index3()
        {
            return View();
        }

        public ActionResult CategoryChart()
        {
            return Json(BlogList(), JsonRequestBehavior.AllowGet);
        }

        public List<CategoryClass> BlogList()
        {
            List<CategoryClass> cc = new List<CategoryClass>();
            var categories = cm.GetList();
            var headings = hm.GetList();

            var result = categories.Select(x => new CategoryClass
            {
                CategoryName = x.CategoryName,
                CategoryCount = headings.Count(y => y.CategoryID == x.CategoryID)
            }).ToList();
            return result;
        }
    }
}