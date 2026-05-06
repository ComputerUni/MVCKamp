using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
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
    }
}