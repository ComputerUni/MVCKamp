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
        // GET: Chart
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
            cc.Add(new CategoryClass()
            {
                CategoryName="Yazılım",
                CategoryCount = 8,
            });
            cc.Add(new CategoryClass()
            {
                CategoryName = "Seyahat",
                CategoryCount = 5,
            });
            cc.Add(new CategoryClass()
            {
                CategoryName = "Teknoloji",
                CategoryCount = 4,
            });
            cc.Add(new CategoryClass()
            {
                CategoryName = "Spor",
                CategoryCount = 10,
            });
            return cc;
        }
    }
}