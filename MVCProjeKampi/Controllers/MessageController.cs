using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace MVCProjeKampi.Controllers
{
    public class MessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        ContactManager cm = new ContactManager(new EfContactDal());
        MessageValidator messageValidator = new MessageValidator();

        public ActionResult Inbox(string p, int page=1)
        {
            string mail = "admin@gmail.com";
            var messageValue = mm.GetListInbox(mail, p);
            return View(messageValue.ToPagedList(page, 7));
        }

        public ActionResult Sendbox(string p, int page = 1)
        {
            string mail = "admin@gmail.com";
            var messageList = mm.GetListSendbox(mail, p);
            return View(messageList.ToPagedList(page, 7));
        }

        public ActionResult GetInBoxMessageDetails(int id)
        {
            mm.IsReadMessage(id);
            var messageValues = mm.GetByID(id);
            return View(messageValues);
        }

        public ActionResult GetSendBoxMessageDetails(int id)
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
        public ActionResult NewMessage(Message p, string actionType)
        {
            ValidationResult result = messageValidator.Validate(p);
            if (!result.IsValid)
            {

                foreach (var irem in result.Errors)
                {
                    ModelState.AddModelError(irem.PropertyName, irem.ErrorMessage);
                }

                return View(p);

            }

            if (actionType == "draft")
            {
                p.IsDraft = true;
                mm.MessageAddBL(p);
                return RedirectToAction("GetDraftMessage");
            }
            else if (actionType == "send")
            {
                p.IsDraft = false;
                mm.MessageAddBL(p);
                return RedirectToAction("Sendbox");
            }
            return View();
        }


        public PartialViewResult MessageList()
        {
            string mail = "admin@gmail.com";

            var inboxCount = mm.GetListInbox(mail).Count();
            ViewBag.inboxCount = inboxCount;

            var sendboxCount = mm.GetListSendbox(mail).Count();
            ViewBag.sendboxCount = sendboxCount;

            var contactCount = cm.GetList().Count();
            ViewBag.contactCount = contactCount;

            var draftCount = mm.GetListDraft().Count();
            ViewBag.draftCount = draftCount;

            var unreadCount = mm.GetListInbox(mail).Where(x => x.IsRead == false).Count();
            ViewBag.unreadCount = unreadCount;

            var readCount = mm.GetListInbox(mail).Where(x => x.IsRead == true).Count();
            ViewBag.readCount = readCount;

            return PartialView();
        }
    }
}