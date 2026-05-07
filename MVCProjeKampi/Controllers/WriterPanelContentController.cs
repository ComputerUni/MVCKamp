using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampi.Controllers
{
    public class WriterPanelContentController : Controller
    {
        // GET: WriterPanelContent
        ContentManager cm = new ContentManager(new EfContentDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        public ActionResult MyContent()
        {
            string mail = (string)Session["WriterMail"];
            var writerInfo = wm.GetByMail(mail);
            var contentValues = cm.GetListByWriter(writerInfo.WriterID);
            ViewBag.d = mail;
            return View(contentValues);
        }

        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.d = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddContent(Content p)
        {
            string mail = (string)Session["WriterMail"];
            var writerInfo = wm.GetByMail(mail);
            p.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID = writerInfo.WriterID;   
            p.ContentStatus = true;
            cm.ContentAddBL(p);
            return RedirectToAction("MyContent");
        }

        public ActionResult ToDoList()
        {
            return View();
        }
    }
}