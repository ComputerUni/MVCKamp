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
    public class MessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        ContactManager cm = new ContactManager(new EfContactDal());
        public ActionResult Inbox()
        {
            var messageValue = mm.GetListInbox();
            return View(messageValue);
        }

        public ActionResult Sendbox()
        {
            var messageList = mm.GetListSendbox();
            return View(messageList);
        }

        public ActionResult GetInBoxMessageDetails(int id)
        {
            var messageValues = mm.GetByID(id);
            return View(messageValues);
        }

        public ActionResult GetDraftMessage()
        {
            var draftValues = mm.GetListDraft();
            return View(draftValues);
        }


        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewMessage(Message p, string actionType)
        {
            if(actionType == "draft")
            {
                mm.SaveDraft(p);
                return RedirectToAction("GetDraftMessage");
            }
            else if(actionType == "send")
            {
                mm.SaveMessage(p);
                return RedirectToAction("Sendbox");
            }

            return RedirectToAction("NewMessage");
        }



        public PartialViewResult MessageList()
        {
            var inboxCount = mm.GetListInbox().Count();
            ViewBag.inboxCount = inboxCount;

            var sendboxCount = mm.GetListSendbox().Count();
            ViewBag.sendboxCount = sendboxCount;

            var contactCount = cm.GetList().Count();
            ViewBag.contactCount = contactCount;   
            
            var draftCount = mm.GetListDraft().Count(); 
            ViewBag.draftCount = draftCount;    

            return PartialView();
        }
    }
}