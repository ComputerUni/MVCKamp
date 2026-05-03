using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampi.Controllers
{
    public class HeadingController : Controller
    {
        // GET: Heading
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        public ActionResult Index()
        {
            var headingValues = hm.GetList();
            return View(headingValues);
        }

        [HttpGet]
        public ActionResult AddHeading()
        {
            List<SelectListItem> categoryValue = (from category in cm.GetList() select new SelectListItem { Text = category.CategoryName, Value = category.CategoryID.ToString() }).ToList();
            List<SelectListItem> writerValue = (from writer in wm.GetList() select new SelectListItem { Text = writer.WriterName + " " + writer.WriterSurname, Value = writer.WriterID.ToString() }).ToList(); 
            ViewBag.vlc = categoryValue;
            ViewBag.wlc = writerValue;
            return View();
        }

        [HttpPost]
        public ActionResult AddHeading(Heading p)
        {
            p.HeadingTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            hm.HeadingAddBL(p);
            return RedirectToAction("Index");
        }

        public ActionResult ContentByHeading()
        {
            return View();
        }

        [HttpGet] 
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> categoryValue = (from category in cm.GetList() select new SelectListItem { Text = category.CategoryName, Value = category.CategoryID.ToString() }).ToList();
            ViewBag.vlc = categoryValue;
            var headingValue = hm.GetByID(id);
            return View(headingValue);
        }


    }
}