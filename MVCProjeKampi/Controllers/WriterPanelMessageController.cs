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

namespace MVCProjeKampi.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();
        public ActionResult Inbox()
        {
            var messageValue = mm.GetListInbox();
            return View(messageValue);
        }

        public PartialViewResult MessageListMenu()
        {
            return PartialView();
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

        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var messageValues = mm.GetByID(id);
            return View(messageValues);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }


        [HttpPost]
        public ActionResult NewMessage(Message p)
        {
            ValidationResult results = messageValidator.Validate(p);
            if(results.IsValid)
            {
                p.SenderMail = "aliyildiz@gmail.com".Trim();
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.IsDraft = false;
                mm.MessageAddBL(p);
                return RedirectToAction("Sendbox");
            }
            else
            {
                foreach(var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}