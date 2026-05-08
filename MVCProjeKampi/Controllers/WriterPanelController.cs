using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.Ajax.Utilities;
using PagedList;
using PagedList.Mvc;
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
        WriterValidator writerValidator = new WriterValidator();

        [HttpGet]
        public ActionResult WriterProfile()
        {
            string writerMail = (string)Session["WriterMail"];
            var writerInfo = wm.GetByMail(writerMail);
            return View(writerInfo);
        }


        [HttpPost]
        public ActionResult WriterProfile(Writer p)
        {
            ValidationResult result = writerValidator.Validate(p);
            if (result.IsValid)
            {
                wm.WriterUpdate(p);
                return RedirectToAction("AllHeading", "WriterPanel");
            }
            else
            {
                foreach (var irem in result.Errors)
                {
                    ModelState.AddModelError(irem.PropertyName, irem.ErrorMessage);
                }
            }
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

        public ActionResult AllHeading(int p = 1)
        {
            var headings = hm.GetList().ToPagedList(p, 4);
            return View(headings);
        }

    }
}