using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampi.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Statistic

        StatisticManager sm = new StatisticManager(new EfCategoryDal(), new EfWriterDal(), new EfHeadingDal());
        public ActionResult Index()
        {
            ViewBag.CategoryCount = sm.GetTotalCategoryCount();
            ViewBag.SoftwareCount = sm.GetSoftwareCategoryHeadingCount();
            ViewBag.WriterA = sm.GetWriterCountWithA();
            ViewBag.MaxHeading = sm.GetCategoryNameWithMostHeadings();
            ViewBag.ActiveStatus = sm.ActiveCategoriesCount();
            ViewBag.InActiveStatus = sm.InActiveCategoriesCount();
            ViewBag.Diff = sm.GetCategoryStatusDifference();

            return View();
        }
    }
}