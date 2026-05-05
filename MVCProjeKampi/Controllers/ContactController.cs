using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampi.Controllers
{
    public class ContactController : Controller
    {
        ContactManager cm = new ContactManager(new EfContactDal());
        MessageManager mm = new MessageManager(new EfMessageDal());
        ContactValidator cv = new ContactValidator();
        public ActionResult Index()
        {
            var contactValue = cm.GetList();
            return View(contactValue);
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactValues = cm.GetByID(id);
            return View(contactValues);
        }

        public PartialViewResult ContactPartial()
        {
            var inboxCount = mm.GetListInbox().Count();
            ViewBag.inboxCount = inboxCount;

            var sendboxCount = mm.GetListSendbox().Count();
            ViewBag.sendboxCount = sendboxCount;

            var contactCount = cm.GetList().Count();
            ViewBag.contactCount = contactCount;

            return PartialView();
        }
    }
}