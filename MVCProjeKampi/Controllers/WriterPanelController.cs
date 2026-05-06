using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampi.Controllers
{
    public class WriterPanelController : Controller
    {

        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());

        public ActionResult WriterProfile()
        {
            return View();
        }

        
        public ActionResult MyHeading()
        {
            string writerMail = (string)Session["WriterMail"];
            var writerInfo = wm.GetByMail(writerMail);
            var headingValues = hm.GetListByWriter(writerInfo.WriterID);
            return View(headingValues);
        }

        [HttpGet]
        public ActionResult NewHeading()
        {
            string writerMail = (string)Session["WriterMail"];
            ViewBag.d = writerMail;
            List<SelectListItem> categoryValue = (from category in cm.GetList() select new SelectListItem { Text = category.CategoryName, Value = category.CategoryID.ToString() }).ToList();
            ViewBag.vlc = categoryValue;
            return View();
        }

        [HttpPost]
        public ActionResult NewHeading(Heading p) 
        {
            string writerMail = (string)Session["WriterMail"];
            var writerInfo = wm.GetByMail(writerMail);
            p.HeadingTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID = writerInfo.WriterID;
            p.HeadingStatus = true;
            hm.HeadingAddBL(p);
            return RedirectToAction("MyHeading");
        }

        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> categoryValue = (from category in cm.GetList() select new SelectListItem { Text = category.CategoryName, Value = category.CategoryID.ToString() }).ToList();
            ViewBag.vlc = categoryValue;
            var headingValue = hm.GetByID(id);
            return View(headingValue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading p)
        {
            hm.HeadingUpdate(p);
            return RedirectToAction("MyHeading");
        }

        public ActionResult DeleteHeading(int id)
        {
            var headingValue = hm.GetByID(id);
            headingValue.HeadingStatus = false;
            hm.HeadingDelete(headingValue);
            return RedirectToAction("MyHeading");
        }

    }
}