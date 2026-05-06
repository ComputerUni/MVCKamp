using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCProjeKampi.Controllers
{

    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login

        AdminManager adm = new AdminManager(new EfAdminDal());
        WriterManager wm = new WriterManager(new EfWriterDal());

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin admin)
        {
            var adminUser = adm.Login(admin);

            if(adminUser != null)
            {
                FormsAuthentication.SetAuthCookie(adminUser.AdminUserName, false);
                Session["AdminUserName"] = adminUser.AdminUserName;
                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                TempData["LoginError"] = "Hatalı Giriş!";
                return View();
            }
        }

        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriterLogin(Writer writer)
        {
            var writerUser = wm.Login(writer);
            if(writerUser != null)
            {
                FormsAuthentication.SetAuthCookie(writerUser.WriterMail, false);
                Session["WriterMail"] = writerUser.WriterMail;
                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            else
            {
                TempData["LoginError"] = "Hatalı Giriş!";
                return RedirectToAction("WriterLogin");
            }
        }

    }
}