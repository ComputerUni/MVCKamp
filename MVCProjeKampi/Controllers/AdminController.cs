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
    public class AdminController : Controller
    {
        AdminManager adm = new AdminManager(new EfAdminDal());

        public ActionResult Index()
        {
            var adminValues = adm.GetList();
            return View(adminValues);
        }

        [HttpPost]
        public ActionResult AddAdmin(Admin p)
        {
            adm.AdminAdd(p);
            return RedirectToAction("Index");
        }

        public ActionResult CreateAdmin()
        {
            Admin admin = new Admin();
            admin.AdminUserName = "irem";
            admin.AdminPassword = "123456";
            admin.AdminRole = "B";

            adm.AdminAdd(admin);


            return Content("Admin başarıyla veritabanına kaydedildi!");
        }
    }
}